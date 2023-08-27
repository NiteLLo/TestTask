using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TestTask.Model.Converter
{
    [ValueConversion(typeof(Boolean[]), typeof(Boolean))]
    class BooleanArrayToBooleanMultiConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = true;
            foreach (object value in values)
            {
                if (value is not bool)
                {
                    throw new InvalidCastException("The argument is not a bool type");
                }
                if ((bool)value)
                {
                    flag = false;
                    break;
                }

            }
            return flag;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
