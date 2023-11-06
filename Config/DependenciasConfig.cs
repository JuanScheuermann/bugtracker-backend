using System.Text;
using backend.Services.IServicio;
using backend.Services.Servicio;
using Microsoft.IdentityModel.Tokens;

namespace backend.Config;

public static class DependenciasConfig
{
    /* public static IServiceCollection AutenticacionJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication().AddJwtBearer(option =>
        {

            option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(configuration.GetSection("AppSettings:Token").Value!))
            };

        });

        return services;
    } */
    public static IServiceCollection AgregarServicios(this IServiceCollection services)
    {
        services.AddScoped<IUserServicio, UserServicio>();
        services.AddScoped<IProyectoServicio, ProyectoServicio>();
        services.AddScoped<IEtiquetaServicio, EtiquetaServicio>();
        services.AddScoped<IMiembroServicio, MiembroServicio>();
        services.AddScoped<IComentarioServicio, ComentarioServicio>();
        return services;
    }
}