using Microsoft.EntityFrameworkCore;

namespace MinhaWebApiComRedis.Database
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        public DbSet<WeatherForecast> PrevisoesClimaticas { get; set; }
    }
}
