using Microsoft.EntityFrameworkCore;
using MinhaWebApiComRedis.Models;

namespace MinhaWebApiComRedis.Database
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
        public DbSet<CotacoesMoedas> CotacoesMoedas { get; set; }
    }
}
