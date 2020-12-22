using Microsoft.Extensions.DependencyInjection;
using PP.Domain.Services;
using PP.Domain.Services.TransactionServices;
using PP.EntityFramework;
using PP.EntityFramework.Services;
using PP.WPF.ViewModels;
using PP.WPF.ViewModels.Factories;
using System;
using System.Windows;
using PP.Domain.Services.AuthenticationServices;
using PP.WPF.State.Authenticators;
using PP.WPF.State.Navigator;

namespace PP.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();
            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<PPDbContextFactory>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<IEmployeeService, ProgrammerDataService>();
            services.AddSingleton<IArticleService, ArticleDataService>();
            services.AddSingleton<ITaskService, TaskDataService>();
            services.AddSingleton<IJobTypeService, JobTypeDataService>();
            services.AddSingleton<IPPArticleService, PPArticleDataService>();
            services.AddSingleton<IProgrammerJobService, ProgrammerJobDataService>();


            services.AddSingleton<IPPViewModelFactory, PPViewModelFactory>();
        
            services.AddSingleton<LoginViewModel>();

            services.AddSingleton( serviceProvider=> new HomeViewModel(
                serviceProvider.GetRequiredService<IArticleService>(),
                serviceProvider.GetRequiredService<IEmployeeService>(),
                serviceProvider.GetRequiredService<ITaskService>(),
                serviceProvider.GetRequiredService<IPPArticleService>()));

            services.AddSingleton(serviceProvider=> new TrackingViewModel(
                serviceProvider.GetRequiredService<IArticleService>(), serviceProvider.GetRequiredService<IEmployeeService>(), serviceProvider.GetRequiredService<IProgrammerJobService>()));

            services.AddSingleton<CreateViewModel<HomeViewModel>>(serviceProvider => serviceProvider.GetRequiredService<HomeViewModel>);

            services.AddSingleton<CreateViewModel<TrackingViewModel>>(serviceProvider => serviceProvider.GetRequiredService<TrackingViewModel>);
            services.AddSingleton<CreateViewModel<LoginViewModel>>(serviceProvider => serviceProvider.GetRequiredService<LoginViewModel>);
            services.AddSingleton<ViewModelDelegateRenavigator<HomeViewModel>>();
            services.AddSingleton<CreateViewModel<LoginViewModel>>(serviceProvider =>
            {
                return () => new LoginViewModel(
                    serviceProvider.GetRequiredService<IAuthenticator>(),
                    serviceProvider.GetRequiredService<IEmployeeService>(),
                    serviceProvider.GetRequiredService<ViewModelDelegateRenavigator<HomeViewModel>>());
            });



            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<IAuthenticator, Authenticator>();

            services.AddScoped<MainViewModel>();
            services.AddScoped<LoginViewModel>();
            services.AddScoped(s => new MainWindow(s.GetRequiredService<MainViewModel>()));


            return services.BuildServiceProvider();
        }
    }
}
