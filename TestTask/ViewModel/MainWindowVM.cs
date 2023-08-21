using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestTask.ViewModel;
using TestTask.Model;
using TestTask.Model.Enum;

namespace TestTask.ViewModel
{
    /// <summary>
    /// Основной класс модели представления, наследуюется от базового
    /// </summary>
    internal class MainWindowVM : BaseVM
    {
        #region COLLECTIONS
        /// <summary>
        /// Два списка для сохранения значений соэффициентов при переходе по степенным функциям
        /// </summary>
        private string[] coefficientAList = new string[5];
        private string[] coefficientBList = new string[5];

        /// <summary>
        /// Свойство для хранения и перезаполнения списка коэффициентов "c"
        /// </summary>
        public IEnumerable<int> CoefficientCSelections
        {
            get { return coefficientCSelections; }
            set
            {
                coefficientCSelections = from x in baseCoefficientC
                                         select x * value.First(); NotifyPropertyChanged(nameof(CoefficientCSelections));

                CoefficientCBox.SelectedIndex = Convert.ToInt32(SelectedCoefficientC.ToString()[0].ToString()) - 1;

            }
        }

        private IEnumerable<int> coefficientCSelections = new ObservableCollection<int>
        { 1, 2, 3, 4, 5};

        private readonly IEnumerable<int> baseCoefficientC = new ObservableCollection<int>
        { 1, 2, 3, 4, 5};

        /// <summary>
        /// Коллекция табличных значений со всеми коэффициентами и переменными
        /// </summary>
        private IEnumerable<TableFunction> tableFunctions = new ObservableCollection<TableFunction>()
        { new TableFunction("0", "0", 0, 0, 0, FunctionRankName.Linear) };
        public IEnumerable<TableFunction> TableFunctions
        {
            get { return tableFunctions; }
            set
            {
                tableFunctions = value; NotifyPropertyChanged(nameof(TableFunctions));
            }
        }

        #endregion

        #region COEFFICIENTS


        private string coefficientA = "0";
        /// <summary>
        /// Публичное свойство коэфициента a
        /// Содержит дополнительно передачу значения в список значений и вызов расчётного метода
        /// </summary>
        public string CoefficientA
        {
            get { return coefficientA; }
            set
            {
                coefficientA = value; NotifyPropertyChanged(nameof(CoefficientA));
                coefficientAList[CoefficientCBox.SelectedValue.ToString().Length - 1] = CoefficientA;
                CalculateRow();
            }
        }

        private string coefficientB = "0";
        /// <summary>
        /// Публичное свойство коэфициента a
        /// Содержит дополнительно передачу значения в список значений и вызов расчётного метода
        /// </summary>
        public string CoefficientB
        {
            get { return coefficientB; }
            set
            {
                coefficientB = value; NotifyPropertyChanged(nameof(CoefficientB));
                coefficientBList[CoefficientCBox.SelectedValue.ToString().Length - 1] = CoefficientB;
                CalculateRow();
            }
        }

        private int selectedCoefficientC;
        /// <summary>
        /// Свойство для хранения выбранного коэффициента "c"
        /// Содержит дополнительно передачу коэффциента "c" выбранному элементу таблицы и вызов расчётного метода
        /// </summary>
        public int SelectedCoefficientC
        {
            get { return selectedCoefficientC; }
            set
            {
                selectedCoefficientC = value; NotifyPropertyChanged(nameof(SelectedCoefficientC));
                SelectedRowFunnction.EditCoefficientC(SelectedCoefficientC);
                if (FunctionTable != null)
                    CalculateRow();
            }
        }

        #endregion

        #region UI_ELEMENTS
        /// <summary>
        /// Свойство для хранения UI элемента выпадающего списка 
        /// </summary>
        private ComboBox coefficientCBox;
        public ComboBox CoefficientCBox
        {
            get { return coefficientCBox; }
            set { coefficientCBox = value; NotifyPropertyChanged(nameof(CoefficientCBox)); }
        }

        /// <summary>
        /// Свойство для хранения UI элемента последнего активированного флажка
        /// </summary>
        private CheckBox lastCheckBox = new CheckBox() { IsChecked = false };
        public CheckBox LastCheckBox
        {
            get { return lastCheckBox; }
            set { lastCheckBox = value; NotifyPropertyChanged(nameof(LastCheckBox)); }
        }

        /// <summary>
        /// Свойство для хранения Ui элемента списка степенных функций
        /// </summary>
        private ListView functionRank;
        public ListView FunctionRank
        {
            get { return functionRank; }
            set { functionRank = value; NotifyPropertyChanged(nameof(FunctionRank)); }
        }

        /// <summary>
        /// Свойство для хранения UI элемента
        /// </summary>
        private DataGrid functionTable;
        public DataGrid FunctionTable
        {
            get { return functionTable; }
            set { functionTable = value; NotifyPropertyChanged(nameof(FunctionTable)); }
        }
        #endregion

        #region COMMANDS
        /// <summary>
        /// Команда перехватывающая событие на изменение TextBox для валидации данных
        /// Дополнительно содержит передачу коэфициентов в выбранный элемент таблицы и вызов расчётного метода
        /// </summary>
        private RelayCommand validOnNumber;
        public RelayCommand ValidOnNumber
        {
            get
            {
                return validOnNumber ?? new RelayCommand(obj =>
                {
                    var tb = obj as TextBox;
                    var output = Regex.Replace(tb.Text, @"[^0-9]+", new string(""));

                    tb.Text = output == "" ? "0" : output;
                    tb.CaretIndex = tb.Text.Length;

                    SelectedRowFunnction.EditCoefficientA(Convert.ToInt32(CoefficientA));
                    SelectedRowFunnction.EditCoefficientB(Convert.ToInt32(CoefficientB));
                    CalculateRow();
                });
            }
        }

        /// <summary>
        /// Команда для передачи ссылки на UI элемент
        /// </summary>
        private RelayCommand loadCoefficientCBox;
        public RelayCommand LoadCoefficientCBox
        {
            get
            {
                return loadCoefficientCBox ?? new RelayCommand(obj =>
                {
                    CoefficientCBox = obj as ComboBox;
                });
            }
        }
        /// <summary>
        /// Команда для запуска расчётного метода при изменении выбранного коэффициента "c"
        /// </summary>
        private RelayCommand selectionChangedCoefficientCBox;
        public RelayCommand SelectionChangedCoefficientCBox
        {
            get
            {
                return selectionChangedCoefficientCBox ?? new RelayCommand(obj =>
                {
                    CalculateRow();
                });
            }
        }


        /// <summary>
        /// Команда для сопряжения выпадающего списка коэффициентов "c" и выбранной функции
        /// </summary>

        private RelayCommand checkedFunctionRank;
        public RelayCommand CheckedFunctionRank
        {
            get
            {
                return checkedFunctionRank ?? new RelayCommand(obj =>
                {
                    if (lastCheckBox == null)
                    {
                        var view = obj as ListViewItem;
                        lastCheckBox = (CheckBox?)(obj as ListViewItem).Content;

                        CoefficientCSelections = new ObservableCollection<int> { Convert.ToInt32(Math.Pow(10, FunctionRank.Items.IndexOf(obj as ListViewItem))) };
                    }
                    else if (lastCheckBox != (CheckBox?)(obj as ListViewItem).Content)
                    {
                        lastCheckBox.IsChecked = false;
                        lastCheckBox = (CheckBox?)(obj as ListViewItem).Content;

                        CoefficientCSelections = new ObservableCollection<int> { Convert.ToInt32(Math.Pow(10, FunctionRank.Items.IndexOf(obj as ListViewItem))) };
                    }

                    SelectedRowFunnction.EditFunctionRank((FunctionRankName)FunctionRank.Items.IndexOf(obj as ListViewItem));
                    CoefficientA = coefficientAList[FunctionRank.Items.IndexOf(obj as ListViewItem)];
                    CoefficientB = coefficientBList[FunctionRank.Items.IndexOf(obj as ListViewItem)];

                    CalculateRow();
                });
            }
        }

        /// <summary>
        /// Команда для загрузки UI элемента
        /// </summary>
        private RelayCommand loadListFunctionRank;
        public RelayCommand LoadListFunctionRank
        {
            get
            {
                return loadListFunctionRank ?? new RelayCommand(obj =>
                {
                    functionRank = obj as ListView;
                });
            }
        }

        /// <summary>
        /// Команда для передачи UI элемента
        /// </summary>
        private RelayCommand loadFunctionTable;
        public RelayCommand LoadFunctionTable
        {
            get
            {
                return loadFunctionTable ?? new RelayCommand(obj =>
                {
                    FunctionTable = obj as DataGrid;
                });
            }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Метод расчёта и обновления функции в таблице
        /// </summary>
        private void CalculateRow()
        {
            SelectedRowFunnction.CalculateMainFunc();
            FunctionTable.Items.Refresh();
        }
        #endregion

        /// <summary>
        /// Свойство для хранения выбранного элемента таблицы
        /// </summary>
        public TableFunction SelectedRowFunnction { get; set; } = new TableFunction("0", "0", 0, 0, 0, FunctionRankName.Linear);
        
        
        
    }
}
