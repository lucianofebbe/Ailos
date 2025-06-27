namespace Domain.EntitiesDomain.Hackerrank
{
    public class HackerrankDomainByTeam
    {
        public int Year { get; private set; }
        public string Team { get; private set; }
        public string Result { get; private set; }

        public HackerrankDomainByTeam(int year, string team)
        {
            SetYear(year);
            SetTeam(team);
        }

        public HackerrankDomainByTeam(int year, string team, string result)
        {
            SetYear(year);
            SetTeam(team);
            SetResult(result);
        }

        public void SetYear(int year)
        {
            Year = year;
        }

        public void SetTeam(string team)
        {
            Team = team;
        }

        public void SetResult(string result)
        {
            Result = result;
        }
    }
}
