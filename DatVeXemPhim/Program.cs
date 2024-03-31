using DatVeXemPhim.DataContext;
using DatVeXemPhim.Handle.Email;
using DatVeXemPhim.Payloads.Converters;
using DatVeXemPhim.Payloads.DataResponses;
using DatVeXemPhim.Payloads.Responses;
using DatVeXemPhim.Services.Implements;
using DatVeXemPhim.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.
services.AddScoped<IUserService, UserService>();
services.AddScoped<IRoomService, RoomService>();
services.AddScoped<ICinemaService, CinemaService>();
services.AddScoped<ISeatService, SeatService>();
services.AddScoped<IMovieService, MovieService>();
services.AddScoped<IFoodService, FoodService>();
services.AddScoped<IScheduleService, ScheduleService>();

services.AddScoped<RoomConverter>();
services.AddScoped<SeatConverter>();
services.AddScoped<MovieConverter>();
services.AddScoped<FoodConverter>();
services.AddScoped<CinemaConverter>();
services.AddScoped<ScheduleConverter>();

services.AddScoped<ResponseObject<DataResponseCinema>>();
services.AddScoped<ResponseObject<DataResponseSeat>>();
services.AddScoped<ResponseObject<DataResponseMovie>>();
services.AddScoped<ResponseObject<DataResponseFood>>();
services.AddScoped<ResponseObject<DataResponseRoom>>();
services.AddScoped<ResponseObject<DataResponseSchedule>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Auth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Bearer {Token}",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:SecretKey").Value))
    };
});
services.AddSingleton<IEmailService, EmailServices>();
/*
services.AddDbContext<AppDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("DB")));*/

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

app.MapControllers();

app.Run();
