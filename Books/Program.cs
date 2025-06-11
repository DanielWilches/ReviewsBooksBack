using Books.ApplicationBusiness.Layer;
using Books.ApplicationBusiness.Layer.Interfaces;
using Books.EnterpriseBusiness.Layer.Entitys;
using Books.EnterpriseBusiness.Layer.Models;
using Books.InterfaceAdapter.Layer;
using Books.InterfaceAdapter.Layer.Respositorys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbConext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DevConnection"));
});


builder.Services.AddScoped<IRepository<BookEntity>, RespositoryBook>();
builder.Services.AddScoped<IModelResult<BookEntity>, ModelResult<BookEntity>>();
builder.Services.AddScoped<BookServices<BookEntity>>();


builder.Services.AddScoped<IRepository<ReviewEntity>, RespositoryReview>();
builder.Services.AddScoped<ReviewController<ReviewEntity>>();


builder.Services.AddScoped<IRepository<UserEntity>, RespositoryUser>();
builder.Services.AddScoped<UserController<UserEntity>>();
var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Configuración de endpoints para la API V1
var v1 = app.MapGroup("/api/v1/books")
    .WithTags("Books V1");

// Endpoint para obtener todos los libros
v1.MapGet("/", async ([FromServices] BookServices<BookEntity> bookServices) =>
{
    var result = await bookServices.GetAllBooks();
    return Results.Json(result, statusCode: result.Code);
    
})
.WithName("GetAllBooks")
.WithOpenApi();

// Endpoint para obtener un libro por ID
v1.MapGet("/{id}", async ([FromServices] BookServices<BookEntity> bookServices, string id) =>
{
    var result = await bookServices.GetBookById(id);
    return Results.Json(result, statusCode: result.Code);
})
.WithName("GetBookById")
.WithOpenApi();

// Endpoint para obtener libros por autor
v1.MapGet("/author/{author}", async ([FromServices] BookServices<BookEntity> bookServices, string author) =>
{
    var result = await bookServices.GetBookByauthor(author);
    return Results.Json(result, statusCode: result.Code);
})
.WithName("GetBooksByAuthor")
.WithOpenApi();

// Endpoint para obtener libros por título
v1.MapGet("/title/{title}", async ([FromServices] BookServices<BookEntity> bookServices, string title) =>
{
    var result = await bookServices.GetBookByTitle(title);
    return Results.Json(result, statusCode: result.Code);
})
.WithName("GetBooksByTitle")
.WithOpenApi();

// Endpoint para obtener libros por categoría
v1.MapGet("/category/{category}", async ([FromServices] BookServices<BookEntity> bookServices, string category) =>
{
    var result = await bookServices.GetBooksByCategory(category);
    return Results.Json(result, statusCode: result.Code);
})
.WithName("GetBooksByCategory")
.WithOpenApi();

app.Run();

