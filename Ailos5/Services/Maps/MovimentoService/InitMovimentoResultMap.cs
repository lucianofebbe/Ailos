using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using EntitieDomain = Domain.Entities.Sql;
using EntitieServices = Services.Domain;

namespace Services.Maps.MovimentoService
{
    public class InitMovimentoResultMap : IMapperSpecific<EntitieServices.Movimento, EntitieDomain.Movimento>
    {
        public async Task<EntitieServices.Movimento> MapperAsync(EntitieDomain.Movimento? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return new EntitieServices.Movimento(item.Guid, item.DataMovimento, item.TipoMovimento, item.Valor);
        }

        public Task<List<EntitieServices.Movimento>> MapperAsync(List<EntitieDomain.Movimento>? item)
        {
            throw new NotImplementedException();
        }

        public Task<EntitieDomain.Movimento> MapperAsync(EntitieServices.Movimento? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<EntitieDomain.Movimento>> MapperAsync(List<EntitieServices.Movimento>? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<EntitieDomain.Movimento>> MapperItemToListAsync(EntitieServices.Movimento? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<EntitieServices.Movimento>> MapperItemToListAsync(EntitieDomain.Movimento? item)
        {
            throw new NotImplementedException();
        }
    }
}
