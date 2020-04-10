namespace Diplom
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button btnRunAllHum;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pb1 = new System.Windows.Forms.PictureBox();
            this.btnSobel = new System.Windows.Forms.Button();
            this.btnDOG = new System.Windows.Forms.Button();
            this.btLoadImage = new System.Windows.Forms.Button();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnLoG = new System.Windows.Forms.Button();
            this.cmbLoG = new System.Windows.Forms.ComboBox();
            this.numWeight2 = new System.Windows.Forms.NumericUpDown();
            this.numWeight1 = new System.Windows.Forms.NumericUpDown();
            this.cmbKernelLenght = new System.Windows.Forms.ComboBox();
            this.cmbLaplace = new System.Windows.Forms.ComboBox();
            this.chb_SaveToFile = new System.Windows.Forms.CheckBox();
            this.btnKirsch = new System.Windows.Forms.Button();
            this.btnPrewitt = new System.Windows.Forms.Button();
            this.btRunAllFilters = new System.Windows.Forms.Button();
            this.btnLaplace = new System.Windows.Forms.Button();
            this.gbHum = new System.Windows.Forms.GroupBox();
            this.cmbGaussian = new System.Windows.Forms.ComboBox();
            this.btnGaussian = new System.Windows.Forms.Button();
            this.cmbMedian = new System.Windows.Forms.ComboBox();
            this.btnMedian = new System.Windows.Forms.Button();
            this.cmbMotionBlur = new System.Windows.Forms.ComboBox();
            this.btnMotionBlur = new System.Windows.Forms.Button();
            this.cmbMean = new System.Windows.Forms.ComboBox();
            this.btnMean = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripHelp = new System.Windows.Forms.ToolStripButton();
            this.btnImageShow = new System.Windows.Forms.Button();
            this.chbGray = new System.Windows.Forms.CheckBox();
            this.gbAll = new System.Windows.Forms.GroupBox();
            this.chbSavePath = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFileExtension = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChangeDir = new System.Windows.Forms.Button();
            this.textBoxDirectory = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnInverseUnsafe = new System.Windows.Forms.Button();
            this.btnInverseSafe = new System.Windows.Forms.Button();
            this.btnForwardUnsafe = new System.Windows.Forms.Button();
            this.btnForwardSafe = new System.Windows.Forms.Button();
            this.txtIterations = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTransformTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblDirection = new System.Windows.Forms.Label();
            btnRunAllHum = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).BeginInit();
            this.gbFilter.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight1)).BeginInit();
            this.gbHum.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gbAll.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRunAllHum
            // 
            btnRunAllHum.Location = new System.Drawing.Point(83, 147);
            btnRunAllHum.Name = "btnRunAllHum";
            btnRunAllHum.Size = new System.Drawing.Size(155, 23);
            btnRunAllHum.TabIndex = 10;
            btnRunAllHum.Text = "Запустить все";
            btnRunAllHum.UseVisualStyleBackColor = true;
            btnRunAllHum.Click += new System.EventHandler(this.btnRunAllHum_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pb1);
            this.groupBox1.Location = new System.Drawing.Point(16, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 175);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Исходное изображение";
            // 
            // pb1
            // 
            this.pb1.Location = new System.Drawing.Point(6, 19);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(191, 150);
            this.pb1.TabIndex = 2;
            this.pb1.TabStop = false;
            // 
            // btnSobel
            // 
            this.btnSobel.Enabled = false;
            this.btnSobel.Location = new System.Drawing.Point(311, 77);
            this.btnSobel.Name = "btnSobel";
            this.btnSobel.Size = new System.Drawing.Size(96, 23);
            this.btnSobel.TabIndex = 16;
            this.btnSobel.Text = "Метод Собеля";
            this.btnSobel.UseVisualStyleBackColor = true;
            this.btnSobel.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDOG
            // 
            this.btnDOG.Enabled = false;
            this.btnDOG.Location = new System.Drawing.Point(311, 164);
            this.btnDOG.Name = "btnDOG";
            this.btnDOG.Size = new System.Drawing.Size(96, 23);
            this.btnDOG.TabIndex = 17;
            this.btnDOG.Text = "вейвлет DOG";
            this.btnDOG.UseVisualStyleBackColor = true;
            this.btnDOG.Click += new System.EventHandler(this.btDOG_Click);
            // 
            // btLoadImage
            // 
            this.btLoadImage.Location = new System.Drawing.Point(6, 18);
            this.btLoadImage.Name = "btLoadImage";
            this.btLoadImage.Size = new System.Drawing.Size(161, 23);
            this.btLoadImage.TabIndex = 0;
            this.btLoadImage.Text = "Загрузите изображение";
            this.btLoadImage.UseVisualStyleBackColor = true;
            this.btLoadImage.Click += new System.EventHandler(this.btLoadImage_Click);
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.groupBox4);
            this.gbFilter.Controls.Add(this.numWeight2);
            this.gbFilter.Controls.Add(this.numWeight1);
            this.gbFilter.Controls.Add(this.cmbKernelLenght);
            this.gbFilter.Controls.Add(this.cmbLaplace);
            this.gbFilter.Controls.Add(this.chb_SaveToFile);
            this.gbFilter.Controls.Add(this.btnKirsch);
            this.gbFilter.Controls.Add(this.btnDOG);
            this.gbFilter.Controls.Add(this.btnPrewitt);
            this.gbFilter.Controls.Add(this.btRunAllFilters);
            this.gbFilter.Controls.Add(this.btnLaplace);
            this.gbFilter.Controls.Add(this.btnSobel);
            this.gbFilter.Location = new System.Drawing.Point(566, 30);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(427, 356);
            this.gbFilter.TabIndex = 19;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Обработать картинку фильтрами";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnLoG);
            this.groupBox4.Controls.Add(this.cmbLoG);
            this.groupBox4.Location = new System.Drawing.Point(37, 37);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(268, 63);
            this.groupBox4.TabIndex = 31;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "LOG";
            // 
            // btnLoG
            // 
            this.btnLoG.Location = new System.Drawing.Point(187, 34);
            this.btnLoG.Name = "btnLoG";
            this.btnLoG.Size = new System.Drawing.Size(70, 23);
            this.btnLoG.TabIndex = 26;
            this.btnLoG.Text = "LoG";
            this.btnLoG.UseVisualStyleBackColor = true;
            this.btnLoG.Click += new System.EventHandler(this.btnLoG_Click);
            // 
            // cmbLoG
            // 
            this.cmbLoG.FormattingEnabled = true;
            this.cmbLoG.Items.AddRange(new object[] {
            "LaplacianOfGaussian",
            "Laplacian3x3OfGaussian3x3 F",
            "Laplacian3x3OfGaussian5x5 F1",
            "Laplacian3x3OfGaussian5x5 F2",
            "Laplacian5x5OfGaussian3x3 F",
            "Laplacian5x5OfGaussian5x5 F1",
            "Laplacian5x5OfGaussian5x5 F2"});
            this.cmbLoG.Location = new System.Drawing.Point(14, 34);
            this.cmbLoG.Name = "cmbLoG";
            this.cmbLoG.Size = new System.Drawing.Size(168, 21);
            this.cmbLoG.TabIndex = 27;
            // 
            // numWeight2
            // 
            this.numWeight2.DecimalPlaces = 4;
            this.numWeight2.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numWeight2.Location = new System.Drawing.Point(224, 167);
            this.numWeight2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numWeight2.Name = "numWeight2";
            this.numWeight2.Size = new System.Drawing.Size(70, 20);
            this.numWeight2.TabIndex = 30;
            this.numWeight2.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numWeight1
            // 
            this.numWeight1.DecimalPlaces = 4;
            this.numWeight1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numWeight1.Location = new System.Drawing.Point(148, 167);
            this.numWeight1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numWeight1.Name = "numWeight1";
            this.numWeight1.Size = new System.Drawing.Size(70, 20);
            this.numWeight1.TabIndex = 29;
            this.numWeight1.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // cmbKernelLenght
            // 
            this.cmbKernelLenght.FormattingEnabled = true;
            this.cmbKernelLenght.Items.AddRange(new object[] {
            "3",
            "5",
            "7",
            "9",
            "11",
            "13",
            "15",
            "17",
            "19"});
            this.cmbKernelLenght.Location = new System.Drawing.Point(20, 166);
            this.cmbKernelLenght.Name = "cmbKernelLenght";
            this.cmbKernelLenght.Size = new System.Drawing.Size(121, 21);
            this.cmbKernelLenght.TabIndex = 28;
            // 
            // cmbLaplace
            // 
            this.cmbLaplace.FormattingEnabled = true;
            this.cmbLaplace.Items.AddRange(new object[] {
            "3x3",
            "5x5"});
            this.cmbLaplace.Location = new System.Drawing.Point(148, 107);
            this.cmbLaplace.Name = "cmbLaplace";
            this.cmbLaplace.Size = new System.Drawing.Size(71, 21);
            this.cmbLaplace.TabIndex = 25;
            // 
            // chb_SaveToFile
            // 
            this.chb_SaveToFile.AutoSize = true;
            this.chb_SaveToFile.Location = new System.Drawing.Point(249, 323);
            this.chb_SaveToFile.Name = "chb_SaveToFile";
            this.chb_SaveToFile.Size = new System.Drawing.Size(117, 17);
            this.chb_SaveToFile.TabIndex = 4;
            this.chb_SaveToFile.Text = "Сохранить в файл";
            this.chb_SaveToFile.UseVisualStyleBackColor = true;
            // 
            // btnKirsch
            // 
            this.btnKirsch.Location = new System.Drawing.Point(311, 48);
            this.btnKirsch.Name = "btnKirsch";
            this.btnKirsch.Size = new System.Drawing.Size(96, 23);
            this.btnKirsch.TabIndex = 24;
            this.btnKirsch.Text = "Кирш";
            this.btnKirsch.UseVisualStyleBackColor = true;
            this.btnKirsch.Click += new System.EventHandler(this.btnKirsch_Click);
            // 
            // btnPrewitt
            // 
            this.btnPrewitt.Location = new System.Drawing.Point(311, 19);
            this.btnPrewitt.Name = "btnPrewitt";
            this.btnPrewitt.Size = new System.Drawing.Size(96, 23);
            this.btnPrewitt.TabIndex = 23;
            this.btnPrewitt.Text = "Прюитт";
            this.btnPrewitt.UseVisualStyleBackColor = true;
            this.btnPrewitt.Click += new System.EventHandler(this.btnPrewitt_Click);
            // 
            // btRunAllFilters
            // 
            this.btRunAllFilters.Enabled = false;
            this.btRunAllFilters.Location = new System.Drawing.Point(37, 317);
            this.btRunAllFilters.Name = "btRunAllFilters";
            this.btRunAllFilters.Size = new System.Drawing.Size(155, 23);
            this.btRunAllFilters.TabIndex = 21;
            this.btRunAllFilters.Text = "Запустить все";
            this.btRunAllFilters.UseVisualStyleBackColor = true;
            this.btRunAllFilters.Click += new System.EventHandler(this.btRunAllFilters_Click);
            // 
            // btnLaplace
            // 
            this.btnLaplace.Enabled = false;
            this.btnLaplace.Location = new System.Drawing.Point(311, 106);
            this.btnLaplace.Name = "btnLaplace";
            this.btnLaplace.Size = new System.Drawing.Size(96, 22);
            this.btnLaplace.TabIndex = 19;
            this.btnLaplace.Text = "Метод Лапласа";
            this.btnLaplace.UseVisualStyleBackColor = true;
            this.btnLaplace.Click += new System.EventHandler(this.btLaplace_Click);
            // 
            // gbHum
            // 
            this.gbHum.Controls.Add(btnRunAllHum);
            this.gbHum.Controls.Add(this.cmbGaussian);
            this.gbHum.Controls.Add(this.btnGaussian);
            this.gbHum.Controls.Add(this.cmbMedian);
            this.gbHum.Controls.Add(this.btnMedian);
            this.gbHum.Controls.Add(this.cmbMotionBlur);
            this.gbHum.Controls.Add(this.btnMotionBlur);
            this.gbHum.Controls.Add(this.cmbMean);
            this.gbHum.Controls.Add(this.btnMean);
            this.gbHum.Location = new System.Drawing.Point(252, 30);
            this.gbHum.Name = "gbHum";
            this.gbHum.Size = new System.Drawing.Size(308, 269);
            this.gbHum.TabIndex = 20;
            this.gbHum.TabStop = false;
            this.gbHum.Text = "Наложить шум на картинку";
            // 
            // cmbGaussian
            // 
            this.cmbGaussian.FormattingEnabled = true;
            this.cmbGaussian.Location = new System.Drawing.Point(19, 107);
            this.cmbGaussian.Name = "cmbGaussian";
            this.cmbGaussian.Size = new System.Drawing.Size(149, 21);
            this.cmbGaussian.TabIndex = 9;
            // 
            // btnGaussian
            // 
            this.btnGaussian.Location = new System.Drawing.Point(196, 106);
            this.btnGaussian.Name = "btnGaussian";
            this.btnGaussian.Size = new System.Drawing.Size(95, 23);
            this.btnGaussian.TabIndex = 8;
            this.btnGaussian.Text = "Gaussian";
            this.btnGaussian.UseVisualStyleBackColor = true;
            this.btnGaussian.Click += new System.EventHandler(this.btnGaussian_Click);
            // 
            // cmbMedian
            // 
            this.cmbMedian.FormattingEnabled = true;
            this.cmbMedian.Location = new System.Drawing.Point(19, 76);
            this.cmbMedian.Name = "cmbMedian";
            this.cmbMedian.Size = new System.Drawing.Size(149, 21);
            this.cmbMedian.TabIndex = 7;
            // 
            // btnMedian
            // 
            this.btnMedian.Location = new System.Drawing.Point(196, 77);
            this.btnMedian.Name = "btnMedian";
            this.btnMedian.Size = new System.Drawing.Size(95, 23);
            this.btnMedian.TabIndex = 6;
            this.btnMedian.Text = "Median";
            this.btnMedian.UseVisualStyleBackColor = true;
            this.btnMedian.Click += new System.EventHandler(this.btnMedian_Click);
            // 
            // cmbMotionBlur
            // 
            this.cmbMotionBlur.FormattingEnabled = true;
            this.cmbMotionBlur.Location = new System.Drawing.Point(19, 50);
            this.cmbMotionBlur.Name = "cmbMotionBlur";
            this.cmbMotionBlur.Size = new System.Drawing.Size(149, 21);
            this.cmbMotionBlur.TabIndex = 5;
            // 
            // btnMotionBlur
            // 
            this.btnMotionBlur.Enabled = false;
            this.btnMotionBlur.Location = new System.Drawing.Point(196, 48);
            this.btnMotionBlur.Name = "btnMotionBlur";
            this.btnMotionBlur.Size = new System.Drawing.Size(95, 23);
            this.btnMotionBlur.TabIndex = 4;
            this.btnMotionBlur.Text = "MotionBlur";
            this.btnMotionBlur.UseVisualStyleBackColor = true;
            this.btnMotionBlur.Click += new System.EventHandler(this.btnMotionBlur_Click);
            // 
            // cmbMean
            // 
            this.cmbMean.FormattingEnabled = true;
            this.cmbMean.Location = new System.Drawing.Point(19, 18);
            this.cmbMean.Name = "cmbMean";
            this.cmbMean.Size = new System.Drawing.Size(149, 21);
            this.cmbMean.TabIndex = 3;
            // 
            // btnMean
            // 
            this.btnMean.Enabled = false;
            this.btnMean.Location = new System.Drawing.Point(196, 16);
            this.btnMean.Name = "btnMean";
            this.btnMean.Size = new System.Drawing.Size(95, 23);
            this.btnMean.TabIndex = 2;
            this.btnMean.Text = "Mean";
            this.btnMean.UseVisualStyleBackColor = true;
            this.btnMean.Click += new System.EventHandler(this.btnMean_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelName,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 565);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1752, 22);
            this.statusStrip1.TabIndex = 22;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelName
            // 
            this.toolStripStatusLabelName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabelName.Name = "toolStripStatusLabelName";
            this.toolStripStatusLabelName.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripFile,
            this.toolStripHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1752, 25);
            this.toolStrip1.TabIndex = 23;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripFile
            // 
            this.toolStripFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходToolStripMenuItem});
            this.toolStripFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripFile.Image")));
            this.toolStripFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripFile.Name = "toolStripFile";
            this.toolStripFile.Size = new System.Drawing.Size(49, 22);
            this.toolStripFile.Text = "Файл";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // toolStripHelp
            // 
            this.toolStripHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripHelp.Image")));
            this.toolStripHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripHelp.Name = "toolStripHelp";
            this.toolStripHelp.Size = new System.Drawing.Size(57, 22);
            this.toolStripHelp.Text = "Справка";
            this.toolStripHelp.Click += new System.EventHandler(this.toolStripHelp_Click);
            // 
            // btnImageShow
            // 
            this.btnImageShow.Location = new System.Drawing.Point(40, 229);
            this.btnImageShow.Name = "btnImageShow";
            this.btnImageShow.Size = new System.Drawing.Size(161, 23);
            this.btnImageShow.TabIndex = 24;
            this.btnImageShow.Text = "Показать изображение";
            this.btnImageShow.UseVisualStyleBackColor = true;
            this.btnImageShow.Click += new System.EventHandler(this.btnImageShow_Click);
            // 
            // chbGray
            // 
            this.chbGray.AutoSize = true;
            this.chbGray.Location = new System.Drawing.Point(19, 19);
            this.chbGray.Name = "chbGray";
            this.chbGray.Size = new System.Drawing.Size(149, 17);
            this.chbGray.TabIndex = 5;
            this.chbGray.Text = "Преобразовать в серый";
            this.chbGray.UseVisualStyleBackColor = true;
            // 
            // gbAll
            // 
            this.gbAll.Controls.Add(this.chbGray);
            this.gbAll.Location = new System.Drawing.Point(102, 392);
            this.gbAll.Name = "gbAll";
            this.gbAll.Size = new System.Drawing.Size(176, 78);
            this.gbAll.TabIndex = 28;
            this.gbAll.TabStop = false;
            this.gbAll.Text = "Общие";
            // 
            // chbSavePath
            // 
            this.chbSavePath.AutoSize = true;
            this.chbSavePath.Checked = true;
            this.chbSavePath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbSavePath.Location = new System.Drawing.Point(6, 19);
            this.chbSavePath.Name = "chbSavePath";
            this.chbSavePath.Size = new System.Drawing.Size(301, 17);
            this.chbSavePath.TabIndex = 6;
            this.chbSavePath.Text = "Сохранять в каталог откуда запускалось приложение";
            this.chbSavePath.UseVisualStyleBackColor = true;
            this.chbSavePath.CheckedChanged += new System.EventHandler(this.chbSavePath_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmbFileExtension);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnChangeDir);
            this.groupBox2.Controls.Add(this.textBoxDirectory);
            this.groupBox2.Controls.Add(this.chbSavePath);
            this.groupBox2.Location = new System.Drawing.Point(312, 392);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(681, 78);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Параметры сохранения";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(475, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Формат изображения";
            // 
            // cmbFileExtension
            // 
            this.cmbFileExtension.FormattingEnabled = true;
            this.cmbFileExtension.Items.AddRange(new object[] {
            "BMP",
            "JPG",
            "PNG"});
            this.cmbFileExtension.Location = new System.Drawing.Point(604, 41);
            this.cmbFileExtension.Name = "cmbFileExtension";
            this.cmbFileExtension.Size = new System.Drawing.Size(57, 21);
            this.cmbFileExtension.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Выбере каталог";
            // 
            // btnChangeDir
            // 
            this.btnChangeDir.Enabled = false;
            this.btnChangeDir.Location = new System.Drawing.Point(437, 40);
            this.btnChangeDir.Name = "btnChangeDir";
            this.btnChangeDir.Size = new System.Drawing.Size(32, 23);
            this.btnChangeDir.TabIndex = 8;
            this.btnChangeDir.Text = "...";
            this.btnChangeDir.UseVisualStyleBackColor = true;
            this.btnChangeDir.Click += new System.EventHandler(this.btnChangeDir_Click);
            // 
            // textBoxDirectory
            // 
            this.textBoxDirectory.Enabled = false;
            this.textBoxDirectory.Location = new System.Drawing.Point(101, 42);
            this.textBoxDirectory.Name = "textBoxDirectory";
            this.textBoxDirectory.Size = new System.Drawing.Size(330, 20);
            this.textBoxDirectory.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btLoadImage);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.btnImageShow);
            this.groupBox3.Location = new System.Drawing.Point(12, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(233, 271);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Исходное изображение";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(335, 347);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "Анализ изображений";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // btnInverseUnsafe
            // 
            this.btnInverseUnsafe.Location = new System.Drawing.Point(17, 284);
            this.btnInverseUnsafe.Name = "btnInverseUnsafe";
            this.btnInverseUnsafe.Size = new System.Drawing.Size(183, 34);
            this.btnInverseUnsafe.TabIndex = 11;
            this.btnInverseUnsafe.Text = "Инверсия (небезоп. код)";
            this.btnInverseUnsafe.UseVisualStyleBackColor = true;
            this.btnInverseUnsafe.Click += new System.EventHandler(this.btnInverseUnsafe_Click);
            // 
            // btnInverseSafe
            // 
            this.btnInverseSafe.Location = new System.Drawing.Point(17, 237);
            this.btnInverseSafe.Name = "btnInverseSafe";
            this.btnInverseSafe.Size = new System.Drawing.Size(183, 39);
            this.btnInverseSafe.TabIndex = 10;
            this.btnInverseSafe.Text = "Инверсия (безоп. код)";
            this.btnInverseSafe.UseVisualStyleBackColor = true;
            this.btnInverseSafe.Click += new System.EventHandler(this.btnInverseSafe_Click);
            // 
            // btnForwardUnsafe
            // 
            this.btnForwardUnsafe.Location = new System.Drawing.Point(17, 160);
            this.btnForwardUnsafe.Name = "btnForwardUnsafe";
            this.btnForwardUnsafe.Size = new System.Drawing.Size(183, 35);
            this.btnForwardUnsafe.TabIndex = 9;
            this.btnForwardUnsafe.Text = "Вперед (небезоп. код)";
            this.btnForwardUnsafe.UseVisualStyleBackColor = true;
            this.btnForwardUnsafe.Click += new System.EventHandler(this.btnForwardUnsafe_Click);
            // 
            // btnForwardSafe
            // 
            this.btnForwardSafe.Location = new System.Drawing.Point(17, 119);
            this.btnForwardSafe.Name = "btnForwardSafe";
            this.btnForwardSafe.Size = new System.Drawing.Size(183, 34);
            this.btnForwardSafe.TabIndex = 8;
            this.btnForwardSafe.Text = "Вперед (безоп. код)";
            this.btnForwardSafe.UseVisualStyleBackColor = true;
            this.btnForwardSafe.Click += new System.EventHandler(this.btnForwardSafe_Click);
            // 
            // txtIterations
            // 
            this.txtIterations.Location = new System.Drawing.Point(110, 66);
            this.txtIterations.Name = "txtIterations";
            this.txtIterations.Size = new System.Drawing.Size(90, 20);
            this.txtIterations.TabIndex = 7;
            this.txtIterations.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Кол-во итераций:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(17, 29);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(183, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Загрузите изображение";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(17, 468);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(183, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Сохранить изображение";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTransformTime
            // 
            this.lblTransformTime.AutoSize = true;
            this.lblTransformTime.Location = new System.Drawing.Point(147, 396);
            this.lblTransformTime.Name = "lblTransformTime";
            this.lblTransformTime.Size = new System.Drawing.Size(13, 13);
            this.lblTransformTime.TabIndex = 5;
            this.lblTransformTime.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 396);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Время трансформации:";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(62, 367);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(25, 13);
            this.lblHeight.TabIndex = 3;
            this.lblHeight.Text = "512";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 367);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Высота:";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(62, 347);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(25, 13);
            this.lblWidth.TabIndex = 1;
            this.lblWidth.Text = "512";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 347);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Ширина: ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Diplom.Properties.Resources.lena;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(1228, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblDirection);
            this.groupBox5.Controls.Add(this.btnInverseUnsafe);
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.Controls.Add(this.btnBrowse);
            this.groupBox5.Controls.Add(this.lblTransformTime);
            this.groupBox5.Controls.Add(this.btnInverseSafe);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.lblHeight);
            this.groupBox5.Controls.Add(this.btnForwardUnsafe);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.txtIterations);
            this.groupBox5.Controls.Add(this.lblWidth);
            this.groupBox5.Controls.Add(this.btnForwardSafe);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Location = new System.Drawing.Point(999, 30);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(222, 510);
            this.groupBox5.TabIndex = 34;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Обработать картинку вейвлетом Хаара";
            // 
            // lblDirection
            // 
            this.lblDirection.AutoSize = true;
            this.lblDirection.Location = new System.Drawing.Point(17, 413);
            this.lblDirection.Name = "lblDirection";
            this.lblDirection.Size = new System.Drawing.Size(0, 13);
            this.lblDirection.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1752, 587);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbAll);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gbHum);
            this.Controls.Add(this.gbFilter);
            this.HelpButton = true;
            this.Name = "MainForm";
            this.Text = "Дипломная работа";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).EndInit();
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numWeight2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeight1)).EndInit();
            this.gbHum.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbAll.ResumeLayout(false);
            this.gbAll.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btLoadImage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pb1;        // поле для вывода исходного изображения        // поле для вывода изображения после наложения шума        // поле для вывода изображения после наложения фильтра      // поле для отображения имени файла          // поле для отображения пути к файлу          // поле для отображения размера файла         // поле для отображения ширины изображения
        private System.Windows.Forms.Button btnSobel;
        private System.Windows.Forms.Button btnDOG;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.GroupBox gbHum;
        private System.Windows.Forms.Button btnLaplace;
        private System.Windows.Forms.Button btRunAllFilters;
        private System.Windows.Forms.CheckBox chb_SaveToFile;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripFile;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelName;
        private System.Windows.Forms.ToolStripButton toolStripHelp;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnImageShow;
        private System.Windows.Forms.Button btnKirsch;
        private System.Windows.Forms.Button btnPrewitt;
        private System.Windows.Forms.CheckBox chbGray;
        private System.Windows.Forms.GroupBox gbAll;
        private System.Windows.Forms.CheckBox chbSavePath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbLoG;
        private System.Windows.Forms.Button btnLoG;
        private System.Windows.Forms.ComboBox cmbLaplace;
        private System.Windows.Forms.NumericUpDown numWeight2;
        private System.Windows.Forms.NumericUpDown numWeight1;
        private System.Windows.Forms.ComboBox cmbKernelLenght;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChangeDir;
        private System.Windows.Forms.TextBox textBoxDirectory;
        private System.Windows.Forms.ComboBox cmbMean;
        private System.Windows.Forms.Button btnMean;
        private System.Windows.Forms.Button btnMotionBlur;
        private System.Windows.Forms.ComboBox cmbMotionBlur;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFileExtension;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbMedian;
        private System.Windows.Forms.Button btnMedian;
        private System.Windows.Forms.ComboBox cmbGaussian;
        private System.Windows.Forms.Button btnGaussian;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox4;         // поле для отображения высоты изображения
        private System.Windows.Forms.Button btnInverseUnsafe;
        private System.Windows.Forms.Button btnInverseSafe;
        private System.Windows.Forms.Button btnForwardUnsafe;
        private System.Windows.Forms.Button btnForwardSafe;
        private System.Windows.Forms.TextBox txtIterations;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTransformTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblDirection;
    }
}

