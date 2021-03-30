using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace WebBrowserP
{
    public partial class Form2 : Form
    {
        String password = "";
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 关闭当前窗口
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // 读取配置文件中的密码是多少
            this.label2.Hide();
            read();
        }

        // 读取配置文件
        private void read()
        {
            String strReadFilePath = Application.StartupPath + "\\config.txt";
            if (!File.Exists(strReadFilePath))
            {
                FileInfo fi1 = new FileInfo(strReadFilePath);
                fi1.Create();
                MessageBox.Show("已生成：" + strReadFilePath + " 请前往配置文件内容！");
            }
            StreamReader srReadFile = new StreamReader(strReadFilePath);

            if (srReadFile.EndOfStream)
            {
                MessageBox.Show("配置信息有误，请前往 " + strReadFilePath + " 检查配置文件");
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
                        // shutDownTime = finalStr;
                    }
                    else if ("url".Equals(sArray[0]))
                    {
                        // url = finalStr;
                    }
                    else 
                    {
                        password = finalStr;
                    }
                    Console.WriteLine(finalStr); //屏幕打印每行数据
                }

                if (null != password && !"".Equals(password))
                {
                    // 关闭读取流文件
                    srReadFile.Close();
                    return;
                }
                else
                {
                    MessageBox.Show("配置信息有误，请前往 " + strReadFilePath + " 检查配置文件");
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String text = this.textBox1.Text;
            if (null != text && !"".Equals(text) && null != password && !"".Equals(password)) 
            {
                if (text.Equals(password))
                {
                    this.label2.Show();
                    // 关闭程序，执行退出的操作.
                    RunBat("kill.bat");
                    /*Process p = new Process();
                    p.StartInfo.FileName = "cmd.exe";//进程打开的文件为Cmd
                    p.StartInfo.UseShellExecute = false;//是否启动系统外壳选否
                    p.StartInfo.RedirectStandardInput = true;//这是是否从StandardInput输入
                    p.StartInfo.CreateNoWindow = true;//这里是启动程序是否显示窗体
                    p.Start();//启动
                    p.StandardInput.WriteLine("taskkill /f /t /im KeyBoard.exe");//运行关机命令shutdown (-s)是关机 (-t)是延迟的时间 这里用秒计算 10就是10秒后关机
                    p.StandardInput.WriteLine("exit");//退出cmd
                    p.WaitForExit();
                    p.Close();
                    // System.Diagnostics.Process.Start("taskkill /f /t /im KeyBoard.exe");
                    Thread.Sleep(3000);*/
                    System.Environment.Exit(0);
                    // this.Close();
                }
                else
                {
                    MessageBox.Show("密码错误，请重新输入");
                }
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

    }
}
