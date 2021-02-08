namespace PoliWebSearch.DatabaseFunctions.Factories.Database
{
    public interface IDatabaseQueryFactory
    {
        /// <summary>
        /// Create query to search for person information given the request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        string CreateGetPersonInformationByCpfQuery(string cpf);

        /// <summary>
        /// Create query to search all persons with the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string CreateSearchPersonByNameQuery(string name, int maxAmountOfResult);
    }
}
