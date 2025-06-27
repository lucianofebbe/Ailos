namespace Domain.Filters.CustomerService
{
    public record CreateCustomerFilter
    {
        public Guid Guid { get; set; }
        public string NameCustomer { get; set; }
        public string CPF { get; set; }
    }
}
