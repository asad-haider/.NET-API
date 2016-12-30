using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DataAccess;
using Business;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using WebAPI.Middlewares;

namespace WebAPI
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        private ILogger<Startup> logger { get; set; }

        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)                
                //.AddJsonFile("appsettings.json", optional: true, reloadOnChange:true)
                .AddJsonFile($"appsettings-{env.EnvironmentName}.json", optional: false, reloadOnChange: true);

            if (env.IsDevelopment())
            {
                //builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();

            logger = loggerFactory.CreateLogger<Startup>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add functionality to inject IOptions<T>
            services.AddOptions();

            //Add the strongly typed settings
            //services.Configure<ConnectionStrings>(options => Configuration.GetSection("ConnectionStrings").Bind(options));

            //Regester DataAccess layer
            DataAccessRegistry.RegisterComponents(services, Configuration);

            //Regester Service layer:
            ComponentResgistry.RegisterComponents(services);

            services.AddCors();

            // Swagger
            services.AddSwaggerGen();

            #region MVC configurations

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;

                    //options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Arrays;

                });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddNLog();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            #region JWT Bearer Authentication

            //app.UseJwtBearerAuthentication(LoadJwtBearerMiddlewareOptions(env));


            #endregion

            if (env.IsDevelopment())
            {
                app.UseCors(x =>
                {
                    x.AllowAnyHeader();
                    x.AllowAnyMethod();
                    x.AllowAnyOrigin();
                });
            }

            if (env.IsProduction())
            {

            }

            #region MVC Configurations

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMvc();

            #endregion

            #region Swagger configurations
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi();
            #endregion

            ;
        }

        //private JwtBearerOptions LoadJwtBearerMiddlewareOptions(IHostingEnvironment envRegisterRoutes
        //{
        //    //TODO: Load certificate from certificate machine store.
        //    //Load certificate, used for validation of the signature.
        //    //X509Certificate2 cert = LoadCertificate(env);
        //    //X509SecurityKey x509Key = new X509SecurityKey(cert);

        //    //Create options based on environment
        //    JwtBearerOptions configurationOptions = new JwtBearerOptions();
        //    configurationOptions.Configuration = new OpenIdConnectConfiguration();

        //    configurationOptions.Configuration.AuthorizationEndpoint = Configuration.GetSection("JwtTokenOptions").GetSection("AuthorizationEndpoint").Value.ToString();
        //    configurationOptions.Configuration.EndSessionEndpoint = Configuration.GetSection("JwtTokenOptions").GetSection("EndSessionEndpoint").Value.ToString();
        //    configurationOptions.Configuration.TokenEndpoint = Configuration.GetSection("JwtTokenOptions").GetSection("TokenEndpoint").Value.ToString();
        //    configurationOptions.Configuration.UserInfoEndpoint = Configuration.GetSection("JwtTokenOptions").GetSection("UserInfoEndpoint").Value.ToString();
        //    configurationOptions.Configuration.JwksUri = Configuration.GetSection("JwtTokenOptions").GetSection("JwksUri").Value.ToString();
        //    configurationOptions.Configuration.Issuer = Configuration.GetSection("JwtTokenOptions").GetSection("Issuer").Value.ToString();
        //    configurationOptions.TokenValidationParameters.ValidAudience = Configuration.GetSection("JwtTokenOptions").GetSection("ValidAudience").Value.ToString();
        //    configurationOptions.TokenValidationParameters.ValidIssuer = Configuration.GetSection("JwtTokenOptions").GetSection("ValidIssuer").Value.ToString();

        //    configurationOptions.AutomaticAuthenticate = true;
        //    configurationOptions.AutomaticChallenge = true;
        //    configurationOptions.RequireHttpsMetadata = false;
        //    configurationOptions.TokenValidationParameters.RequireSignedTokens = false;
        //    configurationOptions.TokenValidationParameters.ValidateIssuer = false;
        //    configurationOptions.TokenValidationParameters.ValidateAudience = false;
        //    //configurationOptions.TokenValidationParameters.ValidateSignature = false;
        //    configurationOptions.TokenValidationParameters.ValidateLifetime = false;
        //    //configurationOptions.TokenValidationParameters.IssuerSigningKey = x509Key;

        //    return configurationOptions;
        //}

    }
}
