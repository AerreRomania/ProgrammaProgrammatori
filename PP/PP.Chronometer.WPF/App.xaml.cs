using Microsoft.Extensions.DependencyInjection;
using PP.Chronometer.WPF.State.Authenticators;
using PP.Chronometer.WPF.State.Navigator;
using PP.Chronometer.WPF.ViewModels;
using PP.Chronometer.WPF.ViewModels.Factories;
using PP.Domain.Services;
using PP.Domain.Services.AuthenticationServices;
using PP.EntityFramework;
using PP.EntityFramework.Services;
using System;
using System.Windows;

namespace PP.Chronometer.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();
            Window window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private static IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<PPDbContextFactory>();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<IEmployeeService, ProgrammerDataService>();
            services.AddSingleton<IArticleService, ArticleDataService>();
            services.AddSingleton<ITaskService, TaskDataService>();
            services.AddSingleton<IJobTypeService, JobTypeDataService>();
            services.AddSingleton<IProgrammerJobService, ProgrammerJobDataService>();

            services.AddSingleton<IChronoViewModelFactory, ChronoViewModelFactory>();
            services.AddSingleton<AssistanceTasksViewModel>();
            services.AddSingleton<AssignedTasksViewModel>();
            services.AddSingleton(serviceProvider => new ChronometerViewModel(serviceProvider.GetRequiredService<IProgrammerJobService>()));

            services.AddSingleton<LoginViewModel>();

            services.AddSingleton<CreateViewModel<ChronometerViewModel>>(serviceProvider => serviceProvider.GetRequiredService<ChronometerViewModel>);
            services.AddSingleton<CreateViewModel<AssistanceTasksViewModel>>(serviceProvider => serviceProvider.GetRequiredService<AssistanceTasksViewModel>);
            services.AddSingleton<CreateViewModel<AssignedTasksViewModel>>(serviceProvider => serviceProvider.GetRequiredService<AssignedTasksViewModel>);

            services.AddSingleton<CreateViewModel<LoginViewModel>>(serviceProvider => serviceProvider.GetRequiredService<LoginViewModel>);
            services.AddSingleton<ViewModelDelegateRenavigator<AssignedTasksViewModel>>();
            services.AddSingleton<CreateViewModel<LoginViewModel>>(serviceProvider =>
            {
                return () => new LoginViewModel(
                    serviceProvider.GetRequiredService<IAuthenticator>(),
                    serviceProvider.GetRequiredService<IEmployeeService>(),
                    serviceProvider.GetRequiredService<ViewModelDelegateRenavigator<AssignedTasksViewModel>>());
            });

            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<IAuthenticator, Authenticator>();

            services.AddScoped<MainViewModel>();
            services.AddScoped(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}