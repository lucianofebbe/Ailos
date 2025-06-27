
using Api.Requests.Hackerrank;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.EndPoints.Hackerrank
{
    public class HackerrankEndPoints : Iendpoints
    {
        public string Tag => "Hackerrank";

        public void Map(WebApplication app)
        {
            app.MapGet("/HackerrankGetAll", async (
                [FromServices] IMediator mediator) =>
            {
                var result = await mediator.Send(new HackerrankRequest());
                return Results.Ok(result);
            }).WithTags(Tag);

            app.MapPost("/HackerrankGet", async (
                [FromServices] IMediator mediator,
                [FromBody] HackerrankByTeamRequest request) =>
            {
                var result = await mediator.Send(request);
                return Results.Ok(result);
            }).WithTags(Tag);
        }
    }
}
