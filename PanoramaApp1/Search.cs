using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone;
using System.Net;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using PanoramaApp1.ViewModels;

namespace PanoramaApp1
{
    class Search
    {
        public void search(string x)
        {
            App.ViewModel.Search.Clear();
            string searchVN = "http://timkiem.vnexpress.net/?q=" + x;
            string searchDT = "http://search.dantri.com.vn/SearchResult.aspx?s=" + x + "&PageIndex=1";
            string searchTT = "http://tuoitre.vn/tim-kiem/gool/?q=" + x;
            string searchVNN = "http://vietnamnet.vn/vn/tim-kiem/0/a/" + x + "/";
            SearchDT(searchDT);
            SearchVN(searchVN);
             SearchVNN(searchVNN);

        }
        public void SearchDT(string y)
        {
            WebClient web1 = new WebClient();
            Uri uri = new Uri(y, UriKind.Absolute);
            web1.DownloadStringAsync(uri);
            web1.DownloadStringCompleted += Web1_DownloadStringCompleted;

        }

        private void Web1_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
          
              
                string html = e.Result;
                HtmlDocument xy = new HtmlDocument();
                xy.LoadHtml(html);
            HtmlNodeCollection node = xy.DocumentNode.SelectNodes("//div[@class=\"mt3 clearfix\"]");
               foreach(HtmlNode x1 in node)
                {
                    char spl = '"';
                    ItemViewModel x = new ItemViewModel();
                    HtmlNode x2 = x1.FirstChild;
                    HtmlNode x3 = x1.LastChild;
                    string[] lin = x2.InnerHtml.Split(spl);
                    x.Image = lin[1];
                    x.Title = lin[5];
                    string[] link = x3.InnerHtml.Split(spl);
                    x.Link = link[3];
                    x.News = "Dân trí";
                    App.ViewModel.Search.Add(x);

                }
            

            }
            catch { }
        }

        public void SearchVN(string y)
        {
            WebClient web2 = new WebClient();
            Uri uri = new Uri(y, UriKind.Absolute);
            web2.DownloadStringAsync(uri);
            web2.DownloadStringCompleted += Web2_DownloadStringCompleted1;
        }

        private void Web2_DownloadStringCompleted1(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {

                
                string html = e.Result;
                HtmlDocument xy = new HtmlDocument();
                xy.LoadHtml(html);
                HtmlNodeCollection node = xy.DocumentNode.SelectNodes("//div[@class=\"thumb\"]");
                foreach (HtmlNode x1 in node)
                {
                    char spl = '"';
                    string[] ht = x1.InnerHtml.Split('\n');
                    ItemViewModel x = new ItemViewModel();
                   
                    string[] lin = ht[1].Split(spl);
                    x.Link = lin[3];                 
                    x.Title = lin[5];
                    string[] link = ht[2].Split(spl);
                    x.Image = link[1];
                    x.News = "VnExpress";
                    App.ViewModel.Search.Add(x);

                }


            }
            catch { }
        }

        public void SearchVNN(string y)
        {
            WebClient web1 = new WebClient();
            Uri uri = new Uri(y, UriKind.Absolute);
            web1.DownloadStringAsync(uri);
            web1.DownloadStringCompleted += Web1_DownloadStringCompleted1;
        }

        private void Web1_DownloadStringCompleted1(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {


                string html = e.Result;
                HtmlDocument xy = new HtmlDocument();
                xy.LoadHtml(html);
                HtmlNodeCollection node = xy.DocumentNode.SelectNodes("//div[@class=\"ArticleCateItem\"]");
                foreach (HtmlNode x1 in node)
                {
                    char spl = '"';
                    string[] ht = x1.InnerHtml.Split('\n');
                    ItemViewModel x = new ItemViewModel();

                    string[] lin = ht[0].Split(spl);
                    string link = "vietnamet.vn" + lin[3];
                    x.Link = link;
                    x.Title = lin[5];
                  
                    x.Image = lin[7];
                    x.News = "VietNamNet";
                    App.ViewModel.Search.Add(x);

                }


            }
            catch { }
        }

     
    }
}
