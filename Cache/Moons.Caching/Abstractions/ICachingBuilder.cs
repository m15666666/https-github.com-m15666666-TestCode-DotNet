using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Moons.Caching.Abstractions
{
    public interface ICachingBuilder
    {
        IServiceCollection Services { get; }
        IConfiguration Configuration { get; }
    }
}
