using System;
using System.Linq;
using logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PolynomialTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddTest()
        {
            var firstPolynomial = new Polynomial(3, 3);
            var secondPolynomial = new Polynomial(6);

            var result = firstPolynomial + secondPolynomial;
            StringAssert.Contains("3x^1+9", result.ToString());
        }

        [TestMethod]
        public void SubstractTest()
        {
            var firstPolynomial = new Polynomial(3, 3);
            var secondPolynomial = new Polynomial(6);

            var result = firstPolynomial - secondPolynomial;
            StringAssert.Contains("3x^1-3", result.ToString());
        }

        [TestMethod]
        public void SubstractTest1()
        {
            var firstPolynomial = new Polynomial(3, 3, 4);
            var secondPolynomial = new Polynomial(1, 3, 3, 3); 

            var result = firstPolynomial - secondPolynomial;
            StringAssert.Contains("-1x^3+1", result.ToString());
        }

        [TestMethod]
        public void MultiplyTest()
        {
            var firstPolynomial = new Polynomial(3, 3);
            var secondPolynomial = new Polynomial(6, 1, 1);

            var result = firstPolynomial * secondPolynomial;
            StringAssert.Contains("18x^3+21x^2+6x^1+3", result.ToString());
        }

        [TestMethod]
        public void DevideTest()
        {
            var firstPolynomial = new Polynomial(10, 5, 2, 8);
            var secondPolynomial = new Polynomial(5, 3);
            Polynomial result, reminder;
            Polynomial.Divede(firstPolynomial, secondPolynomial, out result, out reminder);
            var expectedResult = "2x^2-0,2x^1+0,52";
            var expectedRewinder = "6,44";
            StringAssert.Contains(expectedResult, result.ToString());
            StringAssert.Contains(expectedRewinder, reminder.ToString());
        }

        [TestMethod]
        public void DevideTest1()
        {
            var firstPolynomial = new Polynomial(5, 3);
            var secondPolynomial = new Polynomial(1, 2, 2);
            Polynomial result, reminder;
            Polynomial.Divede(firstPolynomial, secondPolynomial, out result, out reminder);
            var expectedResult = "0";
            var expectedRewinder = "5x^1+3";
            StringAssert.Contains(expectedResult, result.ToString());
            StringAssert.Contains(expectedRewinder, reminder.ToString());
        }
    }
}
