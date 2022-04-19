using SqlKata.Execution;

namespace Vem.MyDev.SqlServer.SqlKataExtensions.Abstractions
{
    public interface IQueryFactoryHelper
    {
        /// <summary>
        /// A warapper utility to use the <c>QueryFactory</c> object of SqlKata.
        /// </summary>
        /// <returns>The <c>QueryFactory</c> object related to the default DB.</returns>
        public QueryFactory Db();

        /// <summary>
        /// A warapper utility to use the <c>QueryFactory</c> object of SqlKata.
        /// </summary>
        /// <param name="dbName">The key of the connection strings dictionary, that stores all the connection strings for each DB; taken by configuration.</param>
        /// <returns>The <c>QueryFactory</c> object related to the specified DB.</returns>
        public QueryFactory Db(string dbName);
    }
}
