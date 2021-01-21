using GraphQL.Server;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MinhaWebApiComRedis.Database;
using GraphQL.Server.Ui.Playground;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinhaWebApiComRedis.GraphQL.Schemes;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MinhaWebApiComRedis
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(x => x.UseInMemoryDatabase("CotacoesDb"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API de cotações",
                    Version = "v1",
                    Description = "API de busca de cotações com banco de dados em memória e cache no Redis"
                });
            });

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Configuration.GetConnectionString("ConexaoRedis");
                options.InstanceName = "ApiCotacoesMoedas";
            });

            services.AddScoped<ApiScheme>();

            services.AddGraphQL(o => {
                o.EnableMetrics = true;
                o.UnhandledExceptionDelegate = ctx => { Console.WriteLine(ctx.OriginalException); };
            })
            .AddGraphTypes(typeof(ApiScheme), ServiceLifetime.Scoped);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de cotações - v1"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseGraphQL<ApiScheme>();
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
