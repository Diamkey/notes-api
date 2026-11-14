using Application.Mappings;
using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;

namespace UnitTests.Factories;

public class MapperFactory
{
    public static IMapper Create()
    {
        var config = new MapperConfiguration(cfg =>
            cfg.AddProfile(typeof(MappingProfile)), 
            NullLoggerFactory.Instance);
        return config.CreateMapper();
    }
}