namespace Domain.EntitiesDomain.Hackerrank
{
    public record HackerrankDomain
    {
        public string Competition { get; private set; }
        public int Year { get; private set; }
        public string Round { get; private set; }
        public string Team1 { get; private set; }
        public string Team2 { get; private set; }
        public string Team1Goals { get; private set; }
        public string Team2Goals { get; private set; }

        public HackerrankDomain(
            string competition,
            int year,
            string round,
            string team1,
            string team2,
            string team1Goals,
            string team2Goals)
        {
            SetCompetition(competition);
            SetYear(year);
            SetRound(round);
            SetTeam1(team1);
            SetTeam2(team2);
            SetTeam1Goals(team1Goals);
            SetTeam2Goals(team2Goals);
        }

        public void SetCompetition(string competition)
        {
            Competition = competition;
        }

        public void SetYear(int year)
        {
            Year = year;
        }

        public void SetRound(string round)
        {
            Round = round;
        }

        public void SetTeam1(string team1)
        {
            Team1 = team1;
        }

        public void SetTeam2(string team2)
        {
            Team2 = team2;
        }

        public void SetTeam1Goals(string team1Goals)
        {
            Team1Goals = team1Goals;
        }

        public void SetTeam2Goals(string team2Goals)
        {
            Team2Goals = team2Goals;
        }
    }
}
