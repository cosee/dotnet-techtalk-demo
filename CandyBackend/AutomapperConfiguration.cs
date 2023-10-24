using System.Reflection;
using AutoMapper;

namespace CandyBackend;

public static class AutomapperConfiguration
{
    public static void Configure(IServiceCollection builderServices)
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