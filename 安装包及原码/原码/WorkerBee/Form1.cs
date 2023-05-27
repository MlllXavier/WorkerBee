using System;
using System.Windows.Forms;

namespace WorkerBee
{
    public partial class Form1 : Form
    {
        public static Form1 form1;//用来返回
        public Form1()
        {
            form1 = this;
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("不再看看了吗？", "提示", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(button1);
            form2.Show();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(1);
            form3.Show();
            this.Hide();
            MessageBox.Show("搜索的时候有一点点慢，请耐心的等两秒哦^o^");
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            MessageBox.Show("请忽略广告。。。");
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(3);
            form3.Show();
            this.Hide();
            MessageBox.Show("搜索的时候有一点点慢，请耐心的等两秒哦^o^");
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("抱歉，，，此模块尚未开发·_·");
        }
    }
}
