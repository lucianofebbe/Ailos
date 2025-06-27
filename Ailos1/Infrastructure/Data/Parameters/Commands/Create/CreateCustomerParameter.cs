namespace Infrastructure.Data.Parameters.Commands.Create
{
    public record CreateCustomerParameter
    {
        public string NameCustomer { get; set; }
        public string CPF { get; set; }
    }
}
