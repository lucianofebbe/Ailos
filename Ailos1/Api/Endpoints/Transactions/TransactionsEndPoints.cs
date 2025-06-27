using Application.Requests.Transactions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Transactions
{
    public class transactionsEndPoints : Iendpoints
    {
        public string Tag => "Transactions";

        public void Map(WebApplication app)
        {
            app.MapPost("/CreateDeposit", async (
                [FromServices] IMediator mediator,
                [FromBody] NewDepositRequest request) =>
            {
                var result = await mediator.Send(request);
                return Results.Ok(result);
            }).WithTags(Tag);
             
            app.MapPost("/CreateWithdraw", async (
                [FromServices] IMediator mediator,
                [FromBody] WithdrawRequest request) =>
            {
                var result = await mediator.Send(request);
                return Results.Ok(result);
            }).WithTags(Tag);
        }
    }
}
