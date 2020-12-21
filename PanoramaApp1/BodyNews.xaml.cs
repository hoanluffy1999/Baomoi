using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HtmlAgilityPack;




namespace PanoramaApp1
{
    public partial class BodyNews : PhoneApplicationPage
    {
        string link;
        string tenBao;
        string title;
        private string html = "aaa";
        public BodyNews()
        {
            this.Loaded += BodyNews_Loaded;
            InitializeComponent();
            
        }


   
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            tenBao = NavigationContext.QueryString["TenBao"];
           

            link = NavigationContext.QueryString["Link"];
          

            title = NavigationContext.QueryString["Title"];
         

            WebClient web = new WebClient();
            Uri urilys = new Uri(link, UriKind.Absolute);
          
            web.DownloadStringCompleted += Weblys_DownloadStringCompleted;
            web.DownloadStringAsync(urilys);
            loadBody load = new loadBody();
           



        }

        private void Weblys_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try {
                string body = "null";
                html = e.Result.ToString();
                //   bodynews.NavigateToString(html);
             /*   loadBody load = new loadBody();
                if (tenBao.Equals("Dân trí")) { body = load.getDanTri(html); }

                if (tenBao.Equals("VnExpress")) { body = load.getVN(html); }

                if (tenBao.Equals("TuoitreOnline")) { body = load.getTuoiTre(html); }

                if (tenBao.Equals("VietNamNet")) { body = load.getVNNet(html); }
                */
                bodynews.NavigateToString(html);
            }
            catch { }
          
        }

        private void BodyNews_Loaded(object sender, RoutedEventArgs e)
        {
          
       
    }
    }
}