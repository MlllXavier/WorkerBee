using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WorkerBee
{
    class OutPut_Novels
    {
        public static Button button = new Button();//用来移动button

        List<NovelIfo> L_Novels;
        List<GroupBox> groupBoxes = new List<GroupBox>();
        Panel panel;
        int number = 6;//每次显示的数量
        int now_number = 0;//当前已显示的数量

        public OutPut_Novels(List<NovelIfo> novels, Panel p)
        {
            L_Novels = novels;
            panel = p;

            button.Text = "继续加载";
            button.Click += new EventHandler(button_Click);
            panel.Controls.Add(button);
        }

        public void Deal_Key()//加载关键信息
        {
            for (int i = now_number; i < number + now_number && i < L_Novels.Count; i++)
            {
                //将小说简要信息添加到GroupBox，并添加到List<GroupBox>链表
                GroupBox groupBox = new GroupBox();
                {
                    groupBox.Size = new Size(750, 150);
                    groupBox.Text = "小说名称：" + L_Novels[i].Name;
                }
                groupBoxes.Add(groupBox);

                //Label
                string[] str1 = { "作者：", "标签：", "TXT文件大小："};
                string[] str2 = { L_Novels[i].Writer, L_Novels[i].Tag, L_Novels[i].Size};
                for (int j = 0; j < 3; j++)
                {
                    Label label = new Label();
                    {
                        label.Location = new Point(30 + 200 * j, 20);//设置位置
                        label.AutoSize = true;
                        label.Text = str1[j] + str2[j];
                    }
                    groupBox.Controls.Add(label);//在当前窗体上添加这个label控件
                }
                Label lable_Introduction = new Label();
                {
                    lable_Introduction.Text = "简介：" + L_Novels[i].Introduction;
                    lable_Introduction.Size = new Size(500, 270);
                    lable_Introduction.Location = new Point(30, 45);
                    groupBox.Controls.Add(lable_Introduction);
                }
                Button button_online = new Button();
                {
                    button_online.Text = "在线阅读";
                    button_online.Location = new Point(650, 50);
                    button_online.Click += (a, e) => button_online_Click(i);//添加点击事件
                    groupBox.Controls.Add(button_online);
                }
                Button button_Download = new Button();
                {
                    button_Download.Text = "TXT下载";
                    button_Download.Location = new Point(650, 90);
                    button_Download.Click += (a, e) => button_Download_Click(i);//添加点击事件
                    groupBox.Controls.Add(button_Download);
                }
            }
        }
        //在线阅读
        private void button_online_Click(int i)
        {
            try
            {
                if (L_Novels[i].OnlineReadLink == null) MessageBox.Show("该资源已失效");
                else System.Diagnostics.Process.Start(L_Novels[i].OnlineReadLink);
            }
            catch (Exception ex)
            {
                MessageBox.Show("资源已失效");
            }    
        }
        //TXT下载
        public void button_Download_Click(int i)
        {
            try
            {
                if (L_Novels[i].Links[0].link == null) MessageBox.Show("该资源已失效");
                System.Diagnostics.Process.Start(L_Novels[i].Links[0].link);
            }
            catch(Exception ex)
            {
                MessageBox.Show("资源已失效");
            }
            
        }

        public void Show_Key()//显示关键信息
        {
            int i; 
            //加载小说信息到panel
            for (i = 0; i < number && now_number + i < groupBoxes.Count; i++)
            {
                groupBoxes[now_number + i].Location = new Point(3, 15 + 160 * (now_number + i));
                panel.Controls.Add(groupBoxes[now_number + i]);
            }
            now_number += i;
            //继续加载
            button.Location = new Point(3, 3 + 160 * (now_number));
            if (now_number >= L_Novels.Count)
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
