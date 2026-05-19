using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CaballaRE
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            InitCellSearchTimer();
            this.MinimumSize = Size.Empty;  // prevent DPI-scaled MinimumSize from clamping our size
            this.Size = NormalWindowSize;   // exactly 680x450
        }

        // ── Core loaders ─────────────────────────────────────────────────────────────
        private readonly NRILoader nril = new NRILoader();
        private readonly DatLoader dl   = new DatLoader();
        private readonly GoodsLoader gl = new GoodsLoader();
        public string nriName = "";

        // ════════════════════════════════════════════════════════════════════════════
        // NRI Viewer
        // ════════════════════════════════════════════════════════════════════════════

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "NRI files (*.nri;*.bac)|*.nri;*.bac|All files|*.*";
            if (ofd.ShowDialog() != DialogResult.OK) return;

            nriName = Path.GetFileName(ofd.FileName);
            if (nril.Load(ofd.FileName))
            {
                if (nril.status != "")
                    MessageBox.Show(nril.status);

                listBox1.Items.Clear();
                int fileCount = nril.GetFileCount();
                for (int i = 0; i < fileCount; i++)
                    listBox1.Items.Add("Image" + (i + 1));

                listBox3.Items.Clear();
                int animCount = nril.GetAnimationsCount();
                for (int i = 0; i < animCount; i++)
                {
                    NRILoader.Animation anim = nril.GetAnimations(i);
                    listBox3.Items.Add((i + 1) + ". " + anim.name);
                }
            }
            else
            {
                MessageBox.Show("Unable to load NRI file");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nril.GetFileCount() <= 0) return;
            int fileid = listBox1.SelectedIndex;
            if (fileid < 0 || fileid >= nril.GetFileCount()) return;
            try
            {
                Stream bmpstream = nril.GetFile(fileid);
                pictureBox1.Image = Image.FromStream(bmpstream);
                bmpstream.Close();
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (nril.GetFileCount() <= 0) return;
            int fileid = listBox1.SelectedIndex;
            if (fileid < 0 || fileid >= nril.GetFileCount()) return;

            SaveFileDialog sfd = new SaveFileDialog { Filter = "BMP file|*.bmp|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            using (MemoryStream bmpstream = nril.GetFile(fileid))
            using (BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName)))
            {
                bmpstream.Flush();
                bw.Write(bmpstream.ToArray());
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "NRI files (*.nri;*.bac)|*.nri;*.bac|All files|*.*" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            byte[] data = nril.DecompressFile(ofd.FileName);
            if (data == null) { MessageBox.Show("Input data not compressed"); return; }

            SaveFileDialog sfd = new SaveFileDialog { Filter = "NRI files (*.nri;*.bac)|*.nri;*.bac|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            using (BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName)))
                bw.Write(data, 0, data.Length);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (nril.GetFileCount() <= 0) { MessageBox.Show("Nothing to extract"); return; }

            FolderBrowserDialog sfd = new FolderBrowserDialog();
            if (sfd.ShowDialog() != DialogResult.OK) return;

            string targetDir = sfd.SelectedPath + Path.DirectorySeparatorChar;
            int files = nril.GetFileCount();
            int numLength = files.ToString().Length;
            for (int i = 1; i <= files; i++)
            {
                string fileNum = i.ToString().PadLeft(numLength, '0');
                string genFileName = targetDir + nriName + "_img" + fileNum + ".bmp";
                using (MemoryStream bmpstream = nril.GetFile(i - 1))
                using (BinaryWriter bw = new BinaryWriter(File.Create(genFileName)))
                {
                    bmpstream.Flush();
                    bw.Write(bmpstream.ToArray());
                }
            }
        }

        // ── Animation tab auto-resize ────────────────────────────────────────────────

        private static readonly Size AnimExpandedSize = new Size(800, 800);
        private static readonly Size NormalWindowSize  = new Size(680, 450);
        private Size  _normalWindowSize     = NormalWindowSize;
        private Point _normalWindowLocation;
        private bool  _animExpanded = false;

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAnimWindowSize();
        }

        private void TabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAnimWindowSize();
        }

        private void UpdateAnimWindowSize()
        {
            bool onAnimations = tabControl1.SelectedIndex == 0
                             && tabControl2.SelectedIndex == 1;

            if (onAnimations && !_animExpanded)
            {
                _normalWindowLocation = this.Location;
                _animExpanded         = true;
                ResizeFromCenter(AnimExpandedSize);
            }
            else if (!onAnimations && _animExpanded)
            {
                _animExpanded = false;
                ResizeFromCenter(NormalWindowSize, _normalWindowLocation);
            }
        }

        // Resize the window, keeping its visual center pinned to the same screen point.
        // If restoreLocation is provided it overrides the center-pin calculation.
        private void ResizeFromCenter(Size newSize, Point? restoreLocation = null)
        {
            Point targetLocation;
            if (restoreLocation.HasValue)
            {
                targetLocation = restoreLocation.Value;
            }
            else
            {
                Point center = new Point(
                    this.Location.X + this.Size.Width  / 2,
                    this.Location.Y + this.Size.Height / 2);
                targetLocation = new Point(
                    center.X - newSize.Width  / 2,
                    center.Y - newSize.Height / 2);
            }

            // Clamp so window doesn't go off-screen.
            System.Drawing.Rectangle screen = Screen.FromControl(this).WorkingArea;
            targetLocation.X = Math.Max(screen.Left, Math.Min(targetLocation.X, screen.Right  - newSize.Width));
            targetLocation.Y = Math.Max(screen.Top,  Math.Min(targetLocation.Y, screen.Bottom - newSize.Height));

            this.SuspendLayout();
            this.Location = targetLocation;
            this.Size      = newSize;
            this.ResumeLayout(true);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            DisplayAnimation(listBox3.SelectedIndex, trackBar1.Value);
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = listBox3.SelectedIndex;
            if (id < 0 || id >= nril.GetAnimationsCount()) return;

            NRILoader.Animation anim = nril.GetAnimations(id);
            trackBar1.Minimum = 0;
            trackBar1.Maximum = anim.frames.Count;
            DisplayAnimation(id, 0);
        }

        private void DisplayAnimation(int id, int frameid)
        {
            if (id < 0 || id >= nril.GetAnimationsCount()) return;
            NRILoader.Animation anim = nril.GetAnimations(id);
            label2.Text = anim.name + " (" + anim.frames.Count + " frames)";
            if (frameid < 0 || frameid >= anim.frames.Count) return;

            NRILoader.Frame frame = anim.frames[frameid];
            int canvaswidth = 500, canvasheight = 500;

            ColorMap cmap1 = new ColorMap { OldColor = Color.FromArgb(0, 255, 0),   NewColor = Color.Transparent };
            ColorMap cmap2 = new ColorMap { OldColor = Color.FromArgb(255, 0, 255), NewColor = Color.Transparent };
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetRemapTable(new ColorMap[] { cmap1, cmap2 }, ColorAdjustType.Bitmap);

            Bitmap canvas = new Bitmap(canvaswidth, canvasheight);
            using (Graphics g = Graphics.FromImage(canvas))
            {
                int baseoffsetx = canvaswidth / 2, baseoffsety = canvasheight / 2;
                using (Pen gridPen = new Pen(Color.Black) { Width = 1 })
                {
                    g.DrawLine(gridPen, 0, baseoffsety, baseoffsetx * 2, baseoffsety);
                    g.DrawLine(gridPen, baseoffsetx, 0, baseoffsetx, baseoffsety * 2);
                }

                foreach (NRILoader.FramePlane plane in frame.planes)
                {
                    int bitmapid = plane.bitmapid;
                    if (bitmapid < 0 || bitmapid >= nril.GetFileCount())
                    {
                        MessageBox.Show("Unable to load " + bitmapid);
                        continue;
                    }
                    Stream bmpstream = nril.GetFile(bitmapid);
                    Bitmap bmp = new Bitmap(bmpstream);
                    switch (plane.reverseflag)
                    {
                        case 1: bmp.RotateFlip(RotateFlipType.RotateNoneFlipX);  break;
                        case 2: bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);  break;
                        case 3: bmp.RotateFlip(RotateFlipType.RotateNoneFlipXY); break;
                    }
                    int x = plane.x + baseoffsetx, y = plane.y + baseoffsety;
                    g.DrawImage(bmp, new Rectangle(x, y, bmp.Width, bmp.Height),
                                0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, imageAttributes);
                    bmpstream.Close();
                }
            }
            pictureBox2.Image = canvas;
        }

        // ════════════════════════════════════════════════════════════════════════════
        // DAT Viewer
        // ════════════════════════════════════════════════════════════════════════════

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "DAT files (*.dat)|*.dat|All files|*.*" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            BeginOperation(
                (s, ev) => dl.Load(ofd.FileName),
                (s, ev) => textBox1.Text = dl.GetString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dl.GetFile() == null) return;
            SaveFileDialog sfd = new SaveFileDialog { Filter = "XML file|*.xml|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            using (BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName)))
                bw.Write(dl.GetFile());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string result = "";
            BeginOperation(
                (s, ev) => result = dl.GetString(true, true),
                (s, ev) => textBox1.Text = result);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "XML file|*.xml|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;
            byte[] str = null;
            BeginOperation(
                (s, ev) => str = Encoding.UTF8.GetBytes(dl.GetString(true)),
                (s, ev) =>
                {
                    using (BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName)))
                        bw.Write(str);
                });
        }

        // ════════════════════════════════════════════════════════════════════════════
        // LibConfig Editor
        // ════════════════════════════════════════════════════════════════════════════

        private List<string> tables      = new List<string>();
        private int currenttable         = -1;
        private BindingSource libConfigBS = new BindingSource();

        // -- Load --
        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "XML files (*.xml)|*.xml|All files|*.*" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            _libLoadPath = ofd.FileName;
            dl.LoadLibConfig(ofd.FileName);
            tables = dl.GetTableList();
            UpdateListbox();
            ClearLibCellSearch();
            if (dl.GetStatus() != "")
                MessageBox.Show(dl.GetStatus());
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selindex = listBox2.SelectedIndex;
            if (selindex < 0 || selindex >= dl.GetTableCount())
            {
                currenttable = -1;
                return;
            }

            string seltext = (string)listBox2.SelectedItem;
            string[] parts = seltext.Split(new char[] { '.' }, 2);
            int tableid = int.Parse(parts[0]);
            currenttable = tableid;
            label1.Text = "Table: " + seltext;

            // Clear the filter BEFORE switching DataSource to prevent EvaluateException
            // when the new table doesn't have the same columns as the previous one.
            libConfigBS.Filter = null;

            DataTable dt = dl.GetTable(tableid);
            libConfigBS.DataSource = dt;
            dataGridView1.DataSource = libConfigBS;

            PopulateLibCellSearchColumns(dt);
            ClearLibCellSearch();
            SetStatus("Table " + currenttable + ". " + seltext + " loaded — " + (dt != null ? dt.Rows.Count.ToString() : "0") + " rows.");
        }

        private void UpdateListbox()
        {
            listBox2.BeginUpdate();
            listBox2.Items.Clear();
            for (int i = 0; i < tables.Count; i++)
            {
                string entry = i + ". " + tables[i];
                if (entry.IndexOf(textBox2.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    listBox2.Items.Add(entry);
            }
            listBox2.EndUpdate();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            UpdateListbox();
        }

        // -- Update Table --
        private void button12_Click(object sender, EventArgs e)
        {
            if (currenttable < 0)
            { MessageBox.Show("No table selected. Please select a table first.", "Nothing to Update"); return; }
            // Clear filter so UpdateTable reads all rows, not just visible ones.
            libConfigBS.Filter = null;
            txtLibCellSearch.Text = "";
            dl.UpdateTable(currenttable);
            SetStatus("Table \"" + tables[currenttable] + "\" updated successfully.");
        }

        // -- Import Table --
        private void button8_Click(object sender, EventArgs e)
        {
            if (currenttable < 0) { MessageBox.Show("No table selected. Please select a table first.", "Nothing to Import Into"); return; }

            OpenFileDialog ofd = new OpenFileDialog { Filter = "CSV files (*.csv)|*.csv|All files|*.*" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                int result = dl.ImportTable(currenttable, ofd.FileName);
                switch (result)
                {
                    case -1: MessageBox.Show("No table selected to override"); break;
                    case  1: MessageBox.Show("Table structure does not match imported format"); break;
                    case  0:
                        DataTable dt = dl.GetTable(currenttable);
                        libConfigBS.DataSource = dt;
                        dataGridView1.DataSource = libConfigBS;
                        PopulateLibCellSearchColumns(dt);
                        ClearLibCellSearch();
                        MessageBox.Show("Selected table has been overwritten.");
                        SetStatus("Table \"" + tables[currenttable] + "\" imported from CSV.");
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Failed to override table – table format does not match");
            }
        }

        // -- Localization Helper --
        private void button10_Click(object sender, EventArgs e)
        {
            new CSVHelper().Show();
        }

        // -- Export dropdown (button9) --
        private void button9_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            int arrowX = button9.ClientRectangle.Width - 14;
            int arrowY = button9.ClientRectangle.Height / 2 - 1;
            Brush brush = Enabled ? SystemBrushes.ControlText : SystemBrushes.ButtonShadow;
            Point[] arrows = { new Point(arrowX, arrowY), new Point(arrowX + 7, arrowY), new Point(arrowX + 3, arrowY + 4) };
            e.Graphics.FillPolygon(brush, arrows);
        }

        private void button9_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
                contextMenuStrip1.Show(button9, 0, button9.Height);
        }

        private void iDXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] data = dl.ExportIdx();
            if (data == null) { MessageBox.Show("Export to DAT first before building IDX"); return; }

            SaveFileDialog sfd = new SaveFileDialog { Filter = "IDX file|*.idx|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            using (BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName)))
                bw.Write(data);
            MessageBox.Show("Export completed");
        }

        private void dATToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "DAT file|*.dat|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            byte[] data = dl.ExportXML();
            if (data == null) { MessageBox.Show("Failed to export. " + dl.GetStatus()); return; }

            data = dl.ExportDAT(data);
            GC.Collect();
            data = dl.EncryptFile(data);
            using (BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName)))
                bw.Write(data);
            _lastLibSavePath = sfd.FileName;
            MessageBox.Show("Export completed");
            GC.Collect();
        }

        private void dATUnencryptedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "XML file|*.xml|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            byte[] data = dl.ExportXML();
            if (data == null) { MessageBox.Show("Failed to export. " + dl.GetStatus()); return; }

            using (BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName)))
                bw.Write(dl.ExportDAT(data));
            MessageBox.Show("Export completed");
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "XML file|*.xml|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            byte[] data = dl.ExportXML();
            if (data == null) { MessageBox.Show("Failed to export. " + dl.GetStatus()); return; }

            using (BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName)))
                bw.Write(data);
            MessageBox.Show("Export completed");
        }

        private void xLSExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currenttable < 0) { MessageBox.Show("Please select a table to export"); return; }

            SaveFileDialog sfd = new SaveFileDialog { Filter = "CSV file|*.csv|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            byte[] data = dl.ExportCSV(currenttable);
            using (BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName)))
            {
                bw.Write(new byte[] { 0xEF, 0xBB, 0xBF }); // UTF-8 BOM
                if (data != null)
                    bw.Write(data);
                else
                    MessageBox.Show("Invalid table selected");
            }
            if (data != null)
                MessageBox.Show("Export completed");
        }

        // ── LibConfig Cell Search ────────────────────────────────────────────────────

        private Timer libCellSearchTimer;

        private void InitCellSearchTimer()
        {
            libCellSearchTimer = new Timer { Interval = 300 };
            libCellSearchTimer.Tick += (s, e) =>
            {
                libCellSearchTimer.Stop();
                ApplyLibCellFilter();
            };
        }

        private void PopulateLibCellSearchColumns(DataTable dt)
        {
            cmbLibCellCol.Items.Clear();
            if (dt == null) return;
            foreach (DataColumn col in dt.Columns)
                cmbLibCellCol.Items.Add(col.ColumnName);
            if (cmbLibCellCol.Items.Count > 0)
                cmbLibCellCol.SelectedIndex = 0;
        }

        private void ClearLibCellSearch()
        {
            txtLibCellSearch.Text = "";
            if (libConfigBS != null)
                libConfigBS.Filter = null;
        }

        private void CmbLibCellCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLibCellSearch.Text))
                ApplyLibCellFilter();
        }

        private void TxtLibCellSearch_TextChanged(object sender, EventArgs e)
        {
            _libFindMatchIndex = -1;
            libCellSearchTimer.Stop();
            libCellSearchTimer.Start();
        }

        private void ApplyLibCellFilter()
        {
            if (libConfigBS == null || libConfigBS.DataSource == null) return;
            string col  = cmbLibCellCol.SelectedItem as string;
            string text = txtLibCellSearch.Text;

            if (string.IsNullOrEmpty(col) || string.IsNullOrEmpty(text))
            {
                libConfigBS.Filter = null;
                return;
            }

            // DataView RowFilter – safe escaping for single-quote in value
            string escaped = text.Replace("'", "''");
            libConfigBS.Filter = "[" + col + "] LIKE '%" + escaped + "%'";
        }

        // ════════════════════════════════════════════════════════════════════════════
        // libcmgds & rch Editor
        // ════════════════════════════════════════════════════════════════════════════

        private BindingSource goodsBS            = new BindingSource();
        private string        goodsCurrentSection = "";
        private Timer         goodsCellSearchTimer;

        // -- Load File --
        private void BtnLoadGoods_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "XML files (*.xml)|*.xml|All files|*.*" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            _goodsLoadPath = ofd.FileName;
            gl.Load(ofd.FileName);

            if (gl.DetectedType == GoodsLoader.FileType.Unknown)
            {
                MessageBox.Show("Unrecognized file format.\n" + gl.StatusMessage);
                return;
            }

            bool isRch = (gl.DetectedType == GoodsLoader.FileType.LibCmgdsRch);

            // Toggle left section panel
            pnlGoodsSections.Visible  = !isRch;
            dataGridViewGoods.Left    = isRch ? 6   : 152;
            dataGridViewGoods.Width   = isRch ? (tabPage6.Width - 12)
                                               : (tabPage6.Width - 158);

            // Populate section list (or pick automatically for _rch)
            lstGoodsSections.Items.Clear();
            List<string> sectionNames = gl.GetSectionNames();
            foreach (string name in sectionNames)
                lstGoodsSections.Items.Add(name);

            lblGoodsStatus.Text = gl.DetectedType == GoodsLoader.FileType.LibCmgds
                ? "Loaded: libcmgds  (" + sectionNames.Count + " sections)"
                : "Loaded: libcmgds_rch";

            if (!string.IsNullOrEmpty(gl.StatusMessage))
                MessageBox.Show(gl.StatusMessage);

            if (isRch)
            {
                // Load directly (no section picker needed)
                LoadGoodsSection(GoodsLoader.SectionRch);
            }
            else if (lstGoodsSections.Items.Count > 0)
            {
                lstGoodsSections.SelectedIndex = 0;
            }
        }

        private void LstGoodsSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = lstGoodsSections.SelectedItem as string;
            if (!string.IsNullOrEmpty(selected))
                LoadGoodsSection(selected);
        }

        private void LoadGoodsSection(string sectionName)
        {
            goodsCurrentSection = sectionName;
            // Clear filter BEFORE swapping DataSource to prevent EvaluateException.
            goodsBS.Filter = null;

            DataTable dt = gl.GetSectionTable(sectionName);
            goodsBS.DataSource = dt;
            dataGridViewGoods.DataSource = goodsBS;

            PopulateGoodsCellSearchColumns(sectionName);
            ClearGoodsCellSearch();
        }

        // -- Export dropdown --
        private void BtnGoodsExport_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            int arrowX = btnGoodsExport.ClientRectangle.Width - 14;
            int arrowY = btnGoodsExport.ClientRectangle.Height / 2 - 1;
            Brush brush = Enabled ? SystemBrushes.ControlText : SystemBrushes.ButtonShadow;
            Point[] arrows = { new Point(arrowX, arrowY), new Point(arrowX + 7, arrowY), new Point(arrowX + 3, arrowY + 4) };
            e.Graphics.FillPolygon(brush, arrows);
        }

        private void BtnGoodsExport_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
                contextMenuStripGoods.Show(btnGoodsExport, 0, btnGoodsExport.Height);
        }

        private void GoodsExportXml_Click(object sender, EventArgs e)
        {
            if (gl.DetectedType == GoodsLoader.FileType.Unknown)
            { MessageBox.Show("No file loaded."); return; }

            SaveFileDialog sfd = new SaveFileDialog { Filter = "XML file|*.xml|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                byte[] data = gl.ExportXml();
                using (BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName)))
                    bw.Write(data);
                MessageBox.Show("Export completed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export failed: " + ex.Message);
            }
        }

        private void GoodsExportDat_Click(object sender, EventArgs e)
        {
            if (gl.DetectedType == GoodsLoader.FileType.Unknown)
            { MessageBox.Show("No file loaded."); return; }

            SaveFileDialog sfd = new SaveFileDialog { Filter = "DAT file|*.dat|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                byte[] xmlData    = gl.ExportXml();
                byte[] paddedData = gl.PadToBlocks(xmlData);
                byte[] encData    = gl.EncryptDat(paddedData);

                using (BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName)))
                    bw.Write(encData);

                _lastGoodsSavePath = sfd.FileName;
                GC.Collect();
                MessageBox.Show("Export completed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Export failed: " + ex.Message);
            }
        }

        // ── Goods Cell Search ────────────────────────────────────────────────────────

        private void PopulateGoodsCellSearchColumns(string sectionName)
        {
            cmbGoodsCellCol.Items.Clear();
            List<string> cols = gl.GetColumnNames(sectionName);
            foreach (string col in cols)
                cmbGoodsCellCol.Items.Add(col);
            if (cmbGoodsCellCol.Items.Count > 0)
                cmbGoodsCellCol.SelectedIndex = 0;

            // Lazy-init goods search timer
            if (goodsCellSearchTimer == null)
            {
                goodsCellSearchTimer = new Timer { Interval = 300 };
                goodsCellSearchTimer.Tick += (s, ev) =>
                {
                    goodsCellSearchTimer.Stop();
                    ApplyGoodsCellFilter();
                };
            }
        }

        private void ClearGoodsCellSearch()
        {
            txtGoodsCellSearch.Text = "";
            if (goodsBS != null)
                goodsBS.Filter = null;
        }

        private void CmbGoodsCellCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGoodsCellSearch.Text))
                ApplyGoodsCellFilter();
        }

        private void TxtGoodsCellSearch_TextChanged(object sender, EventArgs e)
        {
            _goodsFindMatchIndex = -1;
            if (goodsCellSearchTimer == null) return;
            goodsCellSearchTimer.Stop();
            goodsCellSearchTimer.Start();
        }

        private void ApplyGoodsCellFilter()
        {
            if (goodsBS == null || goodsBS.DataSource == null) return;
            string col  = cmbGoodsCellCol.SelectedItem as string;
            string text = txtGoodsCellSearch.Text;

            if (string.IsNullOrEmpty(col) || string.IsNullOrEmpty(text))
            {
                goodsBS.Filter = null;
                return;
            }

            string escaped = text.Replace("'", "''");
            goodsBS.Filter = "[" + col + "] LIKE '%" + escaped + "%'";
        }

        // ════════════════════════════════════════════════════════════════════════════
        // Undo / Redo  (snapshot-based, per DataGridView)
        // ════════════════════════════════════════════════════════════════════════════

        private readonly Dictionary<DataGridView, Stack<string>> _undoStacks
            = new Dictionary<DataGridView, Stack<string>>();
        private readonly Dictionary<DataGridView, Stack<string>> _redoStacks
            = new Dictionary<DataGridView, Stack<string>>();
        private const int MaxUndoSteps = 50;

        // Returns the DataTable behind a DGV (via its BindingSource).
        private static DataTable GetDgvTable(DataGridView dgv)
        {
            if (dgv == null) return null;
            BindingSource bs = dgv.DataSource as BindingSource;
            return bs?.DataSource as DataTable;
        }

        // Snapshot the current DataTable contents so it can be restored later.
        private static string SerializeTable(DataTable dt)
        {
            using (var sw = new StringWriter())
            {
                dt.WriteXml(sw, XmlWriteMode.IgnoreSchema);
                return sw.ToString();
            }
        }

        // Restore DataTable contents in-place (preserving the original DataTable reference
        // so DatLoader / GoodsLoader still point at the same object).
        private static void RestoreTableInPlace(DataTable current, string snapshot)
        {
            DataTable temp = current.Clone();
            using (var sr = new StringReader(snapshot))
                temp.ReadXml(sr);
            current.Clear();
            foreach (DataRow dr in temp.Rows)
                current.Rows.Add(dr.ItemArray);
        }

        private void PushUndo(DataGridView dgv)
        {
            DataTable dt = GetDgvTable(dgv);
            if (dt == null) return;

            if (!_undoStacks.ContainsKey(dgv)) _undoStacks[dgv] = new Stack<string>();
            if (!_redoStacks.ContainsKey(dgv)) _redoStacks[dgv] = new Stack<string>();

            _undoStacks[dgv].Push(SerializeTable(dt));
            if (_undoStacks[dgv].Count > MaxUndoSteps)
            {
                var list = new List<string>(_undoStacks[dgv]);
                list.RemoveAt(list.Count - 1);
                _undoStacks[dgv] = new Stack<string>(list);
            }
            _redoStacks[dgv].Clear();
        }

        private void Undo(DataGridView dgv)
        {
            if (dgv == null) return;
            if (!_undoStacks.ContainsKey(dgv) || _undoStacks[dgv].Count == 0) return;
            DataTable dt = GetDgvTable(dgv);
            if (dt == null) return;

            if (!_redoStacks.ContainsKey(dgv)) _redoStacks[dgv] = new Stack<string>();
            _redoStacks[dgv].Push(SerializeTable(dt));
            RestoreTableInPlace(dt, _undoStacks[dgv].Pop());
        }

        private void Redo(DataGridView dgv)
        {
            if (dgv == null) return;
            if (!_redoStacks.ContainsKey(dgv) || _redoStacks[dgv].Count == 0) return;
            DataTable dt = GetDgvTable(dgv);
            if (dt == null) return;

            if (!_undoStacks.ContainsKey(dgv)) _undoStacks[dgv] = new Stack<string>();
            _undoStacks[dgv].Push(SerializeTable(dt));
            RestoreTableInPlace(dt, _redoStacks[dgv].Pop());
        }

        // ════════════════════════════════════════════════════════════════════════════
        // Clipboard  (Cut / Copy / Paste)
        // ════════════════════════════════════════════════════════════════════════════

        private static void GridCopy(DataGridView dgv)
        {
            if (dgv == null) return;
            dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            DataObject data = dgv.GetClipboardContent();
            if (data != null) Clipboard.SetDataObject(data);
        }

        private void GridCut(DataGridView dgv)
        {
            if (dgv == null) return;
            PushUndo(dgv);
            GridCopy(dgv);
            foreach (DataGridViewCell cell in dgv.SelectedCells)
            {
                if (!cell.ReadOnly && !dgv.Rows[cell.RowIndex].IsNewRow)
                    cell.Value = "";
            }
        }

        // "Don't show again" flags (session-scoped).
        private bool _pasteOverrideNoAsk  = false;
        private bool _pasteInsertRowsNoAsk = false;

        private void GridPaste(DataGridView dgv)
        {
            if (dgv == null) return;
            string text = Clipboard.GetText();
            if (string.IsNullOrEmpty(text)) return;

            string[][] cells = ParseClipboardText(text);
            if (cells == null || cells.Length == 0) return;

            int startRow = dgv.CurrentCell != null ? dgv.CurrentCell.RowIndex : 0;
            int startCol = dgv.CurrentCell != null ? dgv.CurrentCell.ColumnIndex : 0;

            // Check for data override.
            if (!_pasteOverrideNoAsk)
            {
                bool hasExisting = false;
                for (int r = 0; r < cells.Length && !hasExisting; r++)
                {
                    int dgvRow = startRow + r;
                    if (dgvRow >= dgv.Rows.Count) break;
                    if (dgv.Rows[dgvRow].IsNewRow) break;
                    for (int c = 0; c < cells[r].Length && !hasExisting; c++)
                    {
                        int dgvCol = startCol + c;
                        if (dgvCol >= dgv.Columns.Count) break;
                        string val = dgv.Rows[dgvRow].Cells[dgvCol].Value?.ToString();
                        if (!string.IsNullOrEmpty(val)) hasExisting = true;
                    }
                }
                if (hasExisting)
                {
                    using (var dlg = new DontShowAgainDialog(
                        "Paste will override existing cell values. Continue?", "Override Data"))
                    {
                        if (dlg.ShowDialog(this) != DialogResult.OK) return;
                        if (dlg.DontShowAgain) _pasteOverrideNoAsk = true;
                    }
                }
            }

            // Check if more rows are needed.
            DataTable dt = GetDgvTable(dgv);
            if (dt != null)
            {
                int rowsNeeded = startRow + cells.Length;
                if (rowsNeeded > dt.Rows.Count)
                {
                    int needed = rowsNeeded - dt.Rows.Count;
                    if (!_pasteInsertRowsNoAsk)
                    {
                        using (var dlg = new DontShowAgainDialog(
                            "Paste needs " + needed + " more row(s). Insert them?", "Insert Rows"))
                        {
                            if (dlg.ShowDialog(this) != DialogResult.OK) return;
                            if (dlg.DontShowAgain) _pasteInsertRowsNoAsk = true;
                        }
                    }
                    for (int i = 0; i < needed; i++)
                    {
                        DataRow newRow = dt.NewRow();
                        foreach (DataColumn col in dt.Columns) newRow[col] = "";
                        dt.Rows.Add(newRow);
                    }
                }
            }

            PushUndo(dgv);
            for (int r = 0; r < cells.Length; r++)
            {
                int dgvRow = startRow + r;
                if (dgvRow >= dgv.Rows.Count) break;
                if (dgv.Rows[dgvRow].IsNewRow) break;
                for (int c = 0; c < cells[r].Length; c++)
                {
                    int dgvCol = startCol + c;
                    if (dgvCol >= dgv.Columns.Count) break;
                    if (!dgv.Rows[dgvRow].Cells[dgvCol].ReadOnly)
                        dgv.Rows[dgvRow].Cells[dgvCol].Value = StripCdata(cells[r][c]);
                }
            }
        }

        private static string StripCdata(string val)
        {
            if (val == null) return "";
            val = val.Trim();
            if (val.StartsWith("<![CDATA[") && val.EndsWith("]]>"))
                return val.Substring(9, val.Length - 12);
            return val;
        }

        // Parse clipboard text as wiki table, TSV, or CSV.
        private static string[][] ParseClipboardText(string text)
        {
            text = text.Replace("\r\n", "\n").Replace("\r", "\n").TrimEnd('\n');
            string[] lines = text.Split('\n');
            if (lines.Length == 0) return null;

            bool isWiki = lines[0].TrimStart().StartsWith("|");
            if (isWiki)
            {
                var rows = new List<string[]>();
                foreach (string line in lines)
                {
                    string t = line.Trim();
                    if (string.IsNullOrEmpty(t)) continue;
                    if (t.StartsWith("|")) t = t.Substring(1);
                    if (t.EndsWith("|"))   t = t.Substring(0, t.Length - 1);
                    string[] parts = t.Split('|');
                    for (int i = 0; i < parts.Length; i++) parts[i] = parts[i].Trim();
                    rows.Add(parts);
                }
                return rows.ToArray();
            }

            bool isTsv = lines[0].Contains("\t");
            if (isTsv)
                return System.Array.ConvertAll(lines, l => l.Split('\t'));

            // CSV fallback
            return System.Array.ConvertAll(lines, ParseCsvLine);
        }

        private static string[] ParseCsvLine(string line)
        {
            var fields = new List<string>();
            bool inQuote = false;
            var current = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                char ch = line[i];
                if (inQuote)
                {
                    if (ch == '"' && i + 1 < line.Length && line[i + 1] == '"')
                    { current.Append('"'); i++; }
                    else if (ch == '"') inQuote = false;
                    else current.Append(ch);
                }
                else
                {
                    if (ch == '"') { inQuote = true; }
                    else if (ch == ',') { fields.Add(current.ToString()); current.Clear(); }
                    else current.Append(ch);
                }
            }
            fields.Add(current.ToString());
            return fields.ToArray();
        }

        // ════════════════════════════════════════════════════════════════════════════
        // Fill Down / Up / Right / Left
        // ════════════════════════════════════════════════════════════════════════════

        private static void GridFillDown(DataGridView dgv)
        {
            if (dgv == null || dgv.SelectedCells.Count < 2) return;
            var byCol = new Dictionary<int, List<int>>();
            foreach (DataGridViewCell cell in dgv.SelectedCells)
            {
                if (!byCol.ContainsKey(cell.ColumnIndex)) byCol[cell.ColumnIndex] = new List<int>();
                byCol[cell.ColumnIndex].Add(cell.RowIndex);
            }
            foreach (var kvp in byCol)
            {
                kvp.Value.Sort();
                int topRow = kvp.Value[0];
                object topVal = dgv.Rows[topRow].Cells[kvp.Key].Value;
                for (int i = 1; i < kvp.Value.Count; i++)
                {
                    int r = kvp.Value[i];
                    if (!dgv.Rows[r].IsNewRow && !dgv.Rows[r].Cells[kvp.Key].ReadOnly)
                        dgv.Rows[r].Cells[kvp.Key].Value = topVal;
                }
            }
        }

        private static void GridFillUp(DataGridView dgv)
        {
            if (dgv == null || dgv.SelectedCells.Count < 2) return;
            var byCol = new Dictionary<int, List<int>>();
            foreach (DataGridViewCell cell in dgv.SelectedCells)
            {
                if (!byCol.ContainsKey(cell.ColumnIndex)) byCol[cell.ColumnIndex] = new List<int>();
                byCol[cell.ColumnIndex].Add(cell.RowIndex);
            }
            foreach (var kvp in byCol)
            {
                kvp.Value.Sort();
                int bottomRow = kvp.Value[kvp.Value.Count - 1];
                object bottomVal = dgv.Rows[bottomRow].Cells[kvp.Key].Value;
                for (int i = kvp.Value.Count - 2; i >= 0; i--)
                {
                    int r = kvp.Value[i];
                    if (!dgv.Rows[r].IsNewRow && !dgv.Rows[r].Cells[kvp.Key].ReadOnly)
                        dgv.Rows[r].Cells[kvp.Key].Value = bottomVal;
                }
            }
        }

        private static void GridFillRight(DataGridView dgv)
        {
            if (dgv == null || dgv.SelectedCells.Count < 2) return;
            var byRow = new Dictionary<int, List<int>>();
            foreach (DataGridViewCell cell in dgv.SelectedCells)
            {
                if (!byRow.ContainsKey(cell.RowIndex)) byRow[cell.RowIndex] = new List<int>();
                byRow[cell.RowIndex].Add(cell.ColumnIndex);
            }
            foreach (var kvp in byRow)
            {
                if (dgv.Rows[kvp.Key].IsNewRow) continue;
                kvp.Value.Sort();
                object leftVal = dgv.Rows[kvp.Key].Cells[kvp.Value[0]].Value;
                for (int i = 1; i < kvp.Value.Count; i++)
                {
                    int c = kvp.Value[i];
                    if (!dgv.Rows[kvp.Key].Cells[c].ReadOnly)
                        dgv.Rows[kvp.Key].Cells[c].Value = leftVal;
                }
            }
        }

        private static void GridFillLeft(DataGridView dgv)
        {
            if (dgv == null || dgv.SelectedCells.Count < 2) return;
            var byRow = new Dictionary<int, List<int>>();
            foreach (DataGridViewCell cell in dgv.SelectedCells)
            {
                if (!byRow.ContainsKey(cell.RowIndex)) byRow[cell.RowIndex] = new List<int>();
                byRow[cell.RowIndex].Add(cell.ColumnIndex);
            }
            foreach (var kvp in byRow)
            {
                if (dgv.Rows[kvp.Key].IsNewRow) continue;
                kvp.Value.Sort();
                int rightCol = kvp.Value[kvp.Value.Count - 1];
                object rightVal = dgv.Rows[kvp.Key].Cells[rightCol].Value;
                for (int i = kvp.Value.Count - 2; i >= 0; i--)
                {
                    int c = kvp.Value[i];
                    if (!dgv.Rows[kvp.Key].Cells[c].ReadOnly)
                        dgv.Rows[kvp.Key].Cells[c].Value = rightVal;
                }
            }
        }

        // ════════════════════════════════════════════════════════════════════════════
        // Delete key  (rows or cell content depending on selection)
        // ════════════════════════════════════════════════════════════════════════════

        private void GridDeleteKey(DataGridView dgv)
        {
            if (dgv == null) return;

            bool hasFullRows = false;
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                if (!row.IsNewRow) { hasFullRows = true; break; }
            }

            PushUndo(dgv);
            if (hasFullRows)
            {
                GridDeleteSelectedRows(dgv);
            }
            else
            {
                foreach (DataGridViewCell cell in dgv.SelectedCells)
                {
                    if (!cell.ReadOnly && !dgv.Rows[cell.RowIndex].IsNewRow)
                        cell.Value = "";
                }
            }
        }

        // ════════════════════════════════════════════════════════════════════════════
        // Find / Replace
        // ════════════════════════════════════════════════════════════════════════════

        private int _libFindMatchIndex  = -1;
        private int _goodsFindMatchIndex = -1;

        private void TriggerFind(bool onLibConfig)
        {
            DataGridView dgv    = onLibConfig ? dataGridView1 : dataGridViewGoods;
            BindingSource bs    = onLibConfig ? libConfigBS   : goodsBS;
            TextBox findBox     = onLibConfig ? txtLibCellSearch : txtGoodsCellSearch;

            if (!findBox.Focused && !string.IsNullOrEmpty(findBox.Text) && bs.Count > 0)
            {
                // Advance to next visible match row.
                int prev  = onLibConfig ? _libFindMatchIndex : _goodsFindMatchIndex;
                int next  = (prev + 1) % bs.Count;
                if (onLibConfig) _libFindMatchIndex   = next;
                else             _goodsFindMatchIndex = next;

                try
                {
                    int colIdx = dgv.CurrentCell != null ? dgv.CurrentCell.ColumnIndex : 0;
                    dgv.CurrentCell = dgv.Rows[next].Cells[Math.Max(0, colIdx)];
                    dgv.FirstDisplayedScrollingRowIndex = next;
                }
                catch { }
            }
            else
            {
                findBox.Focus();
                findBox.SelectAll();
            }
        }

        private void TriggerReplace(bool onLibConfig)
        {
            TextBox replaceBox = onLibConfig ? txtLibReplace   : txtGoodsReplace;
            TextBox findBox    = onLibConfig ? txtLibCellSearch : txtGoodsCellSearch;
            DataGridView dgv   = onLibConfig ? dataGridView1   : dataGridViewGoods;
            BindingSource bs   = onLibConfig ? libConfigBS     : goodsBS;

            if (replaceBox.Focused)
                DoReplace(dgv, findBox, replaceBox, bs);
            else
            {
                replaceBox.Focus();
                replaceBox.SelectAll();
            }
        }

        // Replace all occurrences of Find text with Replace text in the currently
        // visible (filtered) rows of the active table.
        private void DoReplace(DataGridView dgv, TextBox findBox, TextBox replaceBox, BindingSource bs)
        {
            string find    = findBox.Text;
            string replace = replaceBox.Text;
            if (string.IsNullOrEmpty(find) || bs == null || bs.DataSource == null) return;

            PushUndo(dgv);
            int count = 0;
            DataTable dt = bs.DataSource as DataTable;
            if (dt == null) return;

            for (int i = 0; i < bs.Count; i++)
            {
                DataRowView drv = (DataRowView)bs[i];
                DataRow row     = drv.Row;
                foreach (DataColumn col in dt.Columns)
                {
                    string val = row[col]?.ToString() ?? "";
                    if (val.IndexOf(find, StringComparison.Ordinal) >= 0)
                    {
                        row[col] = val.Replace(find, replace);
                        count++;
                    }
                }
            }
            MessageBox.Show("Replaced " + count + " cell occurrence(s).");
        }

        // Called by Enter key in either Replace textbox.
        private void TxtLibReplace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                DoReplace(dataGridView1, txtLibCellSearch, txtLibReplace, libConfigBS);
                e.Handled = true; e.SuppressKeyPress = true;
            }
        }

        private void TxtGoodsReplace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                DoReplace(dataGridViewGoods, txtGoodsCellSearch, txtGoodsReplace, goodsBS);
                e.Handled = true; e.SuppressKeyPress = true;
            }
        }

        // Also reset match index when Find text changes.
        // (existing TxtLibCellSearch_TextChanged / TxtGoodsCellSearch_TextChanged already
        //  restart the filter timer; we piggyback the index reset here)

        // ════════════════════════════════════════════════════════════════════════════
        // Keyboard shortcuts  (ProcessCmdKey intercepts before child controls)
        // ════════════════════════════════════════════════════════════════════════════

        // Update the status bar with a short message.
        private void SetStatus(string msg)
        {
            toolStripStatusLabel1.Text = msg;
        }

        private bool IsEditControlFocused()
        {
            Control c = this.ActiveControl;
            return c is TextBox || c is ComboBox || c is RichTextBox;
        }

        private DataGridView ActiveEditorGrid()
        {
            int tab = tabControl1.SelectedIndex;
            if (tab == 2) return dataGridView1;
            if (tab == 3) return dataGridViewGoods;
            return null;
        }

        private void TriggerOpenForCurrentTab()
        {
            int tab = tabControl1.SelectedIndex;
            if (tab == 2) button7_Click(null, EventArgs.Empty);
            else if (tab == 3) BtnLoadGoods_Click(null, EventArgs.Empty);
        }

        // Paths for Ctrl+Shift+S quick-save: track both the load (source) and last export path.
        private string _libLoadPath       = null;
        private string _goodsLoadPath     = null;
        private string _lastLibSavePath   = null;
        private string _lastGoodsSavePath = null;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int tabIdx    = tabControl1.SelectedIndex;
            bool onLib    = tabIdx == 2;
            bool onGoods  = tabIdx == 3;
            bool onEditor = onLib || onGoods;

            DataGridView dgv = ActiveEditorGrid();

            // ── Tab navigation (global, any tab) ────────────────────────────────────
            if (keyData == (Keys.Control | Keys.Shift | Keys.N))
            { tabControl1.SelectedIndex = 0; return true; }  // NRI Viewer
            if (keyData == (Keys.Control | Keys.Shift | Keys.E))
            { tabControl1.SelectedIndex = 1; return true; }  // DAT Viewer
            if (keyData == (Keys.Control | Keys.Shift | Keys.L))
            { tabControl1.SelectedIndex = 2; return true; }  // LibConfig Editor
            if (keyData == (Keys.Control | Keys.Shift | Keys.I))
            { tabControl1.SelectedIndex = 3; return true; }  // libcmgds/Goods Editor

            // Next / prev main tab
            if (keyData == (Keys.Control | Keys.Shift | Keys.Next))
            {
                int next = (tabControl1.SelectedIndex + 1) % tabControl1.TabCount;
                tabControl1.SelectedIndex = next; return true;
            }
            if (keyData == (Keys.Control | Keys.Shift | Keys.Prior))
            {
                int prev = (tabControl1.SelectedIndex - 1 + tabControl1.TabCount) % tabControl1.TabCount;
                tabControl1.SelectedIndex = prev; return true;
            }

            // ── Editor-only shortcuts ────────────────────────────────────────────────
            if (!onEditor) return base.ProcessCmdKey(ref msg, keyData);

            // Undo / Redo
            if (keyData == (Keys.Control | Keys.Z))
            {
                bool hadUndo = dgv != null && _undoStacks.ContainsKey(dgv) && _undoStacks[dgv].Count > 0;
                Undo(dgv);
                SetStatus(hadUndo ? "Undo applied." : "Nothing to undo.");
                return true;
            }
            if (keyData == (Keys.Control | Keys.Y))
            {
                bool hadRedo = dgv != null && _redoStacks.ContainsKey(dgv) && _redoStacks[dgv].Count > 0;
                Redo(dgv);
                SetStatus(hadRedo ? "Redo applied." : "Nothing to redo.");
                return true;
            }

            // Open file
            if (keyData == (Keys.Control | Keys.O))
            { TriggerOpenForCurrentTab(); return true; }

            // Find / Replace
            if (keyData == (Keys.Control | Keys.F))
            { TriggerFind(onLib); return true; }
            if (keyData == (Keys.Control | Keys.H))
            { TriggerReplace(onLib); return true; }

            // Next / prev table in current editor's list
            if (keyData == (Keys.Control | Keys.Next))
            {
                if (onLib && listBox2.Items.Count > 0)
                    listBox2.SelectedIndex = Math.Min(listBox2.SelectedIndex + 1, listBox2.Items.Count - 1);
                else if (onGoods && lstGoodsSections.Items.Count > 0)
                    lstGoodsSections.SelectedIndex = Math.Min(lstGoodsSections.SelectedIndex + 1, lstGoodsSections.Items.Count - 1);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Prior))
            {
                if (onLib && listBox2.Items.Count > 0)
                    listBox2.SelectedIndex = Math.Max(listBox2.SelectedIndex - 1, 0);
                else if (onGoods && lstGoodsSections.Items.Count > 0)
                    lstGoodsSections.SelectedIndex = Math.Max(lstGoodsSections.SelectedIndex - 1, 0);
                return true;
            }

            // Localization Helper – both editors
            if (keyData == (Keys.Control | Keys.L))
            { button10_Click(null, EventArgs.Empty); return true; }

            // Actions available on the LibConfig editor
            if (onLib)
            {
                if (keyData == (Keys.Control | Keys.U))
                { button12_Click(null, EventArgs.Empty); return true; }  // Update Table
                if (keyData == (Keys.Control | Keys.M))
                { button8_Click(null, EventArgs.Empty); return true; }   // Import Table
                if (keyData == (Keys.Control | Keys.B))
                { RenameCurrentTable(); return true; }                   // Rename Table
                if (keyData == (Keys.Control | Keys.T))
                { InsertNewTable(); return true; }                        // New Table
            }

            // Actions available on the Goods editor
            if (onGoods)
            {
                if (keyData == (Keys.Control | Keys.U))
                { BtnGoodsUpdate_Click(null, EventArgs.Empty); return true; }
                // Ctrl+M  – import CSV into the current section
                if (keyData == (Keys.Control | Keys.M))
                { BtnGoodsImport_Click(null, EventArgs.Empty); return true; }
            }

            // Export menu  (Ctrl+S shows the dropdown; Ctrl+Shift+S quick-saves)
            if (keyData == (Keys.Control | Keys.S))
            {
                if (onLib)   contextMenuStrip1.Show(button9, 0, button9.Height);
                if (onGoods) contextMenuStripGoods.Show(btnGoodsExport, 0, btnGoodsExport.Height);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Shift | Keys.S))
            { QuickSave(onLib); return true; }

            // ── Shortcuts skipped when a text input has focus ────────────────────────
            if (!IsEditControlFocused())
            {
                if (keyData == Keys.Delete)
                { GridDeleteKey(dgv); SetStatus("Deleted."); return true; }
                if (keyData == (Keys.Control | Keys.X))
                { GridCut(dgv); SetStatus("Cut to clipboard."); return true; }
                if (keyData == (Keys.Control | Keys.C))
                { GridCopy(dgv); SetStatus("Copied to clipboard."); return true; }
                if (keyData == (Keys.Control | Keys.V))
                { GridPaste(dgv); SetStatus("Pasted from clipboard."); return true; }

                // Fill shortcuts
                if (keyData == (Keys.Control | Keys.D))
                { PushUndo(dgv); GridFillDown(dgv); SetStatus("Fill Down applied."); return true; }
                if (keyData == (Keys.Control | Keys.Shift | Keys.D))
                { PushUndo(dgv); GridFillUp(dgv); SetStatus("Fill Up applied."); return true; }
                if (keyData == (Keys.Control | Keys.R))
                { PushUndo(dgv); GridFillRight(dgv); SetStatus("Fill Right applied."); return true; }
                if (keyData == (Keys.Control | Keys.Shift | Keys.R))
                { PushUndo(dgv); GridFillLeft(dgv); SetStatus("Fill Left applied."); return true; }

                // Navigate to first / last row
                if (keyData == (Keys.Control | Keys.Home) && dgv != null && dgv.RowCount > 0)
                {
                    dgv.ClearSelection();
                    dgv.CurrentCell = dgv[0, 0];
                    dgv.FirstDisplayedScrollingRowIndex = 0;
                    return true;
                }
                if (keyData == (Keys.Control | Keys.End) && dgv != null && dgv.RowCount > 0)
                {
                    int lastRow = dgv.RowCount - (dgv.AllowUserToAddRows ? 2 : 1);
                    if (lastRow >= 0)
                    {
                        dgv.ClearSelection();
                        dgv.CurrentCell = dgv[0, lastRow];
                        dgv.FirstDisplayedScrollingRowIndex = lastRow;
                    }
                    return true;
                }

                // Select entire current column (Ctrl+Space)
                if (keyData == (Keys.Control | Keys.Space) && dgv != null && dgv.CurrentCell != null)
                {
                    int col = dgv.CurrentCell.ColumnIndex;
                    dgv.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                    dgv.Columns[col].Selected = true;
                    // Restore cell select so normal navigation still works
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    return true;
                }

                // Select entire current row (Shift+Space)
                if (keyData == (Keys.Shift | Keys.Space) && dgv != null && dgv.CurrentCell != null)
                {
                    int row = dgv.CurrentCell.RowIndex;
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.Rows[row].Selected = true;
                    // Restore cell select so normal navigation still works
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Create a new empty table and select it in the listbox (Ctrl+T).
        private void InsertNewTable()
        {
            if (dl.GetTableCount() == 0 && string.IsNullOrEmpty(_libLoadPath))
            {
                MessageBox.Show("Please load a LibConfig file first before adding tables.", "No File Loaded");
                return;
            }
            string name = ShowInputDialog("New Table", "Table name:", "");
            if (name == null) return;
            name = name.Trim();
            if (string.IsNullOrEmpty(name))
            { MessageBox.Show("Table name cannot be empty.", "Invalid Name"); return; }

            int newIdx = dl.AddTable(name);
            if (newIdx < 0)
            { MessageBox.Show("Failed to create table.", "Error"); return; }

            tables = dl.GetTableList();
            UpdateListbox();

            // Select the new table in the listbox.
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                string item = listBox2.Items[i] as string;
                if (item != null && item.StartsWith(newIdx + "."))
                {
                    listBox2.SelectedIndex = i;
                    break;
                }
            }
            SetStatus("Table \"" + name + "\" created. Use Add Column to define its fields.");
        }

        // Rename the currently-selected LibConfig table.
        private void RenameCurrentTable()
        {
            if (currenttable < 0) { MessageBox.Show("No table selected."); return; }
            string current = tables.Count > currenttable ? tables[currenttable] : "";
            string newName = ShowInputDialog("Rename Table", "New table name:", current);
            if (newName == null) return;
            newName = newName.Trim();
            if (string.IsNullOrEmpty(newName)) return;
            if (dl.RenameTable(currenttable, newName))
            {
                tables = dl.GetTableList();
                int savedIndex = listBox2.SelectedIndex;
                UpdateListbox();
                listBox2.SelectedIndex = Math.Min(savedIndex, listBox2.Items.Count - 1);
                label1.Text = "Table: " + currenttable + ". " + newName;
                SetStatus("Table renamed to \"" + newName + "\".");
            }
            else
            {
                MessageBox.Show("Failed to rename table.", "Error");
            }
        }

        // Quick-save (Ctrl+Shift+S): write XML back to the original load path.
        // Falls back to the export XML dialog if no file has been loaded yet.
        private void QuickSave(bool isLib)
        {
            if (isLib)
            {
                string target = _libLoadPath;
                if (string.IsNullOrEmpty(target))
                { xMLToolStripMenuItem_Click(null, EventArgs.Empty); return; }

                byte[] data = dl.ExportXML();
                if (data == null) { MessageBox.Show("Failed to export. " + dl.GetStatus()); return; }
                using (BinaryWriter bw = new BinaryWriter(File.Create(target)))
                    bw.Write(data);
                SetStatus("Quick-saved to " + Path.GetFileName(target));
            }
            else
            {
                string target = _goodsLoadPath;
                if (string.IsNullOrEmpty(target))
                { GoodsExportXml_Click(null, EventArgs.Empty); return; }

                try
                {
                    byte[] data = gl.ExportXml();
                    using (BinaryWriter bw = new BinaryWriter(File.Create(target)))
                        bw.Write(data);
                    SetStatus("Quick-saved to " + Path.GetFileName(target));
                }
                catch (Exception ex) { MessageBox.Show("Quick-save failed: " + ex.Message); }
            }
        }

        // ════════════════════════════════════════════════════════════════════════════
        // Grid row editing (right-click context menu, shared by both editor grids)
        // ════════════════════════════════════════════════════════════════════════════

        // Track whether Shift was held on the right-click that opened the context menu.
        private bool _rightClickShift = false;

        // Selects the row under the cursor on right-click so the context menu
        // always acts on the row the user actually pointed at.
        private void DataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            _rightClickShift = (Control.ModifierKeys & Keys.Shift) != 0;

            var dgv = (DataGridView)sender;
            DataGridView.HitTestInfo hit = dgv.HitTest(e.X, e.Y);
            if (hit.RowIndex >= 0 && hit.RowIndex < dgv.Rows.Count)
            {
                if (!dgv.Rows[hit.RowIndex].Selected)
                {
                    dgv.ClearSelection();
                    dgv.Rows[hit.RowIndex].Selected = true;
                }
                int col = hit.ColumnIndex >= 0 ? hit.ColumnIndex : 0;
                dgv.CurrentCell = dgv[col, hit.RowIndex];
            }
        }

        private void ContextMenuStripGridEdit_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataGridView dgv  = contextMenuStripGridEdit.SourceControl as DataGridView;
            bool hasRow       = dgv?.CurrentRow != null && !dgv.CurrentRow.IsNewRow;
            bool hasSelection = hasRow && dgv.SelectedRows.Count > 0 && !dgv.SelectedRows[0].IsNewRow;
            bool hasCells     = dgv != null && dgv.SelectedCells.Count > 0;
            bool multiCells   = hasCells && dgv.SelectedCells.Count >= 2;
            bool hasClipboard = !string.IsNullOrEmpty(Clipboard.GetText());
            bool hasUndo = dgv != null && _undoStacks.ContainsKey(dgv) && _undoStacks[dgv].Count > 0;
            bool hasRedo = dgv != null && _redoStacks.ContainsKey(dgv) && _redoStacks[dgv].Count > 0;
            bool shift   = _rightClickShift;

            gridCutMenuItem.Enabled    = hasCells;
            gridCopyMenuItem.Enabled   = hasCells;
            gridPasteMenuItem.Enabled  = hasClipboard;

            // Shift toggles which row/column insert variant is shown.
            gridInsertAboveMenuItem.Visible = !shift;
            gridInsertAboveMenuItem.Enabled = !shift && hasRow;
            gridInsertBelowMenuItem.Visible = shift;
            gridInsertBelowMenuItem.Enabled = shift && hasRow;
            gridAddColLeftMenuItem.Visible  = !shift;
            gridAddColLeftMenuItem.Enabled  = !shift;
            gridAddColRightMenuItem.Visible = shift;
            gridAddColRightMenuItem.Enabled = shift;

            // Shift toggles fill direction pairs.
            gridFillDownMenuItem.Visible  = !shift;
            gridFillDownMenuItem.Enabled  = !shift && multiCells;
            gridFillUpMenuItem.Visible    = shift;
            gridFillUpMenuItem.Enabled    = shift && multiCells;
            gridFillRightMenuItem.Visible = !shift;
            gridFillRightMenuItem.Enabled = !shift && multiCells;
            gridFillLeftMenuItem.Visible  = shift;
            gridFillLeftMenuItem.Enabled  = shift && multiCells;

            gridDeleteRowMenuItem.Enabled = hasSelection || hasCells;
            gridUndoMenuItem.Enabled      = hasUndo;
            gridRedoMenuItem.Enabled      = hasRedo;
        }

        private void GridCut_Click(object sender, EventArgs e)
        {
            GridCut(contextMenuStripGridEdit.SourceControl as DataGridView);
        }

        private void GridCopy_Click(object sender, EventArgs e)
        {
            GridCopy(contextMenuStripGridEdit.SourceControl as DataGridView);
        }

        private void GridPaste_Click(object sender, EventArgs e)
        {
            GridPaste(contextMenuStripGridEdit.SourceControl as DataGridView);
        }

        private void GridFillDown_Click(object sender, EventArgs e)
        {
            DataGridView dgv = contextMenuStripGridEdit.SourceControl as DataGridView;
            PushUndo(dgv);
            GridFillDown(dgv);
        }

        private void GridUndo_Click(object sender, EventArgs e)
        {
            Undo(contextMenuStripGridEdit.SourceControl as DataGridView);
        }

        private void GridRedo_Click(object sender, EventArgs e)
        {
            Redo(contextMenuStripGridEdit.SourceControl as DataGridView);
        }

        private void GridFillUp_Click(object sender, EventArgs e)
        {
            DataGridView dgv = contextMenuStripGridEdit.SourceControl as DataGridView;
            PushUndo(dgv);
            GridFillUp(dgv);
        }

        private void GridFillRight_Click(object sender, EventArgs e)
        {
            DataGridView dgv = contextMenuStripGridEdit.SourceControl as DataGridView;
            PushUndo(dgv);
            GridFillRight(dgv);
        }

        private void GridFillLeft_Click(object sender, EventArgs e)
        {
            DataGridView dgv = contextMenuStripGridEdit.SourceControl as DataGridView;
            PushUndo(dgv);
            GridFillLeft(dgv);
        }

        private void GridAddColLeft_Click(object sender, EventArgs e)
        {
            GridAddColumn(contextMenuStripGridEdit.SourceControl as DataGridView, insertRight: false);
        }

        private void GridAddColRight_Click(object sender, EventArgs e)
        {
            GridAddColumn(contextMenuStripGridEdit.SourceControl as DataGridView, insertRight: true);
        }

        private void GridInsertAbove_Click(object sender, EventArgs e)
        {
            DataGridView dgv = contextMenuStripGridEdit.SourceControl as DataGridView;
            PushUndo(dgv);
            GridInsertRowAt(dgv, insertBelow: false);
        }

        private void GridInsertBelow_Click(object sender, EventArgs e)
        {
            DataGridView dgv = contextMenuStripGridEdit.SourceControl as DataGridView;
            PushUndo(dgv);
            GridInsertRowAt(dgv, insertBelow: true);
        }

        private void GridDeleteRows_Click(object sender, EventArgs e)
        {
            DataGridView dgv = contextMenuStripGridEdit.SourceControl as DataGridView;
            PushUndo(dgv);
            GridDeleteSelectedRows(dgv);
        }

        private static void GridInsertRowAt(DataGridView dgv, bool insertBelow)
        {
            if (dgv?.CurrentRow == null || dgv.CurrentRow.IsNewRow) return;
            DataRowView drv = dgv.CurrentRow.DataBoundItem as DataRowView;
            if (drv == null) return;

            DataTable dt  = drv.Row.Table;
            int baseIdx   = dt.Rows.IndexOf(drv.Row);
            int insertIdx = insertBelow ? baseIdx + 1 : baseIdx;
            insertIdx     = Math.Max(0, Math.Min(insertIdx, dt.Rows.Count));

            DataRow newRow = dt.NewRow();
            foreach (DataColumn col in dt.Columns)
                newRow[col] = "";
            dt.Rows.InsertAt(newRow, insertIdx);
        }

        private static void GridDeleteSelectedRows(DataGridView dgv)
        {
            if (dgv == null || dgv.SelectedRows.Count == 0) return;

            // Collect DataRow references before modifying the table.
            var toDelete = new List<DataRow>(dgv.SelectedRows.Count);
            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                if (!row.IsNewRow && row.DataBoundItem is DataRowView drv)
                    toDelete.Add(drv.Row);
            }
            foreach (DataRow dr in toDelete)
                dr.Table.Rows.Remove(dr);
        }

        // ── Add Column ───────────────────────────────────────────────────────────────

        private void GridAddColumn(DataGridView dgv, bool insertRight)
        {
            if (dgv == null) return;
            bool isLib   = dgv == dataGridView1;
            bool isGoods = dgv == dataGridViewGoods;
            if (!isLib && !isGoods) return;

            // Insertion ordinal: just past current column (right) or at current column (left).
            int insertOrdinal = -1; // -1 = append
            if (dgv.CurrentCell != null)
            {
                int cur = dgv.CurrentCell.ColumnIndex;
                insertOrdinal = insertRight ? cur + 1 : cur;
            }

            if (isLib)
            {
                using (var dlg = new AddColumnDialog())
                {
                    if (dlg.ShowDialog(this) != DialogResult.OK) return;
                    string colName = dlg.ColumnName.Trim();
                    string colType = dlg.ColumnType;
                    string isKey   = dlg.IsKey ? "1" : "0";
                    if (string.IsNullOrEmpty(colName)) return;

                    DataTable dt = dl.GetTable(currenttable);
                    if (dt == null) return;
                    if (dt.Columns.Contains(colName))
                    { MessageBox.Show("A column with that name already exists."); return; }

                    PushUndo(dgv);

                    // Add DataColumn and fill existing rows with empty string.
                    dt.Columns.Add(new DataColumn(colName, typeof(string)) { DefaultValue = "" });
                    foreach (DataRow row in dt.Rows)
                        if (row[colName] == DBNull.Value) row[colName] = "";

                    // Move to requested position.
                    if (insertOrdinal >= 0 && insertOrdinal < dt.Columns.Count - 1)
                        dt.Columns[colName].SetOrdinal(insertOrdinal);

                    // Register in DatLoader metadata.
                    dl.RegisterTableColumn(currenttable, colName, isKey, colType);

                    PopulateLibCellSearchColumns(dt);
                }
            }
            else // Goods
            {
                string colName = ShowInputDialog("Add Column", "Column name:", "");
                if (colName == null) return;
                colName = colName.Trim();
                if (string.IsNullOrEmpty(colName)) return;

                DataTable dt = goodsBS.DataSource as DataTable;
                if (dt == null) return;
                if (dt.Columns.Contains(colName))
                { MessageBox.Show("A column with that name already exists."); return; }

                PushUndo(dgv);

                dt.Columns.Add(new DataColumn(colName, typeof(string)) { DefaultValue = "" });
                foreach (DataRow row in dt.Rows)
                    if (row[colName] == DBNull.Value) row[colName] = "";

                if (insertOrdinal >= 0 && insertOrdinal < dt.Columns.Count - 1)
                    dt.Columns[colName].SetOrdinal(insertOrdinal);

                PopulateGoodsCellSearchColumns(goodsCurrentSection);
            }
        }

        // Simple single-line input dialog. Returns null if cancelled.
        private static string ShowInputDialog(string title, string prompt, string defaultValue)
        {
            Form dlg = new Form
            {
                Text            = title,
                Width           = 320,
                Height          = 130,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition   = FormStartPosition.CenterParent,
                MaximizeBox     = false,
                MinimizeBox     = false,
                ShowInTaskbar   = false
            };
            Label  lbl    = new Label  { Text = prompt,        Location = new Point(12, 14), AutoSize = true };
            TextBox txt   = new TextBox { Text = defaultValue,  Location = new Point(12, 34), Width = 282 };
            Button btnOk  = new Button  { Text = "OK",     DialogResult = DialogResult.OK,     Location = new Point(128, 62), Width = 80 };
            Button btnCnl = new Button  { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(216, 62), Width = 80 };
            dlg.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCnl });
            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCnl;
            return dlg.ShowDialog() == DialogResult.OK ? txt.Text : null;
        }

        // ════════════════════════════════════════════════════════════════════════════
        // CSV / XML export helpers (shared between both editor tabs)
        // ════════════════════════════════════════════════════════════════════════════

        private static string CsvEscape(string value)
        {
            if (value.IndexOfAny(new char[] { ',', '"', '\n', '\r' }) >= 0)
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }

        // All rows in a DataTable — unfiltered full export.
        private static byte[] DataTableToCsvBytes(DataTable dt)
        {
            if (dt == null) return null;
            var sb = new StringBuilder();
            var header = new List<string>();
            foreach (DataColumn col in dt.Columns)
                header.Add(CsvEscape(col.ColumnName));
            sb.AppendLine(string.Join(",", header));
            foreach (DataRow row in dt.Rows)
            {
                var fields = new List<string>();
                foreach (DataColumn col in dt.Columns)
                    fields.Add(CsvEscape(row[col]?.ToString() ?? ""));
                sb.AppendLine(string.Join(",", fields));
            }
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        // Only the rows currently visible through a BindingSource filter.
        private static byte[] FilteredViewToCsvBytes(BindingSource bs)
        {
            DataTable dt = bs.DataSource as DataTable;
            if (dt == null) return null;
            var sb = new StringBuilder();
            var header = new List<string>();
            foreach (DataColumn col in dt.Columns)
                header.Add(CsvEscape(col.ColumnName));
            sb.AppendLine(string.Join(",", header));
            for (int i = 0; i < bs.Count; i++)
            {
                DataRowView drv = (DataRowView)bs[i];
                var fields = new List<string>();
                foreach (DataColumn col in dt.Columns)
                    fields.Add(CsvEscape(drv[col.ColumnName]?.ToString() ?? ""));
                sb.AppendLine(string.Join(",", fields));
            }
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        // Filtered rows as a simple, self-describing XML document.
        // rootElement wraps everything; rowElement wraps each row.
        private static byte[] FilteredViewToXmlBytes(BindingSource bs, string rootElement, string rowElement)
        {
            DataTable dt = bs.DataSource as DataTable;
            if (dt == null) return null;

            var settings = new System.Xml.XmlWriterSettings
            {
                Encoding    = new UTF8Encoding(false),
                Indent      = true,
                IndentChars = "\t",
                NewLineChars = "\n"
            };

            using (var ms = new MemoryStream())
            {
                using (var xw = System.Xml.XmlWriter.Create(ms, settings))
                {
                    xw.WriteStartDocument();
                    xw.WriteStartElement(rootElement);
                    xw.WriteAttributeString("rowCount", bs.Count.ToString());
                    xw.WriteAttributeString("filtered", "true");

                    for (int i = 0; i < bs.Count; i++)
                    {
                        DataRowView drv = (DataRowView)bs[i];
                        xw.WriteStartElement(rowElement);
                        foreach (DataColumn col in dt.Columns)
                        {
                            string val = drv[col.ColumnName]?.ToString() ?? "";
                            xw.WriteStartElement(col.ColumnName);
                            if (val.IndexOfAny(new char[] { '<', '>', '&', '"', '\'' }) >= 0)
                                xw.WriteCData(val);
                            else
                                xw.WriteString(val);
                            xw.WriteEndElement();
                        }
                        xw.WriteEndElement();
                    }

                    xw.WriteEndElement();
                    xw.WriteEndDocument();
                }
                return ms.ToArray();
            }
        }

        private static void WriteCsvFile(string path, byte[] csvBytes)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Create(path)))
            {
                bw.Write(new byte[] { 0xEF, 0xBB, 0xBF }); // UTF-8 BOM
                if (csvBytes != null) bw.Write(csvBytes);
            }
        }

        // ── LibConfig filtered exports ───────────────────────────────────────────────

        private void xLSFilteredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currenttable < 0 || libConfigBS.DataSource == null)
            { MessageBox.Show("Please select a table to export."); return; }

            SaveFileDialog sfd = new SaveFileDialog { Filter = "CSV file|*.csv|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            byte[] data = FilteredViewToCsvBytes(libConfigBS);
            WriteCsvFile(sfd.FileName, data);
            MessageBox.Show("Export completed.");
        }

        private void xMLFilteredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currenttable < 0 || libConfigBS.DataSource == null)
            { MessageBox.Show("Please select a table to export."); return; }

            SaveFileDialog sfd = new SaveFileDialog { Filter = "XML file|*.xml|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            byte[] data = FilteredViewToXmlBytes(libConfigBS, "TABLE", "ROW");
            using (BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName)))
                bw.Write(data);
            MessageBox.Show("Export completed.");
        }

        // ── Goods CSV exports ────────────────────────────────────────────────────────

        private void GoodsExportCsv_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(goodsCurrentSection) || goodsBS.DataSource == null)
            { MessageBox.Show("No section loaded."); return; }

            SaveFileDialog sfd = new SaveFileDialog { Filter = "CSV file|*.csv|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            byte[] data = DataTableToCsvBytes(goodsBS.DataSource as DataTable);
            WriteCsvFile(sfd.FileName, data);
            MessageBox.Show("Export completed.");
        }

        private void GoodsExportCsvFiltered_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(goodsCurrentSection) || goodsBS.DataSource == null)
            { MessageBox.Show("No section loaded."); return; }

            SaveFileDialog sfd = new SaveFileDialog { Filter = "CSV file|*.csv|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            byte[] data = FilteredViewToCsvBytes(goodsBS);
            WriteCsvFile(sfd.FileName, data);
            MessageBox.Show("Export completed.");
        }

        // ── Help button ──────────────────────────────────────────────────────────────

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            Form helpForm = new Form
            {
                Text            = "Keyboard Shortcut Reference",
                Width           = 520,
                Height          = 560,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition   = FormStartPosition.CenterParent,
                MaximizeBox     = false,
                MinimizeBox     = false,
                ShowInTaskbar   = false
            };

            RichTextBox rtb = new RichTextBox
            {
                ReadOnly   = true,
                ScrollBars = RichTextBoxScrollBars.Vertical,
                Dock       = DockStyle.Fill,
                Font       = new System.Drawing.Font("Consolas", 9F),
                BackColor  = System.Drawing.SystemColors.Window,
                BorderStyle = BorderStyle.None,
                Text =
@"── File Management ──
Ctrl+O → Open
Ctrl+S → Save (opens export menu): x=XML, m=XML filtered, d=DAT unencrypted, e=DAT encrypted, c=CSV full, v=CSV filtered, i=IDX
Ctrl+Shift+S → Quick save over original XML
Ctrl+U → Update Table
Ctrl+M → Import table
Ctrl+L → Localization Helper

── Content Management ──
Ctrl+C → Copy
Ctrl+V → Paste (strips CDATA)
Ctrl+X → Cut
Del → Delete
Ctrl+Z → Undo
Ctrl+Y → Redo

── Advanced Content Management ──
Ctrl+D → Fill Down
Ctrl+Shift+D → Fill Up
Ctrl+R → Fill Right
Ctrl+Shift+R → Fill Left
Ctrl+T → New Table (LibConfig only)
Ctrl+B → Rename Table (LibConfig only)

── Selection ──
Ctrl+A → Select all
Ctrl+Space → Select column
Shift+Space → Select row
Shift+PgDn/PgUp → Select to last/first in column
Shift+End/Home → Select to last/first in row
Shift+Arrow → Extend selection
Ctrl+Shift+Arrow → Extend to last nonblank

── Cell & Table Navigation ──
Ctrl+F → Jump to Find
Ctrl+H → Find & Replace
Ctrl+Home/End → first/last cell
Ctrl+PgDn/PgUp → next/prev table

── Menu Navigation ──
Ctrl+Shift+PgDn/PgUp → next/prev main tab
Ctrl+Shift+N → NRI Viewer
Ctrl+Shift+E → DAT Viewer
Ctrl+Shift+L → LibConfig Editor
Ctrl+Shift+I → libcmgds & rch Editor"
            };

            Button btnClose = new Button
            {
                Text         = "Close",
                DialogResult = DialogResult.Cancel,
                Dock         = DockStyle.Bottom,
                Height       = 28
            };

            helpForm.Controls.Add(rtb);
            helpForm.Controls.Add(btnClose);
            helpForm.CancelButton = btnClose;
            helpForm.ShowDialog(this);
        }

        // ── libcmgds Update / Import / Localization Helper buttons ───────────────────

        private void BtnGoodsUpdate_Click(object sender, EventArgs e)
        {
            // GoodsLoader is live-bound; the DataTable IS the source of truth.
            // Nothing needs to be explicitly "pushed" — just inform the user.
            if (string.IsNullOrEmpty(goodsCurrentSection))
            {
                SetStatus("No section loaded.");
                MessageBox.Show("No section loaded. Please open a file first.", "Update Table");
                return;
            }
            SetStatus("Section \"" + goodsCurrentSection + "\" is live — changes are automatically reflected on export.");
            MessageBox.Show(
                "Section data is live-bound.\nChanges you make in the grid are automatically reflected when you export.",
                "Update Table");
        }

        private void BtnGoodsImport_Click(object sender, EventArgs e)
        {
            GoodsImportCsv();
        }

        private void BtnGoodsLocHelper_Click(object sender, EventArgs e)
        {
            new CSVHelper().Show();
        }

        // ── listBox2 double-click → rename table ─────────────────────────────────────

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            RenameCurrentTable();
        }

        // Ctrl+M on Goods: import a CSV file and overwrite the current section's DataTable.
        private void GoodsImportCsv()
        {
            if (string.IsNullOrEmpty(goodsCurrentSection))
            { MessageBox.Show("No section loaded."); return; }

            OpenFileDialog ofd = new OpenFileDialog { Filter = "CSV files (*.csv)|*.csv|All files|*.*" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                string[] lines = File.ReadAllLines(ofd.FileName, Encoding.UTF8);
                if (lines.Length < 2) { MessageBox.Show("CSV has no data rows."); return; }

                // Parse header
                string[] headers = ParseCsvLine(lines[0]);

                DataTable dt = goodsBS.DataSource as DataTable;
                if (dt == null) { MessageBox.Show("No table bound."); return; }

                // Verify columns match
                foreach (string h in headers)
                {
                    if (!dt.Columns.Contains(h))
                    { MessageBox.Show("Column \"" + h + "\" not found in current section.\nImport aborted."); return; }
                }

                PushUndo(dataGridViewGoods);
                dt.Clear();
                for (int i = 1; i < lines.Length; i++)
                {
                    if (string.IsNullOrEmpty(lines[i])) continue;
                    string[] fields = ParseCsvLine(lines[i]);
                    DataRow row = dt.NewRow();
                    for (int c = 0; c < headers.Length && c < fields.Length; c++)
                        row[headers[c]] = StripCdata(fields[c]);
                    dt.Rows.Add(row);
                }
                SetStatus("Imported " + dt.Rows.Count + " row(s) into section \"" + goodsCurrentSection + "\".");
                MessageBox.Show("Imported " + dt.Rows.Count + " row(s) into section \"" + goodsCurrentSection + "\".");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Import failed: " + ex.Message);
            }
        }

        private void GoodsExportXmlFiltered_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(goodsCurrentSection) || goodsBS.DataSource == null)
            { MessageBox.Show("No section loaded."); return; }

            SaveFileDialog sfd = new SaveFileDialog { Filter = "XML file|*.xml|All files|*.*" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            byte[] data = FilteredViewToXmlBytes(goodsBS, "SECTION", "ROW");
            using (BinaryWriter bw = new BinaryWriter(File.Create(sfd.FileName)))
                bw.Write(data);
            MessageBox.Show("Export completed.");
        }

        // ════════════════════════════════════════════════════════════════════════════
        // Background worker
        // ════════════════════════════════════════════════════════════════════════════

        private BackgroundWorker bw = null;

        private void BeginOperation(DoWorkEventHandler taskFunc, RunWorkerCompletedEventHandler completeFunc)
        {
            bw = new BackgroundWorker { WorkerReportsProgress = true };
            bw.DoWork             += taskFunc;
            bw.RunWorkerCompleted += completeFunc;
            bw.RunWorkerCompleted += ThreadedOperationDone;
            bw.ProgressChanged    += ThreadedOperationProgress;
            dl.ProgressCallback    = bw.ReportProgress;
            ThreadedOperationStart();
        }

        private void ThreadedOperationStart()
        {
            toolStripProgressBar1.Visible = true;
            bw.RunWorkerAsync();
        }

        private void ThreadedOperationProgress(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        private void ThreadedOperationDone(object sender, EventArgs e)
        {
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Text    = dl.GetStatus();
        }

        // ════════════════════════════════════════════════════════════════════════════
        // "Don't show again" confirmation dialog
        // ════════════════════════════════════════════════════════════════════════════

        private sealed class DontShowAgainDialog : Form
        {
            public bool DontShowAgain { get; private set; }

            public DontShowAgainDialog(string message, string title)
            {
                Text              = title;
                Width             = 390;
                Height            = 160;
                FormBorderStyle   = FormBorderStyle.FixedDialog;
                StartPosition     = FormStartPosition.CenterParent;
                MaximizeBox       = false;
                MinimizeBox       = false;
                ShowInTaskbar     = false;

                var lbl = new Label
                {
                    Text      = message,
                    Location  = new Point(12, 14),
                    Size      = new Size(360, 40),
                    AutoSize  = false
                };
                var chk = new CheckBox
                {
                    Text     = "Don't ask me again",
                    Location = new Point(12, 60),
                    AutoSize = true
                };
                var btnOk = new Button
                {
                    Text         = "OK",
                    DialogResult = DialogResult.OK,
                    Location     = new Point(208, 90),
                    Width        = 80
                };
                var btnCancel = new Button
                {
                    Text         = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Location     = new Point(296, 90),
                    Width        = 80
                };

                btnOk.Click += (s, ev) => { DontShowAgain = chk.Checked; };

                Controls.AddRange(new Control[] { lbl, chk, btnOk, btnCancel });
                AcceptButton = btnOk;
                CancelButton = btnCancel;
            }
        }

        // ════════════════════════════════════════════════════════════════════════════
        // Add Column dialog (LibConfig – asks for name, type, and isKey)
        // ════════════════════════════════════════════════════════════════════════════

        private sealed class AddColumnDialog : Form
        {
            public string ColumnName { get; private set; }
            public string ColumnType { get; private set; }
            public bool   IsKey      { get; private set; }

            public AddColumnDialog()
            {
                Text            = "Add Column";
                Width           = 320;
                Height          = 195;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition   = FormStartPosition.CenterParent;
                MaximizeBox     = false;
                MinimizeBox     = false;
                ShowInTaskbar   = false;

                var lblName = new Label  { Text = "Column name:", Location = new Point(12, 14),  AutoSize = true };
                var txtName = new TextBox { Location = new Point(120, 11), Width = 178 };

                var lblType = new Label  { Text = "Data type:",   Location = new Point(12, 44),  AutoSize = true };
                var cmbType = new ComboBox
                {
                    Location      = new Point(120, 41),
                    Width         = 178,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cmbType.Items.AddRange(new object[] { "string", "int", "float" });
                cmbType.SelectedIndex = 0;

                var chkKey = new CheckBox { Text = "Is key column", Location = new Point(12, 76), AutoSize = true };

                var btnOk  = new Button { Text = "OK",     DialogResult = DialogResult.OK,     Location = new Point(128, 114), Width = 80 };
                var btnCnl = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(216, 114), Width = 80 };

                btnOk.Click += (s, ev) =>
                {
                    ColumnName = txtName.Text;
                    ColumnType = cmbType.SelectedItem != null ? cmbType.SelectedItem.ToString() : "string";
                    IsKey      = chkKey.Checked;
                };

                Controls.AddRange(new Control[] { lblName, txtName, lblType, cmbType, chkKey, btnOk, btnCnl });
                AcceptButton = btnOk;
                CancelButton = btnCnl;
            }
        }
    }
}
