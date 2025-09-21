using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogApp.Data.Concrate.EfCore;

public static class SeedData
{
    public static void TestVerileriniDoldur(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
        if (context != null)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Tags.Any())
            {
                context.Tags.AddRange(
                    new Entity.Tag { Text = "Web programlama" },
                    new Entity.Tag { Text = "backend" },
                    new Entity.Tag { Text = "Frontend" },
                    new Entity.Tag { Text = "fullstack" },
                    new Entity.Tag { Text = "php" }
                );
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new Entity.User { UserName = "ozgetasdelen" },
                    new Entity.User { UserName = "omerdilaveroglu" },
                    new Entity.User { UserName = "durgaybaydemir" }
                );
            }
            
            context.SaveChanges();

            if (!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Entity.Post { Title = "ASPNET Core", Content = "AspNet core desleri", IsActive = true, PublishedOn = DateTime.Now.AddDays(-10), Tags = context.Tags.Take(3).ToList(), UserId = 1 ,Image = "1.jpg"},
                    new Entity.Post { Title = "PHP", Content = "PHP desleri", IsActive = true, PublishedOn = DateTime.Now.AddDays(-20), Tags = context.Tags.Take(2).ToList(), UserId = 2 ,Image = "2.jpg"},
                    new Entity.Post { Title = "Djongo", Content = "Django core desleri", IsActive = true, PublishedOn = DateTime.Now.AddDays(-5), Tags = context.Tags.Take(3).ToList(), UserId = 3,Image = "3.jpg" }
                );
            }

            context.SaveChanges();
        }
    }
}