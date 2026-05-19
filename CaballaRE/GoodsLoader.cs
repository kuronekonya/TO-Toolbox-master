using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace CaballaRE
{
    /// <summary>
    /// Handles loading, editing, and exporting of libcmgds.xml and libcmgds_rch.xml files.
    /// libcmgds has two sections (CHARACTER/GOODS and MYCAMP); _rch has only CHARACTER.
    /// Detection is automatic: presence of a MYCAMP element signals libcmgds format.
    /// </summary>
    class GoodsLoader
    {
        // ── Public constants ────────────────────────────────────────────────────────

        public enum FileType { Unknown, LibCmgds, LibCmgdsRch }

        public const string SectionGoods  = "GOODS (MyShop)";
        public const string SectionMyCamp = "MyCamp";
        public const string SectionRch    = "CHARACTER";

        // ── XTEA cipher (libcmgds key: "#dyddydwnrrpTwl~") ─────────────────────────

        private static readonly uint[] KeyLibCmgds = { 0x23647964, 0x64796477, 0x6e727270, 0x54776c7e };
        private const uint XteaDelta = 0x9e3779b9;

        // ── Internal data model ─────────────────────────────────────────────────────

        // One entry per top-level <GOODS> element.
        // Attributes  → shown in DataGridView as a row.
        // Children    → preserved for export; not exposed in the grid.
        private class GoodsEntry
        {
            public Dictionary<string, string> Attributes = new Dictionary<string, string>();
            public string ChildTag = "";
            public List<Dictionary<string, string>> Children = new List<Dictionary<string, string>>();
        }

        // One XML section (CHARACTER or MYCAMP).
        private class GoodsSection
        {
            public string XmlTag;           // "CHARACTER" or "MYCAMP"
            public string CountAttr;        // original count="" value
            public List<GoodsEntry> Entries = new List<GoodsEntry>();
            public DataTable Table;         // built lazily; columns = GOODS attribute keys
            public List<string> ColumnOrder = new List<string>(); // preserves attribute order
        }

        // _rch flattened row: each <RECHARGE_LIST> becomes one DataTable row.
        // We keep the original nested structure for export.
        private class RchGoods
        {
            public string GoodsCode;
            public string LimitCount;
            public List<Dictionary<string, string>> RechargeItems = new List<Dictionary<string, string>>();
        }

        // ── State ───────────────────────────────────────────────────────────────────

        public FileType DetectedType { get; private set; } = FileType.Unknown;
        public string StatusMessage  { get; private set; } = "";

        // libcmgds sections (indexed by display name)
        private readonly Dictionary<string, GoodsSection> sections =
            new Dictionary<string, GoodsSection>();

        // _rch raw data (for export reconstruction)
        private List<RchGoods> rchRawData;
        // _rch flat DataTable (goods_code, limit_count, + RECHARGE_LIST attributes)
        private DataTable rchTable;
        private List<string> rchColumnOrder = new List<string>();

        // ── Public API ──────────────────────────────────────────────────────────────

        public void Load(string filePath)
        {
            StatusMessage  = "";
            DetectedType   = FileType.Unknown;
            sections.Clear();
            rchRawData     = null;
            rchTable       = null;
            rchColumnOrder.Clear();

            bool hasMyCamp = FileContainsMycamp(filePath);
            DetectedType   = hasMyCamp ? FileType.LibCmgds : FileType.LibCmgdsRch;

            if (DetectedType == FileType.LibCmgds)
                ParseLibCmgds(filePath);
            else
                ParseRch(filePath);
        }

        /// <summary>Returns display names of available sections.</summary>
        public List<string> GetSectionNames()
        {
            if (DetectedType == FileType.LibCmgdsRch)
                return new List<string> { SectionRch };

            var names = new List<string>();
            if (sections.ContainsKey(SectionGoods))  names.Add(SectionGoods);
            if (sections.ContainsKey(SectionMyCamp)) names.Add(SectionMyCamp);
            return names;
        }

        /// <summary>Returns the DataTable for the named section (suitable for DataGridView binding).</summary>
        public DataTable GetSectionTable(string displayName)
        {
            if (DetectedType == FileType.LibCmgdsRch)
                return rchTable;

            return sections.ContainsKey(displayName) ? sections[displayName].Table : null;
        }

        /// <summary>Returns ordered column names for the named section (for Cell Search dropdown).</summary>
        public List<string> GetColumnNames(string displayName)
        {
            if (DetectedType == FileType.LibCmgdsRch)
                return new List<string>(rchColumnOrder);

            if (sections.ContainsKey(displayName))
                return new List<string>(sections[displayName].ColumnOrder);
            return new List<string>();
        }

        /// <summary>Exports the current in-memory data to XML bytes.</summary>
        public byte[] ExportXml()
        {
            var settings = new XmlWriterSettings
            {
                Encoding    = new UTF8Encoding(false),
                Indent      = true,
                IndentChars = "    ",
                NewLineChars = "\n"
            };

            using (var ms = new MemoryStream())
            using (var xmlw = XmlWriter.Create(ms, settings))
            {
                xmlw.WriteStartDocument();
                xmlw.WriteStartElement("ROOT");

                if (DetectedType == FileType.LibCmgdsRch)
                    WriteRchSection(xmlw);
                else
                    WriteLibCmgdsSections(xmlw);

                xmlw.WriteEndElement(); // ROOT
                xmlw.WriteEndDocument();
                xmlw.Flush();
                return ms.ToArray();
            }
        }

        /// <summary>Pads XML to 8-byte XTEA block boundaries (same layout as .dat files).</summary>
        public byte[] PadToBlocks(byte[] xmlData)
        {
            using (var output = new MemoryStream())
            using (var reader = new StreamReader(new MemoryStream(xmlData), Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    byte[] lineBytes = Encoding.UTF8.GetBytes(line + "\n");
                    int blocks = (lineBytes.Length + 7) / 8;
                    byte[] padded = new byte[blocks * 8]; // zero-initialised
                    Buffer.BlockCopy(lineBytes, 0, padded, 0, lineBytes.Length);
                    output.Write(padded, 0, padded.Length);
                }
                return output.ToArray();
            }
        }

        /// <summary>Encrypts padded data with the libcmgds XTEA key (no header byte).</summary>
        public byte[] EncryptDat(byte[] paddedData)
        {
            if (paddedData.Length % 8 != 0)
                throw new ArgumentException("Data length must be a multiple of 8.");

            byte[] result = new byte[paddedData.Length];
            int blocks = paddedData.Length / 8;

            for (int i = 0; i < blocks; i++)
            {
                int off = i * 8;
                uint y = (uint)(paddedData[off]     << 24 | paddedData[off + 1] << 16 |
                                paddedData[off + 2] << 8  | paddedData[off + 3]);
                uint z = (uint)(paddedData[off + 4] << 24 | paddedData[off + 5] << 16 |
                                paddedData[off + 6] << 8  | paddedData[off + 7]);

                XteaEncrypt(ref y, ref z);

                result[off]     = (byte)(y >> 24); result[off + 1] = (byte)(y >> 16);
                result[off + 2] = (byte)(y >> 8);  result[off + 3] = (byte)y;
                result[off + 4] = (byte)(z >> 24); result[off + 5] = (byte)(z >> 16);
                result[off + 6] = (byte)(z >> 8);  result[off + 7] = (byte)z;
            }
            return result;
        }

        // ── Parsing ─────────────────────────────────────────────────────────────────

        private static bool FileContainsMycamp(string filePath)
        {
            using (var xmlr = XmlReader.Create(filePath))
                while (xmlr.Read())
                    if (xmlr.NodeType == XmlNodeType.Element &&
                        xmlr.Name.Equals("MYCAMP", StringComparison.OrdinalIgnoreCase))
                        return true;
            return false;
        }

        private void ParseLibCmgds(string filePath)
        {
            var charSection  = new GoodsSection { XmlTag = "CHARACTER" };
            var campSection  = new GoodsSection { XmlTag = "MYCAMP" };
            GoodsSection     current = null;
            GoodsEntry       currentEntry = null;

            using (var xmlr = XmlReader.Create(filePath))
            {
                while (xmlr.Read())
                {
                    if (xmlr.NodeType == XmlNodeType.Element)
                    {
                        string tag = xmlr.Name.ToUpper();
                        switch (tag)
                        {
                            case "CHARACTER":
                                current = charSection;
                                current.CountAttr = xmlr["count"] ?? "0";
                                break;
                            case "MYCAMP":
                                current = campSection;
                                current.CountAttr = xmlr["count"] ?? "0";
                                break;
                            case "GOODS":
                                if (current != null && !xmlr.IsEmptyElement)
                                {
                                    currentEntry = ReadAttributes(xmlr);
                                }
                                else if (current != null && xmlr.IsEmptyElement)
                                {
                                    // Goods with no children (unlikely but safe)
                                    currentEntry = ReadAttributes(xmlr);
                                    current.Entries.Add(currentEntry);
                                    UpdateColumnOrder(current, currentEntry);
                                    currentEntry = null;
                                }
                                break;
                            default:
                                // Child element of GOODS (e.g. GOODS_LIST)
                                if (currentEntry != null && xmlr.IsEmptyElement)
                                {
                                    if (currentEntry.ChildTag == "")
                                        currentEntry.ChildTag = xmlr.Name;
                                    currentEntry.Children.Add(ReadAttributeDict(xmlr));
                                }
                                break;
                        }
                    }
                    else if (xmlr.NodeType == XmlNodeType.EndElement)
                    {
                        string tag = xmlr.Name.ToUpper();
                        if (tag == "GOODS" && current != null && currentEntry != null)
                        {
                            current.Entries.Add(currentEntry);
                            UpdateColumnOrder(current, currentEntry);
                            currentEntry = null;
                        }
                        else if (tag == "CHARACTER" || tag == "MYCAMP")
                        {
                            current = null;
                        }
                    }
                }
            }

            if (charSection.Entries.Count > 0)
            {
                charSection.Table = BuildDataTable(charSection.Entries, charSection.ColumnOrder);
                sections[SectionGoods] = charSection;
            }
            if (campSection.Entries.Count > 0)
            {
                campSection.Table = BuildDataTable(campSection.Entries, campSection.ColumnOrder);
                sections[SectionMyCamp] = campSection;
            }
        }

        private void ParseRch(string filePath)
        {
            rchRawData = new List<RchGoods>();
            var flatRows  = new List<Dictionary<string, string>>();
            var columnSet = new HashSet<string>();
            RchGoods currentGoods = null;

            // Ensure goods_code and limit_count are always first
            columnSet.Add("goods_code");
            columnSet.Add("limit_count");
            rchColumnOrder.Add("goods_code");
            rchColumnOrder.Add("limit_count");

            using (var xmlr = XmlReader.Create(filePath))
            {
                while (xmlr.Read())
                {
                    if (xmlr.NodeType != XmlNodeType.Element) continue;

                    string tag = xmlr.Name.ToUpper();
                    if (tag == "GOODS")
                    {
                        currentGoods = new RchGoods
                        {
                            GoodsCode  = xmlr["goods_code"]  ?? "",
                            LimitCount = xmlr["limit_count"] ?? ""
                        };
                        rchRawData.Add(currentGoods);
                    }
                    else if (tag == "RECHARGE_LIST" && currentGoods != null)
                    {
                        var attrs   = ReadAttributeDict(xmlr);
                        currentGoods.RechargeItems.Add(attrs);

                        var flatRow = new Dictionary<string, string>
                        {
                            ["goods_code"]  = currentGoods.GoodsCode,
                            ["limit_count"] = currentGoods.LimitCount
                        };
                        foreach (var kv in attrs)
                        {
                            flatRow[kv.Key] = kv.Value;
                            if (columnSet.Add(kv.Key))
                                rchColumnOrder.Add(kv.Key);
                        }
                        flatRows.Add(flatRow);
                    }
                }
            }

            rchTable = BuildFlatDataTable(flatRows, rchColumnOrder);
        }

        // ── Attribute helpers ───────────────────────────────────────────────────────

        private static GoodsEntry ReadAttributes(XmlReader xmlr)
        {
            var entry = new GoodsEntry();
            for (int i = 0; i < xmlr.AttributeCount; i++)
            {
                xmlr.MoveToAttribute(i);
                entry.Attributes[xmlr.Name] = xmlr.Value;
            }
            xmlr.MoveToElement();
            return entry;
        }

        private static Dictionary<string, string> ReadAttributeDict(XmlReader xmlr)
        {
            var dict = new Dictionary<string, string>();
            for (int i = 0; i < xmlr.AttributeCount; i++)
            {
                xmlr.MoveToAttribute(i);
                dict[xmlr.Name] = xmlr.Value;
            }
            xmlr.MoveToElement();
            return dict;
        }

        private static void UpdateColumnOrder(GoodsSection section, GoodsEntry entry)
        {
            foreach (var key in entry.Attributes.Keys)
                if (!section.ColumnOrder.Contains(key))
                    section.ColumnOrder.Add(key);
        }

        // ── DataTable builders ──────────────────────────────────────────────────────

        private static DataTable BuildDataTable(List<GoodsEntry> entries, List<string> columnOrder)
        {
            var dt = new DataTable();
            foreach (var col in columnOrder)
                dt.Columns.Add(col, typeof(string));

            foreach (var entry in entries)
            {
                DataRow dr = dt.NewRow();
                foreach (var col in columnOrder)
                    dr[col] = entry.Attributes.ContainsKey(col) ? entry.Attributes[col] : "";
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private static DataTable BuildFlatDataTable(List<Dictionary<string, string>> rows,
                                                    List<string> columnOrder)
        {
            var dt = new DataTable();
            foreach (var col in columnOrder)
                dt.Columns.Add(col, typeof(string));

            foreach (var row in rows)
            {
                DataRow dr = dt.NewRow();
                foreach (var col in columnOrder)
                    dr[col] = row.ContainsKey(col) ? row[col] : "";
                dt.Rows.Add(dr);
            }
            return dt;
        }

        // ── XML export writers ──────────────────────────────────────────────────────

        private void WriteLibCmgdsSections(XmlWriter xmlw)
        {
            if (sections.ContainsKey(SectionGoods))
                WriteGoodsSection(xmlw, sections[SectionGoods]);
            if (sections.ContainsKey(SectionMyCamp))
                WriteGoodsSection(xmlw, sections[SectionMyCamp]);
        }

        private static void WriteGoodsSection(XmlWriter xmlw, GoodsSection sec)
        {
            // Sync DataTable edits back to GoodsEntry attributes
            SyncTableToEntries(sec);

            xmlw.WriteStartElement(sec.XmlTag);
            xmlw.WriteAttributeString("count", sec.Entries.Count.ToString());

            foreach (var entry in sec.Entries)
            {
                xmlw.WriteStartElement("GOODS");
                foreach (var kv in entry.Attributes)
                    xmlw.WriteAttributeString(kv.Key, kv.Value);

                foreach (var child in entry.Children)
                {
                    string childTag = entry.ChildTag.Length > 0 ? entry.ChildTag : "GOODS_LIST";
                    xmlw.WriteStartElement(childTag);
                    foreach (var kv in child)
                        xmlw.WriteAttributeString(kv.Key, kv.Value);
                    xmlw.WriteEndElement();
                }

                xmlw.WriteEndElement(); // GOODS
            }

            xmlw.WriteEndElement(); // section tag
        }

        private void WriteRchSection(XmlWriter xmlw)
        {
            // Sync flat DataTable edits back to rchRawData
            SyncRchTableToRawData();

            xmlw.WriteStartElement("CHARACTER");
            xmlw.WriteAttributeString("count", rchRawData.Count.ToString());

            foreach (var goods in rchRawData)
            {
                xmlw.WriteStartElement("GOODS");
                xmlw.WriteAttributeString("goods_code",  goods.GoodsCode);
                xmlw.WriteAttributeString("limit_count", goods.LimitCount);

                foreach (var recharge in goods.RechargeItems)
                {
                    xmlw.WriteStartElement("RECHARGE_LIST");
                    foreach (var kv in recharge)
                        xmlw.WriteAttributeString(kv.Key, kv.Value);
                    xmlw.WriteEndElement();
                }

                xmlw.WriteEndElement(); // GOODS
            }

            xmlw.WriteEndElement(); // CHARACTER
        }

        // ── Sync edits from DataTable back to raw data ──────────────────────────────

        private static void SyncTableToEntries(GoodsSection sec)
        {
            DataTable dt = sec.Table;
            if (dt == null) return;

            var newEntries = new List<GoodsEntry>(dt.Rows.Count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // Reuse existing entry so child elements (GOODS_LIST etc.) are preserved.
                // Rows added beyond the original count get a blank entry with no children.
                GoodsEntry entry = i < sec.Entries.Count ? sec.Entries[i] : new GoodsEntry();
                entry.Attributes.Clear();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn col in dt.Columns)
                    entry.Attributes[col.ColumnName] = dr[col].ToString();
                newEntries.Add(entry);
            }

            sec.Entries.Clear();
            sec.Entries.AddRange(newEntries);
        }

        private void SyncRchTableToRawData()
        {
            if (rchTable == null) return;

            // Group flat DataTable rows into GOODS elements by (goods_code, limit_count) pairs,
            // preserving the order rows appear in the table. Consecutive rows sharing the same
            // goods_code+limit_count are folded under one <GOODS> parent so the structure
            // mirrors what was parsed. Rows with a unique or changed goods_code become their
            // own <GOODS> element, meaning the CHARACTER count reflects the actual row grouping.
            var newData = new List<RchGoods>();
            RchGoods current = null;

            foreach (DataRow dr in rchTable.Rows)
            {
                string code  = dr["goods_code"].ToString();
                string limit = dr["limit_count"].ToString();

                if (current == null || current.GoodsCode != code || current.LimitCount != limit)
                {
                    current = new RchGoods { GoodsCode = code, LimitCount = limit };
                    newData.Add(current);
                }

                var rechargeAttrs = new Dictionary<string, string>();
                foreach (DataColumn col in rchTable.Columns)
                {
                    if (col.ColumnName == "goods_code" || col.ColumnName == "limit_count") continue;
                    rechargeAttrs[col.ColumnName] = dr[col].ToString();
                }
                current.RechargeItems.Add(rechargeAttrs);
            }

            rchRawData = newData;
        }

        // ── XTEA implementation ─────────────────────────────────────────────────────

        private static void XteaEncrypt(ref uint y, ref uint z)
        {
            uint sum = 0;
            for (int i = 0; i < 32; i++)
            {
                y   += (z << 4 ^ z >> 5) + z ^ sum + KeyLibCmgds[sum & 3];
                sum += XteaDelta;
                z   += (y << 4 ^ y >> 5) + y ^ sum + KeyLibCmgds[sum >> 11 & 3];
            }
        }
    }
}
