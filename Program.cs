using BlogApp.Data.Abstract;
using BlogApp.Data.Concrate.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(options =>
{
    var config = builder.Configuration;
    var connectionSting = config.GetConnectionString("DefaultConnection");
    options.UseSqlite(connectionSting);

    // var connectionSting = config.GetConnectionString("mysql_connection");
    // var version = new MySqlServerVersion(new Version(9, 4, 0));
    // options.UseMySql(connectionSting,version);

});

builder.Services.AddScoped<IPostRepository, EfPostRepository>();

var app = builder.Build();

app.UseStaticFiles();

SeedData.TestVerileriniDoldur(app);

app.MapGet("/", () => "Hello World!");

app.MapDefaultControllerRoute();
app.Run();
