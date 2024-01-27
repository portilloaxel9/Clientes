using Microsoft.EntityFrameworkCore;
using Clientes.Web.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ClientesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClientesDbContext") ?? throw new InvalidOperationException("Connection string 'ClientesDbContext' not found.")));

builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:7103")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapGet("/getNombreByCuit", async (HttpContext httpContext, string cuit) =>
{
    using var httpClient = new HttpClient();
    var url = $"https://sistemaintegracomex.com.ar/Account/GetNombreByCuit?cuit={cuit}";
    try
    {
        var response = await httpClient.GetStringAsync(url);
        return Results.Ok(response);
    }
    catch (HttpRequestException e)
    {
        return Results.Problem(e.Message);
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();