namespace Infrastructure.Data.Querys
{
    public static class AccountsQuerys
    {
        public static string Create()=>
            @"INSERT INTO
                Accounts (Guid, IdFather, IdBankAccount, CurrentBalance, Created, Updated, Deleted)
                VALUES (@Guid, @IdFather, @IdBankAccount, @CurrentBalance, @Created, @Updated, @Deleted) SELECT SCOPE_IDENTITY()";

        public static string Get()=>
            @"SELECT TOP 1 Id, IdFather, Guid, Created, Updated, Deleted, 
                CurrentBalance FROM Accounts";

        public static string Delete() =>
            throw new NotImplementedException();

        public static string GetAll() =>
            throw new NotImplementedException();


        public static string Update()=>
            throw new NotImplementedException();
    }
}
