namespace Services.Filters.MovimentoService
{
    public record GetSaldoAtualFilter
    {
        public Guid NumeroDaConta { get; set; }
    }
}
