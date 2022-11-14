using AutoMapper;
using BusinessLogicLayer;
using BusinessLogicLayer.Authentication;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System.Windows;
using WarNotes.View;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace WarNotes
{
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = new ConfigurationBuilder()
                        .AddUserSecrets<App>(true)
                        .Build();

                    var sqlConnectionString = configuration.GetConnectionString("WarNotesDB");

                    services.AddDbContext<WarNotesContext>(options =>
                        options.UseNpgsql(sqlConnectionString));

                    services.AddScoped<IUserService, UserService>();
                    services.AddScoped<IAuthenticationService, AuthenticationService>();
                    services.AddScoped<IAuthenticator, Authenticator>();
                    services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
                    services.AddScoped<IArticleRepository, ArticleRepository>();
                    services.AddScoped<IArticleService, ArticleService>();
                    services.AddScoped<ICategoryService, CategoryService>();
                    services.AddScoped<IUserRepository, UserRepository>();

                    services.AddSingleton<LoginView>();
                    
                    var mapperConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new AutoMapperProfile());
                    });

                    IMapper mapper = mapperConfig.CreateMapper();
                    services.AddSingleton(mapper);
                    services.AddSingleton<MainView>();

                })
                .ConfigureLogging(logBuilder =>
                {
                    logBuilder.SetMinimumLevel(LogLevel.Information);
                    logBuilder.AddNLog("nLog.config");
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var startupForm = AppHost.Services.GetRequiredService<LoginView>();
            startupForm.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}
