using AilosInfra.Bases.Dtos;
using Api.Responses.Hackerrank;
using MediatR;

namespace Api.Requests.Hackerrank
{
    public record HackerrankByTeamRequest : BaseRequest, IRequest<List<HackerrankByTeamResponse>>
    {
        public List<TeamRequest> Teams { get; set; }
    }

    public record TeamRequest
    {
        public int Year { get; set; }
        public string Team { get; set; }
    }
}
