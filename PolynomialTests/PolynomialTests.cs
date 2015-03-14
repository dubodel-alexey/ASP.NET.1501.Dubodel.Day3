using System;
using logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PolynomialTests
{
    [TestClass]
    public class PolynomialTests
    {
        [TestMethod]
        public void AddTest()
        {
            var firstPolynomial = new Polynomial(3, 3);
            var secondPolynomial = new Polynomial(6);
            var expectedPolynomial = new Polynomial(3, 9);

            var result = firstPolynomial + secondPolynomial;
            Assert.AreEqual(expectedPolynomial, result);
        }

        [TestMethod]
        public void SubstractTest()
        {
            var firstPolynomial = new Polynomial(3, 3);
            var secondPolynomial = new Polynomial(6);
            var result = firstPolynomial - secondPolynomial;

            var expectedPolynomial = new Polynomial(3, -3);
            Assert.AreEqual(expectedPolynomial, result);
        }

        [TestMethod]
        public void SubstractTest1()
        {
            var firstPolynomial = new Polynomial(3, 3, 4);
            var secondPolynomial = new Polynomial(1, 3, 3, 3);
            var expectedPolynomial = new Polynomial(-1, 0, 0, 1);
            var result = firstPolynomial - secondPolynomial;
            Assert.AreEqual(expectedPolynomial, result);
        }

        [TestMethod]
        public void MultiplyTest()
        {
            var firstPolynomial = new Polynomial(3, 3);
            var secondPolynomial = new Polynomial(6, 1, 1);
            var expectedPolynomial = new Polynomial(18, 21, 6, 3);
            var result = firstPolynomial * secondPolynomial;
            Assert.AreEqual(expectedPolynomial, result);
        }

        [TestMethod]
        public void DevideTest()
        {
            var firstPolynomial = new Polynomial(10, 5, 2, 8);
            var secondPolynomial = new Polynomial(5, 3);
            Polynomial result, reminder;
            Polynomial.Divede(firstPolynomial, secondPolynomial, out result, out reminder);
            var expectedResult = new Polynomial(2, -0.2, 0.52);
            var expectedRewinder = new Polynomial(6.44);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedRewinder, reminder);
        }

        [TestMethod]
        public void DevideTest1()
        {
            var firstPolynomial = new Polynomial(5, 3);
            var secondPolynomial = new Polynomial(1, 2, 2);
            Polynomial result, reminder;
            Polynomial.Divede(firstPolynomial, secondPolynomial, out result, out reminder);
            var expectedResult = new Polynomial(0);
            var expectedRewinder = new Polynomial(5, 3);
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(expectedRewinder, reminder);
        }

        [ExpectedException(typeof(DivideByZeroException))]
        [TestMethod]
        public void DevideTestByZero()
        {
            var firstPolynomial = new Polynomial(5, 3);
            var secondPolynomial = new Polynomial(0);
            Polynomial result, reminder;
            Polynomial.Divede(firstPolynomial, secondPolynomial, out result, out reminder);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void AddNullExeption()
        {
            var firstPolynomial = new Polynomial(5, 3);
            Polynomial result = firstPolynomial + null;
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void SubstractNullExeption()
        {
            var firstPolynomial = new Polynomial(5, 3);
            Polynomial result = firstPolynomial - null;
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void MultiplyNullExeption()
        {
            var firstPolynomial = new Polynomial(5, 3);
            Polynomial result = firstPolynomial * null;
        }
    }
}
