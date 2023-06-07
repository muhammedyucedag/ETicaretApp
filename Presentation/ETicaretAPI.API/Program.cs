using ETicaretAPI.Application;
using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Infrastructure.Services.Storage.Azure;
using ETicaretAPI.Infrastructure.Services.Storage.Local;
using ETicaretAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ETicaretAPI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddPersistenceServices();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddApplicationServices();

            //builder.Services.AddStorge(StorageType.Azure);
            //builder.Services.AddStorage<LocalStorage>();
            builder.Services.AddStorage<AzureStorage>();

            // cors politikas�n� ayarlamam� sa�layan servis
            builder.Services.AddCors(options => options.AddDefaultPolicy
            (policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()));


            // t�m validator s�n�flar�n bulup register edecektir.
            // SuppressModelStateInvalidFilter = mevcut olan do�rulama filtrelerini g�rmezden gelir.
            builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
                .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Admin", options =>
                {
                    // uygulamaya token �zerinden do�rularken buradaki Configuration �zerinden do�rula
                    options.TokenValidationParameters = new()
                    {
                        ValidateAudience = true, // Olu�turulacak token de�erini kimlerin/hangi orginlerin/sitelerin kullan�c� belirledi�imiz de�erdir.
                        ValidateIssuer = true, // Olu�turacak token de�erini kimin da��tt���n� ifade edece�imiz aland�r.
                        ValidateLifetime = true, // Olu�turulan token de�erinin s�resini kontrol edecek do�rulamad�r.
                        ValidateIssuerSigningKey = true, // �retilecek token de�erinin uygulamam�za ait bir de�er oldu�unu ifade eden do�rulamad�r.

                        // hangi de�erler ile do�rulama yap�lacak

                        ValidAudience = builder.Configuration["Token:Audience"],
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
                    };
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles(); // wwwroot i�in �zel bir app
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}