using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatesAvaloniaProject.Models
{
    public enum StatusEnum
    {

        None = 0,

        /// <summary>
        ///  成功
        /// </summary>
        Success = 1,
        
        /// <summary>
        ///  警告
        /// </summary>
        Warning = 2,

        /// <summary>
        ///  错误
        /// </summary>
        Error = 3,
    }
}
