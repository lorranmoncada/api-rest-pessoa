using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pessoa.Fisica.Infraestructure;
using Pessoa.Juridica.Infraestructure;
using Pessoa.Web.Extensions;
using System;
using System.IO;
using System.Reflection;

namespace Pessoa.Web
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
            services.AddControllersWithViews()
                  .AddNewtonsoftJson(options =>
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers();

            //Contexto
            services.AddDbContext<PessoaFisicaContext>(p => p.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<PessoaJuridicaContext>(p => p.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.RegisterService();
            services.RegisterServicePessoaFisica();
            services.RegisterServicePessoaJuridica();
            services.RegisterPessoaFisicaAutoMap();
            services.RegisterPessoaJuridicaAutoMap();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pessoa", Version = "v1" });
            });
       
            services.AddMediatR(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pessoa v1"));

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

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
