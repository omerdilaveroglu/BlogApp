using BlogApp.Data.Concrete.EfCore; 
using BlogApp.Data.Abstract; // IRepository ve IPostDal için eklendi
using Microsoft.EntityFrameworkCore;
using System.IO;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // MySQL için eklendi
using System; 
using BlogApp.Data;
using BlogApp.Data.Concrete;
using BlogApp.Entity; // SeedData için gerekli

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// DbContext ayarları
builder.Services.AddDbContext<BlogContext>(options =>
{
    var config = builder.Configuration;

    // YENİ EKLEME: SQLite dosyasının mutlak yolunu oluşturmak.
    // Bu, dosyanın uygulamanın kök dizininde (ContentRootPath) oluşmasını garanti eder.
    // var connectionString = config.GetConnectionString("sql_connection"); 
    // var dbPath = Path.Combine(builder.Environment.ContentRootPath, "blog.db");
    // options.UseSqlite($"Data Source={dbPath}");

    var connectionString = config.GetConnectionString("mysql_connection"); 
    options.UseMySql(connectionString,new MySqlServerVersion(new Version(9,4,0)));

});

builder.Services.AddScoped<IRepository<Post>, EfRepository<Post, BlogContext>>();
builder.Services.AddScoped<IRepository<Tag>, EfRepository<Tag, BlogContext>>();



var app = builder.Build();

// Veri doldurma (Seeding)
// Not: SeedData.TestVerileriniDoldur metodu, IPostDal'a bağımlı olabilir.
SeedData.TestVerileriniDoldur(app);
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
