namespace Domain.Data.SqlServer.ContaCorrente.Queries
{
    public static class ContaCorrenteQueries
    {
        public static string Create() =>
            @"INSERT INTO [dbo].[ContaCorrente] (
                [IdFather], [Guid], [Created], [Updated], [Deleted], [NumeroDaConta], [NomeDoCliente], [Ativo])
            VALUES (
                @IdFather, @Guid, @Created, @Updated, @Deleted, @NumeroDaConta, @NomeDoCliente, @Ativo);";

        public static string Update() =>
            @"UPDATE [dbo].[ContaCorrente] SET
                [IdFather] = @IdFather,
                [Updated] = @Updated,
                [Deleted] = @Deleted,
                [NomeDoCliente] = @NomeDoCliente,
                [Ativo] = @Ativo
                WHERE [Id] = @Id;";

        public static string Get() =>
            throw new NotImplementedException();

        public static string Delete() =>
            throw new NotImplementedException();

        public static string GetAll() =>
            throw new NotImplementedException();
    }
}
