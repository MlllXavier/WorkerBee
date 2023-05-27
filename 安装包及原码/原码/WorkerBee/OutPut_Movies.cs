using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WorkerBee
{
    class OutPut_Movies
    {
        public static Button button = new Button();//用来移动button

        List<MovieIfo> L_Movies;
        List<GroupBox> groupBoxes = new List<GroupBox>();
        Panel panel;
        int number = 6;//每次显示的数量
        int now_number = 0;//当前已显示的数量

        public OutPut_Movies(List<MovieIfo> movies, Panel p)
        {
            L_Movies = movies;
            panel = p;

            button.Text = "继续加载";
            button.Click += new EventHandler(button_Click);
            panel.Controls.Add(button);
        }
        
        public void Deal_Key()//加载电影关键信息
        {
            for (int i = now_number; i < number + now_number && i < L_Movies.Count; i++)
            {
                //将电影简要信息添加到GroupBox，并添加到List<GroupBox>链表
                GroupBox groupBox = new GroupBox();
                {
                    groupBox.Size = new Size(750, 150);
                    groupBox.Text = "电影名称：" + L_Movies[i].Name;
                    groupBox.Tag = i;//将i用Tag传过去
                    groupBox.Click += new EventHandler(groupBox_Click);//点击事件
                }
                groupBoxes.Add(groupBox);

                //Label
                string[] str1 = { "导演：", "主演：", "标签：", "日期：", "片长：" };
                string[] str2 = { L_Movies[i].Director, L_Movies[i].Actors, L_Movies[i].Tag, L_Movies[i].Date, L_Movies[i].Time };
                for (int j = 0; j < 5; j++)
                {
                    Label label = new Label();
                    {
                        label.Location = new Point(30, 26 + 20 * j);//设置位置
                        label.AutoSize = true;
                        label.Text = str1[j] + str2[j];
                    }
                    groupBox.Controls.Add(label);//在当前窗体上添加这个label控件
                }
            }
        }

        private void groupBox_Click(object o, EventArgs e)//点击
        {
            GroupBox groupBox = (GroupBox)o;
            Form4 form4 = new Form4(L_Movies[Convert.ToInt32(groupBox.Tag)]);//groupBox.Tag就是i，就是此时的第i个电影
            form4.Show();
        }

        public void Show_Key()//显示电影关键信息
        {
            int i;
            //加载电影信息到panel
            for (i = 0; i < number && now_number + i < groupBoxes.Count; i++)
            {
                groupBoxes[now_number + i].Location = new Point(3, 3 + 160 * (now_number + i));
                panel.Controls.Add(groupBoxes[now_number + i]);
            }
            now_number += i;
            //继续加载按钮
            button.Location = new Point(3, 3 + 160 * (now_number));
            if(now_number >= L_Movies.Count)
            {
                button.Hide();
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Deal_Key();
            Show_Key();
        }
    }
}