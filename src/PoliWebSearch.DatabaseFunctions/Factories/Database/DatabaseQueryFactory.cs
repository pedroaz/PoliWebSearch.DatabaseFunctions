namespace PoliWebSearch.DatabaseFunctions.Factories.Database
{
    public class DatabaseQueryFactory : IDatabaseQueryFactory
    {
        // <inheritdoc/>
        public string CreateGetPersonInformationByCpfQuery(string cpf)
        {
            // TODO: Add code injection verification 
            return $"g.V().has('Cpf','{cpf}')";
        }
    }
}
