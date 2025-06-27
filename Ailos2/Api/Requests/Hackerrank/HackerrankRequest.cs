using AilosInfra.Bases.Dtos;
using Api.Responses.Hackerrank;
using MediatR;

namespace Api.Requests.Hackerrank
{
    public record HackerrankRequest : BaseRequest, IRequest<List<HackerrankResponse>>
    {
    }
}
