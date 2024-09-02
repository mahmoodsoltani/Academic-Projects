namespace Summarization
{
    partial class SummForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btn_run = new System.Windows.Forms.Button();
            this.txt_main = new System.Windows.Forms.TextBox();
            this.btn_browse = new System.Windows.Forms.Button();
            this.txt_title = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chk_sort_result = new System.Windows.Forms.CheckBox();
            this.chk_use_extra_feature = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.nud_sentence_limit = new System.Windows.Forms.NumericUpDown();
            this.txt_summ = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_rules = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txt_range_high = new System.Windows.Forms.TextBox();
            this.txt_variable_name = new System.Windows.Forms.TextBox();
            this.txt_range_low = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btn_load_fis = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_p4 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_p3 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_p2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_p1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_mf_name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lst_io = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lst_mf = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_run_over_corpus = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.progressText = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_sentence_limit)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_run
            // 
            this.btn_run.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_run.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_run.Location = new System.Drawing.Point(95, 490);
            this.btn_run.Name = "btn_run";
            this.btn_run.Size = new System.Drawing.Size(167, 25);
            this.btn_run.TabIndex = 0;
            this.btn_run.Text = "Run";
            this.btn_run.UseVisualStyleBackColor = true;
            this.btn_run.Click += new System.EventHandler(this.btn_run_Click);
            // 
            // txt_main
            // 
            this.txt_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_main.Location = new System.Drawing.Point(3, 95);
            this.txt_main.Multiline = true;
            this.txt_main.Name = "txt_main";
            this.txt_main.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_main.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_main.Size = new System.Drawing.Size(541, 152);
            this.txt_main.TabIndex = 1;
            // 
            // btn_browse
            // 
            this.btn_browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_browse.Location = new System.Drawing.Point(3, 23);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(65, 46);
            this.btn_browse.TabIndex = 2;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // txt_title
            // 
            this.txt_title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_title.Location = new System.Drawing.Point(74, 23);
            this.txt_title.Multiline = true;
            this.txt_title.Name = "txt_title";
            this.txt_title.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_title.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_title.Size = new System.Drawing.Size(470, 46);
            this.txt_title.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(480, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "عنوان (تیتر)";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(452, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "متن اصلی (بدنه)";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(565, 470);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(557, 443);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "News Loader";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txt_title);
            this.splitContainer1.Panel1.Controls.Add(this.txt_main);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btn_browse);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chk_sort_result);
            this.splitContainer1.Panel2.Controls.Add(this.chk_use_extra_feature);
            this.splitContainer1.Panel2.Controls.Add(this.label13);
            this.splitContainer1.Panel2.Controls.Add(this.nud_sentence_limit);
            this.splitContainer1.Panel2.Controls.Add(this.txt_summ);
            this.splitContainer1.Panel2.Controls.Add(this.label12);
            this.splitContainer1.Size = new System.Drawing.Size(551, 437);
            this.splitContainer1.SplitterDistance = 254;
            this.splitContainer1.TabIndex = 6;
            // 
            // chk_sort_result
            // 
            this.chk_sort_result.AutoSize = true;
            this.chk_sort_result.Checked = true;
            this.chk_sort_result.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_sort_result.Location = new System.Drawing.Point(32, 10);
            this.chk_sort_result.Name = "chk_sort_result";
            this.chk_sort_result.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_sort_result.Size = new System.Drawing.Size(209, 18);
            this.chk_sort_result.TabIndex = 11;
            this.chk_sort_result.Text = "مرتب سازی براساس موقعیت در متن";
            this.chk_sort_result.UseVisualStyleBackColor = true;
            // 
            // chk_use_extra_feature
            // 
            this.chk_use_extra_feature.AutoSize = true;
            this.chk_use_extra_feature.Checked = true;
            this.chk_use_extra_feature.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_use_extra_feature.Location = new System.Drawing.Point(10, 35);
            this.chk_use_extra_feature.Name = "chk_use_extra_feature";
            this.chk_use_extra_feature.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chk_use_extra_feature.Size = new System.Drawing.Size(231, 18);
            this.chk_use_extra_feature.TabIndex = 10;
            this.chk_use_extra_feature.Text = "استفاده از ویژگی شباهت جملات خلاصه";
            this.chk_use_extra_feature.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(370, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(174, 14);
            this.label13.TabIndex = 9;
            this.label13.Text = "تعداد (تقریبی) کلمات متن خلاصه";
            // 
            // nud_sentence_limit
            // 
            this.nud_sentence_limit.Location = new System.Drawing.Point(320, 9);
            this.nud_sentence_limit.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nud_sentence_limit.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nud_sentence_limit.Name = "nud_sentence_limit";
            this.nud_sentence_limit.Size = new System.Drawing.Size(47, 22);
            this.nud_sentence_limit.TabIndex = 8;
            this.nud_sentence_limit.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            // 
            // txt_summ
            // 
            this.txt_summ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_summ.Location = new System.Drawing.Point(3, 59);
            this.txt_summ.Multiline = true;
            this.txt_summ.Name = "txt_summ";
            this.txt_summ.ReadOnly = true;
            this.txt_summ.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_summ.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_summ.Size = new System.Drawing.Size(541, 113);
            this.txt_summ.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(388, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(156, 14);
            this.label12.TabIndex = 7;
            this.label12.Text = "مهمترین جملات متن (خلاصه)";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.Controls.Add(this.txt_range_high);
            this.tabPage2.Controls.Add(this.txt_variable_name);
            this.tabPage2.Controls.Add(this.txt_range_low);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.btn_load_fis);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.lst_io);
            this.tabPage2.Controls.Add(this.lst_mf);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(557, 443);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Fuzzy Inference";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txt_rules);
            this.groupBox3.Location = new System.Drawing.Point(6, 362);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(545, 81);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rules";
            // 
            // txt_rules
            // 
            this.txt_rules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_rules.Location = new System.Drawing.Point(3, 18);
            this.txt_rules.Multiline = true;
            this.txt_rules.Name = "txt_rules";
            this.txt_rules.ReadOnly = true;
            this.txt_rules.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_rules.Size = new System.Drawing.Size(539, 60);
            this.txt_rules.TabIndex = 7;
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BackColor = System.Drawing.Color.Gainsboro;
            this.chart1.BorderlineColor = System.Drawing.Color.Black;
            this.chart1.BorderSkin.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.LeftRight;
            this.chart1.BorderSkin.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.Cross;
            this.chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(188, 12);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(363, 215);
            this.chart1.TabIndex = 10;
            this.chart1.Text = "MF Chart";
            // 
            // txt_range_high
            // 
            this.txt_range_high.Location = new System.Drawing.Point(109, 220);
            this.txt_range_high.Name = "txt_range_high";
            this.txt_range_high.ReadOnly = true;
            this.txt_range_high.Size = new System.Drawing.Size(43, 22);
            this.txt_range_high.TabIndex = 6;
            this.txt_range_high.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_double_KeyPress);
            // 
            // txt_variable_name
            // 
            this.txt_variable_name.Location = new System.Drawing.Point(55, 193);
            this.txt_variable_name.Name = "txt_variable_name";
            this.txt_variable_name.ReadOnly = true;
            this.txt_variable_name.Size = new System.Drawing.Size(127, 22);
            this.txt_variable_name.TabIndex = 8;
            // 
            // txt_range_low
            // 
            this.txt_range_low.Location = new System.Drawing.Point(55, 220);
            this.txt_range_low.Name = "txt_range_low";
            this.txt_range_low.ReadOnly = true;
            this.txt_range_low.Size = new System.Drawing.Size(43, 22);
            this.txt_range_low.TabIndex = 4;
            this.txt_range_low.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_double_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label6.Location = new System.Drawing.Point(98, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 14);
            this.label6.TabIndex = 7;
            this.label6.Text = "-";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 196);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 14);
            this.label11.TabIndex = 9;
            this.label11.Text = "Variable";
            // 
            // btn_load_fis
            // 
            this.btn_load_fis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_load_fis.Location = new System.Drawing.Point(6, 12);
            this.btn_load_fis.Name = "btn_load_fis";
            this.btn_load_fis.Size = new System.Drawing.Size(176, 25);
            this.btn_load_fis.TabIndex = 7;
            this.btn_load_fis.Text = "Load FIS File";
            this.btn_load_fis.UseVisualStyleBackColor = true;
            this.btn_load_fis.Click += new System.EventHandler(this.btn_load_fis_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txt_p4);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txt_p3);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txt_p2);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txt_p1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(188, 306);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(358, 50);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Function Parameters";
            // 
            // txt_p4
            // 
            this.txt_p4.Location = new System.Drawing.Point(287, 21);
            this.txt_p4.Name = "txt_p4";
            this.txt_p4.ReadOnly = true;
            this.txt_p4.Size = new System.Drawing.Size(42, 22);
            this.txt_p4.TabIndex = 14;
            this.txt_p4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_double_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(264, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 14);
            this.label10.TabIndex = 15;
            this.label10.Text = "P4";
            // 
            // txt_p3
            // 
            this.txt_p3.Location = new System.Drawing.Point(202, 21);
            this.txt_p3.Name = "txt_p3";
            this.txt_p3.ReadOnly = true;
            this.txt_p3.Size = new System.Drawing.Size(42, 22);
            this.txt_p3.TabIndex = 12;
            this.txt_p3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_double_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(180, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 14);
            this.label9.TabIndex = 13;
            this.label9.Text = "P3";
            // 
            // txt_p2
            // 
            this.txt_p2.Location = new System.Drawing.Point(115, 21);
            this.txt_p2.Name = "txt_p2";
            this.txt_p2.ReadOnly = true;
            this.txt_p2.Size = new System.Drawing.Size(42, 22);
            this.txt_p2.TabIndex = 10;
            this.txt_p2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_double_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(92, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 14);
            this.label8.TabIndex = 11;
            this.label8.Text = "P2";
            // 
            // txt_p1
            // 
            this.txt_p1.Location = new System.Drawing.Point(32, 21);
            this.txt_p1.Name = "txt_p1";
            this.txt_p1.ReadOnly = true;
            this.txt_p1.Size = new System.Drawing.Size(42, 22);
            this.txt_p1.TabIndex = 8;
            this.txt_p1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_double_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 14);
            this.label7.TabIndex = 9;
            this.label7.Text = "P1";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_mf_name);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(188, 236);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 64);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General MF Parameters";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "None",
            "Triangular",
            "Trapezoidal"});
            this.comboBox1.Location = new System.Drawing.Point(209, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(111, 22);
            this.comboBox1.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "Type";
            // 
            // txt_mf_name
            // 
            this.txt_mf_name.Location = new System.Drawing.Point(54, 24);
            this.txt_mf_name.Name = "txt_mf_name";
            this.txt_mf_name.ReadOnly = true;
            this.txt_mf_name.Size = new System.Drawing.Size(97, 22);
            this.txt_mf_name.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 14);
            this.label5.TabIndex = 5;
            this.label5.Text = "Range";
            // 
            // lst_io
            // 
            this.lst_io.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3});
            this.lst_io.FullRowSelect = true;
            this.lst_io.GridLines = true;
            this.lst_io.Location = new System.Drawing.Point(6, 42);
            this.lst_io.MultiSelect = false;
            this.lst_io.Name = "lst_io";
            this.lst_io.ShowGroups = false;
            this.lst_io.ShowItemToolTips = true;
            this.lst_io.Size = new System.Drawing.Size(176, 143);
            this.lst_io.TabIndex = 4;
            this.lst_io.UseCompatibleStateImageBehavior = false;
            this.lst_io.View = System.Windows.Forms.View.Details;
            this.lst_io.SelectedIndexChanged += new System.EventHandler(this.lst_io_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "I/O Variables";
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            this.columnHeader3.Width = 55;
            // 
            // lst_mf
            // 
            this.lst_mf.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lst_mf.FullRowSelect = true;
            this.lst_mf.GridLines = true;
            this.lst_mf.Location = new System.Drawing.Point(6, 248);
            this.lst_mf.MultiSelect = false;
            this.lst_mf.Name = "lst_mf";
            this.lst_mf.Size = new System.Drawing.Size(176, 108);
            this.lst_mf.TabIndex = 3;
            this.lst_mf.UseCompatibleStateImageBehavior = false;
            this.lst_mf.View = System.Windows.Forms.View.Details;
            this.lst_mf.SelectedIndexChanged += new System.EventHandler(this.lst_mf_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Membership Function Name";
            this.columnHeader1.Width = 140;
            // 
            // btn_run_over_corpus
            // 
            this.btn_run_over_corpus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_run_over_corpus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_run_over_corpus.Location = new System.Drawing.Point(337, 490);
            this.btn_run_over_corpus.Name = "btn_run_over_corpus";
            this.btn_run_over_corpus.Size = new System.Drawing.Size(167, 25);
            this.btn_run_over_corpus.TabIndex = 7;
            this.btn_run_over_corpus.Text = "Load Pasokh Corpus";
            this.btn_run_over_corpus.UseVisualStyleBackColor = true;
            this.btn_run_over_corpus.Click += new System.EventHandler(this.btn_run_over_corpus_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.progressText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 522);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(589, 25);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Margin = new System.Windows.Forms.Padding(10, 3, 1, 3);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 19);
            // 
            // progressText
            // 
            this.progressText.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.progressText.Name = "progressText";
            this.progressText.Size = new System.Drawing.Size(13, 20);
            this.progressText.Text = "  ";
            // 
            // SummForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 547);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_run_over_corpus);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btn_run);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.MinimumSize = new System.Drawing.Size(600, 570);
            this.Name = "SummForm";
            this.Text = "Summarization Tools";
            this.Load += new System.EventHandler(this.SummForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nud_sentence_limit)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_run;
        private System.Windows.Forms.TextBox txt_main;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.TextBox txt_title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lst_io;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView lst_mf;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_p4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_p3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_p2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_p1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txt_range_high;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_range_low;
        private System.Windows.Forms.TextBox txt_mf_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_load_fis;
        private System.Windows.Forms.TextBox txt_variable_name;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_rules;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txt_summ;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nud_sentence_limit;
        private System.Windows.Forms.Button btn_run_over_corpus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel progressText;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.CheckBox chk_sort_result;
        private System.Windows.Forms.CheckBox chk_use_extra_feature;
    }
}

