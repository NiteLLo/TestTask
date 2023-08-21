using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using TestTask.ViewModel;

namespace TestTask
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            SetupDependencyInjection();
        }
        /// <summary>
        /// Метод установки DI контейнера
        /// </summary>
        private static void SetupDependencyInjection()
        {
            Ioc.Default.ConfigureServices(
                (IServiceProvider)new ServiceCollection()
                .AddSingleton<BaseVM>()
                .AddSingleton<MainWindowVM>()
                .BuildServiceProvider()
                );
        }
    }
}
