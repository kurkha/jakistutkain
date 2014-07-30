using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp1
{
    /// <summary>
    /// Class containing relevant data about a single match.
    /// </summary>
    class MatchData
    {
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }

        public string HomeTeamLogoAddress { get; set; }

        public string AwayTeamLogoAddress { get; set; }

        public string HomeGoals { get; set; }

        public string AwayGoals { get; set; }

        public string MatchDate { get; set; }

        public MatchData(string homeTeamName, string awayTeamName, string homeTeamLogoAddress,
                        string awayTeamLogoAddress, string homeGoals, string awayGoals,
                        string matchdate)
        {
            this.HomeTeamName = homeTeamName;
            this.AwayTeamName = awayTeamName;
            this.HomeTeamLogoAddress = homeTeamLogoAddress;
            this.AwayTeamLogoAddress = awayTeamLogoAddress;
            this.HomeGoals = homeGoals;
            this.AwayGoals = awayGoals;
            this.MatchDate = matchdate;
        }
    
    }

}
