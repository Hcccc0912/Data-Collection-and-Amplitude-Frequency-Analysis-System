namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TsbConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbStart = new System.Windows.Forms.ToolStripButton();
            this.TsbStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.TsbClear = new System.Windows.Forms.ToolStripButton();
            this.PanelOfTimeGraph = new System.Windows.Forms.Panel();
            this.TimeGraph = new ZedGraph.ZedGraphControl();
            this.PanelOfFreqGraph = new System.Windows.Forms.Panel();
            this.FreqGraph = new ZedGraph.ZedGraphControl();
            this.SetPortGroupBox = new System.Windows.Forms.GroupBox();
            this.btnOpenCom = new System.Windows.Forms.Button();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.btnCheckCOM = new System.Windows.Forms.Button();
            this.cbxStopBits = new System.Windows.Forms.ComboBox();
            this.cbxParity = new System.Windows.Forms.ComboBox();
            this.cbxDataBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxCOMPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ReceiveGroupBox = new System.Windows.Forms.GroupBox();
            this.tbxRecvData = new System.Windows.Forms.RichTextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TsbTest = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.PanelOfTimeGraph.SuspendLayout();
            this.PanelOfFreqGraph.SuspendLayout();
            this.SetPortGroupBox.SuspendLayout();
            this.ReceiveGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(35, 35);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbConnect,
            this.toolStripSeparator2,
            this.toolStripSeparator3,
            this.TsbStart,
            this.TsbStop,
            this.toolStripSeparator4,
            this.toolStripSeparator5,
            this.TsbClear,
            this.toolStripSeparator1,
            this.toolStripSeparator6,
            this.TsbTest});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1152, 42);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TsbConnect
            // 
            this.TsbConnect.Image = ((System.Drawing.Image)(resources.GetObject("TsbConnect.Image")));
            this.TsbConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbConnect.Name = "TsbConnect";
            this.TsbConnect.Size = new System.Drawing.Size(89, 39);
            this.TsbConnect.Text = "连接";
            this.TsbConnect.Click += new System.EventHandler(this.TsbConnect_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 42);
            // 
            // TsbStart
            // 
            this.TsbStart.Image = ((System.Drawing.Image)(resources.GetObject("TsbStart.Image")));
            this.TsbStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbStart.Name = "TsbStart";
            this.TsbStart.Size = new System.Drawing.Size(89, 39);
            this.TsbStart.Text = "开始";
            this.TsbStart.Click += new System.EventHandler(this.TsbStart_Click);
            // 
            // TsbStop
            // 
            this.TsbStop.Enabled = false;
            this.TsbStop.Image = ((System.Drawing.Image)(resources.GetObject("TsbStop.Image")));
            this.TsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbStop.Name = "TsbStop";
            this.TsbStop.Size = new System.Drawing.Size(89, 39);
            this.TsbStop.Text = "停止";
            this.TsbStop.Click += new System.EventHandler(this.tsbStop_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 42);
            // 
            // TsbClear
            // 
            this.TsbClear.Image = ((System.Drawing.Image)(resources.GetObject("TsbClear.Image")));
            this.TsbClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbClear.Name = "TsbClear";
            this.TsbClear.Size = new System.Drawing.Size(127, 39);
            this.TsbClear.Text = "清除图表";
            this.TsbClear.ToolTipText = "清除图表";
            this.TsbClear.Click += new System.EventHandler(this.TsbClear_Click);
            // 
            // PanelOfTimeGraph
            // 
            this.PanelOfTimeGraph.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelOfTimeGraph.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.PanelOfTimeGraph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelOfTimeGraph.Controls.Add(this.TimeGraph);
            this.PanelOfTimeGraph.Location = new System.Drawing.Point(0, 55);
            this.PanelOfTimeGraph.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PanelOfTimeGraph.Name = "PanelOfTimeGraph";
            this.PanelOfTimeGraph.Size = new System.Drawing.Size(1152, 273);
            this.PanelOfTimeGraph.TabIndex = 6;
            // 
            // TimeGraph
            // 
            this.TimeGraph.IsShowPointValues = true;
            this.TimeGraph.Location = new System.Drawing.Point(-2, -2);
            this.TimeGraph.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.TimeGraph.Name = "TimeGraph";
            this.TimeGraph.ScrollGrace = 0D;
            this.TimeGraph.ScrollMaxX = 0D;
            this.TimeGraph.ScrollMaxY = 0D;
            this.TimeGraph.ScrollMaxY2 = 0D;
            this.TimeGraph.ScrollMinX = 0D;
            this.TimeGraph.ScrollMinY = 0D;
            this.TimeGraph.ScrollMinY2 = 0D;
            this.TimeGraph.Size = new System.Drawing.Size(1152, 273);
            this.TimeGraph.TabIndex = 9;
            this.TimeGraph.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.TimeGraph_ContextMenuBuilder);
            // 
            // PanelOfFreqGraph
            // 
            this.PanelOfFreqGraph.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelOfFreqGraph.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.PanelOfFreqGraph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PanelOfFreqGraph.Controls.Add(this.FreqGraph);
            this.PanelOfFreqGraph.Location = new System.Drawing.Point(0, 336);
            this.PanelOfFreqGraph.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PanelOfFreqGraph.Name = "PanelOfFreqGraph";
            this.PanelOfFreqGraph.Size = new System.Drawing.Size(1152, 276);
            this.PanelOfFreqGraph.TabIndex = 7;
            // 
            // FreqGraph
            // 
            this.FreqGraph.IsShowPointValues = true;
            this.FreqGraph.Location = new System.Drawing.Point(-2, -2);
            this.FreqGraph.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.FreqGraph.Name = "FreqGraph";
            this.FreqGraph.ScrollGrace = 0D;
            this.FreqGraph.ScrollMaxX = 0D;
            this.FreqGraph.ScrollMaxY = 0D;
            this.FreqGraph.ScrollMaxY2 = 0D;
            this.FreqGraph.ScrollMinX = 0D;
            this.FreqGraph.ScrollMinY = 0D;
            this.FreqGraph.ScrollMinY2 = 0D;
            this.FreqGraph.Size = new System.Drawing.Size(1152, 276);
            this.FreqGraph.TabIndex = 9;
            this.FreqGraph.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.FreqGraph_ContextMenuBuilder);
            // 
            // SetPortGroupBox
            // 
            this.SetPortGroupBox.Controls.Add(this.btnOpenCom);
            this.SetPortGroupBox.Controls.Add(this.cbxBaudRate);
            this.SetPortGroupBox.Controls.Add(this.btnCheckCOM);
            this.SetPortGroupBox.Controls.Add(this.cbxStopBits);
            this.SetPortGroupBox.Controls.Add(this.cbxParity);
            this.SetPortGroupBox.Controls.Add(this.cbxDataBits);
            this.SetPortGroupBox.Controls.Add(this.label5);
            this.SetPortGroupBox.Controls.Add(this.label4);
            this.SetPortGroupBox.Controls.Add(this.label3);
            this.SetPortGroupBox.Controls.Add(this.label2);
            this.SetPortGroupBox.Controls.Add(this.cbxCOMPort);
            this.SetPortGroupBox.Controls.Add(this.label1);
            this.SetPortGroupBox.Location = new System.Drawing.Point(0, 619);
            this.SetPortGroupBox.Name = "SetPortGroupBox";
            this.SetPortGroupBox.Size = new System.Drawing.Size(313, 223);
            this.SetPortGroupBox.TabIndex = 8;
            this.SetPortGroupBox.TabStop = false;
            this.SetPortGroupBox.Text = "串口设置";
            // 
            // btnOpenCom
            // 
            this.btnOpenCom.Location = new System.Drawing.Point(193, 129);
            this.btnOpenCom.Name = "btnOpenCom";
            this.btnOpenCom.Size = new System.Drawing.Size(86, 26);
            this.btnOpenCom.TabIndex = 1;
            this.btnOpenCom.Text = "打开串口";
            this.btnOpenCom.UseVisualStyleBackColor = true;
            this.btnOpenCom.Click += new System.EventHandler(this.btnOpenCom_Click);
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Location = new System.Drawing.Point(79, 57);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(90, 23);
            this.cbxBaudRate.TabIndex = 9;
            // 
            // btnCheckCOM
            // 
            this.btnCheckCOM.Location = new System.Drawing.Point(193, 21);
            this.btnCheckCOM.Name = "btnCheckCOM";
            this.btnCheckCOM.Size = new System.Drawing.Size(86, 28);
            this.btnCheckCOM.TabIndex = 0;
            this.btnCheckCOM.Text = "串口检测";
            this.btnCheckCOM.UseVisualStyleBackColor = true;
            this.btnCheckCOM.Click += new System.EventHandler(this.btnCheckCOM_Click);
            // 
            // cbxStopBits
            // 
            this.cbxStopBits.FormattingEnabled = true;
            this.cbxStopBits.Location = new System.Drawing.Point(79, 97);
            this.cbxStopBits.Name = "cbxStopBits";
            this.cbxStopBits.Size = new System.Drawing.Size(90, 23);
            this.cbxStopBits.TabIndex = 8;
            // 
            // cbxParity
            // 
            this.cbxParity.FormattingEnabled = true;
            this.cbxParity.Location = new System.Drawing.Point(79, 132);
            this.cbxParity.Name = "cbxParity";
            this.cbxParity.Size = new System.Drawing.Size(90, 23);
            this.cbxParity.TabIndex = 7;
            // 
            // cbxDataBits
            // 
            this.cbxDataBits.FormattingEnabled = true;
            this.cbxDataBits.Location = new System.Drawing.Point(79, 168);
            this.cbxDataBits.Name = "cbxDataBits";
            this.cbxDataBits.Size = new System.Drawing.Size(90, 23);
            this.cbxDataBits.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "波特率：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "停止位：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "数据位：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "奇偶检验：";
            // 
            // cbxCOMPort
            // 
            this.cbxCOMPort.FormattingEnabled = true;
            this.cbxCOMPort.Location = new System.Drawing.Point(79, 21);
            this.cbxCOMPort.Name = "cbxCOMPort";
            this.cbxCOMPort.Size = new System.Drawing.Size(90, 23);
            this.cbxCOMPort.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口号：";
            // 
            // ReceiveGroupBox
            // 
            this.ReceiveGroupBox.Controls.Add(this.label7);
            this.ReceiveGroupBox.Controls.Add(this.label6);
            this.ReceiveGroupBox.Controls.Add(this.tbxRecvData);
            this.ReceiveGroupBox.Location = new System.Drawing.Point(340, 619);
            this.ReceiveGroupBox.Name = "ReceiveGroupBox";
            this.ReceiveGroupBox.Size = new System.Drawing.Size(800, 223);
            this.ReceiveGroupBox.TabIndex = 9;
            this.ReceiveGroupBox.TabStop = false;
            this.ReceiveGroupBox.Text = "数据接收";
            // 
            // tbxRecvData
            // 
            this.tbxRecvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxRecvData.Location = new System.Drawing.Point(3, 21);
            this.tbxRecvData.Name = "tbxRecvData";
            this.tbxRecvData.Size = new System.Drawing.Size(794, 199);
            this.tbxRecvData.TabIndex = 0;
            this.tbxRecvData.Text = "";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "stock_disconnect_128px_1078469_easyicon.net.png");
            this.imageList1.Images.SetKeyName(1, "stock_connect_128px_1078468_easyicon.net.png");
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(119, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 1;
            this.label6.Text = "数量：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(164, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "label7";
            // 
            // TsbTest
            // 
            this.TsbTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TsbTest.Image = ((System.Drawing.Image)(resources.GetObject("TsbTest.Image")));
            this.TsbTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbTest.Name = "TsbTest";
            this.TsbTest.Size = new System.Drawing.Size(54, 39);
            this.TsbTest.Text = "测试";
            this.TsbTest.Click += new System.EventHandler(this.TsbTest_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 42);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1152, 852);
            this.Controls.Add(this.ReceiveGroupBox);
            this.Controls.Add(this.SetPortGroupBox);
            this.Controls.Add(this.PanelOfFreqGraph);
            this.Controls.Add(this.PanelOfTimeGraph);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "实时频谱分析仪";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.PanelOfTimeGraph.ResumeLayout(false);
            this.PanelOfFreqGraph.ResumeLayout(false);
            this.SetPortGroupBox.ResumeLayout(false);
            this.SetPortGroupBox.PerformLayout();
            this.ReceiveGroupBox.ResumeLayout(false);
            this.ReceiveGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TsbConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton TsbStart;
        private System.Windows.Forms.ToolStripButton TsbStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton TsbClear;
        private System.Windows.Forms.Panel PanelOfTimeGraph;
        private ZedGraph.ZedGraphControl TimeGraph;
        private System.Windows.Forms.Panel PanelOfFreqGraph;
        private ZedGraph.ZedGraphControl FreqGraph;
        private System.Windows.Forms.GroupBox SetPortGroupBox;
        private System.Windows.Forms.Button btnOpenCom;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.Button btnCheckCOM;
        private System.Windows.Forms.ComboBox cbxStopBits;
        private System.Windows.Forms.ComboBox cbxParity;
        private System.Windows.Forms.ComboBox cbxDataBits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxCOMPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox ReceiveGroupBox;
        private System.Windows.Forms.RichTextBox tbxRecvData;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton TsbTest;
    }
}

