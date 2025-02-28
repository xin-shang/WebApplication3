using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AmazonOrdersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AmazonOrdersContext") ??
    throw new InvalidOperationException("Connection string 'AmazonOrdersContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(b =>
{
    b.AllowAnyMethod();
    b.AllowAnyOrigin();
    b.AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
