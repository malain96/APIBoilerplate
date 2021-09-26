using API.Extensions;
using API.Middlewares;
using API.Shared.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace API
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration object
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Method used to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDatabase(Configuration)
                .AddRepositories()
                .AddBusinessServices();

            services.AddControllers();

            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

            // Add Swagger doc
            services.AddSwagger();

            services.AddAutoMapper(typeof(Startup));

            // Remove auto model state validation
            services.Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
            {
                apiBehaviorOptions.InvalidModelStateResponseFactory = context =>
                {
                    var allErrors = context.ModelState.Values.SelectMany(v => v.Errors);
                    return new BadRequestObjectResult(new ErrorResponse(StatusCodes.Status400BadRequest, allErrors.Select(e => e.ErrorMessage).ToList()));
                };
            });

            services.AddAuth0(Configuration);
        }

        /// <summary>
        /// Method used to configure the HTTP request pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "APIBoilerplate v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<CallsLoggingMiddleware>();

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                await context.Response.WriteAsJsonAsync(new ErrorResponse(StatusCodes.Status500InternalServerError, "An unexpected error has occurred"));
            }));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}