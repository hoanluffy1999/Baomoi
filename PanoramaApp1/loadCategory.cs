using PanoramaApp1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace PanoramaApp1
{
    class loadCategory
    {
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
                        value = val + " phút trước";
                    }
                    else {
                        val = int.Parse(h[0]) - int.Parse(hitem[0]);
                        value = val + " giờ trước";
                    }

                }
            }
            else {
                val = int.Parse(split[1]) - int.Parse(itemsplit[1]);
                value = val + " ngày trước";
            }

            return value;
        }
       
        public void loadThoisu()
        {
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/thoi-su.rss");
            Loadrss("http://dantri.com.vn/xa-hoi.rss");
            Loadrss("http://vietnamnet.vn/rss/xa-hoi.rss");
            Loadrss("http://tuoitre.vn/rss/tt-chinh-tri-xa-hoi.rss");
        }
        public void loadTheGioi()
        {
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/the-gioi.rss");
            Loadrss("http://dantri.com.vn/the-gioi.rss");
            Loadrss("http://vietnamnet.vn/rss/quoc-te.rss");
            Loadrss("http://tuoitre.vn/rss/tt-the-gioi.rss");
        }
        public void loadKinhTe()
        {
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/kinh-doanh.rss");
            Loadrss("http://dantri.com.vn/kinh-doanh.rss");
            Loadrss("http://vietnamnet.vn/rss/kinh-doanh.rss");
            Loadrss("http://tuoitre.vn/rss/tt-kinh-te.rss");
        }
        public void loadPhapLuat()
        {
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/phap-luat.rss");
            Loadrss("http://dantri.com.vn/phap-luat.rss");
            Loadrss("http://vietnamnet.vn/rss/goc-nhin-thang.rss");
            Loadrss("http://tuoitre.vn/rss/tt-phap-luat.rss");
        }
        public void loadBongDa()
        {
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/the-thao.rss");
            Loadrss("http://dantri.com.vn/the-thao.rss");
            Loadrss("http://vietnamnet.vn/rss/the-thao.rss");
            Loadrss("http://tuoitre.vn/rss/tt-the-thao.rss");
        }
        public void loadGiaoDuc()
        {
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/giao-duc.rss");
            Loadrss("http://dantri.com.vn/giao-duc-khuyen-hoc.rss");
            Loadrss("http://vietnamnet.vn/rss/giao-duc.rss");
            Loadrss("http://tuoitre.vn/rss/tt-giao-duc.rss");
        }
        public void loadSucKhoe()
        {
            App.ViewModel.Category.Clear();

            Loadrss("http://vnexpress.net/rss/suc-khoe.rss");
            Loadrss("http://dantri.com.vn/suc-khoe.rss");
            Loadrss("http://vietnamnet.vn/rss/suc-khoe.rss");
            Loadrss("http://tuoitre.vn/rss/tt-song-khoe.rss");
        }
        public void loadDuLich()
        {
            App.ViewModel.Category.Clear();

            Loadrss("http://vnexpress.net/rss/du-lich.rss");
            Loadrss("http://dantri.com.vn/du-lich.rss");
            // Loadrss("");
            Loadrss("http://tuoitre.vn/rss/tt-du-lich.rss");
        }
        public void loadTinhYeu()
        {
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/gia-dinh.rss");
            Loadrss("http://dantri.com.vn/tinh-yeu-gioi-tinh.rss");
            //  Loadrss("");
            Loadrss("http://tuoitre.vn/rss/tt-du-lich.rss");
        }
        public void loadXe()
        {
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/oto-xe-may.rss");
            Loadrss("http://dantri.com.vn/o-to-xe-may.rss");
            //  Loadrss("");
            //  Loadrss("");
        }
        public void loadKhoaHoc()
        {
            App.ViewModel.Category.Clear();
            Loadrss("http://vnexpress.net/rss/khoa-hoc.rss");
            Loadrss("http://dantri.com.vn/suc-manh-so.rss");
            Loadrss("http://vietnamnet.vn/vn/cong-nghe-thong-tin-vien-thong/index.html");
            Loadrss("http://tuoitre.vn/rss/tt-nhip-song-so.rss");
        }
        public void loadGiaiTri()
        {
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

                            if (item.Name.ToString() == "pubDate") { date = item.Value; }

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
                                    if (y.Name.ToString() == "title")
                                    {
                                        newitem.Title = y.Value;
                                    }
                                    if (y.Name.ToString() == "pubDate")
                                    {
                                        string dateitem = y.Value;
                                        string time = null;
                                        try
                                        {

                                            time = setTime(date, dateitem);
                                        }
                                        catch { }
                                        newitem.News = tenbao + ":" + time;
                                    }
                                    if (y.Name.ToString() == "link") { newitem.Link = y.Value; }
                                    if (y.Name.ToString() == "description")
                                    {

                                        char x = '"';
                                        string data = y.Value;
                                        if (tenbao.Equals("TuoitreOnline"))// { stack.Visibility = Visibility.Collapsed; }
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
                                                String[] value = null;
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
                              
                                App.ViewModel.Category.Add(newitem);

                            }
                        }
                    }
                }
              
            }
            catch
            {
             
            }
        }
       
    }
}
