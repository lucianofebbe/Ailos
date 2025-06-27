namespace Infrastructure.Entities.Hackerrank
{
    public class FootballMatchesByTeam
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public Datum[] data { get; set; }
    }
}
