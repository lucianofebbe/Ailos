using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using Domain.EntitiesDomains.Sigles;
using Infrastructure.EntitiesDataBases;

namespace Domain.Map.CustomerService
{
    public class ResponseMapper : IMapperSpecific<CustomerDomain, Customers>
    {
        public async Task<CustomerDomain> MapperAsync(Customers? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return new CustomerDomain(item.Id, item.Guid, item.NameCustomer, item.CPF);
        }

        public async Task<List<CustomerDomain>> MapperAsync(List<Customers>? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var result = new List<CustomerDomain>();
            foreach (var customer in item)
                result.Add(new CustomerDomain(customer.Id, customer.Guid, customer.NameCustomer, customer.CPF));

            return result;
        }

        public async Task<Customers> MapperAsync(CustomerDomain? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Customers>> MapperAsync(List<CustomerDomain>? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<Customers>> MapperItemToListAsync(CustomerDomain? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<CustomerDomain>> MapperItemToListAsync(Customers? item)
        {
            throw new NotImplementedException();
        }
    }
}
