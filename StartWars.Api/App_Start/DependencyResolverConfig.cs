using StarWars.Infrastructure.CrossCutting.IoC;

namespace StartWars.Api.App_Start
{
    public class DependencyResolverConfig
    {
        public static void Register(WebApplicationBuilder app)
        {
            _ = new DependencyResolverContainer(app.Services);
        }
    }
}
