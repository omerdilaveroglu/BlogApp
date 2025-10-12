using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity; 
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Data.Concrete.EfCore;

public static class SeedData
{
    public static void TestVerileriniDoldur(IApplicationBuilder app)
    {
        // BlogContext'i DI konteynerinden bir kapsam (scope) içinde alıyoruz.
        // Bu, DBContext'in ömrünü doğru yönetmek için iyi bir uygulamadır.
        var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
        
        if (context != null)
        {
            // Bekleyen migration'lar varsa uygula (veritabanı güncel değilse güncelle)
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            // Tag verilerini kontrol et ve doldur
            if (!context.Tags.Any())
            {
                context.Tags.AddRange(
                    new Tag { Text = "Web programlama" },
                    new Tag { Text = "backend" },
                    new Tag { Text = "Frontend" },
                    new Tag { Text = "fullstack" },
                    new Tag { Text = "php" }
                );
            }

            // User verilerini kontrol et ve doldur
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { UserName = "ozgetasdelen" },
                    new User { UserName = "omerdilaveroglu" },
                    new User { UserName = "durgaybaydemir" }
                );
            }
            
            // Tag ve User'ların ID'leri oluşması için kaydet.
            context.SaveChanges();

            if (!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Entity.Post { 
                        Title = "ASPNET Core", 
                        Content = "AspNet core desleri", 
                        IsActive = true, 
                        PublishedOn = DateTime.Now.AddDays(-10), 
                        // İlk 3 Tag'ı al. (Id'leri SaveChanges ile oluştu)
                        Tags = context.Tags.Take(3).ToList(), 
                        UserId = 1 ,
                        Image = "1.jpg"
                    },
                    new Entity.Post { 
                        Title = "PHP", 
                        Content = "PHP desleri", 
                        IsActive = true, 
                        PublishedOn = DateTime.Now.AddDays(-20), 
                        Tags = context.Tags.Skip(1).Take(2).ToList(), // İkinci ve üçüncü Tag'ı al
                        UserId = 2 ,
                        Image = "2.jpg"
                    },
                    new Entity.Post { 
                        Title = "Djongo", 
                        Content = "Django core desleri", 
                        IsActive = true, 
                        PublishedOn = DateTime.Now.AddDays(-5), 
                        Tags = context.Tags.Skip(2).Take(3).ToList(), // Üçüncü, dördüncü ve beşinci Tag'ı al
                        UserId = 3,
                        Image = "3.jpg" 
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
