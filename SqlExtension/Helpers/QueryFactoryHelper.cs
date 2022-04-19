using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using SqlKata.Compilers;
using SqlKata.Execution;
using Vem.MyDev.SqlServer.SqlKataExtensions.Abstractions;

namespace Vem.MyDev.SqlServer.SqlKataExtensions.Helpers
{
    public class QueryFactoryHelper : IQueryFactoryHelper
    {
        private readonly Dictionary<string, QueryFactory> QueryFactories;

        public QueryFactoryHelper(string connectionString) :
            this(new Dictionary<string, string> { { "default", connectionString } })
        {
        }

        public QueryFactoryHelper(Dictionary<string, string> connectionStrings)
        {
            if (connectionStrings?.Count == 0)
                throw new ArgumentNullException(nameof(connectionStrings));

            QueryFactories = new Dictionary<string, QueryFactory>(connectionStrings.Count, StringComparer.OrdinalIgnoreCase);
            var compiler = new SqlServerCompiler();

            foreach (var item in connectionStrings)
            {
                QueryFactories.Add(item.Key, new QueryFactory(new SqlConnection(item.Value), compiler));
            }
        }

        /// <inheritdoc />
        public QueryFactory Db()
        {
            return QueryFactories?.FirstOrDefault().Value;
        }

        /// <inheritdoc />
        public QueryFactory Db(string connectionKey)
        {
            return QueryFactories.TryGetValue(connectionKey, out var value) ? value : null;
        }
    }
}
