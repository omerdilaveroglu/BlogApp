// Gerekli kütüphaneleri ve namespace'leri projeye dahil ediyoruz.
// Bu kütüphaneler, Entity Framework Core, ASP.NET Core MVC ve
// kendi yazdığımız Repository sınıfları için gereklidir.
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrate.EfCore;
using Microsoft.EntityFrameworkCore;

// Uygulama yapılandırıcısını (builder) oluşturuyoruz. 
// Bu nesne, uygulamanın servislerini, ayarlarını ve HTTP istek pipeline'ını oluşturmak için kullanılır.
var builder = WebApplication.CreateBuilder(args);

// Uygulamaya Controller ve View hizmetlerini ekliyoruz.
// Bu, projenin MVC (Model-View-Controller) mimarisinde çalışmasını sağlar.
builder.Services.AddControllersWithViews();

// Uygulamanın veritabanı bağlantısı için DbContext servisini ekliyoruz.
// BlogContext sınıfımız, veritabanı ile etkileşimimizi sağlar.
builder.Services.AddDbContext<BlogContext>(options =>
{
    // appsettings.json dosyasından bağlantı dizesini okuyoruz.
    var config = builder.Configuration;
    var connectionSting = config.GetConnectionString("DefaultConnection");

    // Okunan bağlantı dizesini kullanarak veritabanı sağlayıcısı olarak SQLite'ı belirliyoruz.
    options.UseSqlite(connectionSting);

    // Aşağıdaki satırlar, MySQL gibi farklı bir veritabanı kullanmak isterseniz
    // nasıl bir yapılandırma yapacağınızı gösteren bir örnektir.
    // var connectionSting = config.GetConnectionString("mysql_connection");
    // var version = new MySqlServerVersion(new Version(9, 4, 0));
    // options.UseMySql(connectionSting,version);
});

// Bağımlılık Enjeksiyonu (Dependency Injection) için servislerimizi kaydediyoruz.
// Her HTTP isteği için IPostRepository arayüzü istendiğinde,
// EfPostRepository sınıfından yeni bir örnek oluşturulmasını sağlıyoruz.
builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

// Servisler ve ayarlar ile web uygulamasını oluşturuyoruz.
var app = builder.Build();

// Uygulamanın wwwroot klasöründeki statik dosyaları (CSS, JS, resimler vb.) sunmasını etkinleştiriyoruz.
app.UseStaticFiles();

// Uygulama ilk çalıştığında veritabanına test verilerini eklemek için
// özel bir metodu çağırıyoruz.
SeedData.TestVerileriniDoldur(app);

// Kök URL'ye ("/") gelen bir GET isteği için basit bir HTTP yanıtı tanımlıyoruz.
// Bu satır, projenin başarılı bir şekilde çalıştığını test etmek için kullanılabilir.
app.MapGet("/", () => "Hello World!");

// Varsayılan MVC yönlendirme kuralını belirliyoruz. 
// İstekler, {controller}/{action}/{id?} formatında ilgili Controller ve Action'a yönlendirilir.
app.MapDefaultControllerRoute();

// Uygulamayı başlatıyoruz ve gelen HTTP isteklerini dinlemeye başlıyoruz.
app.Run();