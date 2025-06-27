using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using Application.Profiles.NewDeposit;
using Application.Requests.NewDeposit;
using Application.Responses.NewDeposit;
using AutoMapper;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.AccountsService;
using Domain.Filters.BankAccountsService;
using Domain.Interfaces;
using Domain.Services;
using MediatR;

namespace Application.Handlers.Transactions
{
    public class NewDepositHandler : IRequestHandler<NewDepositRequest, NewDepositResponse>
    {
        private IMapperSpecificFactory<GetBankAccountFilter, NewDepositRequest> _MapperRequest;
        private IBankAccountService _IBankAccountService;
        private IMapperSpecific<GetAccountFilter, BankAccountsDomain> _MapperGetAccount;
        private IMapperSpecificFactory<CreateAccountFilter, GetAccountFilter> _MapperCreateAccount;
        private IAccountService _IAccountService;
        private IList<Profile> _Profiles;

        public NewDepositHandler(
            IMapperSpecificFactory<GetBankAccountFilter, NewDepositRequest> mapperRequest,
            IBankAccountService iBankAccountService,
            IMapperSpecific<GetAccountFilter, BankAccountsDomain> mapperGetAccount,
            IMapperSpecificFactory<CreateAccountFilter, GetAccountFilter> mapperCreateAccount,
            IAccountService iAccountService,
            IList<Profile> profiles)
        {
            _MapperRequest = mapperRequest;
            _IBankAccountService = iBankAccountService;
            _MapperGetAccount = mapperGetAccount;
            _MapperCreateAccount = mapperCreateAccount;
            _IAccountService = iAccountService;
            _Profiles = profiles;
            _Profiles.Add(new NewDepositProfile());
        }

        public async Task<NewDepositResponse> Handle(NewDepositRequest request, CancellationToken cancellationToken)
        {
            var mapRequest = await _MapperRequest.Create(_Profiles);
            var mapRequestResult = await mapRequest.MapperAsync(request);
            var resultGetBankAccount = await _IBankAccountService.GetByAccountNumberAsync(mapRequestResult);
            if (resultGetBankAccount.Success)
            {
                var mapGetAccount = await _MapperGetAccount.MapperAsync(resultGetBankAccount.Item);
                var resultGetByIdAccount = await _IAccountService.GetByIdBankAccountAsync(mapGetAccount);

                if (resultGetByIdAccount.Success)
                {
                    var mapCreateAccount = await _MapperCreateAccount.Create(_Profiles);
                    var mapCreateResult = await mapCreateAccount.MapperAsync(mapGetAccount);
                    mapCreateResult.CurrentBalance = request.Value;
                    var resultCreateAccount = await _IAccountService.DepositAsync(resultGetByIdAccount.Item, mapCreateResult);
                    return new NewDepositResponse() { Deposited = resultCreateAccount.Success };
                }
            }
            return new NewDepositResponse() { Deposited = false };
        }
    }
}
