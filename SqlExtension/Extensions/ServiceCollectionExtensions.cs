using Microsoft.Extensions.DependencyInjection;

using System.Collections.Generic;
using Vem.MyDev.SqlServer.SqlKataExtensions.Abstractions;
using Vem.MyDev.SqlServer.SqlKataExtensions.Helpers;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSqlKataQueryFactoryHelper(this IServiceCollection services, string connectionString)
    {
        if (connectionString?.Length == 0)
            throw new ArgumentNullException(nameof(connectionString));

        services.AddSingleton<IQueryFactoryHelper, QueryFactoryHelper>(_ => new QueryFactoryHelper(connectionString));

        return services;
    }

    public static IServiceCollection AddSqlKataQueryFactoryHelper(this IServiceCollection services, Dictionary<string, string> connectionStrings)
    {
        if (connectionStrings?.Count == 0)
            throw new ArgumentNullException(nameof(connectionStrings));

        services.AddSingleton<IQueryFactoryHelper, QueryFactoryHelper>(_ => new QueryFactoryHelper(connectionStrings));

        return services;
    }
}
