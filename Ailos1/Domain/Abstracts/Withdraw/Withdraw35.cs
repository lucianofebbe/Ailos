using AilosInfra.Util.TransportsResults;
using Domain.Abstracts.Withdraw.Base;
using Infrastructure.Data.Interfaces.Commands.Create;
using Infrastructure.Data.Parameters.Commands.Create;
using Infrastructure.EntitiesDataBases;

namespace Domain.Abstracts.Withdraw
{
    public class Withdraw35 : ABaseWithdraw
    {
        public Withdraw35(ICreateAccountCommand iCreateAccountCommand) : base(iCreateAccountCommand)
        {
            Tax = 3.5m;
        }

        public override Task<TransportResult<Accounts>> Calc(CreateAccountParameter item)
        {
            Tax = 3.5m;
            var obj = new CreateAccountParameter()
            {
                CurrentBalance = item.CurrentBalance - Tax,
                IdBankAccount = item.IdBankAccount,
                IdFather = item.IdFather,
            };
            return base.Calc(obj);
        }
    }
}
