using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCApi.Common.Domain;
using NCApi.Extensions;
using NCApi.Repositories;
using NCApi.Repositories.Impl;
using NLog.Extensions.Logging;
using NLog.Web;

namespace NCApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath($"{env.ContentRootPath}/configs")
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
           Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        private const string _Project_Name = "NCApi";

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var connectionSettingList = Configuration.GetSection("ConnectionSettings").Get<ConnectionSetting[]>();
            services.Configure<ConnectionSettingList>(Options => Options.ConnectionSettings = connectionSettingList);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(new ApiTokenConfig(Configuration.GetSection("TokenKey").Get<String>()));
            services.AddScoped<IApiTokenService, ApiTokenService>();
            //services.AddTransient<IABTestingRepository, ABTestingRepository>();
            services.AddTransient<IABTestingRepository, ContribABTestingRepository>();
            services.AddTransient<IABTestingJDBRepository, ContribABTestingJDBRepository>();
            services.AddTransient<ISqlSugarABTestingRepository, SqlSugarABTestingRepository>();
            
            services.AddSwaggerGen(c =>
            {
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new Swashbuckle.AspNetCore.Swagger.Info
                    {
                        Version = version,
                        Title = $"{_Project_Name} Api Document",
                        Description = $"{_Project_Name} HTTP API " + version,
                        TermsOfService = "None"
                    });
                });
                //添加自定义参数，可通过一些特性标记去判断是否添加
                c.OperationFilter<AssignOperationVendorExtensions>();
                //添加对控制器的标签(描述)
                c.DocumentFilter<ApplyTagDescriptions>();
            });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();
            loggerFactory.AddNLog();
            env.ConfigureNLog($"{env.ContentRootPath}/configs/nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                    {
                        c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{_Project_Name} {version}");
                    });
                });
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            ServiceLocator.Configure(app.ApplicationServices);
            app.UseMvc();
        }
    }
}
