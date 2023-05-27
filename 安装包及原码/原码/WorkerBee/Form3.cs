using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.IO;

namespace WorkerBee
{
    public partial class Form3 : Form
    {
        public int Choice = 0;//1.电影    2.音乐    3.小说    4.应用

        System.Timers.Timer timer = new System.Timers.Timer(1);//动画定时器
        System.Timers.Timer t = new System.Timers.Timer(100);//查询定时器
        bool IsFinishGet = true;//标识是否完成查询
        public static List<MovieIfo> movies = new List<MovieIfo>();
        public static List<NovelIfo> novels = new List<NovelIfo>();
        private string input;

        string path = @"C:\WorkerBee\cmd.ini";//历史记录保存位置

        public Form3(int choice)
        {
            InitializeComponent();

            Choice = choice;

            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Tick);//动画定时器事件
            t.Elapsed += new System.Timers.ElapsedEventHandler(CheckGetStatus); //查询定时器事件
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            switch (Choice)
            {
                case 1:
                    this.Text += "电影";
                    break;
                case 2:
                    this.Text += "音乐";
                    break;
                case 3:
                    this.Text += "小说";
                    break;
                case 4:
                    this.Text += "应用";
                    break;
                default:
                    MessageBox.Show("选择错误！！！");
                    break;
            }
            UTF8Encoding uTF = new UTF8Encoding(false);
            if (File.Exists(path))
            {
                int Count = 0;
                StreamReader sr = new StreamReader(path, uTF);
                while (sr.Peek() > -1 && Count < 8)
                {
                    comboBox1.Items.Add(sr.ReadLine());
                    Count++;
                }
                sr.Close();
                comboBox1.Text = null;
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
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
        
        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (IsFinishGet == false) return;//判断是否正在查询
            //初始化
            panel2.Controls.Clear();//清空当前的panel
            movies.Clear();
            timer.Start();//开始动画定时器
            input = comboBox1.Text;
            //保存记录
            comboBox1.Items.Insert(0, comboBox1.Text);
            StreamWriter sw = new StreamWriter(path);
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                sw.WriteLine(comboBox1.Items[i]);
            }
            sw.Close();
            //开始搜索
            IsFinishGet = false;
            switch (Choice)
            {
                case 1:
                    Task.Run(() => PostDygangMovie());//开始线程
                    //Task.Run(() => PostTLMovie());
                    t.Start();
                    break;
                case 3:
                    Task.Run(() => PostUUNovel());//开始线程
                    t.Start();
                    break;
                case 4:
                    break;
                default:
                    MessageBox.Show("错误！！！");
                    break;
            }
        }

        private void CheckGetStatus(object sender, ElapsedEventArgs e)
        {
            if (IsFinishGet)
            {
                t.Close();
                timer.Close();
                OperatePanel();
            }
        }

        private void PostDygangMovie()
        {
            Movie movie = new Dygang_movie();
            movie.movieName = input;
            movie.PostWebContent();
            movies.AddRange(movie.movies);
            IsFinishGet = true;
        }

        private void PostTLMovie()
        {
            Movie movie = new Tianlang_Movie();
            movie.movieName = input;
            movie.PostWebContent();
            movies.AddRange(movie.movies);
            IsFinishGet = true;
        }

        private void PostUUNovel()
        {
            Novel novel = new UU_Novel();
            novel.novelName = input;
            novel.PostWebContent();
            novels = novel.novels;
            IsFinishGet = true;
        }

        private delegate void FlashDelegate();
        private void Flash()
        {
            if(pictureBox2.InvokeRequired)
            {
                if(!pictureBox2.IsDisposed)
                {
                    pictureBox2.Invoke(new FlashDelegate(Flash));
                }
            }
            else
            {
                pictureBox2.Left += offset_x;
                if (pictureBox2.Left > 225) offset_x = -2;
                if (pictureBox2.Left < 3) offset_x = 2;
                offset_y += Math.PI / 30;
                pictureBox2.Top = 20 - (int)Math.Round(20 * Math.Sin(offset_y));
            }
        }

        private delegate void OperatePanelDelegate();
        private void OperatePanel()
        {
            if (panel2.InvokeRequired)
            {
                if (!panel2.IsDisposed)
                {
                    panel2.Invoke(new OperatePanelDelegate(OperatePanel));
                }
            }
            else
            {
                switch (Choice)
                {
                    case 1:
                        OutPut_Movies op_movies = new OutPut_Movies(movies, panel2);
                        op_movies.Deal_Key();
                        op_movies.Show_Key();
                        break;
                    case 3:
                        OutPut_Novels op_novels = new OutPut_Novels(novels, panel2);
                        op_novels.Deal_Key();
                        op_novels.Show_Key();
                        break;
                    case 4:
                        break;
                    default:
                        MessageBox.Show("错误！！！");
                        break;
                }
            }
        }

        int offset_x = 2;//横向移动速度
        double offset_y = 0;//控制纵向速度
        private void timer_Tick(object sender, EventArgs e)
        {
            Flash();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1.form1.Show();
        }

        private void Form3_Activated(object sender, EventArgs e)
        {
            button1.Focus();
        }
    }
}
