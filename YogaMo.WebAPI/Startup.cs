using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Stripe;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.Swagger;
using YogaMo.WebAPI.Database;
using YogaMo.WebAPI.Filters;
using YogaMo.WebAPI.Security;
using YogaMo.WebAPI.Services;



namespace YogaMo.WebAPI
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
            services.AddCors();
            services.AddMvc(x => x.Filters.Add<ErrorFilter>());

            services.AddControllers();

            services.AddAutoMapper();


            //services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                           {
                               {
                                     new OpenApiSecurityScheme
                                       {
                                           Reference = new OpenApiReference
                                           {
                                               Type = ReferenceType.SecurityScheme,
                                               Id = "basic"
                                           }
                                       },
                                       new string[] {}
                               }
                           });
            });

            /* ************************************************ */

            services.AddAuthentication("BasicAuthentication")
.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddScoped<IInstructorService, InstructorService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IYogaService, YogaService>();
            services.AddScoped<IYogaClassService, YogaClassService>();
            services.AddScoped<IProductService, WebAPI.Services.ProductService>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, WebAPI.Services.OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IRecommenderService, RecommenderService>();

            var connection = @"Server=.;Database=150222; Trusted_Connection=True; ConnectRetryCount=0";
            services.AddDbContext<_150222Context>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey = "sk_test_51HOmvCKhXUiucPF3zhimX93vWHYpGrBFap7qvVXd9FEF5Kz8YcCR5bgUWdnIQMNxfLcsMibhnlJKocDqKwNBlCBr006i1kNok5";

            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

