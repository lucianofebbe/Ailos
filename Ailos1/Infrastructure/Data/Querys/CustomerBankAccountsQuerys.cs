namespace Infrastructure.Data.Querys
{
    public static class CustomerBankAccountsQuerys
    {
        public static string Create() =>
            @"INSERT INTO CustomerBankAccounts (Guid, IdCustomer, IdBankAccount, AccountHolder, IdFather, Created, Updated, Deleted)
                VALUES(@Guid, @IdCustomer, @IdBankAccount, @AccountHolder, @IdFather, @Created, @Updated, @Deleted) SELECT SCOPE_IDENTITY();";

        public static string Get() =>
            @"SELECT TOP 1 Id, IdFather, Guid, IdCustomer, IdBankAccount, Created, Updated, Deleted, 
                FROM CustomerBankAccounts ";

        public static string Delete() =>
            throw new NotImplementedException();

        public static string GetAll() =>
            throw new NotImplementedException();

        public static string GetCustomersBankAccountsBankAccounts() =>
            @"SELECT 
                ba.guid AS GuidBankAccounts,
                ba.AccountNumber,
                ba.JointAccount,
                cba.guid AS GuidCustomerBankAccounts,
                cba.AccountHolder
            FROM 
                CustomerBankAccounts cba
            JOIN 
                BankAccounts ba ON ba.Id = cba.IdBankAccount
            WHERE 
                cba.IdCustomer = @IdCustomer;";


        public static string Update() =>
            throw new NotImplementedException();
    }
}
