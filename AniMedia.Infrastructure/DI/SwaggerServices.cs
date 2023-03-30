using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AniMedia.Infrastructure.DI;

public static class SwaggerServices {

    public static IServiceCollection AddSwagger(this IServiceCollection serviceCollection) {
        serviceCollection.AddSwaggerGen(c => {
            c.AddSecurityDefinition(
                JwtBearerDefaults.AuthenticationScheme,
                new OpenApiSecurityScheme {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });

            c.SwaggerDoc("v1", new OpenApiInfo {
                Version = "v1",
                Title = "AniMedia API"
            });
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        serviceCollection.AddEndpointsApiExplorer();

        return serviceCollection;
    }

    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app) {
        app.UseSwagger(setupAction: null);
        app.UseSwaggerUI();

        return app;
    }
}