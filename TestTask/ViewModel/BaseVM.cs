using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.ViewModel
{
    /// <summary>
    /// Базовый класс модели представления
    /// </summary>
    internal class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Реализация интерфейса INotifyPropertyChanged даёт возможность сообщать об изменениях в свойствах
        /// </summary>
        /// <param name="propertyName">Имя свойства</param>
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
