using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PanoramaApp1.ViewModels;
using System.Xml.Linq;

namespace PanoramaApp1
{
    public partial class Category : PhoneApplicationPage
    {
        int i = 1;
        string ti = null;
        public Category()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
            this.Loaded += Category_Loaded;
        }

        private void Category_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void tap_back(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.GoBack();
        }
      
    
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        
             
                    stack.Visibility = Visibility.Visible;
                    load.Begin();
                    ti = NavigationContext.QueryString["Title"];
                    Title.Text = ti;
                if (ti != null)
                {
                    switch (ti)
                    {
                        case "Du Lịch": loadDuLich(); break;
                        case "Giải Trí": loadGiaiTri(); break;
                        case "Giáo Dục": loadGiaoDuc(); break;
                        case "Khoa Học": loadKhoaHoc(); break;
                        case "Kinh Doanh": loadKinhTe(); break;
                        case "Thời Sự": loadThoisu(); break;
                        case "Pháp Luật": loadPhapLuat(); break;
                        case "Sức Khỏe": loadSucKhoe(); break;
                        case "Thế Giới": loadTheGioi(); break;
                        case "Thể Thao": loadBongDa(); break;
                        case "Tình Yêu": loadTinhYeu(); break;
                        case "Xe": loadXe(); break;
                            // case "Body": stack.Visibility = Visibility.Collapsed; break;
                    }
                    ti = null;

                    }
                
              

            
        }

        private void Category_Click(object sender, SelectionChangedEventArgs e)
        {
            if (MainLongListSelector.SelectedItem == null)
                return;
            string title = (MainLongListSelector.SelectedItem as ItemViewModel).Title;
            string news = (MainLongListSelector.SelectedItem as ItemViewModel).News;
            string link = (MainLongListSelector.SelectedItem as ItemViewModel).Link;
            string[] name = news.Split(':');
            NavigationService.Navigate(new Uri("/BodyNews.xaml?Title=" + ti + "&TenBao=" + name[0] + "&Link=" + link, UriKind.Relative));
            MainLongListSelector.SelectedItem = null;
        }
        string date = null;
        public string setTime(string time, string timeitem)
        {
            string value = "";
            int val;
            //>Tue, 23 Feb 2016 16:11:11 +0700
            string[] split = null;
            string[] itemsplit = null;
            split = time.Split(' ');
            itemsplit = timeitem.Split(' ');

            if (split[0].Equals(itemsplit[0]))
            {
                if (split[1].Equals(itemsplit[1]))
                {
                    string[] h = split[4].Split(':');
                    string[] hitem = itemsplit[4].Split(':');
                    if (h[0].Equals(hitem[0]))
                    {
                        val = int.Parse(h[1]) - int.Parse(hitem[1]);
                        value = Math.Abs(val) + " phút trước";
                    }
                    else {
                        val = int.Parse(h[0]) - int.Parse(hitem[0]);
                        value = Math.Abs(val) + " giờ trước";
                    }

                }
            }
            else {
                val = int.Parse(split[1]) - int.Parse(itemsplit[1]);
                value = Math.Abs( val) + " ngày trước";
            }

            return value;
        }

        public void loadThoisu()
        {
            App.ViewModel.Itemss.Clear();
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/thoi-su.rss");
            Loadrss("http://dantri.com.vn/xa-hoi.rss");
            Loadrss("http://vietnamnet.vn/rss/xa-hoi.rss");
            Loadrss("http://tuoitre.vn/rss/tt-chinh-tri-xa-hoi.rss");
        }
        public void loadTheGioi()
        {
            App.ViewModel.Itemss.Clear();
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/the-gioi.rss");
            Loadrss("http://dantri.com.vn/the-gioi.rss");
            Loadrss("http://vietnamnet.vn/rss/quoc-te.rss");
            Loadrss("http://tuoitre.vn/rss/tt-the-gioi.rss");
        }
        public void loadKinhTe()
        {
            App.ViewModel.Itemss.Clear();
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/kinh-doanh.rss");
            Loadrss("http://dantri.com.vn/kinh-doanh.rss");
            Loadrss("http://vietnamnet.vn/rss/kinh-doanh.rss");
            Loadrss("http://tuoitre.vn/rss/tt-kinh-te.rss");
        }
        public void loadPhapLuat()
        { 

             App.ViewModel.Itemss.Clear();
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/phap-luat.rss");
            Loadrss("http://dantri.com.vn/phap-luat.rss");
            Loadrss("http://vietnamnet.vn/rss/goc-nhin-thang.rss");
            Loadrss("http://tuoitre.vn/rss/tt-phap-luat.rss");
        }
        public void loadBongDa()
        {
            App.ViewModel.Itemss.Clear();
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/the-thao.rss");
            Loadrss("http://dantri.com.vn/the-thao.rss");
            Loadrss("http://vietnamnet.vn/rss/the-thao.rss");
            Loadrss("http://tuoitre.vn/rss/tt-the-thao.rss");
        }
        public void loadGiaoDuc()
        {
            App.ViewModel.Itemss.Clear();
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/giao-duc.rss");
            Loadrss("http://dantri.com.vn/giao-duc-khuyen-hoc.rss");
            Loadrss("http://vietnamnet.vn/rss/giao-duc.rss");
            Loadrss("http://tuoitre.vn/rss/tt-giao-duc.rss");
        }
        public void loadSucKhoe()
        {
            App.ViewModel.Category.Clear();
            App.ViewModel.Itemss.Clear();
            Loadrss("http://vnexpress.net/rss/suc-khoe.rss");
            Loadrss("http://dantri.com.vn/suc-khoe.rss");
            Loadrss("http://vietnamnet.vn/rss/suc-khoe.rss");
            Loadrss("http://tuoitre.vn/rss/tt-song-khoe.rss");
        }
        public void loadDuLich()
        {
            App.ViewModel.Category.Clear();
            App.ViewModel.Itemss.Clear();
            Loadrss("http://vnexpress.net/rss/du-lich.rss");
            Loadrss("http://dantri.com.vn/du-lich.rss");
            // Loadrss("");
            Loadrss("http://tuoitre.vn/rss/tt-du-lich.rss");
        }
        public void loadTinhYeu()
        {
            App.ViewModel.Itemss.Clear();
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/gia-dinh.rss");
            Loadrss("http://dantri.com.vn/tinh-yeu-gioi-tinh.rss");
            //  Loadrss("");
            Loadrss("http://tuoitre.vn/rss/tt-du-lich.rss");
        }
        public void loadXe()
        {
            App.ViewModel.Itemss.Clear();
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/oto-xe-may.rss");
            Loadrss("http://dantri.com.vn/o-to-xe-may.rss");
            //  Loadrss("");
            //  Loadrss("");
        }
        public void loadKhoaHoc()
        {
            App.ViewModel.Itemss.Clear();
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/khoa-hoc.rss");
            Loadrss("http://dantri.com.vn/suc-manh-so.rss");
            Loadrss("http://vietnamnet.vn/vn/cong-nghe-thong-tin-vien-thong/index.html");
            Loadrss("http://tuoitre.vn/rss/tt-nhip-song-so.rss");
        }
        public void loadGiaiTri()
        {
            App.ViewModel.Itemss.Clear();
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/giai-tri.rss");
            Loadrss("http://dantri.com.vn/giai-tri.rss");
            Loadrss("http://vietnamnet.vn/vn/giai-tri/index.html");
            Loadrss("http://tuoitre.vn/rss/tt-van-hoa-giai-tri.rss");
        }
        public void Loadrss(string links)
        {
            WebClient web2 = new WebClient();
            Uri uri = new Uri(links, UriKind.Absolute);
            web2.DownloadStringAsync(uri);
            web2.DownloadStringCompleted += Web_DownloadStringCompleted2;
            
        }
        public void Web_DownloadStringCompleted2(object sender, DownloadStringCompletedEventArgs e)
        {
           
                try
                {
                    string tenbao = null;

                    string xml = e.Result.ToString();
                    //  MessageBox.Show(xml);
                    XDocument xdoc = XDocument.Parse(xml);
                    foreach (var xe in xdoc.Root.Elements())
                    {
                        if (xe.Name.ToString() == "channel")
                        {
                            foreach (var item in xe.Elements())
                            {

                                if (item.Name.ToString() == "pubDate")
                                {
                                    date = item.Value;
                                    if (date[0].Equals(' ')) { date = date.Substring(1); }
                                }

                                if (item.Name.ToString() == "generator")
                                {
                                    string ten = item.Value;
                                    string[] cat = ten.Split('.');
                                    tenbao = cat[0];
                                }
                                if (item.Name.ToString() == "item")
                                {
                                    ItemViewModel newitem = new ItemViewModel();
                                    foreach (var y in item.Elements())
                                    {
                                        int c = 60 * 24 * 60;
                                        if (y.Name.ToString() == "title")
                                        {
                                            newitem.Title = y.Value;
                                        }
                                        if (y.Name.ToString() == "pubDate")
                                        {
                                            string dateitem = y.Value;
                                            string time = "1 ngày trước";
                                            try
                                            {

                                                time = setTime(date, dateitem);
                                                c = inttime(time);
                                                newitem.Time = c.ToString(); ;


                                            }
                                            catch
                                            {
                                                newitem.News = tenbao + ":" + time;
                                                c = inttime(time);
                                                newitem.Time = c.ToString(); ;
                                            }
                                            newitem.News = tenbao + ":" + time;
                                            c = inttime(time);
                                            newitem.Time = c.ToString(); ;

                                        }
                                        if (y.Name.ToString() == "link") { newitem.Link = y.Value; }
                                        if (y.Name.ToString() == "description")
                                        {

                                            char x = '"';
                                            string data = y.Value;
                                            if (tenbao.Equals("VietNamNet"))
                                            {
                                                try
                                                {
                                                    string[] value = null;
                                                    value = data.Split(x);
                                                    newitem.Image = value[1];

                                                }
                                                catch
                                                {

                                                }

                                            }
                                            else
                                            {
                                                 
                                                try
                                                {
                                                    string[] value = null;
                                                    value = data.Split(x);
                                                    try
                                                    {
                                                        newitem.Image = value[3];
                                                        newitem.Link = value[1];
                                                    }
                                                    catch
                                                    {

                                                    }
                                                }
                                                catch
                                                { }
                                            }


                                        }
                                    }
                                    newitem.Busy = Visibility.Collapsed.ToString();
                                    App.ViewModel.Itemss.Add(newitem);


                                }
                            }
                        }
                    }

                }
                catch
                {

                }
                i = i + 1;
                if (i%4==0)
                {
                    sapxep();
                }
            }
           


        public int inttime(string time)
        {
            int x = 0;
            string[] li = time.Split(' ');
            if (li[1].Equals("phút")) return x = int.Parse(li[0]);
            if (li[1].Equals("giờ")) return x = int.Parse(li[0]) * 60;
            if (li[1].Equals("ngày")) return x = int.Parse(li[0]) * 24 * 60;
            return x;
        }
        public void sapxep()
        {
         
            App.ViewModel.Category.Clear();
            ItemViewModel[] tach = App.ViewModel.Itemss.ToArray();
            // tach.Sort();

            for (int i = 0; i < tach.Length; i++)
            {
                for (int j = i; j < tach.Length; j++)
                {
                    if (int.Parse(tach[i].Time) > int.Parse(tach[j].Time))
                    {
                        ItemViewModel x = new ItemViewModel();
                        x = tach[i];
                        tach[i] = tach[j];
                        tach[j] = x;
                    }
                }
            }
            for (int i = 0; i < tach.Length; i++)
            {
                App.ViewModel.Category.Add(tach[i]);
            }
            stack.Visibility = Visibility.Collapsed;
        }
    }
}

