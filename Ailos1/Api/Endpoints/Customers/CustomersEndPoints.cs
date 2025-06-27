
using Application.Requests.Customers;
using Application.Requests.ValidNewAccount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Customers
{
    public class CustomersEndPoints : Iendpoints
    {
        public string Tag => "Customers";

        public void Map(WebApplication app)
        {
            app.MapGet("/GetCustomer", async (
                [FromServices] IMediator mediator,
                [FromQuery] string cpf) =>
            {
                var result = await mediator.Send(new ValidNewAccountRequest() { Cpf = cpf });
                return Results.Ok(result);
            }).WithTags(Tag);

            app.MapPost("/CreateCustomer", async (
                [FromServices] IMediator mediator,
                [FromBody] CustomerCreateRequest request) =>
            {
                var result = await mediator.Send(request);
                return Results.Ok(result);
            }).WithTags(Tag);
        }
    }
}
