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
            //Autofac kullanacaðýz
            //Autofac bize AOP Desteði Sunuyor
            //built in IoC Container kullanýyoruz þuan için daha sonra deðiþtirilecek
            //niye .net'in altyapýsýný kullanmýyoruz ----> AOP kullanacaðýmýz için
            //AOP --->Bir metodun önünde sonunda veya hata verdiðinde çalýþan kod parçacýklarýný AOP Mimarisi Ýle Yazýyoruz

            //[LogAspect]
            //[Validate]
            //[Cache]
            //[RemoveCache]
            //[Transaction]
            //[Performance]

            //AOP Yapýsýný metotlarýn üstünün yerine sýnýf üstüne yazýlýrsa tüm metotlarý kapsar

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
