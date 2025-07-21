using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace ResearchProjectManagement_SE185005
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            // Ví dụ mở MainWindow với DI
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();

            // Ví dụ thêm service:
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectService>();

            // Nếu dùng DbContext:
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("your-connection-string"));
        }
    }
}