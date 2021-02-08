namespace PoliWebSearch.DatabaseFunctions.Factories.Database
{
    public class DatabaseQueryFactory : IDatabaseQueryFactory
    {
        // <inheritdoc/>
        public string CreateGetPersonInformationByCpfQuery(string cpf)
        {
            return $"g.V().has('Cpf','{cpf}')";
        }

        // <inheritdoc/>
        public string CreateSearchPersonByNameQuery(string name, int maxAmountOfResult)
        {
            return $"g.V().has('person', 'Names', containing('{name.ToUpper()}')).limit({maxAmountOfResult})";
        }
    }
}
