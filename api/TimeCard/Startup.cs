using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeCard.App_start;
using TimeCard.CustomAuthentication;
using TimeCard.DTO;
using TimeCard.Middleware;
using TimeCard.Models;

namespace TimeCard
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
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfig>();
            services.AddSingleton(emailConfig);
            
            services.AddCors(options =>
                {
                    options.AddPolicy("EnanbleCORS", builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
                });


            services.AddDbContext<TimeCardAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TimeSheetConnection")));
            services.AddControllers();
            services.AddCors();

            DataBaseService.Scope(services);
            ManagerService.Scope(services);
            services.AddHttpContextAccessor();


            //services.AddAzureClients(builder =>
            //{
            //    builder.AddBlobServiceClient(Configuration.GetSection("Storage:ConnectionString").Value);  
            //});

            // adding authentication


            services.AddAuthentication(option =>
               {
                   option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

               }
           )

            //  adding  jwtbearer

            .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:Key"]))
               };
           }
            );

            services.AddSingleton<JwtAuth>(new JwtAuth(Configuration["JWT:Key"]));



            // Swagger Authorize
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JWT Token Authentication API",
                    Description = "ASP.NET Core 5.0 "
                });
                // To Enable authorization using Swagger (JWT)
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });


            // services.AddMvcCore(options =>
            // {
            //    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));
            // }
            // ).AddXmlSerializerFormatters();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TimeCard v1"));
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();



            app.UseCors("EnanbleCORS");

            app.UseRouting();
            app.UseStaticFiles();

            app.UseCors(x => x
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:4200"));

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
