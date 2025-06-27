using AilosInfra.Bases.Dtos;

namespace Api.Responses.Hackerrank
{
    public record HackerrankByTeamResponse : BaseResponse
    {
        public int Year { get; set; }
        public string Team { get; set; }
        public string Result { get; set; }
    }
}
