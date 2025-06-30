
using Application.Requests.ContaCorrente;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.ContaCorrente
{
    public class ContaCorrenteEndPoints : Iendpoints
    {
        public string Tag => "Conta Corrente";

        public void Map(WebApplication app)
        {
            app.MapPost("/CadastraCliente", async (
                [FromServices] IMediator mediator,
                [FromBody] ContaCorrenteCreateRequest request) =>
            {
                var result = await mediator.Send(request);
                return Results.Ok(result);
            }).WithTags(Tag);
        }
    }
}
