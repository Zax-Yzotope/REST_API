// Unused usings removed
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using RestApi.Models;

namespace RestApi
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
            //Call API semble ne pas toujours fonctionné. Ajout ligne 26-35 pour patcher le problème
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(
                    builder => 
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
                });
        // services.AddCors();

        // services.AddControllers().AddNewtonsoftJson(options =>
        //     options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        // );

        services.AddDbContext<ApiContext>(options =>
        options.UseMySql("server=" + Configuration["SERVER"] + ";port=3306;database=" + Configuration["DATABASE"] + ";user=" + Configuration["USER"] + ";password=" + Configuration["PASSWORD"]));
        services.AddMvc();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}