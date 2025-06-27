using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using Application.Profiles.Transactions;
using Application.Requests.Transactions;
using Application.Responses.Transactions;
using AutoMapper;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.AccountsService;
using Domain.Filters.BankAccountsService;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers.Transactions
{
    internal class WithdrawHandler : IRequestHandler<WithdrawRequest, WithdrawResponse>
    {
        private IMapperSpecificFactory<GetBankAccountFilter, WithdrawRequest> _MapperRequest;
        private IBankAccountService _IBankAccountService;
        private IMapperSpecific<GetAccountFilter, BankAccountsDomain> _MapperGetAccount;
        private IMapperSpecificFactory<CreateAccountFilter, GetAccountFilter> _MapperCreateAccount;
        private IMapperSpecific<WithdrawResponse, AccountsDomain> _MapperResponse;
        private IAccountService _IAccountService;
        private IList<Profile> _Profiles;

        public WithdrawHandler(IMapperSpecificFactory<GetBankAccountFilter, WithdrawRequest> mapperRequest,
            IBankAccountService iBankAccountService,
            IMapperSpecific<GetAccountFilter, BankAccountsDomain> mapperGetAccount,
            IMapperSpecificFactory<CreateAccountFilter, GetAccountFilter> mapperCreateAccount,
            IMapperSpecific<WithdrawResponse, AccountsDomain> mapperResponse,
            IAccountService iAccountService,
            IList<Profile> profiles)
        {
            _MapperRequest = mapperRequest;
            _IBankAccountService = iBankAccountService;
            _MapperGetAccount = mapperGetAccount;
            _MapperCreateAccount = mapperCreateAccount;
            _MapperResponse = mapperResponse;
            _IAccountService = iAccountService;
            _Profiles = profiles;
            _Profiles.Add(new WithdrawProfile());
        }
        public async Task<WithdrawResponse> Handle(WithdrawRequest request, CancellationToken cancellationToken)
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
                    var resultCreateAccount = await _IAccountService.WithdrawAsync(resultGetByIdAccount.Item, mapCreateResult);

                    return await _MapperResponse.MapperAsync(resultCreateAccount.Item);
                }
            }
            return new WithdrawResponse();
        }
    }
}
