using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace PhoneApp1
{
    /// <summary>
    /// Page for showing details about the match clicked on the main page.
    /// </summary>
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void buttonReturnToMainPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           object receivedData;
           if (PhoneApplicationService.Current.State.TryGetValue("SelectedMatchData", out receivedData))
           {
            MatchData mdata = (MatchData) receivedData;
            MatchDateUI.Text = mdata.MatchDate;
            HomeTeamNameUI.Text = mdata.HomeTeamName;
            AwayTeamNameUI.Text = mdata.AwayTeamName;
            HomeTeamGoalsUI.Text = mdata.HomeGoals;
            AwayTeamGoalsUI.Text = mdata.AwayGoals;
            
            Uri homeTeamLogoUri = new Uri(mdata.HomeTeamLogoAddress, UriKind.Absolute);
            HomeTeamLogoUI.Source = new BitmapImage(homeTeamLogoUri);

            Uri awayTeamLogoUri = new Uri(mdata.AwayTeamLogoAddress, UriKind.Absolute);
            AwayTeamLogoUI.Source = new BitmapImage(awayTeamLogoUri);
           }

        }
    }
}