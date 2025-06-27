using Application.Requests.ValidNewAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.NewAccount
{
    public class NewAccountEndpoints : Iendpoints
    {
        public string Tag => "NewAccount";

        public void Map(WebApplication app)
        {
            app.MapPost("/CreateAccount", async (
                [FromServices] IMediator mediator,
                [FromBody] AccountCreateRequest request) =>
            {
                var result = await mediator.Send(request);
                return Results.Ok(result);
            }).WithTags(Tag);
        }
    }
}
