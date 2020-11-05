using System.Text;
using GamesAndFriends.Api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using GamesAndFriends.IoC;
using GamesAndFriends.Application.Services.Interfaces;
using Newtonsoft.Json;

namespace GamesAndFriends.Api
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );
            
            var appSettings = this.ConfigureAppSettings(services);
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            
            services.AddAuthentication(s =>
            {
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(s =>
            {
                s.Events = GetJwtBearerEvents();
                s.RequireHttpsMetadata = false;
                s.SaveToken = true;
                s.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.RegisterServices(this.Configuration);
            
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } 
            else 
            {
                app.UseExceptionHandler("/api/error");
            }

            app.UseAuthentication();

            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().ConfigureDbContext();
        }

        private AppSettings ConfigureAppSettings(IServiceCollection services) 
        {
            var appSettingsSection = this.Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            return appSettingsSection.Get<AppSettings>();
        }

        private static JwtBearerEvents GetJwtBearerEvents() 
        {
            return new JwtBearerEvents
            {
                OnTokenValidated = async context =>
                {
                    var userApplicationService = context.HttpContext.RequestServices.GetRequiredService<IUserApplication>();
                    var user = await userApplicationService.GetAsync(context.Principal.Identity.Name);

                    if (user == null)
                    {
                        context.Fail("Unauthorized");
                    }
                }
            };
        }
    }
}
