using Avalonia.Animation;
using Avalonia.Controls;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Windows.Input;
using Avalonia.Dialogs;
using Avalonia.Controls;
using System.Threading.Tasks;
using System.IO;
using Avalonia;
using System.Linq;
using PlatesAvaloniaProject.Helpers;
using Serilog.Core;
using Serilog;
using LogHelper;
using Microsoft.Extensions.DependencyInjection;
using Avalonia.Platform.Storage;
using PlatesAvaloniaProject.Views;
using System.Security.Cryptography;
using Avalonia.Threading;
using PlatesAvaloniaProject.Models;
using System.Collections.ObjectModel;
using Nett.Coma;
using PlatesAvaloniaProject.Configuration;

namespace PlatesAvaloniaProject.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly ViewModelBase[] Pages =
        {
            Program.Service.GetRequiredService<ProductionViewModel>()
        };

        private ILogger _logger;
        private Config<UpdateOption> _config;
        public MainWindowViewModel(Config<UpdateOption> config) 
        {
            _logger = SerilogHelper.Logger;
            _config = config;

            PageSlide = new CrossFade() { Duration = TimeSpan.FromSeconds(0.3) };
            ConfigCommand = ReactiveCommand.Create(ExecConfig);
            MainCommand = ReactiveCommand.Create(ExecMain);
            ImportCommand = ReactiveCommand.Create(ExecImport);

            CurrentPage = Pages[0];
            Status = StatusEnum.None;       
        }

        [Reactive]
        public ViewModelBase CurrentPage{ get; set; }

        [Reactive]
        public IPageTransition PageSlide { get;set; }

        [Reactive]
        public StatusEnum Status { get; set; }

        public ICommand ConfigCommand { get;set; }
        public ICommand ImportCommand { get;set; }
        public ICommand StopCommand { get;set; }
        public ICommand MainCommand { get;set; }



        private void ExecConfig()
        {
            CurrentPage = Pages[1];
        }
        private void ExecMain()
        {
            CurrentPage = Pages[0];
        }
        private async void ExecImport()
        {
            var model = (ProductionViewModel)Pages[0];
            var path = model.TargetPath;
            if (string.IsNullOrEmpty(path))
            {
                Status = StatusEnum.None;
                return;
            }
            await Task.Run(async() =>
            {
                var topLevel = TopLevel.GetTopLevel(MainWindow.Instance);
                var files = await topLevel!.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    Title = "Open File",
                    AllowMultiple = true,
                });
                var result = files.Select(o=>o.Path.AbsolutePath).ToArray();
                try
                {
                    Status = StatusEnum.None;
                    int num = result.Length;
                    int i = 0;
                    var folderPath = @"D:\net6.0-windows";
                    var backUpPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @$"Backup\{DateTime.Now:yy-MM-dd-HH-mm-ss}");

                    DirectoryInfo folder = new DirectoryInfo(folderPath);
                    var fileInfoHelpers = folder.GetFiles("*.*", SearchOption.AllDirectories).Select(o => new FileInfoHelper(o));

                    foreach (FileInfoHelper file in fileInfoHelpers)
                    {
                        // 处理每个文件
                        foreach (var item in result)
                        {
                            if (file.Name == new DirectoryInfo(item).Name)
                            {
                                if (file.Equals(new FileInfo(item)))
                                {
                                    i++;
                                    continue;
                                }
                                if (!Directory.Exists(backUpPath)) Directory.CreateDirectory(backUpPath);
                                var fileName = file.FullName;                            

                                file.MoveTo(Path.Combine(backUpPath, file.Name));    
                                File.Copy(item, fileName, true);
                                i++;
                            }
                        }
                    }
                    if(num == i && num!=0)
                    {
                        Status = StatusEnum.Success;
                        ((ProductionViewModel)CurrentPage).UpdateOptionList.Edit(innerList => innerList.Add(
                            new Option() { Key = _config.Unmanaged().Options[^1].Key + 1, Description = $"更新成功{string.Join(" ", result)}", UpdateTime = DateTime.Now }));
                       _logger.Information(Status.ToString());
                    }
                    else
                    {
                        Status = StatusEnum.Warning;
                        _logger.Error(Status.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Status = StatusEnum.Error;
                    _logger.Error(Status.ToString());
                }            
            });           
        }
    }
}