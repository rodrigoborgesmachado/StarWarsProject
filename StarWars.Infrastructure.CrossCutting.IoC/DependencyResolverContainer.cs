using SimpleInjector;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Domain.Interfaces.Services;
using StarWars.Domain.Services;
using StarWars.Application.Interfaces;
using StarWars.Application.AppServices;
using StarWars.Infrastructure.CrossCutting.Adapter;

namespace StarWars.Infrastructure.CrossCutting.IoC
{
    public class DependencyResolverContainer : Container
    {
        private static DependencyResolverContainer _instance;

        public static DependencyResolverContainer Instance => _instance ?? (_instance = new DependencyResolverContainer());

        private DependencyResolverContainer()
        {
        }

        public DependencyResolverContainer(IServiceCollection services)
        {
            RegisterServices(services);
        }

        public void RegisterService<TService, TImplementation>(IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddScoped<TService, TImplementation>();
        }

        public void RegisterServices(IServiceCollection services)
        {
            #region Sevice

            RegisterService<ISWApiService, SWApiService>(services);

            #endregion

            #region Application

            RegisterService<IStarshipsAppService, StarshipsAppService>(services);

            #endregion

            TypeAdapterFactory.SetCurrent(new AutomapperTypeAdapterFactory());
        }
    }
}
