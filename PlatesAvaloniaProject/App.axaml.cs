using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using PlatesAvaloniaProject.ViewModels;
using PlatesAvaloniaProject.Views;

namespace PlatesAvaloniaProject
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = Program.Service.GetRequiredService<MainWindowViewModel>(),
            };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}