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
        public IList<Dictionary<string,string>> MatchDataList;

        /// <summary>
        /// Find some data about matches and fill the MatchDataList with it.
        /// </summary>
        public void populateMatchDataList()
        {
            // Get the actual Json in text from the matchdata page.
            string matchDataAddress = "http://adafyvlstorage.blob.core.windows.net/2014/finland/veikkausliiga/matches";
            Task<string> matchesInfo = IOandConversion.readCompressedHtmlPage(matchDataAddress);

            string fullMatchinfo = matchesInfo.Result;
            // Matches are in an array by default.
            JArray matchArray = new JArray(fullMatchinfo);

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