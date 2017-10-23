using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ZalandoShop.Models;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using System.Net.NetworkInformation;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ZalandoShop.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResultView : Page
    {
        public static Articles articles { get; set; }
        public string brandKey;
        public string url;
        public ObservableCollection<Content> FilteredArticles;
        private SearchCriteria selectedFacet;
        private bool incall = false;
        private int pageNumber;
        bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();
        private MessageDialog noInternetMsg;

        public ResultView()
        {
            this.InitializeComponent();
            FilteredArticles = new ObservableCollection<Content>();
            selectedFacet = new SearchCriteria();
            noInternetMsg = new MessageDialog("please connect to Internet ", "No connection ");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            selectedFacet = e.Parameter as SearchCriteria;
           GetArticlesList(selectedFacet.key, 1, 10, selectedFacet.gender);
        }

        public async void GetArticlesList(string facetKey, int pageNumber, int PageSize, string gender)
        {
            if (isInternetConnected)
            {
                HttpClient http = new HttpClient();
                url = String.Format("https://api.zalando.com/articles?brand={0}&page={1}&pageSize={2}&gender={3}", selectedFacet.key, pageNumber, PageSize, selectedFacet.gender);
                var articlesResponse = await http.GetStringAsync(url);
                articles = JsonConvert.DeserializeObject<Articles>(articlesResponse);
                foreach (Content article in articles.content)
                {
                    if (article != null)
                    {
                        FilteredArticles.Add(article);
                    }
                }
                incall = false;
            }
            else
            {
                await noInternetMsg.ShowAsync();
            }
        }

        public static ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            if (depObj is ScrollViewer)
                return depObj as ScrollViewer;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                var result = GetScrollViewer(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        private void articlesList_Loaded(object sender, RoutedEventArgs e)
        {
            if(articlesList.Visibility==Visibility.Visible)
            { 
                ScrollViewer viewer = GetScrollViewer(this.articlesList);
                viewer.ViewChanged += Viewer_ViewChanged;
            }

        }
        private void articlesGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (articlesGrid.Visibility == Visibility.Visible)
            {
                ScrollViewer viewer = GetScrollViewer(this.articlesGrid);
                viewer.ViewChanged += Viewer_ViewChanged;
            }
        }
        private void Viewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            ScrollViewer view = sender as ScrollViewer;
            double progress = view.VerticalOffset / view.ScrollableHeight;
            Debug.WriteLine(progress);
            if (progress > 0.7 && !incall)
            {
                incall = true;
                GetArticlesList(selectedFacet.key, ++pageNumber, 10, selectedFacet.gender);
            }
        }
    }
}
