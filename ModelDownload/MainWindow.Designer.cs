namespace ModelDownload
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.MainPalette = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelFooter = new System.Windows.Forms.TableLayoutPanel();
            this.lbCompanyName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelBuildDate = new System.Windows.Forms.Label();
            this.pictureBoxCompanyLogo = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucDateTime = new ModelDownload.MyUserControl.UcDateTime();
            this.label1 = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbScrews = new System.Windows.Forms.DataGridView();
            this.screwType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.screwName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanelMid = new System.Windows.Forms.TableLayoutPanel();
            this.tbTray = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbModelName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtModelNo = new System.Windows.Forms.NumericUpDown();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDownloadAll = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnDownload = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtDataWrite = new System.Windows.Forms.TextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cboComport = new System.Windows.Forms.ToolStripComboBox();
            this.btnConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCompanyLogo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbScrews)).BeginInit();
            this.tableLayoutPanelMid.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtModelNo)).BeginInit();
            this.panel3.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPalette
            // 
            this.MainPalette.FormStyles.FormMain.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.MainPalette.FormStyles.FormMain.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.MainPalette.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.Border.Rounding = 0;
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.Border.Width = 0;
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.ButtonEdgeInset = 10;
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.ButtonPadding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.Content.LongText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.Content.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.Content.Padding = new System.Windows.Forms.Padding(20, 10, -1, 0);
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.MainPalette.HeaderStyles.HeaderForm.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelFooter, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.panelMain, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(982, 653);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanelFooter
            // 
            this.tableLayoutPanelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tableLayoutPanelFooter.ColumnCount = 4;
            this.tableLayoutPanelFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
            this.tableLayoutPanelFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            this.tableLayoutPanelFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
            this.tableLayoutPanelFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanelFooter.Controls.Add(this.lbCompanyName, 2, 0);
            this.tableLayoutPanelFooter.Controls.Add(this.labelVersion, 0, 0);
            this.tableLayoutPanelFooter.Controls.Add(this.labelBuildDate, 0, 0);
            this.tableLayoutPanelFooter.Controls.Add(this.pictureBoxCompanyLogo, 3, 0);
            this.tableLayoutPanelFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelFooter.Location = new System.Drawing.Point(4, 615);
            this.tableLayoutPanelFooter.Name = "tableLayoutPanelFooter";
            this.tableLayoutPanelFooter.RowCount = 1;
            this.tableLayoutPanelFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelFooter.Size = new System.Drawing.Size(974, 34);
            this.tableLayoutPanelFooter.TabIndex = 6;
            // 
            // lbCompanyName
            // 
            this.lbCompanyName.AutoSize = true;
            this.lbCompanyName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lbCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCompanyName.ForeColor = System.Drawing.Color.White;
            this.lbCompanyName.Location = new System.Drawing.Point(577, 0);
            this.lbCompanyName.Name = "lbCompanyName";
            this.lbCompanyName.Size = new System.Drawing.Size(322, 34);
            this.lbCompanyName.TabIndex = 15;
            this.lbCompanyName.Text = "SORAMACHINE.,JSC";
            this.lbCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.ForeColor = System.Drawing.Color.White;
            this.labelVersion.Location = new System.Drawing.Point(331, 0);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(240, 34);
            this.labelVersion.TabIndex = 14;
            this.labelVersion.Text = "Version: V1.0";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelBuildDate
            // 
            this.labelBuildDate.AutoSize = true;
            this.labelBuildDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.labelBuildDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelBuildDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBuildDate.ForeColor = System.Drawing.Color.White;
            this.labelBuildDate.Location = new System.Drawing.Point(3, 0);
            this.labelBuildDate.Name = "labelBuildDate";
            this.labelBuildDate.Size = new System.Drawing.Size(322, 34);
            this.labelBuildDate.TabIndex = 13;
            this.labelBuildDate.Text = "Built: 2023/09/13";
            this.labelBuildDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBoxCompanyLogo
            // 
            this.pictureBoxCompanyLogo.BackgroundImage = global::ModelDownload.Properties.Resources.LogoSora;
            this.pictureBoxCompanyLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxCompanyLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxCompanyLogo.ErrorImage = global::ModelDownload.Properties.Resources.LogoSora;
            this.pictureBoxCompanyLogo.Location = new System.Drawing.Point(905, 3);
            this.pictureBoxCompanyLogo.Name = "pictureBoxCompanyLogo";
            this.pictureBoxCompanyLogo.Size = new System.Drawing.Size(66, 28);
            this.pictureBoxCompanyLogo.TabIndex = 16;
            this.pictureBoxCompanyLogo.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucDateTime);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pbLogo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(974, 69);
            this.panel1.TabIndex = 0;
            // 
            // ucDateTime
            // 
            this.ucDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucDateTime.BackColor = System.Drawing.Color.Transparent;
            this.ucDateTime.IsRun = false;
            this.ucDateTime.Location = new System.Drawing.Point(594, 8);
            this.ucDateTime.Margin = new System.Windows.Forms.Padding(4);
            this.ucDateTime.Name = "ucDateTime";
            this.ucDateTime.Size = new System.Drawing.Size(382, 56);
            this.ucDateTime.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(148, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 46);
            this.label1.TabIndex = 2;
            this.label1.Text = "Model Program";
            // 
            // pbLogo
            // 
            this.pbLogo.BackgroundImage = global::ModelDownload.Properties.Resources.LogoRorze;
            this.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbLogo.Location = new System.Drawing.Point(11, 4);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(140, 63);
            this.pbLogo.TabIndex = 1;
            this.pbLogo.TabStop = false;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tableLayoutPanel1);
            this.panelMain.Controls.Add(this.menuStrip);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(4, 80);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(974, 528);
            this.panelMain.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanelMid, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(974, 496);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbScrews);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 490);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Screws";
            // 
            // tbScrews
            // 
            this.tbScrews.AllowUserToAddRows = false;
            this.tbScrews.AllowUserToDeleteRows = false;
            this.tbScrews.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tbScrews.BackgroundColor = System.Drawing.SystemColors.Control;
            this.tbScrews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tbScrews.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.screwType,
            this.screwName});
            this.tbScrews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbScrews.Location = new System.Drawing.Point(3, 18);
            this.tbScrews.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbScrews.Name = "tbScrews";
            this.tbScrews.ReadOnly = true;
            this.tbScrews.RowHeadersVisible = false;
            this.tbScrews.RowHeadersWidth = 51;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.tbScrews.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.tbScrews.RowTemplate.Height = 24;
            this.tbScrews.Size = new System.Drawing.Size(238, 469);
            this.tbScrews.TabIndex = 42;
            // 
            // screwType
            // 
            this.screwType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.screwType.HeaderText = "Type";
            this.screwType.MinimumWidth = 6;
            this.screwType.Name = "screwType";
            this.screwType.ReadOnly = true;
            this.screwType.Width = 68;
            // 
            // screwName
            // 
            this.screwName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.screwName.HeaderText = "Name";
            this.screwName.MinimumWidth = 6;
            this.screwName.Name = "screwName";
            this.screwName.ReadOnly = true;
            // 
            // tableLayoutPanelMid
            // 
            this.tableLayoutPanelMid.ColumnCount = 1;
            this.tableLayoutPanelMid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMid.Controls.Add(this.tbTray, 0, 1);
            this.tableLayoutPanelMid.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanelMid.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanelMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMid.Location = new System.Drawing.Point(253, 3);
            this.tableLayoutPanelMid.Name = "tableLayoutPanelMid";
            this.tableLayoutPanelMid.RowCount = 3;
            this.tableLayoutPanelMid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanelMid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanelMid.Size = new System.Drawing.Size(718, 490);
            this.tableLayoutPanelMid.TabIndex = 1;
            // 
            // tbTray
            // 
            this.tbTray.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tbTray.ColumnCount = 14;
            this.tbTray.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142858F));
            this.tbTray.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142858F));
            this.tbTray.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142858F));
            this.tbTray.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142858F));
            this.tbTray.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142858F));
            this.tbTray.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142858F));
            this.tbTray.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142858F));
            this.tbTray.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142858F));
            this.tbTray.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142858F));
            this.tbTray.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142858F));
            this.tbTray.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142858F));
            this.tbTray.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142858F));
            this.tbTray.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142858F));
            this.tbTray.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142858F));
            this.tbTray.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTray.Location = new System.Drawing.Point(3, 53);
            this.tbTray.Name = "tbTray";
            this.tbTray.RowCount = 6;
            this.tbTray.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tbTray.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tbTray.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tbTray.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tbTray.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tbTray.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tbTray.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbTray.Size = new System.Drawing.Size(712, 284);
            this.tbTray.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbModelName);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtModelNo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(712, 44);
            this.panel2.TabIndex = 1;
            // 
            // lbModelName
            // 
            this.lbModelName.AutoSize = true;
            this.lbModelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbModelName.ForeColor = System.Drawing.Color.DarkOrange;
            this.lbModelName.Location = new System.Drawing.Point(209, 10);
            this.lbModelName.Name = "lbModelName";
            this.lbModelName.Size = new System.Drawing.Size(133, 25);
            this.lbModelName.TabIndex = 2;
            this.lbModelName.Text = "Model Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Model No.";
            // 
            // txtModelNo
            // 
            this.txtModelNo.Location = new System.Drawing.Point(119, 7);
            this.txtModelNo.Maximum = new decimal(new int[] {
            101,
            0,
            0,
            0});
            this.txtModelNo.Name = "txtModelNo";
            this.txtModelNo.Size = new System.Drawing.Size(84, 30);
            this.txtModelNo.TabIndex = 0;
            this.txtModelNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtModelNo.ValueChanged += new System.EventHandler(this.txtModelNo_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnDownloadAll);
            this.panel3.Controls.Add(this.btnDownload);
            this.panel3.Controls.Add(this.txtDataWrite);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 343);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(712, 144);
            this.panel3.TabIndex = 2;
            // 
            // btnDownloadAll
            // 
            this.btnDownloadAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDownloadAll.Location = new System.Drawing.Point(3, 89);
            this.btnDownloadAll.Name = "btnDownloadAll";
            this.btnDownloadAll.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.btnDownloadAll.Size = new System.Drawing.Size(300, 50);
            this.btnDownloadAll.StateDisabled.Back.Color1 = System.Drawing.Color.Lime;
            this.btnDownloadAll.StateDisabled.Back.Color2 = System.Drawing.Color.Yellow;
            this.btnDownloadAll.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnDownloadAll.StateDisabled.Content.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnDownloadAll.StateNormal.Back.Color1 = System.Drawing.Color.SeaGreen;
            this.btnDownloadAll.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnDownloadAll.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadAll.StatePressed.Back.Color1 = System.Drawing.Color.SeaGreen;
            this.btnDownloadAll.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnDownloadAll.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnDownloadAll.StatePressed.Content.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnDownloadAll.StateTracking.Back.Color1 = System.Drawing.Color.LightSeaGreen;
            this.btnDownloadAll.StateTracking.Back.Color2 = System.Drawing.Color.Turquoise;
            this.btnDownloadAll.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnDownloadAll.StateTracking.Content.ShortText.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownloadAll.TabIndex = 45;
            this.btnDownloadAll.Values.Text = "DOWNLOAD ALL";
            this.btnDownloadAll.Click += new System.EventHandler(this.btnDownloadAll_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Location = new System.Drawing.Point(409, 89);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.btnDownload.Size = new System.Drawing.Size(300, 50);
            this.btnDownload.StateDisabled.Back.Color1 = System.Drawing.Color.Lime;
            this.btnDownload.StateDisabled.Back.Color2 = System.Drawing.Color.Yellow;
            this.btnDownload.StateDisabled.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnDownload.StateDisabled.Content.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnDownload.StateNormal.Back.Color1 = System.Drawing.Color.SeaGreen;
            this.btnDownload.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnDownload.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.StatePressed.Back.Color1 = System.Drawing.Color.SeaGreen;
            this.btnDownload.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnDownload.StatePressed.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnDownload.StatePressed.Content.ShortText.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnDownload.StateTracking.Back.Color1 = System.Drawing.Color.LightSeaGreen;
            this.btnDownload.StateTracking.Back.Color2 = System.Drawing.Color.Turquoise;
            this.btnDownload.StateTracking.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnDownload.StateTracking.Content.ShortText.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.TabIndex = 44;
            this.btnDownload.Values.Text = "DOWNLOAD";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // txtDataWrite
            // 
            this.txtDataWrite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDataWrite.Location = new System.Drawing.Point(3, 3);
            this.txtDataWrite.Multiline = true;
            this.txtDataWrite.Name = "txtDataWrite";
            this.txtDataWrite.ReadOnly = true;
            this.txtDataWrite.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDataWrite.Size = new System.Drawing.Size(706, 82);
            this.txtDataWrite.TabIndex = 3;
            this.txtDataWrite.Text = "Data download";
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.cboComport,
            this.btnConnect});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(974, 32);
            this.menuStrip.TabIndex = 8;
            this.menuStrip.Text = "menuStrip";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.openToolStripMenuItem.Image = global::ModelDownload.Properties.Resources.OpenFileFolder;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(80, 28);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // cboComport
            // 
            this.cboComport.BackColor = System.Drawing.Color.Red;
            this.cboComport.Name = "cboComport";
            this.cboComport.Size = new System.Drawing.Size(121, 28);
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnConnect.Image = global::ModelDownload.Properties.Resources.UsbConnector;
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 28);
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Palette = this.MainPalette;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Model Download";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelFooter.ResumeLayout(false);
            this.tableLayoutPanelFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCompanyLogo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbScrews)).EndInit();
            this.tableLayoutPanelMid.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtModelNo)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private ComponentFactory.Krypton.Toolkit.KryptonPalette MainPalette;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbLogo;
        private MyUserControl.UcDateTime ucDateTime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFooter;
        private System.Windows.Forms.Label lbCompanyName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelBuildDate;
        private System.Windows.Forms.PictureBox pictureBoxCompanyLogo;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView tbScrews;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox cboComport;
        private System.Windows.Forms.ToolStripMenuItem btnConnect;
        private System.Windows.Forms.DataGridViewTextBoxColumn screwType;
        private System.Windows.Forms.DataGridViewTextBoxColumn screwName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMid;
        private System.Windows.Forms.TableLayoutPanel tbTray;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown txtModelNo;
        private System.Windows.Forms.Label lbModelName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtDataWrite;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDownloadAll;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDownload;
    }
}

