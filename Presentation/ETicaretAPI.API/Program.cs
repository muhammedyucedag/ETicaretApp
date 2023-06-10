using ETicaretAPI.API.Configurations.ColumnWriters;
using ETicaretAPI.Application;
using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Infrastructure.Services.Storage.Azure;
using ETicaretAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
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

            // cors politikasýný ayarlamamý saðlayan servis
            builder.Services.AddCors(options => options.AddDefaultPolicy
            (policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

            Logger log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt")
                .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs", 
                needAutoCreateTable: true,
                columnOptions: new Dictionary<string, ColumnWriterBase>
                {
                    {"message", new RenderedMessageColumnWriter()},
                    {"message_template", new MessageTemplateColumnWriter()},
                    {"level", new LevelColumnWriter()},
                    {"time_stamp", new TimestampColumnWriter()},
                    {"exception", new ExceptionColumnWriter()},
                    {"log_event", new LogEventSerializedColumnWriter()},
                    {"user_name", new UsernameColumnWriter()},
                 })
                .WriteTo.Seq(builder.Configuration["Seq:ServerUrl"])
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .CreateLogger();

            builder.Host.UseSerilog(log);


            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestHeaders.Add("sec-ch-ua");
                logging.MediaTypeOptions.AddText("application/javascript");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;

            });


            // tüm validator sýnýflarýn bulup register edecektir.
            // SuppressModelStateInvalidFilter = mevcut olan doðrulama filtrelerini görmezden gelir.
            builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
                .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Admin", options =>
                {
                    // uygulamaya token üzerinden doðrularken buradaki Configuration üzerinden doðrula
                    options.TokenValidationParameters = new()
                    {
                        ValidateAudience = true, // Oluþturulacak token deðerini kimlerin/hangi orginlerin/sitelerin kullanýcý belirlediðimiz deðerdir.
                        ValidateIssuer = true, // Oluþturacak token deðerini kimin daðýttýðýný ifade edeceðimiz alandýr.
                        ValidateLifetime = true, // Oluþturulan token deðerinin süresini kontrol edecek doðrulamadýr.
                        ValidateIssuerSigningKey = true, // Üretilecek token deðerinin uygulamamýza ait bir deðer olduðunu ifade eden doðrulamadýr.

                        // hangi deðerler ile doðrulama yapýlacak

                        ValidAudience = builder.Configuration["Token:Audience"],
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                        NameClaimType = ClaimTypes.Name // JWT üzerinde name claimne karþýlýk gelen deðeri User.Identity.Name propertysinden elde edebiliriz.
                    };
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseStaticFiles(); // wwwroot için özel bir app

            app.UseSerilogRequestLogging(); // kendisinden önceki loglarý loglatmýyor sonrakileri logluyor

            app.UseHttpLogging();
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();
            

            app.Use(async (context, next) =>
            {
                var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
                LogContext.PushProperty("user_name", username);
                await next();
            });

            app.MapControllers();

            app.Run();
        }
    }
}