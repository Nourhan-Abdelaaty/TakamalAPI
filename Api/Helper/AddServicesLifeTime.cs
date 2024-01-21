using Application.Services.Implemantation;
using Application.Services.Interfaces;
using DataAccess.Context;
using DataAccess.Repositories;
using DataAccess.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Helper;
public static class ServicesLifeTimeService
{
    public static IServiceCollection AddServicesLifeTime(this IServiceCollection service)
    {
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        service.AddTransient<DataContext>();

        #region ClientService
            service.AddScoped<IClientService, ClientService>();
        #endregion
        return service;
    }
}
