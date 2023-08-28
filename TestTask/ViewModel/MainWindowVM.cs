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
using System.Windows.Data;

namespace TestTask.ViewModel
{
    /// <summary>
    /// Основной класс модели представления, наследуюется от базового
    /// </summary>
    internal class MainWindowVM : BaseVM
    {

        #region COLLECTIONS

        private CollectionViewSource viewSource = new CollectionViewSource();
        public CollectionViewSource ViewSource 
        {
            get { viewSource.Source = TableFunctions; return viewSource;  }
            set { viewSource = value; NotifyPropertyChanged(nameof(ViewSource)); } 
        }

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
                coefficientCSelections = value; NotifyPropertyChanged(nameof(CoefficientCSelections));

                SelectedCoefficientC = SelectedCoefficientC == null ? (int)Math.Pow(10, (int)SelectedRank) :
                    (Convert.ToInt32(SelectedCoefficientC.ToString()[0].ToString()) - 1) * (int)Math.Pow(10, (int)SelectedRank);

            }
        }

        private IEnumerable<int> coefficientCSelections = new ObservableCollection<int>
        { 1, 2, 3, 4, 5};

        private readonly IEnumerable<int> baseCoefficientC = new ObservableCollection<int>
        { 1, 2, 3, 4, 5};

        /// <summary>
        /// Коллекция табличных значений со всеми коэффициентами и переменными
        /// </summary>
        private ObservableCollection<TableFunction> tableFunctions = new ObservableCollection<TableFunction>()
        { new TableFunction("0", "0", 0, 0, 1, FunctionRankName.Linear) };
        public ObservableCollection<TableFunction> TableFunctions
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

                if (SelectedTableItem is not null)
                    TableFunctions.First(x => x == SelectedTableItem).EditCoefficientA(Convert.ToInt32(value));

                RefreshTable();
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

                if (SelectedTableItem is not null)
                    TableFunctions.FirstOrDefault(x => x == SelectedTableItem).EditCoefficientB(Convert.ToInt32(value));

                RefreshTable();
            }
        }

        private object? selectedCoefficientC;
        /// <summary>
        /// Свойство для хранения выбранного коэффициента "c"
        /// Содержит дополнительно передачу коэффциента "c" выбранному элементу таблицы и вызов расчётного метода
        /// </summary>
        public object? SelectedCoefficientC
        {
            get { return selectedCoefficientC; }
            set
            {
                selectedCoefficientC = value; NotifyPropertyChanged(nameof(SelectedCoefficientC));
                
                    if (SelectedTableItem is not null)
                        TableFunctions.FirstOrDefault(x => x == SelectedTableItem).EditCoefficientC(Convert.ToInt32(value));
                    RefreshTable();
            }
        }

        /// <summary>
        /// Свойство для хранения выбранной степенной функции
        /// </summary>
        private FunctionRankName selectedRank = FunctionRankName.Linear;
        public FunctionRankName SelectedRank
        {
            get { return selectedRank; }
            set 
            { 
                selectedRank = value; 
                NotifyPropertyChanged(nameof(SelectedRank));

                CoefficientCSelections = from x in baseCoefficientC
                                         select x * (int)Math.Pow(10, (int)SelectedRank);
            }
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

                    RefreshTable();
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
                    RefreshTable();
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
                    try
                    {
                        if (obj is not null)
                        {
                            SelectedRank = (FunctionRankName)Convert.ToInt32(obj as string);
                        }
                    }
                    catch (InvalidCastException ex)
                    {
                        throw new InvalidCastException(ex.Message);
                    }
                    finally 
                    {

                        if (SelectedTableItem is not null)
                            TableFunctions.FirstOrDefault(x => x == SelectedTableItem).EditFunctionRank(SelectedRank);

                        RefreshTable();
                    }


                   
                });
            }
        }    
        #endregion

        #region METHODS
        /// <summary>
        /// Метод расчёта и обновления функции в таблице
        /// </summary>
        private void RefreshTable()
        {
            ViewSource.View.Refresh();
        }

        private void SwitchRank(FunctionRankName tbFunc)
        {
            switch (tbFunc) 
            {
                case FunctionRankName.Linear:
                    LinearIsChecked = true;
                    break;
                case FunctionRankName.Quadratic:
                    QuadraticIsChecked = true;
                    break;
                case FunctionRankName.Cubic:
                    CubicIsChecked = true;
                    break;
                case FunctionRankName.FourthDegree:
                    FourthDegreeIsChecked = true;
                    break;
                case FunctionRankName.FifthDegree:
                    FifthDegreeIsChecked = true;
                    break;
            }
        }
        #endregion

        private TableFunction selectedTableItem;
        public TableFunction SelectedTableItem
        {
            get { return  selectedTableItem; }
            set 
            {
                selectedTableItem = value; NotifyPropertyChanged(nameof(SelectedTableItem));

                if (SelectedTableItem is not null)
                {
                    coefficientA = SelectedTableItem.GetCoefficientA().ToString();
                    NotifyPropertyChanged(nameof(CoefficientA));

                    coefficientB = SelectedTableItem.GetCoefficientB().ToString();
                    NotifyPropertyChanged(nameof(CoefficientB));

                    selectedCoefficientC = SelectedTableItem.GetCoefficientC();
                    NotifyPropertyChanged(nameof(SelectedCoefficientC));

                    selectedRank = SelectedTableItem.GetRank();
                    SwitchRank(selectedRank);
                    NotifyPropertyChanged(nameof(SelectedRank));

                    RefreshTable();
                }
                
            }
        }
        private bool linearIsChecked = true;
        public bool LinearIsChecked
        {
            get { return linearIsChecked; }
            set
            {
                linearIsChecked = value; NotifyPropertyChanged(nameof(LinearIsChecked));
                NotifyPropertyChanged(nameof(SelectedCoefficientC));
            }
        }

        private bool quadraticIsChecked;
        public bool QuadraticIsChecked
        {
            get { return quadraticIsChecked; }
            set
            {
                quadraticIsChecked = value; NotifyPropertyChanged(nameof(QuadraticIsChecked));
                NotifyPropertyChanged(nameof(SelectedCoefficientC));
            }
        }

        private bool cubicIsChecked;
        public bool CubicIsChecked
        {
            get { return cubicIsChecked; }
            set
            {
                cubicIsChecked = value; NotifyPropertyChanged(nameof(CubicIsChecked));
                NotifyPropertyChanged(nameof(SelectedCoefficientC));
            }
        }

        private bool fourthDegreeIsChecked;
        public bool FourthDegreeIsChecked
        {
            get { return fourthDegreeIsChecked; }
            set
            {
                fourthDegreeIsChecked = value; NotifyPropertyChanged(nameof(FourthDegreeIsChecked));
            }
        }

        private bool fifthDegreeIsChecked;
        public bool FifthDegreeIsChecked
        {
            get { return fifthDegreeIsChecked; }
            set
            {
                fifthDegreeIsChecked = value; NotifyPropertyChanged(nameof(FifthDegreeIsChecked));
            }
        }
    }
}
