namespace Services.Filters.ContaCorrenteService
{
    public record GetContaCorrenteFilter
    {
        public Guid NumeroDaConta { get; set; }
        public bool? IncluirAtivas { get; set; } = true;
        public bool? IncluirDeletadas { get; set; } = false;
    }
}
