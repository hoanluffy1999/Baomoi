using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Baoag;
using System.Windows.Threading;
using Microsoft.Phone.Shell;
using Baoag.ViewModels;
using System.Xml.Linq;
using System.Net;
using System;

namespace Baoag
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        public string title = "đến giờ đọc báo bạn ei";
        public string news = "nhấn vào cái này";
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        /// 
        string date = null;

        private string vnexpress = "http://vnexpress.net/rss/tin-moi-nhat.rss";
        private string dantri = "http://dantri.com.vn/trangchu.rss";
        private string vietnam = "http://vietnamnet.vn/rss/home.rss";
        private string tuoitre = "http://tuoitre.vn/rss/tt-tin-moi-nhat.rss";
      

        public void LoadVn()
        {

            //ViewModel.Items.Clear();
           
         
            Loadrss(vietnam);
            Loadrss(tuoitre);
            Loadrss(dantri);
             Loadrss(vnexpress);

        }


        public void Loadrss(string link)
        {
            WebClient web1 = new WebClient();
            Uri uri = new Uri(link, UriKind.Absolute);
            web1.DownloadStringAsync(uri);
            web1.DownloadStringCompleted += Web_DownloadStringCompleted1;
        }

        private void Web_DownloadStringCompleted1(object sender, DownloadStringCompletedEventArgs e)
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
                                    if (y.Name.ToString() == "title")
                                    {
                                        newitem.Title = y.Value;
                                        title = y.Value;
                                    }
                                    if (y.Name.ToString() == "pubDate")
                                    {
                                        string dateitem = y.Value;
                                        string time = null;
                                        try
                                        {

                                            time = setTime(date, dateitem);
                                        }
                                        catch { newitem.News = tenbao + ":" + time; }
                                        newitem.News = tenbao + ":" + time;
                                        news = newitem.News;

                                    }
                                    if (y.Name.ToString() == "link") { newitem.Link = y.Value; }
                                    if (y.Name.ToString() == "description")
                                    {

                                        char x = '"';
                                        string data = y.Value;
                                        //  if (tenbao.Equals("TuoitreOnline")) { .Visibility = Visibility.Collapsed; }
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



                            }
                        }
                    }
                }

            }
            catch
            {

            }
            
        }
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



        private DispatcherTimer dispatcherTimer;
        protected override void OnInvoke(ScheduledTask task)
        {
            LoadVn();
            //TODO: Add code to perform your task in background
            DateTime now;
            DateTime old=DateTime.Now;

            while (true)
            {
                 now = DateTime.Now;
                if (now >= old.AddSeconds(60))
                {
                    LoadVn();
                    ShellToast toast = new ShellToast();
                    toast.Title = title;
                    toast.Content = news;
                    toast.Show();
                    old = old.AddSeconds(60);
//NotifyComplete();
                }
            }

        

        }

       
    }
}