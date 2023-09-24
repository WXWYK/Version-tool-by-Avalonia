using DynamicData;
using Nett.Coma;
using PlatesAvaloniaProject.Configuration;
using PlatesAvaloniaProject.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace PlatesAvaloniaProject.ViewModels
{
    public class ProductionViewModel:ViewModelBase
    {
        private readonly ReadOnlyObservableCollection<UpdateInfo> _updateInfos;
        private Config<UpdateOption> _config;
        public ProductionViewModel(Config<UpdateOption> config)
        {
            _config = config;
           
            UpdateOptionList = new();
            UpdateOptionList.Edit(innerList =>
            {
                if(_config.Unmanaged().Options is not null)
                innerList.AddRange(_config.Unmanaged().Options);
            });
            UpdateOptionList
                .Connect()
                .Filter(option=>option.Key>0)
                .Transform(o=>new UpdateInfo(o))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _updateInfos)
                .AutoRefresh()
                .Subscribe(o => _config.Set(x => x.Options, UpdateOptionList.Items));
        }

        [Reactive]
        public string TargetPath { get; set; }

        public ReadOnlyObservableCollection<UpdateInfo> UpdateInfosSource => _updateInfos;

        public SourceList<Option> UpdateOptionList { get; set; }

        

    }

    
}
