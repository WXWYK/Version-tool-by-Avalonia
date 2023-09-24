using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PlatesAvaloniaProject.Configuration
{
    public class UpdateOption
    {
        /// <summary>
        ///  配置集合
        /// </summary>
        public Option[] Options { get; set; } 
    }

    public class Option
    {
        /// <summary>
        ///  更新标记
        /// </summary>
        public int Key { get; set; } = 1;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///  更新内容描述
        /// </summary>
        public string? Description { get; set; } = string.Empty;
    }
}
