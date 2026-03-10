using Calculatorapplication;
using NUnit.Framework;
using System;

namespace nunitTestProject1
{
    [TestFixture]
    internal class CalculatorTest
    {
        private Calculator calc;

        [SetUp]
        public void Setup()
        {
            calc = new Calculator();
        }

        [Test]
        public void Add_TwoNumbers_ReturnSum()
        {
            int a = 5, b = 3;
            int result = calc.Add(a, b);
            Assert.That(result, Is.EqualTo(8));
        }

        [Test]
        public void Subtract_TwoNumbers_ReturnsDifference()
        {
            int a = 10, b = 4;
            int result = calc.Subtract(a, b);
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Multiply_TwoNumbers_ReturnProduct()
        {
            int a = 6, b = 4;
            int result = calc.Multiply(a, b);
            Assert.That(result, Is.EqualTo(24));
        }

        [Test]
        public void Divide_TwoNumbers_ReturnQuotient()
        {
            int a = 20, b = 5;
            int result = calc.Divide(a, b);
            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void Divide_ByZero_ThrowsException()
        {
            int a = 10, b = 0;
            Assert.Throws<DivideByZeroException>(() => calc.Divide(a, b));
        }
    }
}
