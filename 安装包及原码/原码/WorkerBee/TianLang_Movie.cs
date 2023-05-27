using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;

namespace WorkerBee
{
    public class Tianlang_Movie : Movie
    {
        public override void PostWebContent()//搜索电影
        {
            //用GB2312编码方式访问程序代码（有中文网站不能识别，需要把中文编码）
            byte[] data = Encoding.GetEncoding("GB2312").GetBytes("searchword=" + movieName);
            //访问电影港网址，以POST提交数据，并接受返回的页面内容
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://m.tlyy.tv/search.asp");
            req.Method = "POST";
            //req.AllowAutoRedirect = true;
            req.ContentType = "application/x-www-form-urlencoded";//用这种编码方式将返回的页面内容转换成一个个字符串
            req.ContentLength = data.Length;
            Stream s = req.GetRequestStream();
            s.Write(data, 0, data.Length);//写入文件操作
            s.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();//返回GetResponse对象
            //把响应的数据流绑定到StreamReader对象
            StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("GB2312"));
            string output = reader.ReadToEnd();
            //Console.WriteLine(output);
            MatchToMovie(output);//调用匹配电影函数

        }
        public void MatchToMovie(string WebContent)
        {
            Regex regex = new Regex("<p><a href=\"(.*)\">(.*)</a></p>");
            MatchCollection Matches = regex.Matches(WebContent);//将正则表达式应用于找到匹配的集合，在WebContent中搜索匹配项
            foreach (Match m in Matches)
            {
                //Console.WriteLine("http://m.tlyy.tv" + m.Groups[1].ToString());
                string webcontent = GetWebContent("http://m.tlyy.tv" + m.Groups[1].ToString());//匹配到一个电影链接，调用匹配电影信息函数，eg：匹配到复仇者联盟3和电影链接
                if (webcontent != null)
                    MatchToIfo(webcontent);

            }
        }
        public string GetWebContent(string url)
        {
            //用GET方式提交搜索到的链接
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.ContentType = "image/gif"; //由链接获取网页内容
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36";
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();//发出请求并获取对象
            StreamReader reader = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("GB2312"));
            string output = reader.ReadToEnd();//将读取的字符存储
            return output;
        }
        public void MatchToIfo(string Webcontent)
        {
            Regex regexTitle = new Regex("<h1>(.*)</h1>");
            Match match = regexTitle.Match(Webcontent);
            MovieIfo ifo;//电影信息结构体
            ifo.IconLink = ifo.Introduction = ifo.NowLink = ifo.Name = ifo.OnlinePlayLink =
               ifo.Actors = ifo.Director = ifo.Tag = ifo.Language = ifo.Date = ifo.Time =
               ifo.Introduction = ifo.Source = ifo.Subtitle = null;//初始化
            ifo.Links = new List<Link>();//初始化
            //if表达式匹配成功将其信息赋值给结构体
            if (match.Success)
            {
                ifo.NowLink = Webcontent;
                ifo.Name = match.Groups[1].ToString();
            }
            else return;
            Regex regexIcon = new Regex("<img src=\"(.*)\" alt=\"(.*)\" class=\"pic 1\">");
            match = regexIcon.Match(Webcontent);
            if (match.Success)
            {
                ifo.IconLink = match.Groups[1].ToString();
            }
            //匹配简介
            Regex regexYear = new Regex("<p>(.*)</p><p style=\"display:none\">(.*)</p>");
            match = regexYear.Match(Webcontent);
            if (match.Success)
            {
                ifo.Time = match.Groups[2].ToString();
            }
            Regex regexTime = new Regex("◎上映日期(.*)");
            match = regexTime.Match(Webcontent);
            if (match.Success)
            {
                ifo.Date = match.Groups[1].ToString();
            }
            Regex regexActor = new Regex("<div>\"主演:\"(.*)");
            match = regexActor.Match(Webcontent);
            if (match.Success)
            {
                Regex replace = new Regex("\"&nbsp;&nbsp;\"");
                ifo.Actors = replace.Replace(match.Groups[2].ToString(), "\\");
                //ifo.Actors = match.Groups[1].ToString();
            }

            //匹配类型
            //匹配语言
            Regex regexOnlineLink = new Regex("<li><a title=\"(.*)\" href=\"(.*)\" target=\"_self\">");//匹配在线播放链接
            match = regexOnlineLink.Match(Webcontent);
            Console.WriteLine(match.Groups[2].ToString());
            if (match.Success)
            {

                ifo.OnlinePlayLink = "http://m.tlyy.tv" + match.Groups[1].ToString();
            }
            //Console.WriteLine(ifo.OnlinePlayLink);
            movies.Add(ifo);
        }

    }
}
