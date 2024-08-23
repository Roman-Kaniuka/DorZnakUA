using Domain.DorZnakUA.Settings;
using DorZnakUA.Api;
using DorZnakUA.Application.DependencyInjection;
using DorZnakUA.DAL.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.DefaultSection));

builder.Services.AddControllers();
builder.Services.AddSwagger();
builder.Services.AddAuthenticationAndAuthorization(builder);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DorZnakUA Swagger v1.0");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "DorZnakUA Swagger v2.0");
        /*c.RoutePrefix = string.Empty;*/
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
