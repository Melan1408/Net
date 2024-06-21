using ContactsBook.DAL.Data;
using ContactsBook.UI.ViewModels;
using ContactsBook.UI.Views;
using Microsoft.Extensions.Logging;

namespace ContactsBook.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.AddCore(builder);
            builder.Services.AddScoped<MainPageViewModel>();
            builder.Services.AddScoped<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var host = builder.Build();
            using (var scope = host.Services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dataContext.Database.EnsureCreated();
            }

            return host;
        }
    }
}
