using Books.ApplicationBusiness.Layer;
using Books.ApplicationBusiness.Layer.Interfaces;
using Books.EnterpriseBusiness.Layer.Constants;
using Books.EnterpriseBusiness.Layer.Entitys;
using Books.EnterpriseBusiness.Layer.Models;
using Books.InterfaceAdapter.Layer;
using Books.InterfaceAdapter.Layer.Respositorys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region  DbContext Configuration
builder.Services.AddDbContext<AppDbConext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(Constants.DEV_CONNECTION));
});
#endregion


#region authencation and authorization
var jwtKey = builder.Configuration[Constants.JWT_KEY] ?? "u7!xPz$2kL9@wQe4rT6yBvN8mC5sJ1hG2DOD#4";
var jwtIssuer = builder.Configuration[Constants.JWT_ISS] ?? "BooksApiDanielPrieto";

builder.Services.AddAuthentication(options =>
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

#endregion


#region DI
builder.Services.AddScoped<IRepository<BookEntity>, RespositoryBook>();
builder.Services.AddScoped<IModelResult<BookEntity>, ModelResult<BookEntity>>();
builder.Services.AddScoped<BookServices<BookEntity>>();


builder.Services.AddScoped<IRepository<ReviewEntity>, RespositoryReview>();
builder.Services.AddScoped<IModelResult<ReviewEntity>, ModelResult<ReviewEntity>>();
builder.Services.AddScoped<ReviewServices<ReviewEntity>>();


builder.Services.AddScoped<IRepository<CustomUserProfile>, RespositoryUser>();
builder.Services.AddScoped<IModelResult<CustomUserProfile>, ModelResult<CustomUserProfile>>();
builder.Services.AddIdentity<UserEntity, IdentityRole<int>>().AddEntityFrameworkStores<AppDbConext>();
builder.Services.AddScoped<UserServices<CustomUserProfile>>();
#endregion


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();



#region edpoints books v1Books
// Configuración de endpoints para la API V1
var v1Books = app.MapGroup($"{Constants.API_V1}/books")
    .WithTags("Books V1");

// Endpoint para obtener todos los libros
v1Books.MapGet("/", async ([FromServices] BookServices<BookEntity> bookServices) =>
{
    var result = await bookServices.GetAllBooks();
    return Results.Json(result, statusCode: result.Code);
    
})
.WithName("GetAllBooks")
.WithOpenApi();

// Endpoint para obtener un libro por ID
v1Books.MapGet("/{id}", async ([FromServices] BookServices<BookEntity> bookServices, string id) =>
{
    var result = await bookServices.GetBookById(id);
    return Results.Json(result, statusCode: result.Code);
})
.WithName("GetBookById")
.WithOpenApi();

// Endpoint para obtener libros por autor
v1Books.MapGet("/author/{author}", async ([FromServices] BookServices<BookEntity> bookServices, string author) =>
{
    var result = await bookServices.GetBookByauthor(author);
    return Results.Json(result, statusCode: result.Code);
})
.WithName("GetBooksByAuthor")
.WithOpenApi();

// Endpoint para obtener libros por título
v1Books.MapGet("/title/{title}", async ([FromServices] BookServices<BookEntity> bookServices, string title) =>
{
    var result = await bookServices.GetBookByTitle(title);
    return Results.Json(result, statusCode: result.Code);
})
.WithName("GetBooksByTitle")
.WithOpenApi();

// Endpoint para obtener libros por categoría
v1Books.MapGet("/category/{category}", async ([FromServices] BookServices<BookEntity> bookServices, string category) =>
{
    var result = await bookServices.GetBooksByCategory(category);
    return Results.Json(result, statusCode: result.Code);
})
.WithName("GetBooksByCategory")
.WithOpenApi();
#endregion


#region Users  V1 

var v1User = app.MapGroup($"{Constants.API_V1}/User")
    .WithTags("User V1");


v1User.MapPost("/Create", async (
    [FromServices] UserServices<CustomUserProfile> userService,
    [FromBody] RegisterModel model) =>
{

    var result = await userService.CreateUserAsync(model);
    return Results.Json(result, statusCode: result.Code);
});

v1User.MapPost("/login", async (
    LoginModel login,
    UserServices<CustomUserProfile> userServices,
    IConfiguration config) =>
{
    
    var result = await userServices.LoginUserAsync(login.UserName, login.Password, jwtKey);
    return Results.Json(result, statusCode: result.Code);
});

v1User.MapPost("/logout", async (
    LoginModel login,
    UserServices<CustomUserProfile> userServices) =>
{
    var result = await userServices.LogoutAsync(login.UserName);
    if (!result)
        return Results.NotFound(new { mensaje = "Usuario no encontrado." });

    return Results.Ok(new { mensaje = "Sesión cerrada correctamente." });
});
#endregion


#region endpoints reviews v1Reviews
var v1Reviews = app.MapGroup($"{Constants.API_V1}/reviews")
    .WithTags("Reviews V1");

// Endpoint para agregar una review
v1Reviews.MapPost("/", async (
    [FromServices] ReviewServices<ReviewEntity> reviewServices,
    [FromBody] ReviewEntity review) =>
{
    var result = await reviewServices.AddReviewAsync(review);
    return Results.Json(result, statusCode: result.Code);
})
.WithName("AddReview")
.WithOpenApi();

// Endpoint para obtener reviews por usuario
v1Reviews.MapGet("/user/{userId:int}", async (
    [FromServices] ReviewServices<ReviewEntity> reviewServices,
    int userId) =>
{
    var result = await reviewServices.GetReviewsByUser(userId);
    return Results.Json(result, statusCode: result.Code);
})
.WithName("GetReviewsByUser")
.WithOpenApi();

// Endpoint para obtener reviews por libro
v1Reviews.MapGet("/book/{bookId:int}", async (
    [FromServices] ReviewServices<ReviewEntity> reviewServices,
    int bookId) =>
{
    var result = await reviewServices.GetReviewsByBook(bookId);
    return Results.Json(result, statusCode: result.Code);
})
.WithName("GetReviewsByBook")
.WithOpenApi();

// Endpoint para actualizar una review
v1Reviews.MapPut("/", async (
    [FromServices] ReviewServices<ReviewEntity> reviewServices,
    [FromBody] ReviewEntity review) =>
{
    var result = await reviewServices.UpdateReviewAsync(review);
    return Results.Json(result, statusCode: result.Code);
})
.WithName("UpdateReview")
.WithOpenApi();

#endregion
app.Run();
