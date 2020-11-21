using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using AutoMapper;
using EmployeeManagementSystem.Configuration;
using EmployeeManagementSystem.Hubs;
using EmployeeManagementSystem.Middleware;
using EmployeeManagementSystem.Core.Models;
using EmployeeManagementSystem.Persistence.DBContext;

namespace EmployeeManagementSystem
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            _ = services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            JwtSettings jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);

            services.AddAutoMapper(typeof(Startup));
            services.AddSingleton(jwtSettings);
            // services.AddHostedService<EMSBackgroundService>();
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });
            //.AddGoogle(options =>
            //{
            //    options.ClientId = "112155948230-1smtid8tt013rhqp7pdv4jv3b52fqcm5.apps.googleusercontent.com";
            //    options.ClientSecret = "9dlDe2VPRNojD1SLmll73Oyv";
            //});

            services.AddCors();
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("EmsSqliteConnection"));
                //options.UseSqlServer(Configuration.GetConnectionString("EmsSqlServerConnection"));
            });

            services.AddControllers();
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee Managment System", Version = "v1" });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                        },
                        new List<string> ()
                    }
                });
            });

            services.ResolveDependencies();
            services.AddSignalR();
            services.AddSingleton<DashboardHub>();
            services.AddControllers().AddNewtonsoftJson(options =>
              options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            if (env.IsDevelopment())
            {
                // app.UseExceptionHandler("/error-local-development");
                // IdentityModelEventSource.ShowPII = true;
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapHub<DashboardHub>("/dashboardhub");
                //endpoints.MapFallbackToController("Index", "Fallback");

            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Managment System API(v1)");
            });
        }
    }
}