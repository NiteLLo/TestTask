using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestTask.Model.Enum;

namespace TestTask.Model
{
    /// <summary>
    /// Класс-Модель данных для таблицы данных
    /// </summary>
    public class TableFunction
    {
        /// <summary>
        /// Пустой конструктор для тестов
        /// </summary>
        public TableFunction() { }
        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="x">Значение переменной x</param>
        /// <param name="y">Значение переменной y</param>
        /// <param name="a">Значение коэффициента a</param>
        /// <param name="b">Значение коэффициента b</param>
        /// <param name="c">Значение коэффициента c</param>
        /// <param name="rank">Степень функции</param>
        public TableFunction(string x, string y, int a, int b, int c, FunctionRankName rank)
        {
            X = x;
            Y = y;
            this.a = a;
            this.b = b;
            this.c = c;
            this.rank = rank;

            CalculateMainFunc();
        }

        /// <summary>
        /// Публичные свойства переменных x и y и валидация данных внутри
        /// </summary>
        private string x;
        public string X
        {
            get { return x; }
            set
            {
                x = value;

                var output = Regex.Replace(x, @"[^0-9]+", new string(""));

                x = output == "" ? "0" : output;

                CalculateMainFunc();
            }
        }

        private string y;
        public string Y
        {
            get { return y; }
            set
            {
                y = value;

                var output = Regex.Replace(y, @"[^0-9]+", new string(""));

                y = output == "" ? "0" : output;

                CalculateMainFunc();
            }
        }

        /// <summary>
        /// Публичное свойство результата функции
        /// </summary>
        private int f = 0;
        public int F { get { return f; } }

        private int a;
        private int b;
        private int c;
        private FunctionRankName rank;

        /// <summary>
        /// Метод для изменения коэффициента a
        /// </summary>
        /// <param name="a"></param>
        public void EditCoefficientA(int a)
        {
            this.a = a;
        }
        /// <summary>
        /// Метод для изменения коэффициента b
        /// </summary>
        /// <param name="b"></param>
        public void EditCoefficientB(int b)
        {
            this.b = b;
        }
        /// <summary>
        /// Метод для изменения коэффициента c
        /// </summary>
        /// <param name="c"></param>
        public void EditCoefficientC(int c)
        {
            this.c = c;
        }
        /// <summary>
        /// Метод для изменения степени функции
        /// </summary>
        /// <param name="rank"></param>
        public void EditFunctionRank(FunctionRankName rank)
        {
            this.rank = rank;
        }
        /// <summary>
        /// Метод выбора расчётов
        /// </summary>
        public void CalculateMainFunc()
        {
            switch (rank)
            {
                case FunctionRankName.Linear:
                    LinearResult();
                    break;
                case FunctionRankName.Quadratic:
                    QuadraticResult();
                    break;
                case FunctionRankName.Cubic:
                    CubicResult();
                    break;
                case FunctionRankName.FourthDegree:
                    FourthDegreeResult();
                    break;
                case FunctionRankName.FifthDegree:
                    FifthDegreeResult();
                    break;
            }
        }
        /// <summary>
        /// Метод расчёта линейной функции
        /// </summary>
        public void LinearResult()
        {
            f = a * Convert.ToInt32(Math.Pow(Convert.ToInt32(X), 1)) + b * Convert.ToInt32(Math.Pow(Convert.ToInt32(Y), 0)) + c;
        }
        /// <summary>
        /// Метод расчёта квадратичной функции
        /// </summary>
        public void QuadraticResult()
        {
            f = a * Convert.ToInt32(Math.Pow(Convert.ToInt32(X), 2)) + b * Convert.ToInt32(Math.Pow(Convert.ToInt32(Y), 1)) + c;
        }
        /// <summary>
        /// Метод расчёта кубической функции
        /// </summary>
        public void CubicResult()
        {
            f = a * Convert.ToInt32(Math.Pow(Convert.ToInt32(X), 3)) + b * Convert.ToInt32(Math.Pow(Convert.ToInt32(Y), 2)) + c;
        }
        /// <summary>
        /// Метод расчёта функции 4-ой степени
        /// </summary>
        public void FourthDegreeResult()
        {
            f = a * Convert.ToInt32(Math.Pow(Convert.ToInt32(X), 4)) + b * Convert.ToInt32(Math.Pow(Convert.ToInt32(Y), 3)) + c;
        }
        /// <summary>
        /// Метод расчёта функции 5-ой степени
        /// </summary>
        public void FifthDegreeResult()
        {
            f = a * Convert.ToInt32(Math.Pow(Convert.ToInt32(X), 5)) + b * Convert.ToInt32(Math.Pow(Convert.ToInt32(Y), 4)) + c;
        }

        /// <summary>
        /// Функции для тестов
        /// </summary>
        #region FOR_TEST
        public int LinearResult(int x, int y, int a, int b, int c)
        {
            return a * Convert.ToInt32(Math.Pow(x, 1)) + b * Convert.ToInt32(Math.Pow(y, 0)) + c;
        }

        public int QuadraticResult(int x, int y, int a, int b, int c)
        {
            return a * Convert.ToInt32(Math.Pow(x, 2)) + b * Convert.ToInt32(Math.Pow(y, 1)) + c;
        }

        public int CubicResult(int x, int y, int a, int b, int c)
        {
            return a * Convert.ToInt32(Math.Pow(x, 3)) + b * Convert.ToInt32(Math.Pow(y, 2)) + c;
        }

        public int FourthDegreeResult(int x, int y, int a, int b, int c)
        {
            return a * Convert.ToInt32(Math.Pow(x, 4)) + b * Convert.ToInt32(Math.Pow(y, 3)) + c;
        }

        public int FifthDegreeResult(int x, int y, int a, int b, int c)
        {
            return a * Convert.ToInt32(Math.Pow(x, 5)) + b * Convert.ToInt32(Math.Pow(y, 4)) + c;
        }
        #endregion
    }
}
