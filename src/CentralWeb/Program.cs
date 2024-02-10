using Autofac;
using Autofac.Extensions.DependencyInjection;
using CentralWebInHospital.Hubs;
using Common;
using Data;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Services.WebFramework.CustomMapping;
using System.Text.Json;
using WebFrameworks.Configurations;
using WebFrameworks.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(ConfigureContainer);

void ConfigureContainer(ContainerBuilder containerBuilder) => containerBuilder.AddServices();

var configuration = builder.Configuration;
var siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();


builder.Services.Configure<SiteSettings>(configuration.GetSection(nameof(SiteSettings)));
builder.Services.InitializeAutoMapper();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddCustomIdentity(siteSettings.IdentitySettings);


builder.Services.AddElmah<SqlErrorLog>(options =>
{
    options.Path = "/elmah";
    options.ConnectionString = configuration.GetConnectionString("Elmah");
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond: 1024 * 100, gracePeriod: TimeSpan.FromSeconds(10));
});

builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ApplicationDbContext>())
.AddNewtonsoftJson();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
})
    .AddOData(options =>
        options.Select()
            .Filter()
            .OrderBy()
            .SetMaxTop(20)
            .Count()
            .Expand()
    );


builder.Services.AddRazorPages();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerServices();

builder.Services.AddJwtAuthentication(siteSettings.JwtSettings);

builder.Services.AddCors();

builder.Services.AddSignalR(options =>
    {
        options.EnableDetailedErrors = true;
    })
       .AddJsonProtocol(options =>
       {
           options.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
       });


var app = builder.Build();

app.UseCustomExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseElmah();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<CentralMessageHub>("/parameter");
});


app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        if (context.Database.IsSqlServer()) context.Database.Migrate();
        var userManager = services.GetRequiredService<UserManager<Entities.User.User>>();
        var roleManager = services.GetRequiredService<RoleManager<Entities.Role.Role>>();
        await ApplicationDbContextSeed.SeedDefaultRolesAsync(roleManager);
        await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

app.Run();