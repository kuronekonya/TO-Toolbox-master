namespace CaballaRE
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ── Shared / existing controls ──────────────────────────────────────────
            this.button1            = new System.Windows.Forms.Button();
            this.button2            = new System.Windows.Forms.Button();
            this.listBox1           = new System.Windows.Forms.ListBox();
            this.pictureBox1        = new System.Windows.Forms.PictureBox();
            this.button3            = new System.Windows.Forms.Button();
            this.tabControl1        = new System.Windows.Forms.TabControl();

            // NRI Viewer tab
            this.tabPage1           = new System.Windows.Forms.TabPage();
            this.tabControl2        = new System.Windows.Forms.TabControl();
            this.tabPage4           = new System.Windows.Forms.TabPage();
            this.tabPage5           = new System.Windows.Forms.TabPage();
            this.label2             = new System.Windows.Forms.Label();
            this.trackBar1          = new System.Windows.Forms.TrackBar();
            this.pictureBox2        = new System.Windows.Forms.PictureBox();
            this.listBox3           = new System.Windows.Forms.ListBox();
            this.button13           = new System.Windows.Forms.Button();
            this.button11           = new System.Windows.Forms.Button();

            // DAT Viewer tab
            this.tabPage2           = new System.Windows.Forms.TabPage();
            this.button6            = new System.Windows.Forms.Button();
            this.button5            = new System.Windows.Forms.Button();
            this.button4            = new System.Windows.Forms.Button();
            this.textBox1           = new System.Windows.Forms.TextBox();

            // LibConfig Editor tab
            this.tabPage3           = new System.Windows.Forms.TabPage();
            this.label1             = new System.Windows.Forms.Label();
            this.button10           = new System.Windows.Forms.Button();
            this.button8            = new System.Windows.Forms.Button();
            this.button12           = new System.Windows.Forms.Button();
            this.textBox2           = new System.Windows.Forms.TextBox();
            this.button9            = new System.Windows.Forms.Button();
            this.listBox2           = new System.Windows.Forms.ListBox();
            this.dataGridView1      = new System.Windows.Forms.DataGridView();
            this.button7            = new System.Windows.Forms.Button();
            this.contextMenuStrip1  = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xLSExcelToolStripMenuItem       = new System.Windows.Forms.ToolStripMenuItem();
            this.xLSFilteredToolStripMenuItem    = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem            = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLFilteredToolStripMenuItem    = new System.Windows.Forms.ToolStripMenuItem();
            this.dATUnencryptedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dATToolStripMenuItem            = new System.Windows.Forms.ToolStripMenuItem();
            this.iDXToolStripMenuItem            = new System.Windows.Forms.ToolStripMenuItem();
            // LibConfig Cell Search / Replace
            this.lblLibCellSearch   = new System.Windows.Forms.Label();
            this.cmbLibCellCol      = new System.Windows.Forms.ComboBox();
            this.txtLibCellSearch   = new System.Windows.Forms.TextBox();
            this.lblLibCol          = new System.Windows.Forms.Label();
            this.lblLibReplace      = new System.Windows.Forms.Label();
            this.txtLibReplace      = new System.Windows.Forms.TextBox();

            // Goods Editor tab
            this.tabPage6                   = new System.Windows.Forms.TabPage();
            this.btnLoadGoods               = new System.Windows.Forms.Button();
            this.btnGoodsUpdate             = new System.Windows.Forms.Button();
            this.btnGoodsImport             = new System.Windows.Forms.Button();
            this.btnGoodsLocHelper          = new System.Windows.Forms.Button();
            this.btnGoodsExport             = new System.Windows.Forms.Button();
            this.lblGoodsStatus             = new System.Windows.Forms.Label();
            this.pnlGoodsSections           = new System.Windows.Forms.Panel();
            this.lblGoodsSections           = new System.Windows.Forms.Label();
            this.lstGoodsSections           = new System.Windows.Forms.ListBox();
            this.dataGridViewGoods          = new System.Windows.Forms.DataGridView();
            this.contextMenuStripGoods              = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.goodsExportCsvMenuItem             = new System.Windows.Forms.ToolStripMenuItem();
            this.goodsExportCsvFilteredMenuItem     = new System.Windows.Forms.ToolStripMenuItem();
            this.goodsExportXmlMenuItem             = new System.Windows.Forms.ToolStripMenuItem();
            this.goodsExportXmlFilteredMenuItem     = new System.Windows.Forms.ToolStripMenuItem();
            this.goodsExportDatMenuItem             = new System.Windows.Forms.ToolStripMenuItem();
            // Goods Find / Replace
            this.lblGoodsCellSearch         = new System.Windows.Forms.Label();
            this.cmbGoodsCellCol            = new System.Windows.Forms.ComboBox();
            this.txtGoodsCellSearch         = new System.Windows.Forms.TextBox();
            this.lblGoodsCol                = new System.Windows.Forms.Label();
            this.lblGoodsReplace            = new System.Windows.Forms.Label();
            this.txtGoodsReplace            = new System.Windows.Forms.TextBox();

            // Shared grid row-edit context menu
            this.contextMenuStripGridEdit   = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gridCutMenuItem            = new System.Windows.Forms.ToolStripMenuItem();
            this.gridCopyMenuItem           = new System.Windows.Forms.ToolStripMenuItem();
            this.gridPasteMenuItem          = new System.Windows.Forms.ToolStripMenuItem();
            this.gridInsertAboveMenuItem    = new System.Windows.Forms.ToolStripMenuItem();
            this.gridInsertBelowMenuItem    = new System.Windows.Forms.ToolStripMenuItem();
            this.gridAddColLeftMenuItem     = new System.Windows.Forms.ToolStripMenuItem();
            this.gridAddColRightMenuItem    = new System.Windows.Forms.ToolStripMenuItem();
            this.gridFillDownMenuItem       = new System.Windows.Forms.ToolStripMenuItem();
            this.gridFillUpMenuItem         = new System.Windows.Forms.ToolStripMenuItem();
            this.gridFillRightMenuItem      = new System.Windows.Forms.ToolStripMenuItem();
            this.gridFillLeftMenuItem       = new System.Windows.Forms.ToolStripMenuItem();
            this.gridDeleteRowMenuItem      = new System.Windows.Forms.ToolStripMenuItem();
            this.gridUndoMenuItem           = new System.Windows.Forms.ToolStripMenuItem();
            this.gridRedoMenuItem           = new System.Windows.Forms.ToolStripMenuItem();

            // Help button (floats over tab strip, always visible)
            this.btnHelp                    = new System.Windows.Forms.Button();

            // Status strip
            this.statusStrip1               = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1      = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1      = new System.Windows.Forms.ToolStripProgressBar();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGoods)).BeginInit();
            this.pnlGoodsSections.SuspendLayout();
            this.contextMenuStripGoods.SuspendLayout();
            this.contextMenuStripGridEdit.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();

            // ── button1 – Open NRI ──────────────────────────────────────────────────
            this.button1.Location = new System.Drawing.Point(6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open NRI";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // ── button2 – Extract Image (Top|Right so it tracks the window edge) ────
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(442, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 28);
            this.button2.TabIndex = 1;
            this.button2.Text = "Extract Image";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);

            // ── listBox1 ────────────────────────────────────────────────────────────
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 264);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);

            // ── pictureBox1 ─────────────────────────────────────────────────────────
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(132, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(482, 264);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;

            // ── button3 – Open DAT ──────────────────────────────────────────────────
            this.button3.Location = new System.Drawing.Point(6, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 28);
            this.button3.TabIndex = 4;
            this.button3.Text = "Open DAT";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);

            // ── tabControl1 ─────────────────────────────────────────────────────────
            // Size matches design ClientSize(654,411): right gap=12, bottom gap=23 (statusStrip)
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(630, 376);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);

            // ── tabPage1 – NRI Viewer ───────────────────────────────────────────────
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.Controls.Add(this.button13);
            this.tabPage1.Controls.Add(this.button11);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(622, 350);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "NRI Viewer";
            this.tabPage1.UseVisualStyleBackColor = true;

            // ── tabControl2 (Images / Animations) ──────────────────────────────────
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Location = new System.Drawing.Point(6, 40);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(610, 304);
            this.tabControl2.TabIndex = 6;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.TabControl2_SelectedIndexChanged);

            // ── tabPage4 – Images ───────────────────────────────────────────────────
            this.tabPage4.Controls.Add(this.listBox1);
            this.tabPage4.Controls.Add(this.pictureBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(602, 278);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Images";
            this.tabPage4.UseVisualStyleBackColor = true;

            // ── tabPage5 – Animations ───────────────────────────────────────────────
            this.tabPage5.Controls.Add(this.label2);
            this.tabPage5.Controls.Add(this.trackBar1);
            this.tabPage5.Controls.Add(this.pictureBox2);
            this.tabPage5.Controls.Add(this.listBox3);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(602, 278);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Animations";
            this.tabPage5.UseVisualStyleBackColor = true;

            // ── label2 ──────────────────────────────────────────────────────────────
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(129, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "No animation selected";

            // ── trackBar1 ───────────────────────────────────────────────────────────
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(129, 222);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(467, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);

            // ── pictureBox2 ─────────────────────────────────────────────────────────
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Location = new System.Drawing.Point(129, 19);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(467, 197);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;

            // ── listBox3 ────────────────────────────────────────────────────────────
            this.listBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(3, 3);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(120, 264);
            this.listBox3.TabIndex = 0;
            this.listBox3.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);

            // ── button13 – Extract All (Top|Right so it tracks window edge) ─────────
            this.button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button13.Location = new System.Drawing.Point(532, 6);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(84, 28);
            this.button13.TabIndex = 5;
            this.button13.Text = "Extract All";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);

            // ── button11 – Decompress File ──────────────────────────────────────────
            this.button11.Location = new System.Drawing.Point(96, 6);
            this.button11.Size = new System.Drawing.Size(100, 28);
            this.button11.Name = "button11";
            this.button11.TabIndex = 4;
            this.button11.Text = "Decompress File";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);

            // ── tabPage2 – DAT Viewer ───────────────────────────────────────────────
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(622, 350);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "DAT Viewer";
            this.tabPage2.UseVisualStyleBackColor = true;

            // ── button6 – Export XML (DAT tab) ──────────────────────────────────────
            this.button6.Location = new System.Drawing.Point(186, 6);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(105, 28);
            this.button6.TabIndex = 8;
            this.button6.Text = "Export XML";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);

            // ── button5 – Display (Top|Right so it tracks the right edge) ───────────
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(532, 6);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(84, 28);
            this.button5.TabIndex = 7;
            this.button5.Text = "Display";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);

            // ── button4 – Export (DAT tab) ──────────────────────────────────────────
            this.button4.Location = new System.Drawing.Point(96, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(84, 28);
            this.button4.TabIndex = 6;
            this.button4.Text = "Export";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);

            // ── textBox1 ────────────────────────────────────────────────────────────
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(3, 40);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(616, 304);
            this.textBox1.TabIndex = 5;

            // ════════════════════════════════════════════════════════════════════════
            // tabPage3 – LibConfig Editor
            // Row 1 (y=6):  [Load LibConfig.xml] [Update Table] [Import Table] [Localization Helper]   [Export ▼]
            // Row 2 (y=34): [Table filter────────]  [Find: box  in: combo  Replace: box]
            // Row 2b(y=57): [Table: current name──]
            // Row 3 (y=73): [listBox2────────────] [dataGridView1──────────────────────────────────────]
            // ════════════════════════════════════════════════════════════════════════

            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.button10);
            this.tabPage3.Controls.Add(this.button8);
            this.tabPage3.Controls.Add(this.button12);
            this.tabPage3.Controls.Add(this.textBox2);
            this.tabPage3.Controls.Add(this.button9);
            this.tabPage3.Controls.Add(this.listBox2);
            this.tabPage3.Controls.Add(this.dataGridView1);
            this.tabPage3.Controls.Add(this.button7);
            this.tabPage3.Controls.Add(this.lblLibCellSearch);
            this.tabPage3.Controls.Add(this.txtLibCellSearch);
            this.tabPage3.Controls.Add(this.lblLibCol);
            this.tabPage3.Controls.Add(this.cmbLibCellCol);
            this.tabPage3.Controls.Add(this.lblLibReplace);
            this.tabPage3.Controls.Add(this.txtLibReplace);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(622, 350);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "LibConfig Editor";
            this.tabPage3.UseVisualStyleBackColor = true;

            // ── label1 – Table status (below search box) ────────────────────────────
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Table: (none)";

            // ── button10 – Localization Helper ──────────────────────────────────────
            this.button10.Location = new System.Drawing.Point(316, 6);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(114, 23);
            this.button10.TabIndex = 10;
            this.button10.Text = "Localization Helper";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);

            // ── button8 – Import Table ──────────────────────────────────────────────
            this.button8.Location = new System.Drawing.Point(221, 6);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(89, 23);
            this.button8.TabIndex = 9;
            this.button8.Text = "Import Table";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);

            // ── button12 – Update Table ─────────────────────────────────────────────
            this.button12.Location = new System.Drawing.Point(126, 6);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(89, 23);
            this.button12.TabIndex = 8;
            this.button12.Text = "Update Table";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);

            // ── textBox2 – Table name filter ────────────────────────────────────────
            this.textBox2.Location = new System.Drawing.Point(6, 35);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(176, 20);
            this.textBox2.TabIndex = 7;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);

            // ── button9 – Export ▼ (LibConfig, Top|Right) ───────────────────────────
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.Location = new System.Drawing.Point(535, 6);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(81, 23);
            this.button9.TabIndex = 4;
            this.button9.Text = "Export";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Paint += new System.Windows.Forms.PaintEventHandler(this.button9_Paint);
            this.button9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button9_MouseDown);

            // ── LibConfig Find / Replace row (fits within 622px tabPage width) ──────
            // "Find:" label — fixed width to prevent AutoSize overlap with Find textbox
            this.lblLibCellSearch.AutoSize = false;
            this.lblLibCellSearch.Size = new System.Drawing.Size(33, 17);
            this.lblLibCellSearch.Location = new System.Drawing.Point(264, 37);
            this.lblLibCellSearch.Name = "lblLibCellSearch";
            this.lblLibCellSearch.TabIndex = 12;
            this.lblLibCellSearch.Text = "Find:";

            // Find textbox (3px gap from label right edge)
            this.txtLibCellSearch.Location = new System.Drawing.Point(300, 34);
            this.txtLibCellSearch.Name = "txtLibCellSearch";
            this.txtLibCellSearch.Size = new System.Drawing.Size(68, 20);
            this.txtLibCellSearch.TabIndex = 13;
            this.txtLibCellSearch.TextChanged += new System.EventHandler(this.TxtLibCellSearch_TextChanged);

            // "in:" label — fixed width to prevent AutoSize overlap with combo
            this.lblLibCol.AutoSize = false;
            this.lblLibCol.Size = new System.Drawing.Size(22, 17);
            this.lblLibCol.Location = new System.Drawing.Point(372, 37);
            this.lblLibCol.Name = "lblLibCol";
            this.lblLibCol.TabIndex = 14;
            this.lblLibCol.Text = "in:";

            // Column picker ComboBox (3px gap from label right edge)
            this.cmbLibCellCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLibCellCol.Location = new System.Drawing.Point(397, 34);
            this.cmbLibCellCol.Name = "cmbLibCellCol";
            this.cmbLibCellCol.Size = new System.Drawing.Size(96, 21);
            this.cmbLibCellCol.TabIndex = 15;
            this.cmbLibCellCol.SelectedIndexChanged += new System.EventHandler(this.CmbLibCellCol_SelectedIndexChanged);

            // "Replace:" label — fixed width
            this.lblLibReplace.AutoSize = false;
            this.lblLibReplace.Size = new System.Drawing.Size(56, 17);
            this.lblLibReplace.Location = new System.Drawing.Point(496, 37);
            this.lblLibReplace.Name = "lblLibReplace";
            this.lblLibReplace.TabIndex = 16;
            this.lblLibReplace.Text = "Replace:";

            // Replace textbox (3px gap from label right edge; right end ≈ 619px < 622px)
            this.txtLibReplace.Location = new System.Drawing.Point(555, 34);
            this.txtLibReplace.Name = "txtLibReplace";
            this.txtLibReplace.Size = new System.Drawing.Size(62, 20);
            this.txtLibReplace.TabIndex = 17;
            this.txtLibReplace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtLibReplace_KeyDown);

            // ── listBox2 ────────────────────────────────────────────────────────────
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(6, 73);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(176, 272);
            this.listBox2.TabIndex = 2;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            this.listBox2.DoubleClick += new System.EventHandler(this.listBox2_DoubleClick);

            // ── dataGridView1 ────────────────────────────────────────────────────────
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(188, 73);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(428, 272);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStripGridEdit;
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridView_MouseDown);

            // ── button7 – Load LibConfig.xml ────────────────────────────────────────
            this.button7.Location = new System.Drawing.Point(6, 6);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(114, 23);
            this.button7.TabIndex = 0;
            this.button7.Text = "Load LibConfig.xml";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);

            // ── contextMenuStrip1 (LibConfig Export menu) ────────────────────────────
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.xLSExcelToolStripMenuItem,
                this.xLSFilteredToolStripMenuItem,
                this.xMLToolStripMenuItem,
                this.xMLFilteredToolStripMenuItem,
                this.dATUnencryptedToolStripMenuItem,
                this.dATToolStripMenuItem,
                this.iDXToolStripMenuItem
            });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(231, 114);

            this.xLSExcelToolStripMenuItem.Name = "xLSExcelToolStripMenuItem";
            this.xLSExcelToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.xLSExcelToolStripMenuItem.Text = "&c  CSV (current table)";
            this.xLSExcelToolStripMenuItem.Click += new System.EventHandler(this.xLSExcelToolStripMenuItem_Click);

            this.xLSFilteredToolStripMenuItem.Name = "xLSFilteredToolStripMenuItem";
            this.xLSFilteredToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.xLSFilteredToolStripMenuItem.Text = "&v  CSV (filtered table)";
            this.xLSFilteredToolStripMenuItem.Click += new System.EventHandler(this.xLSFilteredToolStripMenuItem_Click);

            this.xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            this.xMLToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.xMLToolStripMenuItem.Text = "&x  XML";
            this.xMLToolStripMenuItem.Click += new System.EventHandler(this.xMLToolStripMenuItem_Click);

            this.xMLFilteredToolStripMenuItem.Name = "xMLFilteredToolStripMenuItem";
            this.xMLFilteredToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.xMLFilteredToolStripMenuItem.Text = "&m  XML (filtered table)";
            this.xMLFilteredToolStripMenuItem.Click += new System.EventHandler(this.xMLFilteredToolStripMenuItem_Click);

            this.dATUnencryptedToolStripMenuItem.Name = "dATUnencryptedToolStripMenuItem";
            this.dATUnencryptedToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.dATUnencryptedToolStripMenuItem.Text = "&d  DAT (unencrypted)";
            this.dATUnencryptedToolStripMenuItem.Click += new System.EventHandler(this.dATUnencryptedToolStripMenuItem_Click);

            this.dATToolStripMenuItem.Name = "dATToolStripMenuItem";
            this.dATToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.dATToolStripMenuItem.Text = "&e  DAT (encrypted)";
            this.dATToolStripMenuItem.Click += new System.EventHandler(this.dATToolStripMenuItem_Click);

            this.iDXToolStripMenuItem.Name = "iDXToolStripMenuItem";
            this.iDXToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.iDXToolStripMenuItem.Text = "&i  IDX";
            this.iDXToolStripMenuItem.Click += new System.EventHandler(this.iDXToolStripMenuItem_Click);

            // ════════════════════════════════════════════════════════════════════════
            // tabPage6 – Libcmgds & rch Editor
            //
            // Row 1 (y=6):  [Load File] [Update] [Import] [Localization Helper] [Export ▼]
            // Row 2 (y=34): [status  Find: box  in: combo  Replace: box]
            // Row 3 (y=60): [pnlSections│dataGridViewGoods]
            // ════════════════════════════════════════════════════════════════════════

            this.tabPage6.Controls.Add(this.btnLoadGoods);
            this.tabPage6.Controls.Add(this.btnGoodsUpdate);
            this.tabPage6.Controls.Add(this.btnGoodsImport);
            this.tabPage6.Controls.Add(this.btnGoodsLocHelper);
            this.tabPage6.Controls.Add(this.btnGoodsExport);
            this.tabPage6.Controls.Add(this.lblGoodsStatus);
            this.tabPage6.Controls.Add(this.lblGoodsCellSearch);
            this.tabPage6.Controls.Add(this.txtGoodsCellSearch);
            this.tabPage6.Controls.Add(this.lblGoodsCol);
            this.tabPage6.Controls.Add(this.cmbGoodsCellCol);
            this.tabPage6.Controls.Add(this.lblGoodsReplace);
            this.tabPage6.Controls.Add(this.txtGoodsReplace);
            this.tabPage6.Controls.Add(this.pnlGoodsSections);
            this.tabPage6.Controls.Add(this.dataGridViewGoods);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(622, 350);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Libcmgds & rch Editor";
            this.tabPage6.UseVisualStyleBackColor = true;

            // ── btnLoadGoods ─────────────────────────────────────────────────────────
            this.btnLoadGoods.Location = new System.Drawing.Point(6, 6);
            this.btnLoadGoods.Name = "btnLoadGoods";
            this.btnLoadGoods.Size = new System.Drawing.Size(80, 23);
            this.btnLoadGoods.TabIndex = 0;
            this.btnLoadGoods.Text = "Load File";
            this.btnLoadGoods.UseVisualStyleBackColor = true;
            this.btnLoadGoods.Click += new System.EventHandler(this.BtnLoadGoods_Click);

            // ── btnGoodsUpdate ────────────────────────────────────────────────────────
            this.btnGoodsUpdate.Location = new System.Drawing.Point(92, 6);
            this.btnGoodsUpdate.Name = "btnGoodsUpdate";
            this.btnGoodsUpdate.Size = new System.Drawing.Size(89, 23);
            this.btnGoodsUpdate.TabIndex = 1;
            this.btnGoodsUpdate.Text = "Update Table";
            this.btnGoodsUpdate.UseVisualStyleBackColor = true;
            this.btnGoodsUpdate.Click += new System.EventHandler(this.BtnGoodsUpdate_Click);

            // ── btnGoodsImport ────────────────────────────────────────────────────────
            this.btnGoodsImport.Location = new System.Drawing.Point(187, 6);
            this.btnGoodsImport.Name = "btnGoodsImport";
            this.btnGoodsImport.Size = new System.Drawing.Size(89, 23);
            this.btnGoodsImport.TabIndex = 2;
            this.btnGoodsImport.Text = "Import Table";
            this.btnGoodsImport.UseVisualStyleBackColor = true;
            this.btnGoodsImport.Click += new System.EventHandler(this.BtnGoodsImport_Click);

            // ── btnGoodsLocHelper ─────────────────────────────────────────────────────
            this.btnGoodsLocHelper.Location = new System.Drawing.Point(282, 6);
            this.btnGoodsLocHelper.Name = "btnGoodsLocHelper";
            this.btnGoodsLocHelper.Size = new System.Drawing.Size(114, 23);
            this.btnGoodsLocHelper.TabIndex = 3;
            this.btnGoodsLocHelper.Text = "Localization Helper";
            this.btnGoodsLocHelper.UseVisualStyleBackColor = true;
            this.btnGoodsLocHelper.Click += new System.EventHandler(this.BtnGoodsLocHelper_Click);

            // ── btnGoodsExport – Export ▼ (Top|Right, mirrors LibConfig) ─────────────
            this.btnGoodsExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoodsExport.Location = new System.Drawing.Point(535, 6);
            this.btnGoodsExport.Name = "btnGoodsExport";
            this.btnGoodsExport.Size = new System.Drawing.Size(81, 23);
            this.btnGoodsExport.TabIndex = 5;
            this.btnGoodsExport.Text = "Export";
            this.btnGoodsExport.UseVisualStyleBackColor = true;
            this.btnGoodsExport.Paint += new System.Windows.Forms.PaintEventHandler(this.BtnGoodsExport_Paint);
            this.btnGoodsExport.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BtnGoodsExport_MouseDown);

            // ── Goods Find / Replace row  (status label sits at left of this row) ──────
            // Status label – left anchor on find row
            this.lblGoodsStatus.AutoSize = true;
            this.lblGoodsStatus.Location = new System.Drawing.Point(6, 37);
            this.lblGoodsStatus.Name = "lblGoodsStatus";
            this.lblGoodsStatus.TabIndex = 4;
            this.lblGoodsStatus.Text = "No file loaded.";

            // "Find:" label — shifted right to leave room for status label (~166px)
            this.lblGoodsCellSearch.AutoSize = false;
            this.lblGoodsCellSearch.Size = new System.Drawing.Size(33, 17);
            this.lblGoodsCellSearch.Location = new System.Drawing.Point(172, 37);
            this.lblGoodsCellSearch.Name = "lblGoodsCellSearch";
            this.lblGoodsCellSearch.TabIndex = 6;
            this.lblGoodsCellSearch.Text = "Find:";

            this.txtGoodsCellSearch.Location = new System.Drawing.Point(208, 34);
            this.txtGoodsCellSearch.Name = "txtGoodsCellSearch";
            this.txtGoodsCellSearch.Size = new System.Drawing.Size(96, 20);
            this.txtGoodsCellSearch.TabIndex = 7;
            this.txtGoodsCellSearch.TextChanged += new System.EventHandler(this.TxtGoodsCellSearch_TextChanged);

            // "in:" label
            this.lblGoodsCol.AutoSize = false;
            this.lblGoodsCol.Size = new System.Drawing.Size(22, 17);
            this.lblGoodsCol.Location = new System.Drawing.Point(307, 37);
            this.lblGoodsCol.Name = "lblGoodsCol";
            this.lblGoodsCol.TabIndex = 8;
            this.lblGoodsCol.Text = "in:";

            this.cmbGoodsCellCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGoodsCellCol.Location = new System.Drawing.Point(332, 34);
            this.cmbGoodsCellCol.Name = "cmbGoodsCellCol";
            this.cmbGoodsCellCol.Size = new System.Drawing.Size(120, 21);
            this.cmbGoodsCellCol.TabIndex = 9;
            this.cmbGoodsCellCol.SelectedIndexChanged += new System.EventHandler(this.CmbGoodsCellCol_SelectedIndexChanged);

            // "Replace:" label
            this.lblGoodsReplace.AutoSize = false;
            this.lblGoodsReplace.Size = new System.Drawing.Size(56, 17);
            this.lblGoodsReplace.Location = new System.Drawing.Point(455, 37);
            this.lblGoodsReplace.Name = "lblGoodsReplace";
            this.lblGoodsReplace.TabIndex = 10;
            this.lblGoodsReplace.Text = "Replace:";

            this.txtGoodsReplace.Location = new System.Drawing.Point(514, 34);
            this.txtGoodsReplace.Name = "txtGoodsReplace";
            this.txtGoodsReplace.Size = new System.Drawing.Size(90, 20);
            this.txtGoodsReplace.TabIndex = 11;
            this.txtGoodsReplace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtGoodsReplace_KeyDown);

            // ── pnlGoodsSections (left panel; hidden for _rch) ───────────────────────
            this.pnlGoodsSections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlGoodsSections.Controls.Add(this.lblGoodsSections);
            this.pnlGoodsSections.Controls.Add(this.lstGoodsSections);
            this.pnlGoodsSections.Location = new System.Drawing.Point(6, 60);
            this.pnlGoodsSections.Name = "pnlGoodsSections";
            this.pnlGoodsSections.Size = new System.Drawing.Size(140, 284);
            this.pnlGoodsSections.TabIndex = 12;

            this.lblGoodsSections.AutoSize = true;
            this.lblGoodsSections.Location = new System.Drawing.Point(0, 0);
            this.lblGoodsSections.Name = "lblGoodsSections";
            this.lblGoodsSections.Size = new System.Drawing.Size(52, 13);
            this.lblGoodsSections.Text = "Section:";

            this.lstGoodsSections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lstGoodsSections.FormattingEnabled = true;
            this.lstGoodsSections.Location = new System.Drawing.Point(0, 16);
            this.lstGoodsSections.Name = "lstGoodsSections";
            this.lstGoodsSections.Size = new System.Drawing.Size(140, 268);
            this.lstGoodsSections.TabIndex = 0;
            this.lstGoodsSections.SelectedIndexChanged += new System.EventHandler(this.LstGoodsSections_SelectedIndexChanged);

            // ── dataGridViewGoods ─────────────────────────────────────────────────────
            this.dataGridViewGoods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewGoods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGoods.Location = new System.Drawing.Point(152, 60);
            this.dataGridViewGoods.Name = "dataGridViewGoods";
            this.dataGridViewGoods.Size = new System.Drawing.Size(464, 284);
            this.dataGridViewGoods.TabIndex = 13;
            this.dataGridViewGoods.ContextMenuStrip = this.contextMenuStripGridEdit;
            this.dataGridViewGoods.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridView_MouseDown);

            // ── contextMenuStripGoods ─────────────────────────────────────────────────
            this.contextMenuStripGoods.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.goodsExportCsvMenuItem,
                this.goodsExportCsvFilteredMenuItem,
                this.goodsExportXmlMenuItem,
                this.goodsExportXmlFilteredMenuItem,
                this.goodsExportDatMenuItem
            });
            this.contextMenuStripGoods.Name = "contextMenuStripGoods";
            this.contextMenuStripGoods.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStripGoods.Size = new System.Drawing.Size(230, 114);

            this.goodsExportCsvMenuItem.Name = "goodsExportCsvMenuItem";
            this.goodsExportCsvMenuItem.Size = new System.Drawing.Size(230, 22);
            this.goodsExportCsvMenuItem.Text = "&c  CSV (current section)";
            this.goodsExportCsvMenuItem.Click += new System.EventHandler(this.GoodsExportCsv_Click);

            this.goodsExportCsvFilteredMenuItem.Name = "goodsExportCsvFilteredMenuItem";
            this.goodsExportCsvFilteredMenuItem.Size = new System.Drawing.Size(230, 22);
            this.goodsExportCsvFilteredMenuItem.Text = "&v  CSV (filtered table)";
            this.goodsExportCsvFilteredMenuItem.Click += new System.EventHandler(this.GoodsExportCsvFiltered_Click);

            this.goodsExportXmlMenuItem.Name = "goodsExportXmlMenuItem";
            this.goodsExportXmlMenuItem.Size = new System.Drawing.Size(230, 22);
            this.goodsExportXmlMenuItem.Text = "&x  XML";
            this.goodsExportXmlMenuItem.Click += new System.EventHandler(this.GoodsExportXml_Click);

            this.goodsExportXmlFilteredMenuItem.Name = "goodsExportXmlFilteredMenuItem";
            this.goodsExportXmlFilteredMenuItem.Size = new System.Drawing.Size(230, 22);
            this.goodsExportXmlFilteredMenuItem.Text = "&m  XML (filtered table)";
            this.goodsExportXmlFilteredMenuItem.Click += new System.EventHandler(this.GoodsExportXmlFiltered_Click);

            this.goodsExportDatMenuItem.Name = "goodsExportDatMenuItem";
            this.goodsExportDatMenuItem.Size = new System.Drawing.Size(230, 22);
            this.goodsExportDatMenuItem.Text = "&e  DAT (encrypted)";
            this.goodsExportDatMenuItem.Click += new System.EventHandler(this.GoodsExportDat_Click);

            // ── contextMenuStripGridEdit (shared row-edit menu for both editor grids) ─
            this.contextMenuStripGridEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.gridCutMenuItem,
                this.gridCopyMenuItem,
                this.gridPasteMenuItem,
                new System.Windows.Forms.ToolStripSeparator(),
                this.gridInsertAboveMenuItem,
                this.gridInsertBelowMenuItem,
                this.gridAddColLeftMenuItem,
                this.gridAddColRightMenuItem,
                new System.Windows.Forms.ToolStripSeparator(),
                this.gridFillDownMenuItem,
                this.gridFillUpMenuItem,
                this.gridFillRightMenuItem,
                this.gridFillLeftMenuItem,
                new System.Windows.Forms.ToolStripSeparator(),
                this.gridDeleteRowMenuItem,
                new System.Windows.Forms.ToolStripSeparator(),
                this.gridUndoMenuItem,
                this.gridRedoMenuItem
            });
            this.contextMenuStripGridEdit.Name = "contextMenuStripGridEdit";
            this.contextMenuStripGridEdit.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStripGridEdit.Size = new System.Drawing.Size(230, 330);
            this.contextMenuStripGridEdit.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripGridEdit_Opening);

            this.gridCutMenuItem.Name = "gridCutMenuItem";
            this.gridCutMenuItem.Text = "Cut\tCtrl+X";
            this.gridCutMenuItem.Click += new System.EventHandler(this.GridCut_Click);

            this.gridCopyMenuItem.Name = "gridCopyMenuItem";
            this.gridCopyMenuItem.Text = "Copy\tCtrl+C";
            this.gridCopyMenuItem.Click += new System.EventHandler(this.GridCopy_Click);

            this.gridPasteMenuItem.Name = "gridPasteMenuItem";
            this.gridPasteMenuItem.Text = "Paste\tCtrl+V";
            this.gridPasteMenuItem.Click += new System.EventHandler(this.GridPaste_Click);

            this.gridInsertAboveMenuItem.Name = "gridInsertAboveMenuItem";
            this.gridInsertAboveMenuItem.Text = "Add Row Above";
            this.gridInsertAboveMenuItem.Click += new System.EventHandler(this.GridInsertAbove_Click);

            this.gridInsertBelowMenuItem.Name = "gridInsertBelowMenuItem";
            this.gridInsertBelowMenuItem.Text = "Add Row Below\t(Shift)";
            this.gridInsertBelowMenuItem.Click += new System.EventHandler(this.GridInsertBelow_Click);

            this.gridAddColLeftMenuItem.Name = "gridAddColLeftMenuItem";
            this.gridAddColLeftMenuItem.Text = "Add Column Left";
            this.gridAddColLeftMenuItem.Click += new System.EventHandler(this.GridAddColLeft_Click);

            this.gridAddColRightMenuItem.Name = "gridAddColRightMenuItem";
            this.gridAddColRightMenuItem.Text = "Add Column Right\t(Shift)";
            this.gridAddColRightMenuItem.Click += new System.EventHandler(this.GridAddColRight_Click);

            this.gridFillDownMenuItem.Name = "gridFillDownMenuItem";
            this.gridFillDownMenuItem.Text = "Fill Down\tCtrl+D";
            this.gridFillDownMenuItem.Click += new System.EventHandler(this.GridFillDown_Click);

            this.gridFillUpMenuItem.Name = "gridFillUpMenuItem";
            this.gridFillUpMenuItem.Text = "Fill Up\tCtrl+Shift+D";
            this.gridFillUpMenuItem.Click += new System.EventHandler(this.GridFillUp_Click);

            this.gridFillRightMenuItem.Name = "gridFillRightMenuItem";
            this.gridFillRightMenuItem.Text = "Fill Right\tCtrl+R";
            this.gridFillRightMenuItem.Click += new System.EventHandler(this.GridFillRight_Click);

            this.gridFillLeftMenuItem.Name = "gridFillLeftMenuItem";
            this.gridFillLeftMenuItem.Text = "Fill Left\tCtrl+Shift+R";
            this.gridFillLeftMenuItem.Click += new System.EventHandler(this.GridFillLeft_Click);

            this.gridDeleteRowMenuItem.Name = "gridDeleteRowMenuItem";
            this.gridDeleteRowMenuItem.Text = "Delete Selected Row(s)\tDel";
            this.gridDeleteRowMenuItem.Click += new System.EventHandler(this.GridDeleteRows_Click);

            this.gridUndoMenuItem.Name = "gridUndoMenuItem";
            this.gridUndoMenuItem.Text = "Undo\tCtrl+Z";
            this.gridUndoMenuItem.Click += new System.EventHandler(this.GridUndo_Click);

            this.gridRedoMenuItem.Name = "gridRedoMenuItem";
            this.gridRedoMenuItem.Text = "Redo\tCtrl+Y";
            this.gridRedoMenuItem.Click += new System.EventHandler(this.GridRedo_Click);

            // ── statusStrip1 ─────────────────────────────────────────────────────────
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.toolStripStatusLabel1,
                this.toolStripProgressBar1
            });
            this.statusStrip1.Location = new System.Drawing.Point(0, 415);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";

            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(769, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Visible = false;

            // ── btnHelp – "?" shortcut reference (floats over tab strip top-right) ───
            // Anchor=Top|Right so it stays 6px from the form right edge at all sizes.
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(612, 14);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(26, 21);
            this.btnHelp.TabIndex = 10;
            this.btnHelp.Text = "?";
            this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.BtnHelp_Click);

            // ── MainWindow ────────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 411);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnHelp);   // added after tabControl so it has higher z-order
            this.Name = "MainWindow";
            this.Text = "TO Toolbox";
            this.KeyPreview = true;

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGoods)).EndInit();
            this.pnlGoodsSections.ResumeLayout(false);
            this.pnlGoodsSections.PerformLayout();
            this.contextMenuStripGoods.ResumeLayout(false);
            this.contextMenuStripGridEdit.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // ── Field declarations ────────────────────────────────────────────────────────
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem xLSExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xLSFilteredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLFilteredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dATUnencryptedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dATToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iDXToolStripMenuItem;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        // LibConfig Find / Replace
        private System.Windows.Forms.Label lblLibCellSearch;
        private System.Windows.Forms.TextBox txtLibCellSearch;
        private System.Windows.Forms.Label lblLibCol;
        private System.Windows.Forms.ComboBox cmbLibCellCol;
        private System.Windows.Forms.Label lblLibReplace;
        private System.Windows.Forms.TextBox txtLibReplace;
        // Goods Editor tab
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button btnLoadGoods;
        private System.Windows.Forms.Button btnGoodsUpdate;
        private System.Windows.Forms.Button btnGoodsImport;
        private System.Windows.Forms.Button btnGoodsLocHelper;
        private System.Windows.Forms.Button btnGoodsExport;
        private System.Windows.Forms.Label lblGoodsStatus;
        private System.Windows.Forms.Panel pnlGoodsSections;
        private System.Windows.Forms.Label lblGoodsSections;
        private System.Windows.Forms.ListBox lstGoodsSections;
        private System.Windows.Forms.DataGridView dataGridViewGoods;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripGoods;
        private System.Windows.Forms.ToolStripMenuItem goodsExportCsvMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goodsExportCsvFilteredMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goodsExportXmlMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goodsExportXmlFilteredMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goodsExportDatMenuItem;
        // Goods Find / Replace
        private System.Windows.Forms.Label lblGoodsCellSearch;
        private System.Windows.Forms.TextBox txtGoodsCellSearch;
        private System.Windows.Forms.Label lblGoodsCol;
        private System.Windows.Forms.ComboBox cmbGoodsCellCol;
        private System.Windows.Forms.Label lblGoodsReplace;
        private System.Windows.Forms.TextBox txtGoodsReplace;
        // Shared grid row-edit context menu
        private System.Windows.Forms.ContextMenuStrip contextMenuStripGridEdit;
        private System.Windows.Forms.ToolStripMenuItem gridCutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridCopyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridPasteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridInsertAboveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridInsertBelowMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridAddColLeftMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridAddColRightMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridFillDownMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridFillUpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridFillRightMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridFillLeftMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridDeleteRowMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridUndoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridRedoMenuItem;
        // Help button (always visible, floats over tab strip)
        private System.Windows.Forms.Button btnHelp;
    }
}
