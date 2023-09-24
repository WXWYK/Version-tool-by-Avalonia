using Avalonia;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Text;
using LogHelper;
using Microsoft.Extensions.Logging;
using PlatesAvaloniaProject.ViewModels;
using ReactiveUI;
using Nett.Coma;
using PlatesAvaloniaProject.Configuration;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

namespace PlatesAvaloniaProject
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            Service = ConfigureServices();
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);         
        }
       
        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace()
                .UseReactiveUI();
        public static ServiceProvider ConfigureServices()
        {
            var config = new LoggerConfiguration()
          //设置最小日志级别
          .MinimumLevel.Information()
          .WriteTo.Conditional(x => x.Level == Serilog.Events.LogEventLevel.Information || x.Level == Serilog.Events.LogEventLevel.Error, con => con.File(
              $"logs/{DateTime.Now:yyyy-MM-dd}/logInfo.dat", //日志按照天为单位创建文件夹
              outputTemplate: @"{Timestamp:yyyy-MM-dd HH:mm-ss.fff }[{Level:u3}] {Message:lj}{NewLine}{Exception}",  // 设置输出格式，显示详细异常信息
              rollingInterval: RollingInterval.Day, //日志按天保存
              rollOnFileSizeLimit: true,            // 限制单个文件的最大长度
              fileSizeLimitBytes: 10 * 1024,        // 单个文件最大长度10K
              encoding: Encoding.Default,              // 文件字符编码
              retainedFileCountLimit: 10           // 最大保存文件数,超过最大文件数会自动覆盖原有文件
              ));

            var services = new ServiceCollection();
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilogSetup(config);
            });
            var assembly = Assembly.GetExecutingAssembly();
            var configPath = Path.ChangeExtension(assembly.Location, "tml");
            var tomlConfig = Config.CreateAs()
              .MappedToType(() => new UpdateOption())
              .StoredAs(store => store.File(configPath))
              .Initialize();
            services.AddSingleton(tomlConfig);
            services.AddSingleton<ProductionViewModel>();
            services.AddSingleton<ConfigViewModel>();
            services.AddSingleton<MainWindowViewModel>();
        
            return services.BuildServiceProvider();
        }

        public static ServiceProvider Service { get;private set; }
    }
}