using Api_Cloudinary_Media.Config;
using Api_Cloudinary_Media.Middlewares;
using Api_Cloudinary_Media.Services.Impl;
using Api_Cloudinary_Media.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("Cloudinary")
);

builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
