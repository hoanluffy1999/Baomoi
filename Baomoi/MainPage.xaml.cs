using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Baomoi.Resources;
using Baomoi.ViewModels;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Baomoi
{
    public partial class MainPage : PhoneApplicationPage
    {
        WebClient web = new WebClient();
        string url = "http://vnexpress.net/rss/tin-moi-nhat.rss";
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;
            App.ViewModel.Items.Clear();
            
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            Uri uri = new Uri(url, UriKind.Absolute);
            web.DownloadStringAsync(uri);
            web.DownloadStringCompleted += Web_DownloadStringCompleted;
            
        }

        private void Web_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            App.ViewModel.Items.Clear();
            try {
                string xml = e.Result;
                XDocument xdoc = XDocument.Parse(xml);
                foreach (var xe in xdoc.Root.Elements())
                {
                    if (xe.Name.ToString() == "channel")
                    {
                        foreach (var item in xe.Elements())
                        {
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
                                        newitem.Body = y.Value;
                                    }
                                    if (y.Name.ToString() == "description")
                                    {
                                        string data = y.Value;
                                        // <a href="(.*?)"><img width=130 height=100 src="(.*?)" ></a>
                                        char x = '"';
                                        string[] kq = data.Split(x);
                                        newitem.LineThree = kq[1];
                                        newitem.Image = kq[3];
                                    }
                                }
                                Deployment.Current.Dispatcher.BeginInvoke(delegate
                                {
                                    App.ViewModel.Items.Add(newitem);
                                });
                            }
                        }
                    }
                }
            }
            catch {  }
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/Details.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).LineThree, UriKind.Relative));

            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;
        }

    

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}