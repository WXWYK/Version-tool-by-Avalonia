using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Data.Converters;
using Avalonia.Data;
using PlatesAvaloniaProject.Models;

namespace PlatesAvaloniaProject.Converters
{
    public class StatusConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var status = (StatusEnum)value;
            switch (status)
            {
                case StatusEnum.Success:
                    return Brushes.Green;
                case StatusEnum.Warning:
                    return Brushes.GreenYellow;
                case StatusEnum.Error:
                    return Brushes.Red;
                default:
                    return Brushes.Gray;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}




