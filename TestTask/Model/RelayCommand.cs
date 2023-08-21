using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestTask.Model
{
    public class RelayCommand : ICommand
    //Класс, реализующий интерфейс команд
    //Нужен для использования паттерна команд в MVVM
    {
        /// <summary>
        /// Приватные поля базовых делегатов
        /// <param name="_execute">Один из встроенных базовых делегатов, что реализует исполнение команды</param>
        /// <param name="_canExecute">Один из встроенных базовых делегатов, что реализует исполнение команды при соблюдениии условия</param>
        /// </summary>
        private Action<object> _execute;
        private Func<object, bool> _canExecute;
        /// <summary>
        /// <param name="CanExecuteChanged">Свойство, отвечающее за возможность изменения команды</param>
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="execute">Определение делегата исполнителя</param>
        /// <param name="canExecute">Определение делегада возможности исполнить, изначально равен null</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        /// <summary>
        /// Функция определения возможности произведения команды
        /// </summary>
        /// <param name="parameter">Параметр</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }
        /// <summary>
        /// Функция выполнения команды
        /// </summary>
        /// <param name="parameter">Параметр</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
