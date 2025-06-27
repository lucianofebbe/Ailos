namespace Infrastructure.Apis.Hackerrank.Settings
{
    public record GetHackerrankSettings
    {
        public int Year { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int Page { get; set; }
    }
}
