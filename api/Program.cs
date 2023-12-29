using System.Net;
using api.Extensions;
using api.Middleware;
using api.Utilities;
using infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddApplicationServices();
        builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState.Keys
                    .SelectMany(key => context.ModelState[key].Errors.Select(x => $"{key}: {x.ErrorMessage}"))
                    .Aggregate((current, next) => $"{current}\n {next}");

                var apiError = new BadRequestObjectResult(new ApiResponse<string>(HttpStatusCode.BadRequest, errors));

                return apiError;
            };
        });
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1.0",
                Title = "Magic financial Market v1",
                Description = "Pass your financial order",
                TermsOfService = new Uri("https://www.boursorama.com/bourse/"),
                Contact = new OpenApiContact
                {
                    Name = "Capfi Academy 2023 ",
                    Url = new Uri("https://www.boursorama.com/bourse/")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://www.boursorama.com/bourse/")
                }
            });
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2.0",
                Title = "Magic financial Market v2",
                Description = "Pass your financial order",
                TermsOfService = new Uri("https://www.boursorama.com/bourse/"),
                Contact = new OpenApiContact
                {
                    Name = "Capfi Academy 2023 ",
                    Url = new Uri("https://www.boursorama.com/bourse/")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://www.boursorama.com/bourse/")
                }
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Magic Financial Market v1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Magic Financial Market v2");
            });
        }
        app.UseSwaggerUI();
        

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseStatusCodePagesWithReExecute("/error/{0}");
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}