namespace Infrastructure.Data.Querys
{
    public static class CustomerQuerys
    {
        public static string Create() =>
            @"INSERT INTO Customers (NameCustomer, CPF, IdFather, Guid, Created, Updated, Deleted)
                VALUES(@NameCustomer, @CPF, @IdFather, @Guid, @Created, @Updated, @Deleted) SELECT SCOPE_IDENTITY();";

        public static string Get() =>
            @"SELECT TOP 1 Id, IdFather, Guid, Created, Updated, Deleted, 
                NameCustomer, CPF FROM Customers ";

        public static string Delete() =>
            throw new NotImplementedException();

        public static string GetAll() =>
            throw new NotImplementedException();


        public static string Update() =>
            throw new NotImplementedException();
    }
}
