using Avalonia.Controls;
using DynamicData;
using DynamicData.Binding;
using Microsoft.Extensions.DependencyInjection;
using PlatesAvaloniaProject.Configuration;
using PlatesAvaloniaProject.Models;
using PlatesAvaloniaProject.ViewModels;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace PlatesAvaloniaProject.Views
{
    public partial class MainWindow : Window,IViewFor<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            ViewModel = Program.Service.GetRequiredService<MainWindowViewModel>();
            //this.WhenActivated(dispose =>
            //{
            //    this.WhenValueChanged(o => o.ViewModel!.Status)
            //    .Subscribe(o =>
            //    {
            //        if(ViewModel!.Status == StatusEnum.Success)
            //            ((ProductionViewModel)ViewModel.CurrentPage).UpdateOptionList.Edit(innerList => 
            //            {
            //                innerList.Add(new Option() { Key = 1, Description = "xxx", UpdateTime = DateTime.Now }); 
            //            });
            //    });
            //});
        }

        public static Window Instance { get; internal set; }
        public MainWindowViewModel? ViewModel { get; set ; }
        object? IViewFor.ViewModel { get; set ; }


    }
}