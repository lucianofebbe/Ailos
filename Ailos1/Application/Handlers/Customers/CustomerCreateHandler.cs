using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using Application.Profiles.Customers;
using Application.Requests.Customers;
using Application.Responses.Customers;
using AutoMapper;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.CustomerService;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers.Customers
{
    public class CustomerCreateHandler : IRequestHandler<CustomerCreateRequest, CustomerCreateResponse>
    {
        private ICustomerService _ICustomerService;
        private IMapperSpecificFactory<CreateCustomerFilter, CustomerCreateRequest> _MapperCreateCustomerFilter;
        private IMapperSpecificFactory<CustomerDomain, CustomerCreateResponse> _MapperCreateCustomerResponse;
        private IList<Profile> _Profiles;

        public CustomerCreateHandler(
            ICustomerService iCustomerService,
            IMapperSpecificFactory<CreateCustomerFilter, CustomerCreateRequest> mapperCreateCustomerFilter,
            IMapperSpecificFactory<CustomerDomain, CustomerCreateResponse> mapperCreateCustomerResponse,
            IList<Profile> profiles)
        {
            _ICustomerService = iCustomerService;
            _MapperCreateCustomerFilter = mapperCreateCustomerFilter;
            _MapperCreateCustomerResponse = mapperCreateCustomerResponse;
            _Profiles = profiles;
        }

        public async Task<CustomerCreateResponse> Handle(CustomerCreateRequest request, CancellationToken cancellationToken)
        {
            _Profiles.Add(new CustomerCreateProfile());
            var mapFilter = await _MapperCreateCustomerFilter.Create(_Profiles);
            var filterCustomer = await mapFilter.MapperAsync(request);
            var resultCreate = await _ICustomerService.CreateAsync(filterCustomer);
            var mapResponse = await _MapperCreateCustomerResponse.Create(_Profiles);
            var result = await mapResponse.MapperAsync(resultCreate.Item);
            return result;
        }
    }
}
