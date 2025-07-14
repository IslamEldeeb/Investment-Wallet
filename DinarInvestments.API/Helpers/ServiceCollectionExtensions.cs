namespace DinarInvestments.API.Helpers;

public static class ServiceCollectionExtensions
{
    public static void RegisterServicesByConvention(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName != null && a.FullName.StartsWith("DinarInvestments"))
            .ToArray();
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();

            var interfaces = types.Where(t => t.IsInterface).ToList();
            var implementations = types
                .Where(t => t is { IsClass: true, IsAbstract: false } && t.GetInterfaces().Length > 0).ToList();

            foreach (var @interface in interfaces)
            {
                // Skip the specific interfaces
                if (@interface == typeof(ISingletonDependency) ||
                    @interface == typeof(IScopedDependency) ||
                    @interface == typeof(ITransientDependency)
                   )
                {
                    continue;
                }

                var implementation = implementations.FirstOrDefault(t => t.GetInterfaces().Contains(@interface));
                if (implementation != null)
                {
                    if (typeof(IScopedDependency).IsAssignableFrom(@interface))
                    {
                        services.AddScoped(@interface, implementation);
                    }
                    else if (typeof(ITransientDependency).IsAssignableFrom(@interface))
                    {
                        services.AddTransient(@interface, implementation);
                    }
                    else if (typeof(ISingletonDependency).IsAssignableFrom(@interface))
                    {
                        services.AddSingleton(@interface, implementation);
                    }
                    else
                    {
                        services.AddTransient(@interface, implementation);
                    }
                }
            }
        }
    }
}

public interface ISingletonDependency
{
}

public interface IScopedDependency
{
}

public interface ITransientDependency
{
}