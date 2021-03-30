using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WebBrowserP
{
    public partial class Form1 : Form
    {
        // 主页
        String url = "";

        // 自动关机时间
        String shutDownTime = "";

        String password = "";

        /**
         * 系统版本号：用于判断当前系统的版本
         */

        public static Form fm = null;
        public Form1()
        {
            read();
        }

        //屏蔽窗口显示
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | CS_NOCLOSE;
                return cp;
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // 将所有的链接的目标，指向本窗体
            foreach (HtmlElement archor in this.webBrowser1.Document.Links)
            {
                archor.SetAttribute("target", "_self");
            }

            // 将所有的 FORM 的提交目标，指向本窗体
            foreach (HtmlElement form in this.webBrowser1.Document.Forms)
            {
                form.SetAttribute("target", "_self");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Document.ExecCommand("Refresh", false, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new System.Uri(url, System.UriKind.Absolute);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.webBrowser1.GoBack();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.webBrowser1.GoForward();
        }

        private void read()
        {
            String strReadFilePath = Application.StartupPath + "\\config.txt";
            if (!File.Exists(strReadFilePath))
            {
                FileInfo fi1 = new FileInfo(strReadFilePath);
                fi1.Create();
                MessageBox.Show("已生成：" + strReadFilePath + " 请前往配置文件内容！");
                System.Environment.Exit(0);
                return;
            }
            StreamReader srReadFile = new StreamReader(strReadFilePath);

            if (srReadFile.EndOfStream)
            {
                MessageBox.Show("配置信息有误，请前往 " + strReadFilePath + " 检查配置文件");
                System.Environment.Exit(0);
                return;
            }
            else
            {
                while (!srReadFile.EndOfStream)
                {
                    string strReadLine = srReadFile.ReadLine(); //读取每行数据
                    String[] sArray = strReadLine.Split('=');
                    String finalStr = "";
                    for (int i = 0; i < sArray.Length; i++)
                    {
                        if (i != 0)
                        {
                            finalStr += sArray[i];
                            // 如果不是最后一个，就追加=
                            if (i != sArray.Length - 1)
                            {
                                finalStr += "=";
                            }
                        }
                    }

                    if ("time".Equals(sArray[0]))
                    {
                        shutDownTime = finalStr;
                    }
                    else if ("url".Equals(sArray[0]))
                    {
                        url = finalStr;
                    }
                    else
                    {
                        password = finalStr;
                    }
                }

                if (null != password && !"".Equals(password) && null != shutDownTime && !"".Equals(shutDownTime) && null != url && !"".Equals(url))
                {
                    // 关闭读取流文件
                    srReadFile.Close();
                    InitializeComponent();
                }
                else
                {
                    MessageBox.Show("配置信息有误，请前往 " + strReadFilePath + " 检查配置文件");
                    System.Environment.Exit(0);
                    // 关闭整个程序
                    return;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";//进程打开的文件为Cmd
            p.StartInfo.UseShellExecute = false;//是否启动系统外壳选否
            p.StartInfo.RedirectStandardInput = true;//这是是否从StandardInput输入
            p.StartInfo.CreateNoWindow = true;//这里是启动程序是否显示窗体
            p.Start();//启动
            p.StandardInput.WriteLine("KeyBoard.exe");//运行关机命令shutdown (-s)是关机 (-t)是延迟的时间 这里用秒计算 10就是10秒后关机
            p.StandardInput.WriteLine("exit");//退出cmd*/
        }

        //显示屏幕键盘
        public static int ShowInputPanel()
        {
            try
            {
                dynamic file = "C:\\Program Files\\Common Files\\microsoft shared\\ink\\TabTip.exe";
                if (!System.IO.File.Exists(file))
                    return -1;
                Process.Start(file);
                // return SetUnDock(); //不知SetUnDock()是什么，所以直接注释返回1
                return 1;
            }
            catch (Exception)
            {
                return 255;
            }
        }

        int timeCount = 0;
        private void timer_MainForm_Tick(object sender, EventArgs e)
        {
            // 定时器需要生成的方法现在这里
            // Console.WriteLine("shutDownTime: " + shutDownTime);
            String time = DateTime.Now.ToShortTimeString().ToString();   // 20:16
            Console.WriteLine(time);
            String thisTime = shutDownTime;
            // Console.WriteLine(thisTime);
            int res = thisTime.CompareTo(time);
            if (0 == res && 0 == timeCount)
            {
                timeCount++;
                RunBat("shutdownComputer.bat");
                // 执行关机的命令
                // Process p = new Process();
                /*p.StartInfo.FileName = "cmd.exe";//进程打开的文件为Cmd
                p.StartInfo.UseShellExecute = false;//是否启动系统外壳选否
                p.StartInfo.RedirectStandardInput = true;//这是是否从StandardInput输入
                p.StartInfo.CreateNoWindow = true;//这里是启动程序是否显示窗体
                p.Start();//启动
                p.StandardInput.WriteLine("shutdown -s -t 10");//运行关机命令shutdown (-s)是关机 (-t)是延迟的时间 这里用秒计算 10就是10秒后关机
                p.StandardInput.WriteLine("exit");//退出cmd*/
                /* p.StartInfo.FileName = "shutdown.exe";
                p.StartInfo.Arguments = "-s -t 0"; // 马上关机
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;*/
                // 停止定时器
                this.timer_MainForm.Enabled = false;
                this.timer_MainForm.Stop();
            }
        }

        private void RunBat(string batPath)
        {
            Process pro = new Process();

            FileInfo file = new FileInfo(batPath);
            pro.StartInfo.WorkingDirectory = file.Directory.FullName;
            pro.StartInfo.FileName = batPath;
            pro.StartInfo.CreateNoWindow = false;
            pro.Start();
            pro.WaitForExit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("KeyBoard.exe");
            Form2 form = new Form2();
            form.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.webBrowser1.GoBack();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.webBrowser1.GoForward();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Document.ExecCommand("Refresh", false, null);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Url = new System.Uri(url, System.UriKind.Absolute);
        }

        private KeyBoard kb = null;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("KeyBoard.exe");
        }

        int showAndHide = 0;
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (1 == showAndHide)
            {
                showAndHide = 0;
                pictureBox7.Image = global::WebBrowserP.Properties.Resources.hide;
                pictureBox2.Hide();
                pictureBox3.Hide();
                pictureBox4.Hide();
                pictureBox5.Hide();
                pictureBox6.Hide();
            }
            else 
            {
                showAndHide = 1;
                pictureBox7.Image = global::WebBrowserP.Properties.Resources.show;
                pictureBox2.Show();
                pictureBox3.Show();
                pictureBox4.Show();
                pictureBox5.Show();
                pictureBox6.Show();
            }
        }

    }
}
