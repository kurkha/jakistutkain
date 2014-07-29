using System;
using System.IO;
using Windows.Storage;
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


namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // List of match objects, all containing data for a single match.
        public List<Dictionary<string, string>> MatchDataList = new List<Dictionary<string,string>>();

        /// <summary>
        /// Find some data about matches and fill the MatchDataList with it.
        /// </summary>
        public async void populateMatchDataListFromHttp()
        {
            // Get the actual Json in text from the matchdata page.
            string matchDataAddress = "http://adafyvlstorage.blob.core.windows.net/2014/finland/veikkausliiga/matches";
            string matchesInfo = await IOandConversion.readCompressedHtmlPageAsync(matchDataAddress);
            
            // Matches are in an array by default.
            JArray matchArray = JArray.Parse(matchesInfo);

            // TODO actually should clear after checking the received JSON is valid.
            MatchDataList.Clear();

            // One dictionary for each match, includes the desired info.
            foreach (JObject jobj in matchArray)
            {
                Dictionary<string, string> dic  = new Dictionary<string, string>();

                dic.Add("matchID", jobj["Id"].ToString());
                dic.Add("matchDate", jobj["MatchDate"].ToString());
                dic.Add("homeTeamName", jobj["HomeTeam"]["Name"].ToString());
                dic.Add("awayTeamName", jobj["AwayTeam"]["Name"].ToString());
                dic.Add("homeGoals", jobj["HomeGoals"].ToString());
                dic.Add("awayGoals", jobj["AwayGoals"].ToString());
                dic.Add("homeTeamLogoAddress", jobj["HomeTeam"]["LogoUrl"].ToString());
                dic.Add("awayTeamLogoAddress", jobj["AwayTeam"]["LogoUrl"].ToString());
                MatchDataList.Add(dic);
            }
        }

        /// <summary>
        /// Same as populateMatchDataListFromHttp(), except that the matchdata is
        /// read from a file.
        /// </summary>
        /*
        public async void populateMatchDataListFromFile()
        {


            var fpath = (@"C:\Users\Kari\Documents\Visual Studio 2013\Projects\App1\PhoneApp1");

            StorageFolder matchDataFolder = StorageFolder.GetFolderFromPathAsync(fpath);

            var file = dir.OpenStreamForReadAsync(fpath);
            var streamReader = new StreamReader(file);
                
            string fullMatchInfo = streamReader.ReadToEnd();
                
            // Matches are in an array by default.
            JArray matchArray = new JArray(fullMatchInfo);

            // TODO actually should clear after checking the received JSON is valid.
            MatchDataList.Clear();

            // One dictionary for each match, includes the desired info.
            foreach (JObject jobj in matchArray)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("matchID", jobj["Id"].ToString());
                dic.Add("matchDate", jobj["MatchDate"].ToString());
                dic.Add("homeTeamName", jobj["HomeTeam"]["Name"].ToString());
                dic.Add("awayTeamName", jobj["AwayTeam"]["Name"].ToString());
                dic.Add("homeGoals", jobj["HomeGoals"].ToString());
                dic.Add("awayGoals", jobj["AwayGoals"].ToString());
                dic.Add("homeTeamLogoAddress", jobj["HomeTeam"]["LogoUrl"].ToString());
                dic.Add("awayTeamLogoAddress", jobj["AwayTeam"]["LogoUrl"].ToString());
                MatchDataList.Add(dic);
            }

        }

         */
 
        public async void buttonPaivita_Click(object sender, RoutedEventArgs e)
        {
           SearchingBlock.Text = "Haetaan ottelutietoja..."; 
           populateMatchDataListFromHttp();
           SearchingBlock.Text = "";
           updateMatchDataListUI();
        }

        private void updateMatchDataListUI()
        {
            foreach (Dictionary<string,string> match in MatchDataList)
            {
            TextBlock matchBlock = new TextBlock();
            matchBlock.Margin = new Thickness(0,5,0,0);
            matchBlock.Text = match["homeTeamName"] + " -- " + match["awayTeamName"] 
                + " : " + match["homeGoals"] + " -- " + match["awayGoals"];

            MatchPanel.Children.Add(matchBlock);
            }
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