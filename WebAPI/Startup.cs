using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Autofac,Ninject,CastleWindsor,StructureMap,LightInject,DryInject ---->IoC Container
            //Autofac kullanaca��z
            //Autofac bize AOP Deste�i Sunuyor
            //built in IoC Container kullan�yoruz �uan i�in daha sonra de�i�tirilecek
            //niye .net'in altyap�s�n� kullanm�yoruz ----> AOP kullanaca��m�z i�in
            //AOP --->Bir metodun �n�nde sonunda veya hata verdi�inde �al��an kod par�ac�klar�n� AOP Mimarisi �le Yaz�yoruz

            //[LogAspect]
            //[Validate]
            //[Cache]
            //[RemoveCache]
            //[Transaction]
            //[Performance]

            //AOP Yap�s�n� metotlar�n �st�n�n yerine s�n�f �st�ne yaz�l�rsa t�m metotlar� kapsar

            services.AddControllers();
            services.AddSingleton<IProductService, ProductManager>();
            services.AddSingleton<IProductDal, EfProductDal>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
