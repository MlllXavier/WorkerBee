using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace WorkerBee
{
    public class UU_Novel : Novel
    {
        public override void PostWebContent()
        {
            try
            {
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                Byte[] pageData = MyWebClient.DownloadData("http://www.uu234.cc/search?wd=" + novelName); //从指定网站下载数据
                string pageHtml = Encoding.UTF8.GetString(pageData); //string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
                MatchToNovel(pageHtml);
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message.ToString());
            }
        }

        public void MatchToNovel(string WebContent)//匹配小说函数，参数为搜索到的页面内容
        {
            Regex regex = new Regex("<a href=\"(.*)\" class=\"green\" target=\"_blank\">(.*)</a>");
            MatchCollection Matches = regex.Matches(WebContent);
            foreach (Match m in Matches)
            {
                string webcontent = GetWebContent(m.Groups[1].ToString());//匹配到一个小说链接，调用匹配小说信息函数
                if (webcontent != null)
                    MatchToIfo(webcontent);
            }
        }

        public string GetWebContent(string url)//获取搜索到的小说内的具体网页内容，参数为小说链接，
        {
            string pageHtml = "";
            try
            {
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                Byte[] pageData = MyWebClient.DownloadData(url); //从指定网站下载数据
                pageHtml = Encoding.UTF8.GetString(pageData); //string pageHtml = Encoding.UTF8.GetString(pageData); //如果获取网站页面采用的是UTF-8，则使用这句
            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message.ToString());
            }
            return pageHtml;
        }

        public void MatchToIfo(string Webcontent)//匹配小说信息函数，参数为获取到的具体小说网页内容
        {
            Regex regexTitle = new Regex("<h1>(.*)</h1>");
            Match match = regexTitle.Match(Webcontent);
            NovelIfo ifo;//小说信息结构体
            ifo.Size = ifo.Introduction = ifo.Name = ifo.Writer = ifo.Tag = ifo.Introduction = "暂无。。。";//初始化
            ifo.OnlineReadLink = ifo.IconLink = null;
            ifo.Links = new List<Link>();//初始化
            if (match.Success)
            {
                ifo.Name = match.Groups[1].ToString();  //书名   //Done
            }
            else return;

            Regex regexIcon = new Regex("<img src=\"(.*)\" alt=\"(.*)\" title=\"(.*)\">");
            match = regexIcon.Match(Webcontent);
            if (match.Success)
            {
                ifo.IconLink = match.Groups[1].ToString();   //Done
            }


            Regex regexWriter = new Regex("<span class=\"black\"><a href=\"http://www.uu234.cc/writer/(.*)/\" target=\"_blank\">(.*)</a></span>");
            match = regexWriter.Match(Webcontent);
            if (match.Success)
            {
                ifo.Writer = match.Groups[1].ToString();  //作者
            }

            Regex regexTag = new Regex("<span class=\"green\"><a href=\"(.*)\" target=\"_blank\">(.*)</a> </span>");
            match = regexTag.Match(Webcontent);
            if (match.Success)
            {
                ifo.Tag = match.Groups[2].ToString();  //标签
            }

            Regex regexSize = new Regex("TXT大小：<span class=\"black\">(.*)</span>压缩包大小：");
            match = regexSize.Match(Webcontent);
            if (match.Success)
            {
                ifo.Size = match.Groups[1].ToString();  //大小
            }

            Regex regexOnlineLink = new Regex("<a href=\"(.*)\" class=\"startedbtn\" target=\"_blank\">开始阅读</a>");//匹配在线阅读链接
            match = regexOnlineLink.Match(Webcontent);
            if (match.Success)
            {
                ifo.OnlineReadLink = match.Groups[1].ToString(); //在线阅读    //Done
            }
            Regex regexIntroduction = new Regex("<div class=\"r_cons\">(.*)</div>");
            match = regexIntroduction.Match(Webcontent);
            if (match.Success)
            {
                ifo.Introduction = match.Groups[1].ToString();  //简介    //Done
            }
            Regex regexLinks = new Regex("<a href=\"(.*)\" rel=\"nofollow\">(.*)</a>");
            MatchCollection matchlinks = regexLinks.Matches(Webcontent);
            foreach (Match link in matchlinks)
            {
                Link item;
                item.type = "";
                item.link = link.Groups[1].ToString();
                item.linkname = link.Groups[2].ToString();
                ifo.Links.Add(item);
            }
            novels.Add(ifo);
        }
    }
}
