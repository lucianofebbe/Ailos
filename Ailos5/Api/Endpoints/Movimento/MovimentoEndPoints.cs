
using Application.Requests.ContaCorrente;
using Application.Requests.Movimento;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Movimento
{
    public class MovimentoEndPoints : Iendpoints
    {
        public string Tag => "Movimentos";

        public void Map(WebApplication app)
        {
            app.MapGet("/ConsultaSaldo", async (
                [FromServices] IMediator mediator,
                [FromQuery] Guid contaCorrente) =>
            {
                var item = new GetSaldoAtualRequest() { NumeroDaConta = contaCorrente };
                var result = await mediator.Send(item);
                return result.Success ? Results.Ok(result) : Results.BadRequest(result);
            }).WithTags(Tag);

            app.MapPost("/MovimentarConta", async (
                [FromServices] IMediator mediator,
                [FromBody] InitMovimentoRequest movimento) =>
            {
                var result = await mediator.Send(movimento);
                return result.Success ? Results.Ok(result) : Results.BadRequest(result);
            }).WithTags(Tag);
        }
    }
}
