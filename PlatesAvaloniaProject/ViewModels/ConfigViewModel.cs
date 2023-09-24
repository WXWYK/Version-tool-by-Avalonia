using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatesAvaloniaProject.ViewModels
{
    public class ConfigViewModel: ViewModelBase
    {

        public ConfigViewModel()
        {
            Title = "配置";
        }
        
        
        [Reactive]
        public string Title { get; set; }
    }
}
