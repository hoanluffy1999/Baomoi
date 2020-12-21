using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using PanoramaApp1.ViewModels;

namespace PanoramaApp1
{
    class loadBody

    {
        private string body;
        private string html;
        public void setBody(string bo)
        {
            body = bo;
        }
        public string getBody()
        {
            return body;
        }
        public void setHtml(string bo)
        {
            html = bo;
        }
        public string getHtml()
        {
            return html;
        }
        public void load()
        {
            loadHtml("http://vnexpress.net/tin-tuc/thoi-su/giao-thong/nhung-cau-hoi-quanh-vu-xe-camry-dam-chet-3-nguoi-3362474.html");

        
        }
        public string getTuoiTre(string html)
        {
            string body = "null";
            HtmlNode node = null;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            try
            {
                ///html/body/div[2]/section/section/div[1]/div[1]/div[2]
                node = doc.DocumentNode.SelectSingleNode("/html/body/div[2]/section/section/div[1]/div[1]");
                body = node.InnerHtml.ToString();
                return body;
            }
            catch { }
            return body;
        }

        public string getVNNet(string html)
        {
            string body = "null";
            HtmlNode node = null;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            try
            {
                ///html/body/div[1]/div[3]/div/div[1]/div/div[3]
                node = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div[3]/div/div[1]/div/div[3]");
                body = node.InnerHtml.ToString();
                return body;
            }
            catch { }
            try
            {
                ///html/body/div[1]/div[3]/div/div[1]/div/div[3]
                node = doc.DocumentNode.SelectSingleNode("//*[@id=\"ArticleContent\"]");
                body = node.InnerHtml.ToString();
                return body;
            }
            catch { }
            return body;
           // / html / body / div[1] / div[3] / div / div[1] / div / div[2]
        }
        public string getVN(string html)
        {
            string body = "null";
            HtmlNode node = null;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            ////*[@id="wrapper_container"]
            try
            {
                node = doc.DocumentNode.SelectSingleNode("//*[@id=\"wrapper_container\"]");
                body = node.InnerHtml.ToString();
                return body;
            }
            catch { }
            try
            {
                node = doc.DocumentNode.SelectSingleNode("//*[@id=\"article_content\"]");
                body = node.InnerHtml.ToString();
                return body;
            }
            catch { }
            try {
                node = doc.DocumentNode.SelectSingleNode("//div[@class=\"fck_detail width_common\"]");
                body = node.InnerHtml.ToString();
                return body;
            }
            catch
            {

            }
           
          return body;
                 
        }
        public string getDanTri(string html)
        {
            // //*[@id="divNewsContent"]
            HtmlDocument doc = new HtmlDocument();
            HtmlNode node = null;
            doc.LoadHtml(html);
            try {
                 node = doc.DocumentNode.SelectSingleNode("//*[@id=\"divNewsContent\"]");
                return node.InnerHtml.ToString();
            }
            catch
            {

            }
            try
            {
                ////*[@id="divNewsContent"]
                node = doc.DocumentNode.SelectSingleNode("//*[@id=\"divNewsContent\"]");
                return node.InnerHtml.ToString();
            }
            catch
            {

            }
            try
            {
                //*[@id="container"]/div[1]
                node = doc.DocumentNode.SelectSingleNode("//*[@id=\"ContentPlaceHolder1_Detail1_divContent\"]/div[2]");
                return node.InnerHtml.ToString();
            }
            catch
            {

            }
            try
            {
               node = doc.DocumentNode.SelectSingleNode("//*[@id=\"ContentPlaceHolder1_ContentPlaceHolder1_Detail1_divContent\"]");
                return node.InnerHtml.ToString();
            }
            
            catch
            {

            }
            // return node.InnerHtml.ToString();
            return "null";

        }
        public void loadHtml(string link)
        {
            WebClient web3 = new WebClient();
            Uri uri = new Uri(link, UriKind.Absolute);
            web3.DownloadStringAsync(uri);
            web3.DownloadStringCompleted += Web_DownloadStringCompleted3;
        }

        public void Web_DownloadStringCompleted3(object sender, DownloadStringCompletedEventArgs e)
        {
            setHtml( e.Result.ToString());
            App.ViewModel.Check.Clear();
            setBody(getVN(getHtml()));
         


        }
    }
}
