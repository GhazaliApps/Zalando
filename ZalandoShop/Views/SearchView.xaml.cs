using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ZalandoShop.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ZalandoShop.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchView : Page
    {
        private ObservableCollection<Facets> filteredFacets;
        private ObservableCollection<Facet> results;
        private string url = "https://api.zalando.com/facets";
        private Facet selectedFacet;
        private string gender { get; set; }
        private SearchCriteria searchParameters;
        bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();
        private MessageDialog noInternetMsg;
        public SearchView()
        {
            this.InitializeComponent();
            filteredFacets = new ObservableCollection<Facets>();
            results = new ObservableCollection<Facet>();
            selectedFacet = new Facet();
            searchParameters = new SearchCriteria();
            noInternetMsg = new MessageDialog("please connect to Internet ", "No connection ");
        }
        public async Task GetFacetsList()
        {
            if (isInternetConnected)
            {
                HttpClient http = new HttpClient();
                HttpResponseMessage reponse = http.GetAsync(url).Result;
                if (reponse.IsSuccessStatusCode)
                {
                    var facetsResponse = await http.GetStringAsync(url);
                    filteredFacets = JsonConvert.DeserializeObject<ObservableCollection<Facets>>(facetsResponse);
                }
                else
                {
                    MessageDialog errorMsg = new MessageDialog(reponse.ReasonPhrase.ToString());
                    await errorMsg.ShowAsync();
                }
            }
            else
            {
                await noInternetMsg.ShowAsync();
            }
        }
        private async void articlesAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (isInternetConnected)
            {
                results.Clear();
                var search_term = sender.Text.ToLower();
                if (search_term.Length >= 3)
                {
                    if (filteredFacets != null)
                    {
                        foreach (Facets f in filteredFacets)
                        {
                            foreach (Facet item in f.facets)
                            {
                                if (item != null && (item.displayName.ToString().ToLower().StartsWith(search_term.ToLower())))
                                {
                                    results.Add(item);
                                }
                            }
                        }
                        if (results.Count <= 0)
                        {

                            sender.Text = "No Results";
                            sender.IsSuggestionListOpen = false;
                            results.Clear();
                        }
                    }
                }
            }
            else
            {
                await noInternetMsg.ShowAsync();
            }
        }

        private void MaleButton_Click(object sender, RoutedEventArgs e)
        {
            gender = "female";
            MaleButton.IsEnabled = false;
            FemaleButton.IsEnabled = true;
        }

        private void FemaleButton_Click(object sender, RoutedEventArgs e)
        {
            gender = "male";
            FemaleButton.IsEnabled = false;
            MaleButton.IsEnabled = true;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void SuggestedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedFacet = (sender as ListView).SelectedItem as Facet;
            if(selectedFacet.key!=null)
                 searchParameters.key = selectedFacet.key.ToString();
            if (gender == null)
            {
                MessageDialog msg = new MessageDialog("please Select Gender","Gender Selection");
                await msg.ShowAsync();
            }
            else
            {
                searchParameters.gender = gender.ToString();
                Frame.Navigate(typeof(ResultView), searchParameters);

            }
        }

       async private void Page_Loaded(object sender, RoutedEventArgs e)
        {
          await GetFacetsList();
        }
    }
}
