using BookStats.Api.OpenApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BookStats.Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStats.Api", Version = "v1" });
              // remove additional properties, see https://github.com/Brixel/SpaceAPI/pull/19
              c.DocumentFilter<AdditionalPropertiesDocumentFilter>();

              c.CustomOperationIds(apiDesc =>
              {
                // use ControllerName_Method as operation id. That will group the methods in the generated client
                if (apiDesc.ActionDescriptor is ControllerActionDescriptor desc)
                {
                  var operationAttribute = (desc.EndpointMetadata
                        .FirstOrDefault(a => a is SwaggerOperationAttribute) as SwaggerOperationAttribute);
                  return $"{desc.ControllerName}_{operationAttribute?.OperationId ?? desc.ActionName}";
                }

                // otherwise get the method name from the methodInfo
                var controller = apiDesc.ActionDescriptor.RouteValues["controller"];
                apiDesc.TryGetMethodInfo(out MethodInfo methodInfo);
                string methodName = methodInfo?.Name ?? null;

                return $"{controller}_{methodName}";
              });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStats.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
