using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Vem.MyDev.SqlServer.SqlKataExtensions.Attributes;

namespace Vem.MyDev.SqlServer.SqlKataExtensions.Helpers
{
    internal static class Helpers
    {
        internal static (string, string, string) GetJoinParams<T1, T2>(string[] parameters)
        {
            if (parameters.Length != 2)
                throw new Exception("Invalid join statement");

            var targetClassAlias = GetClassAliasName<T2>();
            var joinSourceParam = parameters[0];
            var joinTargetParam = parameters[1];

            var sorceClassRef = GetClassReferenceName<T1>();
            var targetClassRef = GetClassReferenceName<T2>();
            var sourceFieldAlias = GetFieldAliasName<T1>(joinSourceParam);
            var targetFieldAlias = GetFieldAliasName<T2>(joinTargetParam);

            return ($"{targetClassAlias} as {targetClassRef}", $"{sorceClassRef}.{sourceFieldAlias}", $"{targetClassRef}.{targetFieldAlias}");
        }

        internal static string[] GetDoublePredicateNames<T, Y>(Expression<Func<T, Y, object>> predicate)
        {
            var me = (NewExpression)predicate.Body;
            return me.Members!.Select(x => x.Name).ToArray();
        }

        internal static string[] BuildSelectStatementInQuery<T>(Expression<Func<T, object>> predicate)
        {
            var names = GetPredicateNames(predicate);
            return BuildSelectStatement<T>(names).ToArray();
        }

        internal static string[] BuildSelectStatementInQuery<T>(Expression<Func<T, object>> predicate, string aliasName)
        {
            var names = GetPredicateNames(predicate);
            var classNameRef = (typeof(T).GetCustomAttributes(typeof(ClassAliasAttribute), true).FirstOrDefault() as ClassAliasAttribute)?.NameRef;
            var fieldAlias = typeof(T).GetProperty(names[0]).GetCustomAttribute<FieldAliasAttribute>().Alias;
            return new string[] { $"{classNameRef}.{fieldAlias} as {aliasName}" };
        }

        internal static string[] BuildSelectStatementInQuery<T, T1>(Expression<Func<T, object>> predicate, Expression<Func<T1, object>> aliasPredicate)
        {
            var names = GetPredicateNames(predicate);
            var aliasNames = GetPredicateNames(aliasPredicate);
            var classNameRef = (typeof(T).GetCustomAttributes(typeof(ClassAliasAttribute), true).FirstOrDefault() as ClassAliasAttribute)?.NameRef;
            var fieldAlias = typeof(T).GetProperty(names[0]).GetCustomAttribute<FieldAliasAttribute>().Alias;
            var fieldAliasAs = aliasNames[0];
            return new string[] { $"{classNameRef}.{fieldAlias} as {fieldAliasAs}" };
        }

        internal static List<string> BuildSelectStatement<T>(params string[] fields)
        {
            var f = new List<string>();
            var classNameRef = (typeof(T).GetCustomAttributes(typeof(ClassAliasAttribute), true).FirstOrDefault() as ClassAliasAttribute)?.NameRef;
            foreach (var field in fields)
            {
                var fieldAlias = typeof(T).GetProperty(field).GetCustomAttribute<FieldAliasAttribute>().Alias;
                var fieldAliasAs = typeof(T).GetProperty(field).GetCustomAttribute<FieldAliasAttribute>().As;
                f.Add($"{classNameRef}.{fieldAlias} as {fieldAliasAs}");
            }
            return f;
        }

        internal static string[] GetPredicateNames<T>(Expression<Func<T, object>> predicate)
        {
            LambdaExpression lambda = predicate;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)(unaryExpression.Operand);
            }
            else
            {
                memberExpression = (MemberExpression)(lambda.Body);
            }

            var name = ((PropertyInfo)memberExpression.Member).Name;
            return new string[] { name };
        }

        /*
         * Versione nuova
         * */
        //internal static string[] GetPredicateNames<T>(Expression<Func<T, object>> predicate)
        //{
        //    var me = (((UnaryExpression)predicate.Body).Operand as MemberExpression)?.Member as PropertyInfo;
        //    return me.Members.Select(x => x.Name).ToArray();
        //    return new string[] { me.Name };
        //}

        internal static string GetClassAliasName<T>()
        {
            return (typeof(T).GetCustomAttributes(typeof(ClassAliasAttribute), true).FirstOrDefault() as ClassAliasAttribute)?.Alias;
        }

        internal static string GetClassReferenceName<T>()
        {
            return (typeof(T).GetCustomAttributes(typeof(ClassAliasAttribute), true).FirstOrDefault() as ClassAliasAttribute)?.NameRef;
        }

        internal static string GetFieldAliasName<T>(string field)
        {
            return typeof(T).GetProperty(field).GetCustomAttribute<FieldAliasAttribute>().Alias;
        }

        internal static string GetFieldAsRef<T>(string field)
        {
            return typeof(T).GetProperty(field).GetCustomAttribute<FieldAliasAttribute>().As;
        }
    }
}
