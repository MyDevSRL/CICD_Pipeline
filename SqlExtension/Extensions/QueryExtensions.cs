using System.Linq.Expressions;
using System.Reflection;
using Vem.MyDev.SqlServer.SqlKataExtensions.Attributes;
using Vem.MyDev.SqlServer.SqlKataExtensions.Helpers;

namespace SqlKata
{
    public static class QueryExtensions
    {
        public static Query Select<T>(this Query q, Expression<Func<T, object>> predicate)
        {
            var selectStatements = Helpers.BuildSelectStatementInQuery(predicate);
            q.Select(selectStatements);

            return q;
        }

        //public static Query Select<T>(this Query q, Expression<Func<T, object>> predicate)
        //{
        //    var selectStatements = Helpers.BuildSelectStatementInQuery(predicate);
        //    q.Select(selectStatements);

        //    Func<int, int, bool> sum = (a, b) => a + b > 0;
        //    var s = sum(1, 2);

        //    return q;
        //}

        public static Query Select<T>(this Query q, Expression<Func<T, object>> predicate, string aliasName)
        {
            var selectStatements = Helpers.BuildSelectStatementInQuery<T>(predicate, aliasName);
            q.Select(selectStatements);

            return q;
        }

        public static Query Select<T, T1>(this Query q, Expression<Func<T, object>> predicate, Expression<Func<T1, object>> aliasPredicate)
        {
            var selectStatements = Helpers.BuildSelectStatementInQuery(predicate, aliasPredicate);
            q.Select(selectStatements);

            return q;
        }

        //public static Query Select<T>(this Query q, Expression<Func<T, object>> predicate)
        //{
        //    var selectStatements = Helpers.BuildSelectStatementInQuery(predicate);
        //    q.Select(selectStatements);

        //    return q;
        //}

        public static Query LeftJoin<T1, T2>(this Query q, Expression<Func<T1, T2, object>> predicate)
        {
            var parameters = Helpers.GetDoublePredicateNames(predicate);
            var res = Helpers.GetJoinParams<T1, T2>(parameters);
            q.LeftJoin(res.Item1, res.Item2, res.Item3);

            return q;
        }

        public static Query Join<T1, T2>(this Query q, Expression<Func<T1, T2, object>> predicate)
        {
            var parameters = Helpers.GetDoublePredicateNames<T1, T2>(predicate);
            var res = Helpers.GetJoinParams<T1, T2>(parameters);
            q.Join(res.Item1, res.Item2, res.Item3);

            return q;
        }

        //public static Query Join<T1, T2>(this Query q, Expression<Func<T1, object>> p1, Expression<Func<T2, object>> p2)
        //{
        //    var parameters = Helpers.GetPredicateNames<T1>(p1).Concat(Helpers.GetPredicateNames<T2>(p2));
        //    var res = Helpers.GetJoinParams<T1, T2>(parameters);
        //    q.Join(res.Item1, res.Item2, res.Item3);

        //    return q;
        //}

        public static Query Where<T>(this Query q, Expression<Func<T, object>> predicate, string op, object parameter)
        {
            var parameters = Helpers.GetPredicateNames(predicate);
            var classNameRef = Helpers.GetClassReferenceName<T>();
            var field = parameters[0];
            var fieldAlias = typeof(T).GetProperty(field)!.GetCustomAttribute<FieldAliasAttribute>().Alias;

            q.Where($"{classNameRef}.{fieldAlias}", op, parameter);

            return q;
        }
    }
}
