using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using Services.Domain;
using Entitie = Domain.Entities.Sql;

namespace Services.Maps.ContaCorrenteService
{
    public class CreateAsyncMap : IMapperSpecific<ContaCorrente, Entitie.ContaCorrente>
    {
        public async Task<ContaCorrente> MapperAsync(Entitie.ContaCorrente? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return new ContaCorrente(item.Guid, item.NumeroDaConta, item.NomeDoCliente, item.Ativo);
        }

        public Task<List<ContaCorrente>> MapperAsync(List<Entitie.ContaCorrente>? item)
        {
            throw new NotImplementedException();
        }

        public Task<Entitie.ContaCorrente> MapperAsync(ContaCorrente? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<Entitie.ContaCorrente>> MapperAsync(List<ContaCorrente>? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<Entitie.ContaCorrente>> MapperItemToListAsync(ContaCorrente? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContaCorrente>> MapperItemToListAsync(Entitie.ContaCorrente? item)
        {
            throw new NotImplementedException();
        }
    }
}
