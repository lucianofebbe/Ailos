namespace Infrastructure.Data.Parameters.Readers.Get
{
    public record GetCustomerParameter
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
    }
}
