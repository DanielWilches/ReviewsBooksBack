using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Books.Domain.Layer.Constants;

namespace BooksPresentation.Authencation
{
    public class AuthencationAndAuthorization
    {
        public static void AuthencationConfiguration(IConfiguration configuration, IServiceCollection services)
        {
            var jwtKey = configuration[Constants.JWT_KEY] ?? "u7!xPz$2kL9@wQe4rT6yBvN8mC5sJ1hG2DOD#4";
            var jwtIssuer = configuration[Constants.JWT_ISS] ?? "BooksApiDanielPrieto";

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });
        }
    }
}
