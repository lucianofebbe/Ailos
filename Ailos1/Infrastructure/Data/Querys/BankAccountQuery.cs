namespace Infrastructure.Data.Querys
{
    public static class BankAccountQuery
    {
        public static string Create() =>
            @"INSERT INTO BankAccounts (Guid, IdFather, JointAccount, AccountNumber, Created, Updated, Deleted)
                VALUES(@Guid, @IdFather, @JointAccount, @AccountNumber, @Created, @Updated, @Deleted) SELECT SCOPE_IDENTITY();";

        public static string Get() =>
            @"SELECT TOP 1 Id, IdFather, Guid, JointAccount, AccountNumber, IdFather, Created, Updated, Deleted FROM BankAccounts ";

        public static string Delete() =>
            throw new NotImplementedException();

        public static string GetAll() =>
            throw new NotImplementedException();


        public static string Update() =>
            throw new NotImplementedException();
    }
}
