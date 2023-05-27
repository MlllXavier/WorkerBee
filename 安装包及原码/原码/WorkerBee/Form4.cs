using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkerBee
{
    public partial class Form4 : Form
    {
        MovieIfo ifo;
        private string http = null;
        private bool FinishGetPictureStream = false;
        private System.Timers.Timer t;
        private Stream stream = null;

        public Form4(MovieIfo movie)
        {
            InitializeComponent();
            ifo = movie;

            Label label = new Label();
            label.Location = new Point(300, 12);
            label.Size = new Size(500, 25);
            label.Text = "电影名：" + movie.Name;
            float size = label.Font.Size;
            label.Font = new Font(label.Font.FontFamily, size + 4);
            label.ForeColor = Color.Red;
            if (label.Text.Length > 25)
            {
                string text = label.Text;
                label.Text = text.Substring(0, 25);
                label.Text = label.Text + "......";
            }
            this.Controls.Add(label);
            label.Show();

            Label labe2 = new Label();
            labe2.Location = new Point(300, 50);
            labe2.Size = new Size(500, 25);
            labe2.Text = "导演：" + movie.Director;
            if (labe2.Text.Length > 40)
            {
                string text = labe2.Text;
                labe2.Text = text.Substring(0, 40);
                labe2.Text = labe2.Text + "......";
            }
            this.Controls.Add(labe2);
            labe2.Show();


            Label labe3 = new Label();
            labe3.Location = new Point(300, 80);
            labe3.Size = new Size(500, 25);
            labe3.Text = "主演：" + movie.Actors;
            if (labe3.Text.Length > 40)
            {
                string text = labe3.Text;
                labe3.Text = text.Substring(0, 40);
                labe3.Text = labe3.Text + "......";
            }
            this.Controls.Add(labe3);
            labe3.Show();

            Label labe4 = new Label();
            labe4.Location = new Point(300, 110);
            labe4.Size = new Size(500, 25);
            labe4.Text = "标签：" + movie.Tag;
            if (labe4.Text.Length > 40)
            {
                string text = labe4.Text;
                labe4.Text = text.Substring(0, 40);
                labe4.Text = labe4.Text + "......";
            }
            this.Controls.Add(labe4);
            labe4.Show();

            Label labe5 = new Label();
            labe5.Location = new Point(300, 140);
            labe5.Size = new Size(500, 25);
            labe5.Text = "语言：" + movie.Language;
            if (labe5.Text.Length > 40)
            {
                string text = labe5.Text;
                labe5.Text = text.Substring(0, 40);
                labe5.Text = labe5.Text + "......";
            }
            this.Controls.Add(labe5);
            labe5.Show();

            Label labe6 = new Label();
            labe6.Location = new Point(300, 170);
            labe6.Size = new Size(500, 25);
            labe6.Text = "字幕：" + movie.Subtitle;
            if (labe6.Text.Length > 40)
            {
                string text = labe6.Text;
                labe6.Text = text.Substring(0, 40);
                labe6.Text = labe6.Text + "......";
            }
            this.Controls.Add(labe6);
            labe6.Show();

            Label labe7 = new Label();
            labe7.Location = new Point(300, 200);
            labe7.Size = new Size(500, 25);
            labe7.Text = "日期：" + movie.Date;
            if (labe7.Text.Length > 40)
            {
                string text = labe7.Text;
                labe7.Text = text.Substring(0, 40);
                labe7.Text = labe7.Text + "......";
            }
            this.Controls.Add(labe7);
            labe7.Show();

            Label labe8 = new Label();
            labe8.Location = new Point(300, 230);
            labe8.Size = new Size(500, 25);
            labe8.Text = "片长：" + movie.Time;
            if (labe8.Text.Length > 40)
            {
                string text = labe8.Text;
                labe8.Text = text.Substring(0, 40);
                labe8.Text = labe8.Text + "......";
            }
            this.Controls.Add(labe8);
            labe8.Show();

            Label labe9 = new Label();
            labe9.Location = new Point(300, 260);
            labe9.Size = new Size(500, 25);
            labe9.Text = "来源：" + movie.Source;
            if (labe9.Text.Length > 40)
            {
                string text = labe9.Text;
                labe9.Text = text.Substring(0, 40);
                labe9.Text = labe9.Text + "......";
            }
            this.Controls.Add(labe9);
            labe9.Show();


            Label labe10 = new Label();
            labe10.Location = new Point(5, 400);
            labe10.Text = "简介：" + movie.Introduction;
            labe10.AutoSize = true;
            this.Controls.Add(labe10);
            labe10.Show();


            Label labe11 = new Label();
            labe11.Location = new Point(200, 505);
            labe11.Size = new Size(500, 25);
            labe11.Text = "下载链接数量：" + movie.Links.Count.ToString();
            this.Controls.Add(labe11);
            labe11.Show();

            PictureBox picture = new PictureBox();
            picture.Location = new Point(5, 10);
            picture.Size = new Size(150, 245);
            this.Controls.Add(picture);
            picture.Show();


            Button button1 = new Button();
            button1.Location = new Point(25, 535);
            button1.Size = new Size(150, 25);
            button1.Text = "在线播放" + movie.OnlinePlayLink;
            button1.Click += Button1_Click;
            this.Controls.Add(button1);
            button1.Show();

            Button button2 = new Button();
            button2.Location = new Point(200, 535);
            button2.Size = new Size(150, 25);
            button2.Text = "下载";
            button2.Click += Button2_Click;
            this.Controls.Add(button2);
            button1.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (ifo.OnlinePlayLink == null) MessageBox.Show("该资源不可在线播放");
            else System.Diagnostics.Process.Start(ifo.OnlinePlayLink);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (ifo.Links.Count == 0) MessageBox.Show("资源已失效");
            else
            {
                Clipboard.SetText(ifo.Links[0].linkname);
                MessageBox.Show("已复制下载链接到剪贴板");
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            http = ifo.IconLink;
            FinishGetPictureStream = false;
            Task.Run(() => GetPictureStream());
            Thread t = new Thread(CheckPictureStream);
            t.Start();
        }

        private void GetPictureStream()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(http);
            req.Method = "GET";
            //"application/x-www-form-urlencoded"
            req.ContentType = "ext/html,application/xhtml+xml,application/xml;" +
                "q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3"; //由链接获取网页内容
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 " +
                "(KHTML, like Gecko) Chrome/76.0.3789.0 Safari/537.36 Edg/76.0.159.0";//识别用户浏览器环境
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            stream = res.GetResponseStream();
            FinishGetPictureStream = true;
        }

        private void CheckPictureStream()
        {
            t = new System.Timers.Timer(100);
            t.AutoReset = true;
            t.Elapsed += T_Elapsed;
            t.Start();
        }

        private void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (FinishGetPictureStream)
            {
                t.Close();
                pictureBox1.Image = Image.FromStream(stream);
            }
        }
    }
}
