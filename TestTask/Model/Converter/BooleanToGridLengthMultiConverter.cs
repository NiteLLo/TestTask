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
    /// <summary>
    /// Класс предназначенный для переводна одних значений в другие для View, без участия ViewModel
    /// </summary>
    [ValueConversion(typeof(Boolean[]), typeof(GridLength))]
    internal class BooleanToGridLengthMultiConverter : IMultiValueConverter
    {
        /// <summary>
        /// Свойство хранения стандартного значения размерности
        /// </summary>
        public GridLength GridLength { get; set; } = new GridLength(1, GridUnitType.Star);

        /// <summary>
        /// Базовый класс реализуемого интерфейса IMultiValueConverter
        /// Вызывается при попытке конвертации значений 
        /// </summary>
        /// <param name="values">Массив значений для конвертации</param>
        /// <param name="targetType">Целевой тип</param>
        /// <param name="parameter">Необязательный параметр конвертации</param>
        /// <param name="culture">Параметр для передачи настройки текущегго языка системы</param>
        /// <returns>Возврат значения размерности GridLength по условию</returns>
        /// <exception cref="InvalidCastException"></exception>
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
        /// <summary>
        /// Базовый класс реализуемого интрфейса IMultiValueConverter
        /// Вызывается при обратной конвертации значений
        /// Необходим лишь в части случаев
        /// </summary>
        /// <param name="value">Значение для конвертации</param>
        /// <param name="targetTypes">Целевой тип</param>
        /// <param name="parameter">Необязательный параметр конвертации</param>
        /// <param name="culture">Параметр для передачи настройки текущегго языка системы</param>
        /// <returns>Выкидывание исключения о нереализованной функции</returns>
        /// <exception cref="NotImplementedException"></exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
