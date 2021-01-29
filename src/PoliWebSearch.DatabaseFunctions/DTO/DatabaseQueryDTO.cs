namespace PoliWebSearch.DatabaseFunctions.DTO
{
    /// <summary>
    /// DTO class for the database query operation
    /// The Query string contians a gremlin query that will be executed on the database
    /// </summary>
    public class DatabaseQueryDTO
    {
        public string Query { get; set; }
    }
}
