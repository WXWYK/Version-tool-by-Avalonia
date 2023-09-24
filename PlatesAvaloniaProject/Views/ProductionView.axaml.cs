using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Avalonia.ReactiveUI;
using DynamicData;
using DynamicData.Binding;
using Microsoft.Extensions.DependencyInjection;
using Nett.Coma;
using PlatesAvaloniaProject.Configuration;
using PlatesAvaloniaProject.Helpers;
using PlatesAvaloniaProject.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

namespace PlatesAvaloniaProject.Views;

public partial class ProductionView : ReactiveUserControl<ProductionViewModel>
{
    public ProductionView()
    {
        InitializeComponent();      
        SetupDnd();
        ViewModel = Program.Service.GetRequiredService<ProductionViewModel>();
    }

    private void SetupDnd()
    {            
        AddHandler(DragDrop.DragOverEvent, DragOver);
    }

    private void DragOver(object? sender, DragEventArgs e)
    {

        if (e.Data.Contains(DataFormats.Files))
        {
            e.DragEffects = DragDropEffects.Link;
        }
        else
        {
            e.DragEffects = DragDropEffects.None;
        }
        var filePath = e.Data.GetFiles().First().TryGetLocalPath();
        var targetPath = FileInfoHelper.GetPathByShortcut(filePath);
        ((ProductionViewModel)DataContext!).TargetPath = targetPath?.WorkingDirectory ?? string.Empty;
    }

    

}