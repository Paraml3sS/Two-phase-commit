using System;
using Application.Database.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application.Api
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
            var envVariables = Environment.GetEnvironmentVariables();
            
            var connectionString = "host=localhost;port=5432;database=postgresdb;username=superuser;password=closeyoureyes;";
            // var connectionString = $"host={envVariables["DB_HOST"]};port=5432;database={envVariables["DB_DATABASE"]};username={envVariables["POSTGRES_USER"]};password={envVariables["DB_PASSWORD"]};";

            services.AddDbContextPool<HotelContext>(options 
                => options.UseNpgsql(connectionString));
            
            services.AddDbContextPool<FlyContext>(options 
                => options.UseNpgsql(connectionString));
            
            services.AddDbContextPool<AccountContext>(options 
                => options.UseNpgsql(connectionString));
            
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}