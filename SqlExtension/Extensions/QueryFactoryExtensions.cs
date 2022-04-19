using Vem.MyDev.SqlServer.SqlKataExtensions.Helpers;

namespace SqlKata.Execution
{
    public static class QueryFactoryExtensions
    {
        public static Query Query<T>(this QueryFactory qf)
        {
            var classAlias = Helpers.GetClassAliasName<T>();
            var classRef = Helpers.GetClassReferenceName<T>();

            return qf.Query($"{classAlias} as {classRef}");
        }
    }
}
