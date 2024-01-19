using System.Reflection;
using AutoMapper;

namespace CandyBackend;

public static class AutoMapperConfiguration
{
    public static void ConfigureAutoMapper(this IServiceCollection builderServices)
    {
        Validate();
        
        builderServices.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    public static void Validate()
    {
        MapperConfiguration mapperConfiguration = new(delegate(IMapperConfigurationExpression cfg) 
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });
        mapperConfiguration.AssertConfigurationIsValid();
    }
}