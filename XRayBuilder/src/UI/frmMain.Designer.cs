﻿namespace XRayBuilderGUI.UI
{
    partial class frmMain
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
                _cancelTokens.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblGoodreads = new System.Windows.Forms.Label();
            this.lblSeperator1 = new System.Windows.Forms.Label();
            this.lblSeperator2 = new System.Windows.Forms.Label();
            this.txtMobi = new System.Windows.Forms.TextBox();
            this.txtXMLFile = new System.Windows.Forms.TextBox();
            this.rdoGoodreads = new System.Windows.Forms.RadioButton();
            this.rdoFile = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblXMLFile = new System.Windows.Forms.Label();
            this.txtGoodreads = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSeperator3 = new System.Windows.Forms.Label();
            this.tmiAuthorProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPreview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmiEndAction = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiStartAction = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiXray = new System.Windows.Forms.ToolStripMenuItem();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pbFile4 = new System.Windows.Forms.PictureBox();
            this.pbFile3 = new System.Windows.Forms.PictureBox();
            this.pbFile2 = new System.Windows.Forms.PictureBox();
            this.pbFile1 = new System.Windows.Forms.PictureBox();
            this.lblFiles = new System.Windows.Forms.Label();
            this.txtAsin = new System.Windows.Forms.LinkLabel();
            this.txtAuthor = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.Label();
            this.lblAsin = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbCover = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnExtractTerms = new System.Windows.Forms.Button();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.btnOneClick = new System.Windows.Forms.Button();
            this.btnUnpack = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnBrowseMobi = new System.Windows.Forms.Button();
            this.btnKindleExtras = new System.Windows.Forms.Button();
            this.btnSearchGoodreads = new System.Windows.Forms.Button();
            this.btnDownloadTerms = new System.Windows.Forms.Button();
            this.btnBuild = new System.Windows.Forms.Button();
            this.btnBrowseXML = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.cmsPreview.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pbFile4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.pbFile3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.pbFile2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.pbFile1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.pbCover)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGoodreads
            // 
            this.lblGoodreads.AutoSize = true;
            this.lblGoodreads.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblGoodreads.Location = new System.Drawing.Point(156, 27);
            this.lblGoodreads.Name = "lblGoodreads";
            this.lblGoodreads.Size = new System.Drawing.Size(87, 13);
            this.lblGoodreads.TabIndex = 8;
            this.lblGoodreads.Text = "Goodreads URL:";
            this.lblGoodreads.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSeperator1
            // 
            this.lblSeperator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeperator1.Location = new System.Drawing.Point(140, 14);
            this.lblSeperator1.Name = "lblSeperator1";
            this.lblSeperator1.Size = new System.Drawing.Size(2, 54);
            this.lblSeperator1.TabIndex = 32;
            // 
            // lblSeperator2
            // 
            this.lblSeperator2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeperator2.Location = new System.Drawing.Point(341, 14);
            this.lblSeperator2.Name = "lblSeperator2";
            this.lblSeperator2.Size = new System.Drawing.Size(2, 54);
            this.lblSeperator2.TabIndex = 33;
            // 
            // txtMobi
            // 
            this.txtMobi.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMobi.Location = new System.Drawing.Point(16, 23);
            this.txtMobi.Name = "txtMobi";
            this.txtMobi.Size = new System.Drawing.Size(905, 23);
            this.txtMobi.TabIndex = 1;
            this.txtMobi.TextChanged += new System.EventHandler(this.txtMobi_TextChanged);
            // 
            // txtXMLFile
            // 
            this.txtXMLFile.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtXMLFile.Location = new System.Drawing.Point(197, 23);
            this.txtXMLFile.Name = "txtXMLFile";
            this.txtXMLFile.Size = new System.Drawing.Size(724, 23);
            this.txtXMLFile.TabIndex = 22;
            this.txtXMLFile.Visible = false;
            // 
            // rdoGoodreads
            // 
            this.rdoGoodreads.AutoSize = true;
            this.rdoGoodreads.Checked = true;
            this.rdoGoodreads.Location = new System.Drawing.Point(14, 24);
            this.rdoGoodreads.Name = "rdoGoodreads";
            this.rdoGoodreads.Size = new System.Drawing.Size(82, 19);
            this.rdoGoodreads.TabIndex = 0;
            this.rdoGoodreads.TabStop = true;
            this.rdoGoodreads.Text = "Goodreads";
            this.rdoGoodreads.UseVisualStyleBackColor = true;
            this.rdoGoodreads.CheckedChanged += new System.EventHandler(this.rdoSource_CheckedChanged);
            // 
            // rdoFile
            // 
            this.rdoFile.AutoSize = true;
            this.rdoFile.Location = new System.Drawing.Point(107, 24);
            this.rdoFile.Name = "rdoFile";
            this.rdoFile.Size = new System.Drawing.Size(43, 19);
            this.rdoFile.TabIndex = 1;
            this.rdoFile.Text = "File";
            this.rdoFile.UseVisualStyleBackColor = true;
            this.rdoFile.CheckedChanged += new System.EventHandler(this.rdoSource_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblXMLFile);
            this.groupBox1.Controls.Add(this.rdoFile);
            this.groupBox1.Controls.Add(this.txtXMLFile);
            this.groupBox1.Controls.Add(this.rdoGoodreads);
            this.groupBox1.Controls.Add(this.lblGoodreads);
            this.groupBox1.Controls.Add(this.txtGoodreads);
            this.groupBox1.Location = new System.Drawing.Point(14, 143);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(938, 61);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source";
            // 
            // lblXMLFile
            // 
            this.lblXMLFile.AutoSize = true;
            this.lblXMLFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblXMLFile.Location = new System.Drawing.Point(161, 27);
            this.lblXMLFile.Name = "lblXMLFile";
            this.lblXMLFile.Size = new System.Drawing.Size(26, 13);
            this.lblXMLFile.TabIndex = 26;
            this.lblXMLFile.Text = "File:";
            this.lblXMLFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblXMLFile.Visible = false;
            // 
            // txtGoodreads
            // 
            this.txtGoodreads.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGoodreads.Location = new System.Drawing.Point(262, 23);
            this.txtGoodreads.Name = "txtGoodreads";
            this.txtGoodreads.Size = new System.Drawing.Size(658, 23);
            this.txtGoodreads.TabIndex = 27;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtMobi);
            this.groupBox3.Location = new System.Drawing.Point(14, 74);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(938, 62);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Book";
            // 
            // lblSeperator3
            // 
            this.lblSeperator3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeperator3.Location = new System.Drawing.Point(686, 14);
            this.lblSeperator3.Name = "lblSeperator3";
            this.lblSeperator3.Size = new System.Drawing.Size(2, 54);
            this.lblSeperator3.TabIndex = 59;
            // 
            // tmiAuthorProfile
            // 
            this.tmiAuthorProfile.AutoSize = false;
            this.tmiAuthorProfile.Name = "tmiAuthorProfile";
            this.tmiAuthorProfile.Size = new System.Drawing.Size(114, 22);
            this.tmiAuthorProfile.Text = "Author Profile";
            this.tmiAuthorProfile.Click += new System.EventHandler(this.tmiAuthorProfile_Click);
            // 
            // cmsPreview
            // 
            this.cmsPreview.AutoSize = false;
            this.cmsPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.cmsPreview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.tmiAuthorProfile, this.tmiEndAction, this.tmiStartAction, this.tmiXray});
            this.cmsPreview.Name = "cmsPreview";
            this.cmsPreview.ShowImageMargin = false;
            this.cmsPreview.Size = new System.Drawing.Size(115, 91);
            // 
            // tmiEndAction
            // 
            this.tmiEndAction.AutoSize = false;
            this.tmiEndAction.Name = "tmiEndAction";
            this.tmiEndAction.Size = new System.Drawing.Size(114, 22);
            this.tmiEndAction.Text = "End Actions";
            this.tmiEndAction.Click += new System.EventHandler(this.tmiEndAction_Click);
            // 
            // tmiStartAction
            // 
            this.tmiStartAction.AutoSize = false;
            this.tmiStartAction.Name = "tmiStartAction";
            this.tmiStartAction.Size = new System.Drawing.Size(114, 22);
            this.tmiStartAction.Text = "Start Actions";
            this.tmiStartAction.Click += new System.EventHandler(this.tmiStartAction_Click);
            // 
            // tmiXray
            // 
            this.tmiXray.AutoSize = false;
            this.tmiXray.Name = "tmiXray";
            this.tmiXray.Size = new System.Drawing.Size(114, 22);
            this.tmiXray.Text = "X-Ray";
            this.tmiXray.Click += new System.EventHandler(this.tmiXray_Click);
            // 
            // prgBar
            // 
            this.prgBar.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBar.Location = new System.Drawing.Point(14, 627);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(901, 16);
            this.prgBar.Step = 1;
            this.prgBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgBar.TabIndex = 18;
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.BackColor = System.Drawing.SystemColors.Window;
            this.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutput.HideSelection = false;
            this.txtOutput.Location = new System.Drawing.Point(15, 219);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(695, 390);
            this.txtOutput.TabIndex = 61;
            this.txtOutput.Text = "";
            this.txtOutput.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtOutput_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.pbFile4);
            this.groupBox2.Controls.Add(this.pbFile3);
            this.groupBox2.Controls.Add(this.pbFile2);
            this.groupBox2.Controls.Add(this.pbFile1);
            this.groupBox2.Controls.Add(this.lblFiles);
            this.groupBox2.Controls.Add(this.txtAsin);
            this.groupBox2.Controls.Add(this.txtAuthor);
            this.groupBox2.Controls.Add(this.txtTitle);
            this.groupBox2.Controls.Add(this.lblAsin);
            this.groupBox2.Controls.Add(this.lblAuthor);
            this.groupBox2.Controls.Add(this.lblTitle);
            this.groupBox2.Controls.Add(this.pbCover);
            this.groupBox2.Location = new System.Drawing.Point(726, 211);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 398);
            this.groupBox2.TabIndex = 63;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Book Details";
            // 
            // pbFile4
            // 
            this.pbFile4.Image = global::XRayBuilderGUI.Properties.Resources.file_off;
            this.pbFile4.Location = new System.Drawing.Point(125, 373);
            this.pbFile4.Name = "pbFile4";
            this.pbFile4.Size = new System.Drawing.Size(10, 10);
            this.pbFile4.TabIndex = 73;
            this.pbFile4.TabStop = false;
            // 
            // pbFile3
            // 
            this.pbFile3.Image = global::XRayBuilderGUI.Properties.Resources.file_off;
            this.pbFile3.Location = new System.Drawing.Point(105, 373);
            this.pbFile3.Name = "pbFile3";
            this.pbFile3.Size = new System.Drawing.Size(10, 10);
            this.pbFile3.TabIndex = 72;
            this.pbFile3.TabStop = false;
            // 
            // pbFile2
            // 
            this.pbFile2.Image = global::XRayBuilderGUI.Properties.Resources.file_off;
            this.pbFile2.Location = new System.Drawing.Point(85, 373);
            this.pbFile2.Name = "pbFile2";
            this.pbFile2.Size = new System.Drawing.Size(10, 10);
            this.pbFile2.TabIndex = 71;
            this.pbFile2.TabStop = false;
            // 
            // pbFile1
            // 
            this.pbFile1.Image = global::XRayBuilderGUI.Properties.Resources.file_off;
            this.pbFile1.Location = new System.Drawing.Point(65, 373);
            this.pbFile1.Name = "pbFile1";
            this.pbFile1.Size = new System.Drawing.Size(10, 10);
            this.pbFile1.TabIndex = 70;
            this.pbFile1.TabStop = false;
            // 
            // lblFiles
            // 
            this.lblFiles.AutoSize = true;
            this.lblFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblFiles.Location = new System.Drawing.Point(14, 370);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(28, 12);
            this.lblFiles.TabIndex = 69;
            this.lblFiles.Text = "Files:";
            this.lblFiles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAsin
            // 
            this.txtAsin.ActiveLinkColor = System.Drawing.Color.MediumBlue;
            this.txtAsin.AutoSize = true;
            this.txtAsin.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.txtAsin.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.txtAsin.LinkColor = System.Drawing.Color.MediumBlue;
            this.txtAsin.Location = new System.Drawing.Point(63, 353);
            this.txtAsin.Name = "txtAsin";
            this.txtAsin.Size = new System.Drawing.Size(28, 12);
            this.txtAsin.TabIndex = 68;
            this.txtAsin.TabStop = true;
            this.txtAsin.Text = "ASIN";
            this.txtAsin.Visible = false;
            this.txtAsin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.txtAsin_LinkClicked);
            // 
            // txtAuthor
            // 
            this.txtAuthor.AutoSize = true;
            this.txtAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.txtAuthor.Location = new System.Drawing.Point(63, 336);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(33, 12);
            this.txtAuthor.TabIndex = 5;
            this.txtAuthor.Text = "Author";
            this.txtAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtAuthor.Visible = false;
            // 
            // txtTitle
            // 
            this.txtTitle.AutoSize = true;
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.txtTitle.Location = new System.Drawing.Point(63, 318);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(22, 12);
            this.txtTitle.TabIndex = 4;
            this.txtTitle.Text = "Title";
            this.txtTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtTitle.Visible = false;
            // 
            // lblAsin
            // 
            this.lblAsin.AutoSize = true;
            this.lblAsin.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblAsin.Location = new System.Drawing.Point(14, 353);
            this.lblAsin.Name = "lblAsin";
            this.lblAsin.Size = new System.Drawing.Size(31, 12);
            this.lblAsin.TabIndex = 3;
            this.lblAsin.Text = "ASIN:";
            this.lblAsin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAsin.Visible = false;
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblAuthor.Location = new System.Drawing.Point(14, 336);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(36, 12);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "Author:";
            this.lblAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAuthor.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.lblTitle.Location = new System.Drawing.Point(14, 318);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(25, 12);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Title:";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.Visible = false;
            // 
            // pbCover
            // 
            this.pbCover.Location = new System.Drawing.Point(16, 23);
            this.pbCover.Name = "pbCover";
            this.pbCover.Size = new System.Drawing.Size(194, 287);
            this.pbCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCover.TabIndex = 0;
            this.pbCover.TabStop = false;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(824, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2, 54);
            this.label1.TabIndex = 67;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Image = global::XRayBuilderGUI.Properties.Resources.cancel;
            this.btnCancel.Location = new System.Drawing.Point(927, 622);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(26, 25);
            this.btnCancel.TabIndex = 69;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Image = ((System.Drawing.Image) (resources.GetObject("btnCreate.Image")));
            this.btnCreate.Location = new System.Drawing.Point(433, 13);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(56, 55);
            this.btnCreate.TabIndex = 68;
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Image = ((System.Drawing.Image) (resources.GetObject("btnPreview.Image")));
            this.btnPreview.Location = new System.Drawing.Point(351, 13);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(75, 55);
            this.btnPreview.TabIndex = 12;
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Image = ((System.Drawing.Image) (resources.GetObject("btnAbout.Image")));
            this.btnAbout.Location = new System.Drawing.Point(897, 13);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(56, 55);
            this.btnAbout.TabIndex = 66;
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Image = ((System.Drawing.Image) (resources.GetObject("btnHelp.Image")));
            this.btnHelp.Location = new System.Drawing.Point(834, 13);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(56, 55);
            this.btnHelp.TabIndex = 65;
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnExtractTerms
            // 
            this.btnExtractTerms.Image = ((System.Drawing.Image) (resources.GetObject("btnExtractTerms.Image")));
            this.btnExtractTerms.Location = new System.Drawing.Point(622, 13);
            this.btnExtractTerms.Name = "btnExtractTerms";
            this.btnExtractTerms.Size = new System.Drawing.Size(56, 55);
            this.btnExtractTerms.TabIndex = 64;
            this.btnExtractTerms.UseVisualStyleBackColor = true;
            this.btnExtractTerms.Click += new System.EventHandler(this.btnExtractTerms_Click);
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Image = ((System.Drawing.Image) (resources.GetObject("btnBrowseOutput.Image")));
            this.btnBrowseOutput.Location = new System.Drawing.Point(696, 13);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(56, 55);
            this.btnBrowseOutput.TabIndex = 11;
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // btnOneClick
            // 
            this.btnOneClick.Image = ((System.Drawing.Image) (resources.GetObject("btnOneClick.Image")));
            this.btnOneClick.Location = new System.Drawing.Point(276, 13);
            this.btnOneClick.Name = "btnOneClick";
            this.btnOneClick.Size = new System.Drawing.Size(56, 55);
            this.btnOneClick.TabIndex = 28;
            this.btnOneClick.UseVisualStyleBackColor = true;
            this.btnOneClick.Click += new System.EventHandler(this.btnOneClick_Click);
            // 
            // btnUnpack
            // 
            this.btnUnpack.Image = ((System.Drawing.Image) (resources.GetObject("btnUnpack.Image")));
            this.btnUnpack.Location = new System.Drawing.Point(559, 13);
            this.btnUnpack.Name = "btnUnpack";
            this.btnUnpack.Size = new System.Drawing.Size(56, 55);
            this.btnUnpack.TabIndex = 60;
            this.btnUnpack.UseVisualStyleBackColor = true;
            this.btnUnpack.Click += new System.EventHandler(this.btnUnpack_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Image = ((System.Drawing.Image) (resources.GetObject("btnSettings.Image")));
            this.btnSettings.Location = new System.Drawing.Point(759, 13);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(56, 55);
            this.btnSettings.TabIndex = 16;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnBrowseMobi
            // 
            this.btnBrowseMobi.Image = ((System.Drawing.Image) (resources.GetObject("btnBrowseMobi.Image")));
            this.btnBrowseMobi.Location = new System.Drawing.Point(13, 13);
            this.btnBrowseMobi.Name = "btnBrowseMobi";
            this.btnBrowseMobi.Size = new System.Drawing.Size(56, 55);
            this.btnBrowseMobi.TabIndex = 10;
            this.btnBrowseMobi.UseVisualStyleBackColor = true;
            this.btnBrowseMobi.Click += new System.EventHandler(this.btnBrowseMobi_Click);
            // 
            // btnKindleExtras
            // 
            this.btnKindleExtras.Image = ((System.Drawing.Image) (resources.GetObject("btnKindleExtras.Image")));
            this.btnKindleExtras.Location = new System.Drawing.Point(150, 13);
            this.btnKindleExtras.Name = "btnKindleExtras";
            this.btnKindleExtras.Size = new System.Drawing.Size(56, 55);
            this.btnKindleExtras.TabIndex = 27;
            this.btnKindleExtras.UseVisualStyleBackColor = true;
            this.btnKindleExtras.Click += new System.EventHandler(this.btnKindleExtras_Click);
            // 
            // btnSearchGoodreads
            // 
            this.btnSearchGoodreads.Image = ((System.Drawing.Image) (resources.GetObject("btnSearchGoodreads.Image")));
            this.btnSearchGoodreads.Location = new System.Drawing.Point(76, 13);
            this.btnSearchGoodreads.Name = "btnSearchGoodreads";
            this.btnSearchGoodreads.Size = new System.Drawing.Size(56, 55);
            this.btnSearchGoodreads.TabIndex = 26;
            this.btnSearchGoodreads.UseVisualStyleBackColor = true;
            this.btnSearchGoodreads.Click += new System.EventHandler(this.btnSearchGoodreads_Click);
            // 
            // btnDownloadTerms
            // 
            this.btnDownloadTerms.Image = ((System.Drawing.Image) (resources.GetObject("btnDownloadTerms.Image")));
            this.btnDownloadTerms.Location = new System.Drawing.Point(496, 13);
            this.btnDownloadTerms.Name = "btnDownloadTerms";
            this.btnDownloadTerms.Size = new System.Drawing.Size(56, 55);
            this.btnDownloadTerms.TabIndex = 19;
            this.btnDownloadTerms.UseVisualStyleBackColor = true;
            this.btnDownloadTerms.Click += new System.EventHandler(this.btnDownloadTerms_Click);
            // 
            // btnBuild
            // 
            this.btnBuild.Image = ((System.Drawing.Image) (resources.GetObject("btnBuild.Image")));
            this.btnBuild.Location = new System.Drawing.Point(213, 13);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(56, 55);
            this.btnBuild.TabIndex = 14;
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // btnBrowseXML
            // 
            this.btnBrowseXML.Image = ((System.Drawing.Image) (resources.GetObject("btnBrowseXML.Image")));
            this.btnBrowseXML.Location = new System.Drawing.Point(76, 13);
            this.btnBrowseXML.Name = "btnBrowseXML";
            this.btnBrowseXML.Size = new System.Drawing.Size(56, 55);
            this.btnBrowseXML.TabIndex = 23;
            this.btnBrowseXML.UseVisualStyleBackColor = true;
            this.btnBrowseXML.Visible = false;
            this.btnBrowseXML.Click += new System.EventHandler(this.btnBrowseXML_Click);
            // 
            // frmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 660);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnExtractTerms);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnBrowseOutput);
            this.Controls.Add(this.btnOneClick);
            this.Controls.Add(this.btnUnpack);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.lblSeperator3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.lblSeperator2);
            this.Controls.Add(this.lblSeperator1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBrowseMobi);
            this.Controls.Add(this.btnKindleExtras);
            this.Controls.Add(this.btnSearchGoodreads);
            this.Controls.Add(this.btnDownloadTerms);
            this.Controls.Add(this.prgBar);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.btnBrowseXML);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "X-Ray Builder GUI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.cmsPreview.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.pbFile4)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.pbFile3)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.pbFile2)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.pbFile1)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.pbCover)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnDownloadTerms;
        private System.Windows.Forms.Button btnSearchGoodreads;
        private System.Windows.Forms.Label lblGoodreads;
        private System.Windows.Forms.Button btnKindleExtras;
        private System.Windows.Forms.Label lblSeperator1;
        private System.Windows.Forms.Label lblSeperator2;
        private System.Windows.Forms.TextBox txtMobi;
        private System.Windows.Forms.Button btnBrowseMobi;
        private System.Windows.Forms.Button btnBrowseXML;
        private System.Windows.Forms.TextBox txtXMLFile;
        private System.Windows.Forms.RadioButton rdoGoodreads;
        private System.Windows.Forms.RadioButton rdoFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblXMLFile;
        private System.Windows.Forms.Button btnOneClick;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.Label lblSeperator3;
        private System.Windows.Forms.ToolStripMenuItem tmiAuthorProfile;
        private System.Windows.Forms.ToolStripMenuItem tmiEndAction;
        private System.Windows.Forms.ToolStripMenuItem tmiXray;
        private System.Windows.Forms.ToolStripMenuItem tmiStartAction;
        private System.Windows.Forms.Button btnUnpack;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.TextBox txtGoodreads;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pbCover;
        private System.Windows.Forms.Label lblAsin;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label txtAuthor;
        private System.Windows.Forms.Label txtTitle;
        private System.Windows.Forms.LinkLabel txtAsin;
        private System.Windows.Forms.Button btnExtractTerms;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.PictureBox pbFile1;
        private System.Windows.Forms.Label lblFiles;
        private System.Windows.Forms.PictureBox pbFile4;
        private System.Windows.Forms.PictureBox pbFile3;
        private System.Windows.Forms.PictureBox pbFile2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.ContextMenuStrip cmsPreview;
        private System.Windows.Forms.Button btnPreview;
    }
}

