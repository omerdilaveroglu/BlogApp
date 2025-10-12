using BlogApp.Data.Concrete.EfCore; // Doğru yazım
using BlogApp.Data; 
using Microsoft.EntityFrameworkCore;
using System.IO; 

var builder = WebApplication.CreateBuilder(args);

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


var app = builder.Build();

// Veri doldurma (Seeding)
SeedData.TestVerileriniDoldur(app);


app.MapGet("/", () => "Hello World!");

app.Run();
