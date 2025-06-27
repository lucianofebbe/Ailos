using AilosInfra.Util.TransportsResults;
using Domain.Interfaces;
using Infrastructure.Data.Interfaces.Commands.Create;
using Infrastructure.Data.Parameters.Commands.Create;
using Infrastructure.EntitiesDataBases;

namespace Domain.Abstracts.Withdraw.Base
{
    public abstract class ABaseWithdraw
    {
        private ICreateAccountCommand _ICreateAccountCommand;
        public decimal Tax { get; protected set; }

        public ABaseWithdraw(ICreateAccountCommand iCreateAccountCommand)
        {
            _ICreateAccountCommand = iCreateAccountCommand;
        }
        public virtual async Task<TransportResult<Accounts>> Calc(CreateAccountParameter item)
        {
            var result = await _ICreateAccountCommand.CreateAsync(item);
            if (result.Success)
            {
                result.Item.CurrentBalance = item.CurrentBalance;
                return result;
            }
            return TransportResult<Accounts>.Create(null);
        }
    }
}
