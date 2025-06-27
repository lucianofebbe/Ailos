using AilosInfra.Util.TransportsResults;
using Domain.Abstracts.Withdraw.Base;
using Infrastructure.Data.Interfaces.Commands.Create;
using Infrastructure.Data.Parameters.Commands.Create;
using Infrastructure.EntitiesDataBases;

namespace Domain.Abstracts.Withdraw
{
    public class Withdraw2 : ABaseWithdraw
    {
        public Withdraw2(ICreateAccountCommand iCreateAccountCommand) : base(iCreateAccountCommand)
        {
            Tax = 2;
        }

        public override Task<TransportResult<Accounts>> Calc(CreateAccountParameter item)
        {

            var obj = new CreateAccountParameter()
            {
                CurrentBalance = item.CurrentBalance - Tax
            };
            return base.Calc(obj);
        }
    }
}
