using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Reflection;
using System.Diagnostics;

namespace osu_counter
{
    public partial class Form1 : Form
    {
        public Keys keyo = Keys.Z;
        public Keys keyt = Keys.X;
        public Form1()
        {
            InitializeComponent();
        }
        KeyboardHook kh;
        private void Form1_Load(object sender, EventArgs e)
        {
            label5.Text = ConfigurationManager.AppSettings["key1count"];
            label6.Text = ConfigurationManager.AppSettings["key2count"];
            
            textBox1.Text = ConfigurationManager.AppSettings["key1"];
            textBox2.Text = ConfigurationManager.AppSettings["key2"];
            keyo = (Keys)Enum.Parse(typeof(Keys), textBox1.Text);
            keyt = (Keys)Enum.Parse(typeof(Keys), textBox2.Text);
        }
        void zhuce()
        {
            kh = new KeyboardHook();
            kh.SetHook();
            kh.OnKeyDownEvent += ondown;
        }
        void unzhuce()
        {
            kh.UnHook();
        }
        void ondown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (keyo))
            {
                int z = int.Parse(label5.Text);
                z += 1;
                label5.Text = z.ToString();
            }
            if (e.KeyData == (keyt))
            {
                int x = int.Parse(label6.Text);
                x += 1;
                label6.Text = x.ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult mo = MessageBox.Show(null, "若执行此操作，您的所有记录将被清空，届时将无法恢复\n不过您仍可以通过手动设置来修改回来", "您确定吗？", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(mo == DialogResult.Yes)
            {
                //清空记录数据
                label5.Text = "0";
                label6.Text = "0";
                MessageBox.Show(null, "已清空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //不变
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings["key1count"].Value = label5.Text;
            cfa.AppSettings.Settings["key2count"].Value = label6.Text;
            cfa.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings["key1count"].Value = label5.Text;
            cfa.AppSettings.Settings["key2count"].Value = label6.Text;
            cfa.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings["key1"].Value = textBox1.Text;
            cfa.AppSettings.Settings["key2"].Value = textBox2.Text;
            cfa.Save();
            ConfigurationManager.RefreshSection("appSettings");
            ConfigurationManager.RefreshSection("appSettings");
            textBox1.Text = ConfigurationManager.AppSettings["key1"];
            textBox2.Text = ConfigurationManager.AppSettings["key2"];
            keyo = (Keys)Enum.Parse(typeof(Keys), textBox1.Text);
            keyt = (Keys)Enum.Parse(typeof(Keys), textBox2.Text);
            MessageBox.Show(null, "已成功修改！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox1.Enabled = false;
            textBox2.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process[] app = Process.GetProcessesByName("osu!");
            if (app.Length > 0)
                {
                    if (label2.Text == "未运行")
                    {
                        label2.Text = "正在运行";
                        label2.ForeColor = Color.Green;
                        zhuce();
                    }
                }
                else
                {
                    if(label2.Text == "正在运行")
                    {
                        label2.Text = "未运行";
                        label2.ForeColor = Color.Red;
                    unzhuce();
                    }
                }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult ppp = MessageBox.Show(null, "你真是个会发现的孩子呢！\n感谢您使用osu!counter\n本程序是由ddhello用C#开发的按键记录程序！\n您如果对我们的源码感兴趣的话可以点击取消进入我们的Github查看哦！（虽然没有人）\n如果你想联系我的话可以发送email到sgfick@163.com\n此上。2018.5.12","隐藏的提醒",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            if(ppp == DialogResult.OK)
            {
            }
            else
            {

            }
        }
    }
}
