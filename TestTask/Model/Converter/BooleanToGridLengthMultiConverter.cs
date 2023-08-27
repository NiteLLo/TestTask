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
    [ValueConversion(typeof(Boolean[]), typeof(GridLength))]
    internal class BooleanToGridLengthMultiConverter : IMultiValueConverter
    {
        public GridLength GridLength { get; set; } = new GridLength(1, GridUnitType.Star);

        //public BooleanToWidthMultiConverter(int numberOfUnits, GridUnitType gridUnitType)
        //{
        //    GridLength = new GridLength(numberOfUnits, gridUnitType);
        //}

        public BooleanToGridLengthMultiConverter() { }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;

            foreach (object value in values) 
            {
                if (value is not bool)
                {
                    throw new InvalidCastException("The argument is not a bool type");
                }

                if ((bool)value)
                    flag = true;

            }
            return flag ? GridLength.Value : new GridLength(0, GridUnitType.Pixel);

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
