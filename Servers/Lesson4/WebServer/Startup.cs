//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using WebServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace WebServer
{
    public class Startup
    {

        //1. Add constants
        #region Constans
        public string CONNECTION_STRING
            = $"Data Source=localhost;Initial Catalog={nameof(DbServerContext)};Integrated Security=True";
        public const string JWT_ISSUER = "MyAuthServer";
        public const string JWT_AUDIENCE = "http://localhost:60719"; //recipient or client (some use resource's URI other use scope names)
        public const string JWT_KEY = "b802bec8fd52b0a75f201d8b37274e1081c39b740293f765eae731f5a65ed1";
        public const int JWT_LIFETIME = 30;
        public static SymmetricSecurityKey JwtSymmetricSecurityKey
            => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWT_KEY));
        #endregion

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbServerContext>(option => option.UseSqlServer(CONNECTION_STRING));

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<DbServerContext>()
                    .AddDefaultTokenProviders();

            //https://blogs.msdn.microsoft.com/webdev/2017/04/06/jwt-validation-and-authorization-in-asp-net-core/
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // Gets or sets a value indicating whether the Issuer should be validated.
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = false,
                            // Gets or sets an issuer that is considered valid.
                            // строка, представляющая издателя
                            ValidIssuer = JWT_ISSUER,
                            // Gets or sets an audience that is considered valid.
                            // будет ли валидироваться потребитель токена
                            ValidateAudience = false,
                            // Set audience token
                            // установка потребителя токена
                            ValidAudience = JWT_AUDIENCE,
                            // Key validation
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                            // Set security key
                            // установка ключа безопасности. Обычно URI ресурста или областьприменения
                            IssuerSigningKey = JwtSymmetricSecurityKey,
                            // JWT lifetime
                            // будет ли валидироваться время существования
                            ValidateLifetime = true

                        };

                        /* Authority is the address of the token-issuing authentication server. 
                        * The JWT bearer authentication middleware will use this URI to find 
                        * and retrieve the public key that can be used to validate the token’s signature. 
                        * It will also confirm that the iss parameter in the token matches this URI
                        */
                        options.Authority = JWT_ISSUER;
                        /* Audience represents the intended recipient of the incoming token or the resource 
                         * that the token grants access to. If the value specified in this parameter 
                         * doesn’t match the aud parameter in the token, the token will be rejected 
                         * because it was meant to be used for accessing a different resource. 
                         * Note that different security token providers have different behaviors 
                         * regarding what is used as the ‘aud’ claim (some use the URI of a resource 
                         * a user wants to access, others use scope names). Be sure to use an audience 
                         * that makes sense given the tokens you plan to accept.
                         */
                        options.Audience = JWT_AUDIENCE;
                        /* RequireHttpsMetadata is useful for testing purposes. 
                         * In real-world deployments, JWT bearer tokens should always be passed 
                         * only over HTTPS.
                         */
                        options.RequireHttpsMetadata = false;
                        options.Configuration = new OpenIdConnectConfiguration();
                    });

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();  
            }


            app.UseAuthentication();

            app.UseMvc();

            app
            .UseDefaultFiles()
            .UseStaticFiles()
            .Use(async (context, next) =>
            {
                await next();

                if
                (
                    context.Response.StatusCode == StatusCodes.Status404NotFound &&
                    !Path.HasExtension(context.Request.Path.Value) &&
                    !context.Request.Path.Value.StartsWith("api")
                )
                {
                    context.Request.Path = "/Index.html";
                    context.Response.StatusCode = StatusCodes.Status200OK;

                    await next();
                }
            });

        }
    }
}
