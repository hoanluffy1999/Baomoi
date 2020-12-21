using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PanoramaApp1.ViewModels;
using Microsoft.Phone.Tasks;
using HtmlAgilityPack;
using System.Xml.Linq;
using System.Windows.Threading;
using Microsoft.Phone.Scheduler;
using System.Diagnostics;

namespace PanoramaApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        
        loadCategory cate = new loadCategory();
        LoadHotNews a = new LoadHotNews();
    
        int i = 1;
        private DispatcherTimer dispatcherTimer;
        // Constructor
        public MainPage()
        {
            
            InitializeComponent();
            //loading.Begin();
            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += MainPage_Loaded;
           
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 5, 0);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            

            LoadVn();
          //  MessageBox.Show("load lai");
            i = 0;

        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
          
        }


        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            i = 0;
            
            stackPanel.Visibility = Visibility.Visible;

            if (App.ViewModel.Items.Count == 0)
            {
                loading.Begin();
                LoadVn();
            }
            else {
                stackPanel.Visibility = Visibility.Collapsed;
                    }
            if (!App.ViewModel.IsDataLoaded) 
            {

                App.ViewModel.LoadData();

            }
            string agentId = "alo";
            PeriodicTask task = new PeriodicTask(agentId);
            task.Description = "Timestamped position data";
            task.ExpirationTime = DateTime.Now.AddDays(1);
            Debug.WriteLine(task.LastExitReason.ToString());
            if (ScheduledActionService.Find(agentId) != null)
            {
                ScheduledActionService.Remove(agentId);
            }
            try
            {
                ScheduledActionService.Add(task);
                   ScheduledActionService.LaunchForTest(agentId,TimeSpan.FromSeconds(60));

            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("BNS Error: The action is disabled"))
                {
                    MessageBox.Show("The user has disabled background agents for this application.");
                }
                if (ex.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    // No action required: the system prompts the user when the hard limit of periodic tasks has been reached.   
                    Debug.WriteLine(ex.Message);
                }
            }
            catch (SchedulerServiceException ex)
            {
                // No action required: adding a scheduled task can throw a SchedulerServiceException if the device has just booted and the Scheduled Action Service hasn’t started yet. 
                Debug.WriteLine(ex.Message);
            }

        }


        private void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainLongListSelector.SelectedItem == null)
                return;
            string title = (MainLongListSelector.SelectedItem as ItemViewModel).Title.ToString();
            string news = (MainLongListSelector.SelectedItem as ItemViewModel).News.ToString();
            string link = (MainLongListSelector.SelectedItem as ItemViewModel).Link.ToString();
            string[] name = news.Split(':');
            NavigationService.Navigate(new Uri("/BodyNews.xaml?Title=" + title + "&TenBao=" + name[0] + "&Link=" + link, UriKind.Relative));
            MainLongListSelector.SelectedItem = null;
        }

        private void tap_thoi_su(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //cate.loadThoisu();
            NavigationService.Navigate(new Uri("/Category.xaml?Title="+"Thời Sự",UriKind.Relative));
        }

        private void tap_the_gioi(object sender, System.Windows.Input.GestureEventArgs e)
        {
           // cate.loadTheGioi();
            NavigationService.Navigate(new Uri("/Category.xaml?Title=" + "Thế Giới", UriKind.Relative));
        }

        private void tap_kinh_doanh(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //cate.loadKinhTe();
            NavigationService.Navigate(new Uri("/Category.xaml?Title=" + "Kinh Doanh", UriKind.Relative));
        }

        private void tap_phap_luat(object sender, System.Windows.Input.GestureEventArgs e)
        {
         //   cate.loadPhapLuat();
            NavigationService.Navigate(new Uri("/Category.xaml?Title=" + "Pháp Luật", UriKind.Relative));
        }

        private void tap_bong_da(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //cate.loadBongDa();
            NavigationService.Navigate(new Uri("/Category.xaml?Title=" + "Thể Thao", UriKind.Relative));

        }

        private void tap_giao_duc(object sender, System.Windows.Input.GestureEventArgs e)
        {
          //  cate.loadGiaoDuc();
            NavigationService.Navigate(new Uri("/Category.xaml?Title=" + "Giáo Dục", UriKind.Relative));
        }

        private void tap_suc_khoe(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //cate.loadSucKhoe();
            NavigationService.Navigate(new Uri("/Category.xaml?Title=" + "Sức Khỏe", UriKind.Relative));
        }

        private void tap_du_lich(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //cate.loadDuLich();
            NavigationService.Navigate(new Uri("/Category.xaml?Title=" + "Du Lịch", UriKind.Relative));
        }

        private void tap_tinh_yeu(object sender, System.Windows.Input.GestureEventArgs e)
        {
          //  cate.loadTinhYeu();
            NavigationService.Navigate(new Uri("/Category.xaml?Title=" + "Tình Yêu", UriKind.Relative));
        }

        private void tap_xe(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //cate.loadXe();
            NavigationService.Navigate(new Uri("/Category.xaml?Title=" + "Xe", UriKind.Relative));
        }

        private void tap_giai_tri(object sender, System.Windows.Input.GestureEventArgs e)
        {
           // cate.loadGiaiTri();
            NavigationService.Navigate(new Uri("/Category.xaml?Title=" + "Giải Trí", UriKind.Relative));
        }

        private void tap_khoa_hoc(object sender, System.Windows.Input.GestureEventArgs e)
        {
           // cate.loadKhoaHoc();
            NavigationService.Navigate(new Uri("/Category.xaml?Title=" + "Khoa Học", UriKind.Relative));
        }
      
        string date = null;

        private string vnexpress = "http://vnexpress.net/rss/tin-moi-nhat.rss";
        private string dantri = "http://dantri.com.vn/trangchu.rss";
        private string vietnam = "http://vietnamnet.vn/rss/home.rss";
        private string tuoitre = "http://tuoitre.vn/rss/tt-tin-moi-nhat.rss";
        

        public void LoadVn()
        {

            App.ViewModel.Itemss.Clear();
            Loadrss(vnexpress);
            Loadrss(dantri);
            Loadrss(vietnam);
            Loadrss(tuoitre);
          

        }


        public void Loadrss(string link)
        {
            WebClient web1 = new WebClient();
            Uri uri = new Uri(link, UriKind.Absolute);
            web1.DownloadStringAsync(uri);
            web1.DownloadStringCompleted += Web_DownloadStringCompleted1;
          //  MessageBox.Show("x");
        }

        private void Web_DownloadStringCompleted1(object sender, DownloadStringCompletedEventArgs e)
        {
           // int i = 0;
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
                                    int c = 60*24*60;
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
                                            c=inttime(time);
                                            newitem.Time = c.ToString(); ;


                                        }
                                        catch { newitem.News = tenbao + ":" + time;
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
            if (i%4== 0)
            {
                sapxep();
            }

        }
        //int i = 0;      
        public void sapxep()
        {
            App.ViewModel.Items.Clear();
            ItemViewModel[] tach = App.ViewModel.Itemss.ToArray();
            // tach.Sort();

            for (int i = 0; i < tach.Length; i++)
            {
                for (int j = i; j < tach.Length ; j++)
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
            for(int i = 0; i < tach.Length; i++)
            {
                App.ViewModel.Items.Add(tach[i]);
            }
            stackPanel.Visibility = Visibility.Collapsed;
        }     
      
 
        




 
        public int inttime(string time)
        {
            int x = 0;
            string[] li = time.Split(' ');
            if (li[1].Equals("phút")) return x=int.Parse(li[0]);
            if (li[1].Equals("giờ")) return x=int.Parse(li[0])*60;
            if (li[1].Equals("ngày")) return x=int.Parse(li[0])*24*60;
            return x;
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
                value = Math.Abs(val) + " ngày trước";
            }

            return value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string x = search.Text.ToString();
          Search se = new Search();
          se.search(x);
  
        }

        private void LongListSelector_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("â");
            if (ListSearch.SelectedItem == null)
                return;
            string title = (ListSearch.SelectedItem as ItemViewModel).Title.ToString();
            string news = (ListSearch.SelectedItem as ItemViewModel).News.ToString();
            string link = (ListSearch.SelectedItem as ItemViewModel).Link.ToString();
         
            NavigationService.Navigate(new Uri("/BodyNews.xaml?Title=" + title + "&TenBao=" + news + "&Link=" + link, UriKind.Relative));
            ListSearch.SelectedItem = null;
        }
    }

}
