#region Usings
using Asp.Versioning;
#endregion

#region Using Books
using static BooksPresentation.Authencation.AuthencationAndAuthorization;
using static BooksPresentation.DependencyInjection.DependencyInjection;
using static BooksPresentation.DataBaseConfiguration.DataBaseConfiguration;
using Books.Domain.Layer.Constants;
#endregion

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de versionado de API
builder.Services.AddApiVersioning(options => 
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options => 
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

ConfigureDatabase(builder.Configuration, builder.Services);
AuthencationConfiguration(builder.Configuration, builder.Services);
ConfigureServices(builder.Services);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

var jwtKey = builder.Configuration[Constants.JWT_KEY] ?? "u7!xPz$2kL9@wQe4rT6yBvN8mC5sJ1hG2DOD#4";
var jwtIssuer = builder.Configuration[Constants.JWT_ISS] ?? "BooksApiDanielPrieto";

app.MapControllers();
app.Run();
