using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp1.Resources;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.ObjectModel;


namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            // List of match objects, all containing data for a single match.
            ObservableCollection <MatchData> MDataList = new ObservableCollection<MatchData>();
            this.MatchDataList = MDataList;
            // A LongListSelector in the UI uses this as a binded source.
            MatchListUI.ItemsSource = MatchDataList;

        }

        private ObservableCollection<MatchData> MatchDataList { get; set; }

        /// <summary>
        /// Find some data (in JSON array form) about matches, parse it, 
        /// and fill the MatchDataList with relevant parts of it.
        /// 
        /// <return>true if populating was successfull, else false.</return>
        /// </summary>
        public async Task<Boolean> populateMatchDataListFromHttp()
        {
            // Get the actual Json in text from the matchdata page.
            string matchDataAddress = "http://adafyvlstorage.blob.core.windows.net/2014/finland/veikkausliiga/matches";

            string matchesInfo;
            try
            { 
            matchesInfo = await MatchDataFetcher.readCompressedHtmlPageAsync(matchDataAddress);
            } 
            catch(Exception e) // TODO use less generic exception here.
            { 
            StatusBlockUI.Text = "Verkkovirhe. Yritä piakkoin uudestaan.";
            return false;
            }

            // Matches are in an array by default.
            JArray matchArray = JArray.Parse(matchesInfo);

            // TODO actually should clear after checking the received JSON is valid.
            if (MatchDataList != null)
            { 
             MatchDataList.Clear();
            }
            // One dictionary for each match, includes the desired info.
            foreach (JObject jobj in matchArray)
            {           
                MatchData match  = 
                    new MatchData(jobj["HomeTeam"]["Name"].ToString(),
                                               jobj["AwayTeam"]["Name"].ToString(), 
                                               jobj["HomeTeam"]["LogoUrl"].ToString(),
                                               jobj["AwayTeam"]["LogoUrl"].ToString(),
                                               jobj["HomeGoals"].ToString(),
                                               jobj["AwayGoals"].ToString(),
                                               jobj["MatchDate"].ToString());
                MatchDataList.Add(match);
            }
            return true;
        }

        /// <summary>
        /// Updates the match data from the http resource and informs the user about the status of 
        /// this task while doing so.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        public async void buttonPaivita_Click(object sender, RoutedEventArgs e)
        {
           StatusBlockUI.Text = "Haetaan ottelutietoja..."; 
           if (await populateMatchDataListFromHttp())
           {
               StatusBlockUI.Text = ""; 
           }
        }

        /// <summary>
        /// Shows detailed information about a match on the other page when the user clicks an appropriate list item.
        /// A bit hackish solution, as the LongListSelector has no OnClick event.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void MatchListUISelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MatchListUI.SelectedItem == null) { return; }

            MatchData selectedMatchData = (MatchData) MatchListUI.SelectedItem;

            // Uses a global application state dictionary to pass info to the other page.
            PhoneApplicationService.Current.State["SelectedMatchData"] = selectedMatchData;
            NavigationService.Navigate(new Uri("/MatchDetailsPage.xaml", UriKind.Relative));

            MatchListUI.SelectedItem = null;
        }
    }
}