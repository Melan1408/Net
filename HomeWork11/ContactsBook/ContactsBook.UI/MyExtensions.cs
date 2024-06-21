using ContactsBook.DAL.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ContactsBook.UI
{
    public static class MyExtensions
    {
        public static MauiAppBuilder AddCore(this MauiAppBuilder build, MauiAppBuilder builder)
        {
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("ContactsBook.UI.appsettings.json");
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            builder.Configuration.AddConfiguration(config);
            var baseConnectionString = builder.Configuration.GetConnectionString("AppDbContext");
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "app.db");
            builder.Services.AddDbContext<AppDbContext>(options =>
                  options.UseSqlite(baseConnectionString));
            return builder;
        }
    }
}
