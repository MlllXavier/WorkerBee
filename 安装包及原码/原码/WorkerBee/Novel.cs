using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerBee
{
    //小说结构体
    public struct NovelIfo
    {
        public string Name;//书名
        public string Writer;//作者
        public string Tag;//标签
        public string Size;//大小
        public string Introduction;//简介
        public string OnlineReadLink;//在线阅读链接
        public List<Link> Links;//下载链接链表
        public string IconLink;//封面
    }

    public class Novel
    {
        public string novelName;//要搜索的名称

        public List<NovelIfo> novels = new List<NovelIfo>();//存放搜索得到的多个小说

        public virtual void PostWebContent() { }
    }
}
