using System.Reflection;

namespace Api.Common.MinimalAPI
{
    public static class EndpointRouteBuilder
    {
        public static void LoadEndpointModules(this IEndpointRouteBuilder app, Assembly assembly, IServiceProvider? serviceProvider = null)
        {
            serviceProvider ??= app.ServiceProvider;

            var moduleType = typeof(EndpointModule);
            var modules = assembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && moduleType.IsAssignableFrom(t))
                .Select(t => ActivatorUtilities.CreateInstance(serviceProvider, t) as EndpointModule)
                .Where(m => m != null)
                .ToList();

            foreach (var module in modules)
            {
                module!.MapEndpoints(app);
            }
        }

    }
}
