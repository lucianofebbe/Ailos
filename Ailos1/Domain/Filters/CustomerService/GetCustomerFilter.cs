namespace Domain.Filters.CustomerService
{
    public record GetCustomerFilter
    {
        public Guid Guid { get; set; }
        public string NameCustomer { get; set; }
        public string CPF { get; set; }
    }
}
