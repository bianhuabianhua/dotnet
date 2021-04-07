using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using aspnetapp.Models;
using Microsoft.Extensions.Hosting;

namespace aspnetapp
{
    public class Startup 
    {//Startup类定义请求处理管道和配置app需求的服务

        /*public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }*/

        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {//1.定义app用到的服务
            services.AddRazorPages();
            //add one line
          // services.Add(new ServiceDescriptor(typeof(MusicStoreContext), new MusicStoreContext(Configuration.GetConnectionString("DefaultConnection"))));
            //services.AddTransient<MySqlConnection>(_ => new MySqlConnection(Configuration["ConnectionStrings:Default"])); //注册一个mysql连接
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {//2.定义请求管道的中间件。中间件用于构建请求管道（如配置http请求管道）---------定义如何响应请求
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();//静态文件中间件所做的：针对请求，查看请求路径，将此路径与文件系统上的内容对比。如果静态文件可用，返回此文件，不会再调用下一个中间件。若没找到匹配的文件，则会继续调用下一个中间件。此中间件匹配wwwroot目录下的JavaScript，css或html文件。

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
       
    }
}
