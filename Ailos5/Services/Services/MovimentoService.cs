using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Util.TransportsResults;
using AutoMapper;
using Domain.Data.SqlServer.Movimento.Interfaces.Readers;
using Domain.Data.SqlServer.Movimento.Parameters.Readers;
using Domain.Data.SqlServer.Movimento.Readers;
using Services.Filters.ContaCorrenteService;
using Services.Filters.MovimentoService;
using Services.Interfaces.ContaCorrenteService;
using Services.Interfaces.MovimentoService;
using Services.Profiles.MovimentoService;
using EntitieServices = Services.Domain;
using EntitieDomain = Domain.Entities.Sql;
using Domain.Data.SqlServer.Movimento.Commands;
using Domain.Data.SqlServer.Movimento.Interfaces.Commands;
using Domain.Data.SqlServer.Movimento.Parameters.Commands;
using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
namespace Services.Services
{
    public class MovimentoService : IMovimentoService
    {
        //GetSaldoAtualAsync
        //InitMovimentoAsync
        private IContaCorrenteService _IContaCorrenteService;

        //GetSaldoAtualAsync
        private IMapperSpecificFactory<GetSaldoAtualFilter, GetContaCorrenteFilter> _MapperGetSaldoAtualFilter;
        private IMovimentoByIdContaCorrente _IMovimentoByIdContaCorrente;
        private IMapperSpecificFactory<MovimentoGetByIdContaCorrenteParameter, EntitieServices.ContaCorrente> _MapperContaCorrenteGetByIdContaCorrenteParameter;
        private IMapperSpecificFactory<GetSaldoAtualFilter, InitMovimentoFilter> _MapperValidaSaldoParaDebito;

        //InitMovimentoAsync
        private IMapperSpecificFactory<InitMovimentoFilter, GetContaCorrenteFilter> _MapperInitMovimentoFilter;
        private IMapperSpecificFactory<EntitieServices.ContaCorrente, UltimoMovimentoGetByIdContaCorrenteParameter> _MapperContaCorrenteToFilterUltimoMovimento;
        private IUltimoMovimentoByIdContaCorrente _IUltimoMovimentoByIdContaCorrente;
        private IMovimentoCreate _IMovimentoCreate;
        private IMapperSpecific<EntitieServices.Movimento, EntitieDomain.Movimento> _MapperResultInitMovimento;

        private IList<Profile> _Profiles;

        public MovimentoService(
            IContaCorrenteService iContaCorrenteService,
            IMapperSpecificFactory<GetSaldoAtualFilter, GetContaCorrenteFilter> mapperGetSaldoAtualFilter,
            IMovimentoByIdContaCorrente iMovimentoByIdContaCorrente,
            IMapperSpecificFactory<MovimentoGetByIdContaCorrenteParameter, EntitieServices.ContaCorrente> mapperContaCorrenteGetByIdContaCorrenteParameter,
            IMapperSpecificFactory<InitMovimentoFilter, GetContaCorrenteFilter> mapperInitMovimentoFilter,
            IUltimoMovimentoByIdContaCorrente iUltimoMovimentoByIdContaCorrente,
            IMapperSpecificFactory<EntitieServices.ContaCorrente, UltimoMovimentoGetByIdContaCorrenteParameter> mapperContaCorrenteToFilterUltimoMovimento,
            IMapperSpecificFactory<GetSaldoAtualFilter, InitMovimentoFilter> mapperValidaSaldoParaDebito,
            IMovimentoCreate iMovimentoCreate,
            IMapperSpecific<EntitieServices.Movimento, EntitieDomain.Movimento> mapperResultInitMovimento,
            IList<Profile> profiles)
        {
            _IContaCorrenteService = iContaCorrenteService;
            _MapperGetSaldoAtualFilter = mapperGetSaldoAtualFilter;
            _IMovimentoByIdContaCorrente = iMovimentoByIdContaCorrente;
            _MapperContaCorrenteGetByIdContaCorrenteParameter = mapperContaCorrenteGetByIdContaCorrenteParameter;
            _MapperInitMovimentoFilter = mapperInitMovimentoFilter;
            _MapperContaCorrenteToFilterUltimoMovimento = mapperContaCorrenteToFilterUltimoMovimento;
            _IUltimoMovimentoByIdContaCorrente = iUltimoMovimentoByIdContaCorrente;
            _MapperValidaSaldoParaDebito = mapperValidaSaldoParaDebito;
            _IMovimentoCreate = iMovimentoCreate;
            _MapperResultInitMovimento = mapperResultInitMovimento;
            _Profiles = profiles;
        }

        public async Task<TransportResult<EntitieServices.Movimento>> GetSaldoAtualAsync(GetSaldoAtualFilter item)
        {
            _Profiles.Add(new GetSaldoAtualFilterProfile());
            var facMapperGetSaldoAtualFilter = await _MapperGetSaldoAtualFilter.Create(_Profiles);
            var parameterGetContaCorrenteAsync = await facMapperGetSaldoAtualFilter.MapperAsync(item);
            var contaCorrente = await _IContaCorrenteService.GetContaCorrenteAsync(parameterGetContaCorrenteAsync);
            if (contaCorrente.Success)
            {
                var facMapperContaCorrenteGetByIdContaCorrenteParameter = await _MapperContaCorrenteGetByIdContaCorrenteParameter.Create(_Profiles);
                var parameterGetByIdContaCorrente = await facMapperContaCorrenteGetByIdContaCorrenteParameter.MapperAsync(contaCorrente.Item);
                var movimentos = await _IMovimentoByIdContaCorrente.GetByIdContaCorrente(parameterGetByIdContaCorrente);

                if (movimentos.Success)
                {
                    var creditos = movimentos.Item.Where(o => o.TipoMovimento == 'c').Sum(o => o.Valor);
                    var debitos = movimentos.Item.Where(o => o.TipoMovimento == 'd').Sum(o => o.Valor);
                    var saldo = debitos - creditos;
                    var result = new EntitieServices.Movimento(DateTime.UtcNow, saldo);
                    return TransportResult<EntitieServices.Movimento>.Create(result);
                }
                return TransportResult<EntitieServices.Movimento>.Create(null, notFoundMessage: "Nao Foram encontrados movimentos");
            }
            return TransportResult<EntitieServices.Movimento>.Create(null, notFoundMessage: contaCorrente.Message);
        }

        public async Task<TransportResult<EntitieServices.Movimento>> InitMovimentoAsync(InitMovimentoFilter item)
        {
            _Profiles.Add(new InitMovimentoFilterProfile());

            if (item.TipoDeMovimento == 'c' && item.Valor <= 0)
                return TransportResult<EntitieServices.Movimento>.Create(null, notFoundMessage: "Para operacoes de credito, somente valores positivos");
            if (item.TipoDeMovimento == 'd')
            {
                var facParameter = await _MapperValidaSaldoParaDebito.Create(_Profiles);
                var parameter = await facParameter.MapperAsync(item);
                var saldoCorrente = await GetSaldoAtualAsync(parameter);
                if (saldoCorrente.Success)
                {
                    if (saldoCorrente.Item.Valor < 0 || (item.Valor - saldoCorrente.Item.Valor) < 0)
                        return TransportResult<EntitieServices.Movimento>.Create(null, notFoundMessage: "Saldo insuficiente.");
                }
            }

            if (item.TipoDeMovimento == 'c' || item.TipoDeMovimento == 'd')
            {
                var facMapperGetSaldoAtualFilter = await _MapperInitMovimentoFilter.Create(_Profiles);
                var parameterGetContaCorrenteAsync = await facMapperGetSaldoAtualFilter.MapperAsync(item);
                var contaCorrente = await _IContaCorrenteService.GetContaCorrenteAsync(parameterGetContaCorrenteAsync);
                if (contaCorrente.Success)
                {
                    var facFilterUltimoMovimento = await _MapperContaCorrenteToFilterUltimoMovimento.Create(_Profiles);
                    var parameterUltimoMovimento = await facFilterUltimoMovimento.MapperAsync(contaCorrente.Item);
                    var ultimaMovimentacao = await _IUltimoMovimentoByIdContaCorrente.GetByIdUltimoMovimentoContaCorrente(parameterUltimoMovimento);
                    var novoMovimento = new MovimentoCreateParameter()
                    {
                        DataMovimento = DateTime.UtcNow,
                        IdContaCorrente = contaCorrente.Item.Id,
                        IdFather = ultimaMovimentacao.Success ? ultimaMovimentacao.Item.Id : 0,
                        TipoMovimento = item.TipoDeMovimento,
                        Valor = item.Valor
                    };
                    var result = await _IMovimentoCreate.CreateAsync(novoMovimento);
                    if (result.Success)
                    {
                        var response = await _MapperResultInitMovimento.MapperAsync(result.Item);
                        response.SetTipoMovimento(novoMovimento.TipoMovimento);
                        response.SetValor(novoMovimento.Valor);
                        response.SetDataMovimento(novoMovimento.DataMovimento);

                        return TransportResult<EntitieServices.Movimento>.Create(response);
                    }
                    return TransportResult<EntitieServices.Movimento>.Create(null, notFoundMessage: "Ocorreu um erro ao fazer o movimento, tente denovo");
                }
                return TransportResult<EntitieServices.Movimento>.Create(null, notFoundMessage: contaCorrente.Message);
            }
            return TransportResult<EntitieServices.Movimento>.Create(null, notFoundMessage: "Somente operacoes de Credito 'c' ou debito 'd' em TipoDeMovimento;");
        }
    }
}
