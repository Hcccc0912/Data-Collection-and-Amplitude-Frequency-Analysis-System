using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        #region 加载dll
        [DllImport("Fourier.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "fft")]
        extern static IntPtr fft(double[] data, int datanum);
        #endregion

        #region 窗体初始化
        public Form1()
        {
            InitializeComponent();
            //设置绘图标题
            TimeGraph.GraphPane.Title.Text = "时域";
            TimeGraph.GraphPane.XAxis.Title.Text = "时间";
            TimeGraph.GraphPane.YAxis.Title.Text = "幅度";
            this.TimeGraph.GraphPane.XAxis.Type = ZedGraph.AxisType.Linear;
            FreqGraph.GraphPane.Title.Text = "频域";
            FreqGraph.GraphPane.XAxis.Title.Text = "频率";
            FreqGraph.GraphPane.YAxis.Title.Text = "幅度";
            this.FreqGraph.GraphPane.XAxis.Type = ZedGraph.AxisType.Linear;
            FontSpec myFont = new FontSpec("Microsoft YaHei UI", 17, Color.Black, true, false, false);
            TimeGraph.GraphPane.Title.FontSpec = myFont;
            TimeGraph.GraphPane.XAxis.Title.FontSpec = myFont;
            TimeGraph.GraphPane.YAxis.Title.FontSpec = myFont;
            FreqGraph.GraphPane.Title.FontSpec = myFont;
            FreqGraph.GraphPane.XAxis.Title.FontSpec = myFont;
            FreqGraph.GraphPane.YAxis.Title.FontSpec = myFont;
            //网格及颜色
            this.TimeGraph.GraphPane.XAxis.MajorGrid.IsVisible = true;
            this.TimeGraph.GraphPane.XAxis.MajorGrid.Color = Color.Gray;
            this.TimeGraph.GraphPane.YAxis.MajorGrid.IsVisible = true;
            this.TimeGraph.GraphPane.YAxis.MajorGrid.Color = Color.Gray;
            this.FreqGraph.GraphPane.XAxis.MajorGrid.IsVisible = true;
            this.FreqGraph.GraphPane.XAxis.MajorGrid.Color = Color.Gray;
            this.FreqGraph.GraphPane.YAxis.MajorGrid.IsVisible = true;
            this.FreqGraph.GraphPane.YAxis.MajorGrid.Color = Color.Gray;
            label7.Text = 0.ToString();
        }
        #endregion

        #region 变量定义
        //串口类的声明
        SerialPort sp = null;//声明一个串口类
        bool isOpen = false;//打开串口标志位
        bool isSetProperty = false;//属性设置标志位
        int receivenum = 0;//记录当前所接收的数据量
        int FrameLength = 6000;//每帧数据6000个
        int AxIs = 0;//用来表示x轴坐标
        //刷新队列
        PointPairList TimeGraphData = new PointPairList();
        PointPairList FreqGraphData = new PointPairList();
        //刷新图表显示数据链
        LineItem TimeGraphItem;
        LineItem FreqGraphItem;
        //接收数据暂存队列
        private Queue<double> TimeGraphDataTemp = new Queue<double>();
        private Queue<double> FreqGraphDataTemp = new Queue<double>();
        double fs = 37500.0;
        #endregion

        #region 串口数据接收部分
        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int a = sp.BytesToRead;
            byte[] recivebuffer = new byte[a];
            sp.Read(recivebuffer, 0, a);
            changerecivetext(recivebuffer);
        }
        delegate void recivetext(byte[] s);//创建一个委托
        private void changerecivetext(byte[] receive)//对控件进行的操作放在这里
        {
            if (!tbxRecvData.InvokeRequired)//判断是否需要进行唤醒的请求
            {
                if (receivenum == 0)
                {
                    tbxRecvData.Text = "";
                }
                //入队  待达到一定数量后 再处理
                for (int i = 0; i < receive.Length; i++)
                {
                    TimeGraphDataTemp.Enqueue(receive[i]);
                }
                tbxRecvData.AppendText(HexToAscii(receive));//同一线程内的操作
                receivenum += receive.Length;
                label7.Text = receivenum.ToString();
            }
            else
            {
                recivetext a1 = new recivetext(changerecivetext);//执行唤醒操作
                Invoke(a1, new object[] { receive });//不同线程内的操作
            }
        }
        #endregion

        #region 定时处理绘图
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(receivenum >= FrameLength)//若收满一帧，开辟新线程完成绘制
            {
                FreqGraphDataTemp = TimeGraphDataTemp;
                DrawTimeList();
                DrawFreqList();
                //清空暂存队列
                TimeGraphDataTemp.Clear();
                FreqGraphDataTemp.Clear();
                //清零接收
                receivenum = 0;
            }
        }
        #endregion

        #region 绘制线程
        delegate void DrawTimeListThread();
        private void DrawTimeList()
        {
            if (!TimeGraph.InvokeRequired)
            {
                //时域
                AxIs = 0;
                //清空上一次绘制数据
                TimeGraphData.Clear();
                //将当前的绘制数据装填
                for (int i = 0; i < TimeGraphDataTemp.Count; i++)
                {
                    TimeGraphData.Add(AxIs++, TimeGraphDataTemp.ElementAt(i));
                }
                //更新绘图区
                TimeGraph.GraphPane.CurveList.Clear();
                TimeGraph.GraphPane.GraphObjList.Clear();
                TimeGraphItem = TimeGraph.GraphPane.AddCurve("时域数据",
                TimeGraphData, Color.DarkGreen, SymbolType.None);
                this.TimeGraph.AxisChange();
                this.TimeGraph.Refresh();

            }
            else
            {
                DrawTimeListThread a1 = new DrawTimeListThread(DrawTimeList);
                Invoke(a1);
            }
        }
        delegate void DrawFreqListThread();
        private void DrawFreqList()
        {
            if (!FreqGraph.InvokeRequired)
            {

                //频域
                double[] Data = FreqGraphDataTemp.ToArray();
                Data = ReturnOne(Data);
                Data = RemoveMean(Data);
                Int32 Num = 4096;
                IntPtr p = fft(Data, FreqGraphDataTemp.Count);
                double[] FFTResult = new double[Num];
                Marshal.Copy(p, FFTResult, 0, Num);
                double x = 0.0;
                //清空上一次绘制数据
                FreqGraphData.Clear();
                for (int i = 0; i < Num/2; i++)
                {
                    x = Convert.ToDouble(fs / (Num - 1) * i);
                    FreqGraphData.Add(x, FFTResult[i]);
                }
                //更新绘图区
                FreqGraph.GraphPane.CurveList.Clear();
                FreqGraph.GraphPane.GraphObjList.Clear();
                FreqGraphItem = FreqGraph.GraphPane.AddCurve("频域数据",
                FreqGraphData, Color.DarkGreen, SymbolType.None);
                this.FreqGraph.AxisChange();
                this.FreqGraph.Refresh();
            }
            else
            {
                DrawFreqListThread a1 = new DrawFreqListThread(DrawFreqList);
                Invoke(a1);
            }
        }
        #endregion

        #region 计算函数
        private double[] ReturnOne(double[] data)
        {
            double max = 0.0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > max)
                    max = data[i];
            }
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i] / max;
            }
            return data;
        }
        private double[] RemoveMean(double[] data)
        {
            double sum = 0.0;
            for (int i = 0;i< data.Length;i++)
            {
                sum = sum + data[i];
            }
            double aver = sum / data.Length;
            for (int i = 0;i < data.Length;i++)
            {
                data[i] = data[i] - aver;
            }
            return data;
        }
        #endregion

        #region 与串口配置相关的控件
        private void btnCheckCOM_Click(object sender, EventArgs e)
        {
            bool comExistence = false;//有可用串口标志位
            cbxCOMPort.Items.Clear(); //清除当前串口号中的所有串口名称
            for (int i = 0; i < 20; i++)
            {
                try
                {
                    SerialPort sp = new SerialPort("COM" + (i + 1).ToString());//使用指定的端口名称初始化 SerialPort 类的新实例。
                    sp.Open();                                  //如果能正常打开 关闭则会执行以下代码  否则执行 catch  continue
                    sp.Close();
                    cbxCOMPort.Items.Add("COM" + (i + 1).ToString());
                    comExistence = true;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            if (comExistence)
            {
                cbxCOMPort.SelectedIndex = 0;//使 ListBox 显示第 1 个添加的索引
            }
            else
            {
                MessageBox.Show("没有找到可用串口！", "错误提示");
            }
        }
        private bool CheckPortSetting()//检查串口是否设置
        {
            if (cbxCOMPort.Text.Trim() == "") return false;
            if (cbxBaudRate.Text.Trim() == "") return false;
            if (cbxDataBits.Text.Trim() == "") return false;
            if (cbxParity.Text.Trim() == "") return false;
            if (cbxStopBits.Text.Trim() == "") return false;
            return true;
        }
        private void SetPortProperty()//设置串口的属性
        {
            sp = new SerialPort();
            sp.PortName = cbxCOMPort.Text.Trim();//设置串口名
            sp.BaudRate = Convert.ToInt32(cbxBaudRate.Text.Trim());//设置串口的波特率
            float f = Convert.ToSingle(cbxStopBits.Text.Trim());//设置停止位
            if (f == 0)
            {
                sp.StopBits = StopBits.None;
            }
            else if (f == 1.5)
            {
                sp.StopBits = StopBits.OnePointFive;
            }
            else if (f == 1)
            {
                sp.StopBits = StopBits.One;
            }
            else if (f == 2)
            {
                sp.StopBits = StopBits.Two;
            }
            else
            {
                sp.StopBits = StopBits.One;
            }
            sp.DataBits = Convert.ToInt16(cbxDataBits.Text.Trim());//设置数据位
            string s = cbxParity.Text.Trim(); //设置奇偶校验位
            if (s.CompareTo("无") == 0)
            {
                sp.Parity = Parity.None;
            }
            else if (s.CompareTo("奇校验") == 0)
            {
                sp.Parity = Parity.Odd;
            }
            else if (s.CompareTo("偶校验") == 0)
            {
                sp.Parity = Parity.Even;
            }
            else
            {
                sp.Parity = Parity.None;
            }
            sp.ReadTimeout = -1;//设置超时读取时间
            sp.RtsEnable = true;
            //定义 DataReceived 事件，当串口收到数据后触发事件
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
        }
        private void btnOpenCom_Click(object sender, EventArgs e)
        {
            if (isOpen == false)
            {
                if (!CheckPortSetting())//检测串口设置
                {
                    MessageBox.Show("串口未设置！", "错误提示");
                    return;
                }
                if (!isSetProperty)//串口未设置则设置串口
                {
                    SetPortProperty();
                    isSetProperty = true;
                }
                try//打开串口
                {
                    sp.Open();
                    isOpen = true;
                    btnOpenCom.Text = "关闭串口";
                    //串口打开后则相关的串口设置按钮便不可再用
                    cbxCOMPort.Enabled = false;
                    cbxBaudRate.Enabled = false;
                    cbxDataBits.Enabled = false;
                    cbxParity.Enabled = false;
                    cbxStopBits.Enabled = false;
                    this.TsbConnect.Image = imageList1.Images[1];//更新连接图标 连接成功
                }
                catch (Exception)
                {
                    //打开串口失败后，相应标志位取消
                    isSetProperty = false;
                    isOpen = false;
                    MessageBox.Show("串口无效或已被占用！", "错误提示");
                    this.TsbConnect.Image = imageList1.Images[0];//更新连接图标 连接失败
                }
            }
            else
            {
                try//打开串口
                {
                    sp.Close();
                    isOpen = false;
                    isSetProperty = false;
                    btnOpenCom.Text = "打开串口";
                    //关闭串口后，串口设置选项便可以继续使用
                    cbxCOMPort.Enabled = true;
                    cbxBaudRate.Enabled = true;
                    cbxDataBits.Enabled = true;
                    cbxParity.Enabled = true;
                    cbxStopBits.Enabled = true;
                    this.TsbConnect.Image = imageList1.Images[0];//更新连接图标 关闭
                }
                catch (Exception)
                {
                    MessageBox.Show("关闭串口时发生错误");
                    this.TsbConnect.Image = imageList1.Images[1];//更新连接图标 关闭失败 

                }
            }
        }
        #endregion

        #region 十六进制与字符转换函数
        string HexToAscii(byte[] pHex) //HEX转ASIIC
        {
            byte high_char, low_char, temp;
            string sAscii = "";
            for (int i = 0; i < pHex.Length; i++)
            {
                temp = pHex[i];
                high_char = System.Convert.ToByte((temp & 0xf0) >> 4);
                low_char = System.Convert.ToByte(temp & 0x0f);
                if ((high_char >= 0x00) && (high_char <= 0x09))
                    high_char += 0x30;
                if ((high_char >= 0x0A) && (high_char <= 0x0F))
                    high_char = System.Convert.ToByte(high_char + 'A' - 10);
                if ((low_char >= 0x00) && (low_char <= 0x09))
                    low_char += 0x30;
                if ((low_char >= 0x0A) && (low_char <= 0x0F))
                    low_char = System.Convert.ToByte(low_char + 'A' - 10);
                sAscii += System.Convert.ToChar(high_char);
                sAscii += System.Convert.ToChar(low_char);
                sAscii += " ";
            }
            return sAscii;
        }
        private byte[] AsciiToHex(string Ascii)//ASCII转换成HEX
        {
            byte[] Nibble = new byte[2];
            int nAsciiLen = Ascii.Length;
            Ascii = Ascii + '0';//预防0 在末尾符  使最后一个判断语句报错
            string sAscii = "";
            for (int i = 0; i < nAsciiLen; i++)//判断是否有非法的字符
            {
                if (System.Convert.ToByte(Ascii[i]) <= 'F' && System.Convert.ToByte(Ascii[i]) >= 'A')
                    sAscii += Ascii[i];
                else if (System.Convert.ToByte(Ascii[i]) <= 'f' && System.Convert.ToByte(Ascii[i]) >= 'a')
                    sAscii += Ascii[i];
                else if (System.Convert.ToByte(Ascii[i]) <= '9' && System.Convert.ToByte(Ascii[i]) >= '1')
                    sAscii += Ascii[i];
                else if ((System.Convert.ToByte(Ascii[i]) == '0' && System.Convert.ToByte(Ascii[i + 1]) != 'x'
                    && System.Convert.ToByte(Ascii[i + 1]) != 'X'))
                    sAscii += Ascii[i];
            }
            nAsciiLen = sAscii.Length;
            byte[] pHex = new byte[nAsciiLen / 2];
            for (int i = 0; i < nAsciiLen / 2; i++)
            {
                Nibble[0] = System.Convert.ToByte(sAscii[i * 2]);
                Nibble[1] = System.Convert.ToByte(sAscii[i * 2 + 1]);
                for (int j = 0; j < 2; j++)
                {
                    if (Nibble[j] <= 'F' && Nibble[j] >= 'A')
                        Nibble[j] = System.Convert.ToByte(Nibble[j] - 'A' + 10);

                    else if (Nibble[j] <= 'f' && Nibble[j] >= 'a')
                        Nibble[j] = System.Convert.ToByte(Nibble[j] - 'a' + 10);
                    else if (Nibble[j] >= '0' && Nibble[j] <= '9')
                        Nibble[j] = System.Convert.ToByte(Nibble[j] - '0');
                }
                pHex[i] = System.Convert.ToByte(Nibble[0] << 4); // Set the high nibble
                pHex[i] |= Nibble[1]; //Set the low nibble 
            }
            return pHex;
        }
        #endregion

        #region 连接 开始 结束 清除图表按键功能
        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbConnect_Click(object sender, EventArgs e)
        {
            if (isOpen == false)
            {
                if (!CheckPortSetting())//检测串口设置
                {
                    MessageBox.Show("串口未设置！", "错误提示");
                    return;
                }
                if (!isSetProperty)//串口未设置则设置串口
                {
                    SetPortProperty();
                    isSetProperty = true;
                }
                try//打开串口
                {
                    sp.Open();
                    isOpen = true;
                    btnOpenCom.Text = "关闭串口";
                    //串口打开后则相关的串口设置按钮便不可再用
                    cbxCOMPort.Enabled = false;
                    cbxBaudRate.Enabled = false;
                    cbxDataBits.Enabled = false;
                    cbxParity.Enabled = false;
                    cbxStopBits.Enabled = false;
                    this.TsbConnect.Image = imageList1.Images[1];//更新连接图标 连接成功
                }
                catch (Exception)
                {
                    //打开串口失败后，相应标志位取消
                    isSetProperty = false;
                    isOpen = false;
                    MessageBox.Show("串口无效或已被占用！", "错误提示");
                    this.TsbConnect.Image = imageList1.Images[0];//更新连接图标 连接失败
                }
            }
            else
            {
                try//打开串口
                {
                    sp.Close();
                    isOpen = false;
                    isSetProperty = false;
                    btnOpenCom.Text = "打开串口";
                    //关闭串口后，串口设置选项便可以继续使用
                    cbxCOMPort.Enabled = true;
                    cbxBaudRate.Enabled = true;
                    cbxDataBits.Enabled = true;
                    cbxParity.Enabled = true;
                    cbxStopBits.Enabled = true;
                    this.TsbConnect.Image = imageList1.Images[0];//更新连接图标 关闭
                }
                catch (Exception)
                {
                    MessageBox.Show("关闭串口时发生错误");
                    this.TsbConnect.Image = imageList1.Images[1];//更新连接图标 关闭失败 

                }
            }
        }
        /// <summary>
        /// 启动采样命令发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbStart_Click(object sender, EventArgs e)
        {
            if (isOpen)//写串口数据  isOpen为是否打开串口标志位
            {
                try
                {
                    string readytosend = "ff";//开始扫描  
                    sp.Write(AsciiToHex(readytosend), 0, AsciiToHex(readytosend).Length);//十六进制发送
                    TsbStop.Enabled = true;
                    TsbStart.Enabled = false;

                }
                catch (Exception)
                {
                    MessageBox.Show("发送数据时发生错误！", "错误提示");
                    TsbStop.Enabled = false;
                    TsbStart.Enabled = true;
                    return;
                }
            }
            else
            {
                MessageBox.Show("串口未打开！", "错误提示");
                return;
            }
        }
        /// <summary>
        /// 结束采样命令发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbStop_Click(object sender, EventArgs e)
        {
            if (isOpen)//写串口数据  isOpen为是否打开串口标志位
            {
                try
                {
                    string readytosend = "00";//结束扫描 
                    sp.Write(AsciiToHex(readytosend), 0, AsciiToHex(readytosend).Length);//十六进制发送
                    TsbStop.Enabled = false;
                    TsbStart.Enabled = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("发送数据时发生错误！", "错误提示");
                    TsbStop.Enabled = true;
                    TsbStart.Enabled = false;
                    return;
                }
            }
            else
            {
                MessageBox.Show("串口未打开！", "错误提示");
                return;
            }
        }
        /// <summary>
        /// 清除图表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbClear_Click(object sender, EventArgs e)
        {
            FreqGraph.GraphPane.CurveList.Clear();
            FreqGraph.GraphPane.GraphObjList.Clear();
            FreqGraphData.Clear();
            this.FreqGraph.AxisChange();
            this.FreqGraph.Refresh();
            TimeGraph.GraphPane.CurveList.Clear();
            TimeGraph.GraphPane.GraphObjList.Clear();
            TimeGraphData.Clear();
            this.TimeGraph.AxisChange();
            this.TimeGraph.Refresh();
            tbxRecvData.Text = "";
            receivenum = 0;
            label7.Text = 0.ToString();
        }
        #endregion

        #region 窗体加载与变动事件
        /// <summary>
        /// 窗口变动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            TimeGraph.Width = PanelOfTimeGraph.Width;
            FreqGraph.Width = PanelOfFreqGraph.Width;
            ReceiveGroupBox.Width = this.Width - ReceiveGroupBox.Location.X - 10;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            TimeGraph.Width = PanelOfTimeGraph.Width;
            FreqGraph.Width = PanelOfFreqGraph.Width;
            ReceiveGroupBox.Width = this.Width - ReceiveGroupBox.Location.X - 30;
            for (int i = 0; i < 20; i++)//最大支持到串口 10，可根据自己需求增加
            {
                cbxCOMPort.Items.Add("COM" + (i + 1).ToString());
            }
            cbxCOMPort.SelectedIndex = 0;
            //列出常用的波特率
            cbxBaudRate.Items.Add("1200");
            cbxBaudRate.Items.Add("2400");
            cbxBaudRate.Items.Add("4800");
            cbxBaudRate.Items.Add("9600");
            cbxBaudRate.Items.Add("19200");
            cbxBaudRate.Items.Add("38400");
            cbxBaudRate.Items.Add("43000");
            cbxBaudRate.Items.Add("56000");
            cbxBaudRate.Items.Add("57600");
            cbxBaudRate.Items.Add("115200");
            cbxBaudRate.Items.Add("600000");
            cbxBaudRate.Items.Add("921600");
            cbxBaudRate.Items.Add("2000000");
            cbxBaudRate.SelectedIndex = 10;
            //列出停止位
            cbxStopBits.Items.Add("0");
            cbxStopBits.Items.Add("1");
            cbxStopBits.Items.Add("1.5");
            cbxStopBits.Items.Add("2");
            cbxStopBits.SelectedIndex = 1;
            //列出数据位
            cbxDataBits.Items.Add("8");
            cbxDataBits.Items.Add("7");
            cbxDataBits.Items.Add("6");
            cbxDataBits.Items.Add("5");
            cbxDataBits.SelectedIndex = 0;
            //列出奇偶校验位
            cbxParity.Items.Add("无");
            cbxParity.Items.Add("奇校验");
            cbxParity.Items.Add("偶校验");
            cbxParity.SelectedIndex = 0;
        }
        #endregion

        #region 右键菜单
        private void TimeGraph_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            try
            {
                //每次循环只能遍历一个键
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "copy")
                    {
                        item.Text = "复制";
                        item.Visible = true;
                        break;
                    }
                }
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "save_as")
                    {
                        item.Text = "另存图表";
                        item.Visible = true;
                        break;
                    }
                }

                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "show_val")
                    {
                        item.Text = "显示XY值";
                        item.Visible = true;
                        break;
                    }
                }
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "unzoom")
                    {
                        item.Text = "上一视图";
                        item.Visible = true;
                        break;
                    }
                }
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "undo_all")
                    {
                        item.Text = "还原缩放/移动";
                        item.Visible = true;
                        break;
                    }
                }
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "print")
                    {
                        menuStrip.Items.Remove(item);
                        item.Visible = false; //不显示
                        break;
                    }
                }
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "page_setup")
                    {
                        menuStrip.Items.Remove(item);//移除菜单项
                        item.Visible = false; //不显示
                        break;
                    }
                }
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "set_default")
                    {
                        item.Text = "初始化";
                        item.Visible = true; //不显示
                        break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("初始化右键菜单错误" + ex.ToString());
            }
        }

        private void FreqGraph_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            try
            {
                //每次循环只能遍历一个键
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "copy")
                    {
                        item.Text = "复制";
                        item.Visible = true;
                        break;
                    }
                }
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "save_as")
                    {
                        item.Text = "另存图表";
                        item.Visible = true;
                        break;
                    }
                }

                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "show_val")
                    {
                        item.Text = "显示XY值";
                        item.Visible = true;
                        break;
                    }
                }
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "unzoom")
                    {
                        item.Text = "上一视图";
                        item.Visible = true;
                        break;
                    }
                }
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "undo_all")
                    {
                        item.Text = "还原缩放/移动";
                        item.Visible = true;
                        break;
                    }
                }
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "print")
                    {
                        menuStrip.Items.Remove(item);
                        item.Visible = false; //不显示
                        break;
                    }
                }
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "page_setup")
                    {
                        menuStrip.Items.Remove(item);//移除菜单项
                        item.Visible = false; //不显示
                        break;
                    }
                }
                foreach (ToolStripMenuItem item in menuStrip.Items)
                {
                    if ((string)item.Tag == "set_default")
                    {
                        item.Text = "初始化";
                        item.Visible = true; //不显示
                        break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("初始化右键菜单错误" + ex.ToString());
            }
        }
        #endregion

        #region 测试按键
        private void TsbTest_Click(object sender, EventArgs e)
        {
            double f = 100;
            double T = 0.1;
            int n = Convert.ToInt32(fs * T);//fs=37.5k
            double[] temp = new double[n];
            //时域绘图
            int t = 0;
            double x, y;
            //清空绘制数据
            TimeGraphData.Clear();
            for(t = 0;t<n;t++)
            {
                x = Convert.ToDouble(t * 1.0 / fs);
                y = Math.Sin(2 * Math.PI * f * x);
                if (y > 0)
                    y = 5;
                else
                    y = -5;
                TimeGraphData.Add(x, y);
                temp[t] = y;
            }
            //画图
            TimeGraphItem = TimeGraph.GraphPane.AddCurve("时域数据",
TimeGraphData, Color.DarkGreen, SymbolType.None);
            this.TimeGraph.AxisChange();
            this.TimeGraph.Refresh();

            //频域
            IntPtr p = fft(temp, temp.Length);
            double[] fftResult = new double[4096];
            Marshal.Copy(p, fftResult, 0, 4096);
            FreqGraphData.Clear();
            x = 0.0;
            for (int i= 0;i < 2048;i++)
            {
                x = Convert.ToDouble(fs / (4096 - 1) * i);
                FreqGraphData.Add(x, fftResult[i]);
            }
            //更新绘图区
            FreqGraph.GraphPane.CurveList.Clear();
            FreqGraph.GraphPane.GraphObjList.Clear();
            FreqGraphItem = FreqGraph.GraphPane.AddCurve("频域数据",
            FreqGraphData, Color.DarkGreen, SymbolType.None);
            this.FreqGraph.AxisChange();
            this.FreqGraph.Refresh();

        }
        #endregion
    }
}
