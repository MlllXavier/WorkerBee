using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerBee
{
    public struct Link//链接结构体
    {
        public string type;//保存下载链接属于迅雷、磁力等类型
        public string linkname;//链接的名称如复仇者联盟3-1080p
        public string link;//具体下载链接
    }
    
    public struct MovieIfo//电影结构体
    {
        public string Name;//名称
        public string Director;//导演
        public string Actors;//主演
        public string Tag;//标签
        public string Language;//语言
        public string Subtitle;//字幕
        public string Date;//日期
        public string Time;//片长
        public string Introduction;//简介
        public string Source;//来源
        public string OnlinePlayLink;//在线播放链接
        public List<Link> Links;//下载链接链表
        public string IconLink;//封面
        public string NowLink;//抓取链接的所属网页
    }

    public class Movie
    {
        public string movieName;

        public List<MovieIfo> movies = new List<MovieIfo>();//电影信息结构体链表，记得初始化
        public virtual void PostWebContent() { } //电影搜索函数
    }
}
