using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StarWars.Domain.Settings;
using System.Text;

namespace StartWars.Api.App_Start
{
    public class Start
    {
        private WebApplicationBuilder _app;

        public Start(WebApplicationBuilder app)
        {
            _app = app;
        }

        public Start Builder()
        {
            Mapping();
            AddAuth();

            _app.Services.AddControllers();
            _app.Services.AddEndpointsApiExplorer();

            // Creates swagger style
            AddSwagger(_app.Environment.IsDevelopment());

            _app.Services.AddHttpContextAccessor();

            var appSettings = $"appsettings.json";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(appSettings, optional: true)
                .Build();

            _app.Services.AddSingleton(configuration);

            _app.Services.Configure<Settings>(options =>
            {
                _app.Configuration.Bind(options);
            });

            _app.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });


            return this;
        }

        private void Mapping()
        {
            DependencyResolverConfig.Register(this._app);
        }

        private void AddAuth()
        {
            _app.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = _app.Configuration["Jwt:Issuer"],
                    ValidAudience = _app.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_app.Configuration["Jwt:Key"] ?? string.Empty)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                };
            });

            _app.Services.AddAuthorization();
        }

        private void AddSwagger(bool isDevelopment)
        {
            _app.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Star Wars API",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Email = "",
                        Name = "",
                        Url = new Uri("https://loja.moderna.com.br/")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization"
                });

                // Apply the security requirement globally for all operations
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                        Array.Empty<string>()
                    }
                });
            });
        }


    }
}
