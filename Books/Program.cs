using Books.ApplicationBusiness.Layer;
using Books.ApplicationBusiness.Layer.Interfaces;
using Books.EnterpriseBusiness.Layer.Entitys;
using Books.InterfaceAdapter.Layer;
using Books.InterfaceAdapter.Layer.Respositorys;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

builder.Services.AddDbContext<AppDbConext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IRepository<BookEntity>, RespositoryBook>();
builder.Services.AddScoped<BookController<BookEntity>>();

builder.Services.AddScoped<IRepository<ReviewEntity>, RespositoryReview>();
builder.Services.AddScoped<ReviewController<ReviewEntity>>();

builder.Services.AddScoped<IRepository<UserEntity>, RespositoryUser>();
builder.Services.AddScoped<UserController<UserEntity>>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/Books", () =>
{
   
    return "Hola mubndo";
})
.WithName("books")
.WithOpenApi();

app.Run();

