using TestTask.Model;

namespace TestTaslTests
{
    public class TableFunctionTests
    {
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0, 5, 0, 0, 0, 0)]
        [InlineData(0, 0, 7, 0, 0, 0)]
        [InlineData(0, 0, 0, 4, 0, 0)]
        [InlineData(8, 0, 0, 0, 8, 0)]
        [InlineData(3, 0, 0, 0, 0, 3)]
        [InlineData(11, 2, 0, 4, 0, 3)]
        [InlineData(5, 0, 5, 0, 2, 3)]
        [InlineData(21, 6, 4, 3, 0, 3)]
        [InlineData(28, 10, 5, 2, 3, 5)]
        public void LinearFunctionCalculationTest(int expected, int x, int y, int a, int b, int c)
        {
            var func = new TableFunction();

            Assert.Equal(expected, func.LinearResult(x, y, a, b, c));
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0, 5, 0, 0, 0, 0)]
        [InlineData(0, 0, 7, 0, 0, 0)]
        [InlineData(0, 0, 0, 4, 0, 0)]
        [InlineData(0, 0, 0, 0, 8, 0)]
        [InlineData(3, 0, 0, 0, 0, 3)]
        [InlineData(35, 2, 0, 4, 0, 3)]
        [InlineData(53, 0, 5, 0, 2, 3)]
        [InlineData(651, 6, 4, 3, 0, 3)]
        [InlineData(2080, 10, 5, 2, 3, 5)]
        public void QuadraticFunctionCalculationTest(int expected, int x, int y, int a, int b, int c)
        {
            var func = new TableFunction();

            Assert.Equal(expected, func.CubicResult(x, y, a, b, c));
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0, 5, 0, 0, 0, 0)]
        [InlineData(0, 0, 7, 0, 0, 0)]
        [InlineData(0, 0, 0, 4, 0, 0)]
        [InlineData(0, 0, 0, 0, 8, 0)]
        [InlineData(3, 0, 0, 0, 0, 3)]
        [InlineData(19, 2, 0, 4, 0, 3)]
        [InlineData(13, 0, 5, 0, 2, 3)]
        [InlineData(111, 6, 4, 3, 0, 3)]
        [InlineData(220, 10, 5, 2, 3, 5)]
        public void CubicFunctionCalculationTest(int expected, int x, int y, int a, int b, int c)
        {
            var func = new TableFunction();

            Assert.Equal(expected, func.QuadraticResult(x, y, a, b, c));
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0, 5, 0, 0, 0, 0)]
        [InlineData(0, 0, 7, 0, 0, 0)]
        [InlineData(0, 0, 0, 4, 0, 0)]
        [InlineData(0, 0, 0, 0, 8, 0)]
        [InlineData(3, 0, 0, 0, 0, 3)]
        [InlineData(67, 2, 0, 4, 0, 3)]
        [InlineData(253, 0, 5, 0, 2, 3)]
        [InlineData(3891, 6, 4, 3, 0, 3)]
        [InlineData(20380, 10, 5, 2, 3, 5)]
        public void FourthDegreeFunctionCalculationTest(int expected, int x, int y, int a, int b, int c)
        {
            var func = new TableFunction();

            Assert.Equal(expected, func.FourthDegreeResult(x, y, a, b, c));
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(0, 5, 0, 0, 0, 0)]
        [InlineData(0, 0, 7, 0, 0, 0)]
        [InlineData(0, 0, 0, 4, 0, 0)]
        [InlineData(0, 0, 0, 0, 8, 0)]
        [InlineData(3, 0, 0, 0, 0, 3)]
        [InlineData(131, 2, 0, 4, 0, 3)]
        [InlineData(1253, 0, 5, 0, 2, 3)]
        [InlineData(23331, 6, 4, 3, 0, 3)]
        [InlineData(201880, 10, 5, 2, 3, 5)]
        public void FifthDegreeFunctionCalculationTest(int expected, int x, int y, int a, int b, int c)
        {
            var func = new TableFunction();

            Assert.Equal(expected, func.FifthDegreeResult(x, y, a, b, c));
        }
    }
}