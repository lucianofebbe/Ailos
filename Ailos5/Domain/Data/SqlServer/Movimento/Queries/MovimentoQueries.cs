namespace Domain.Data.SqlServer.Movimento.Queries
{
    public static class MovimentoQueries
    {
        public static string Create() =>
            @"INSERT INTO [dbo].[Movimento] ([IdFather], [Guid], [Created], [Updated], [Deleted], 
                [IdContaCorrente], [DataMovimento], [TipoMovimento], [Valor]
            )
            VALUES (@IdFather, @Guid, @Created, @Updated, @Deleted, @IdContaCorrente, @DataMovimento, 
                @TipoMovimento, @Valor);";

        public static string Update() =>
            throw new NotImplementedException();

        public static string Get() =>
            throw new NotImplementedException();

        public static string Delete() =>
            throw new NotImplementedException();

        public static string GetAll() =>
            throw new NotImplementedException();

        public static string GetByIdContaCorrente() =>
            @"SELECT TOP 1 
                        Id,
                        IdFather,
                        Guid,
                        Created,
                        Updated,
                        Deleted,
                        IdContaCorrente,
                        DataMovimento,
                        TipoMovimento,
                        Valor
                    FROM Movimento
                    WHERE IdContaCorrente = @IdContaCorrente
                    ORDER BY Id ASC;";
    }
}
