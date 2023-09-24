using PlatesAvaloniaProject.Configuration;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatesAvaloniaProject.Models
{
    public class UpdateInfo:ReactiveObject
    {
        public UpdateInfo(Option option) 
        {
            Key = option.Key;
            UpdateTime = option.UpdateTime; 
            Description = option.Description;
        }
        /// <summary>
        ///  更新标记
        /// </summary>
        [Reactive]
        public int Key { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Reactive]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        ///  更新内容描述
        /// </summary>
        [Reactive]
        public string? Description { get; set; }
    }
}
