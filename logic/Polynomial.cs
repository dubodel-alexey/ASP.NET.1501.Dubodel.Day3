using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logic
{
    public class Polynomial
    {
        private double[] coefficients;

        public int Degree
        {
            get { return coefficients.Length - 1; }
        }

        public Polynomial(params double[] coefficients)
        {
            this.coefficients = coefficients.Reverse().ToArray();
        }

        public Polynomial(Polynomial targetPolynomial)
        {
            coefficients = (double[])targetPolynomial.coefficients.Clone();
        }

        public static Polynomial operator +(Polynomial firstSummand, Polynomial secondSummand)
        {
            if (firstSummand == null)
            {
                throw new ArgumentNullException("firstSummand");
            }

            if (secondSummand == null)
            {
                throw new ArgumentNullException("secondSummand");
            }

            Polynomial sum, smallerPolynomial;
            if (firstSummand.Degree > secondSummand.Degree)
            {
                smallerPolynomial = secondSummand;
                sum = new Polynomial(firstSummand);
            }
            else
            {
                smallerPolynomial = firstSummand;
                sum = new Polynomial(secondSummand);
            }

            for (int i = 0; i <= smallerPolynomial.Degree; i++)
            {
                sum[i] += smallerPolynomial[i];
            }

            sum.RemoveRedustantSeniorMembers();
            return sum;
        }

        public static Polynomial operator -(Polynomial operand)
        {
            if (operand == null)
            {
                throw new ArgumentNullException("operand");
            }

            var result = new Polynomial(operand);
            for (int i = 0; i <= result.Degree; i++)
            {
                result[i] = -1 * result[i];
            }

            return result;
        }

        public static Polynomial operator -(Polynomial minuend, Polynomial subtrahend)
        {
            if (minuend == null)
            {
                throw new ArgumentNullException("minuend");
            }

            if (subtrahend == null)
            {
                throw new ArgumentNullException("subtrahend");
            }

            return minuend + (-subtrahend);
        }

        public static Polynomial operator *(Polynomial firstFactor, Polynomial secondFactor)
        {
            if (firstFactor == null)
            {
                throw new ArgumentNullException("firstFactor");
            }

            if (secondFactor == null)
            {
                throw new ArgumentNullException("secondFactor");
            }

            var product = new Polynomial(new double[firstFactor.Degree +
                secondFactor.Degree + 1]);
            for (int i = 0; i <= firstFactor.Degree; i++)
            {
                for (int j = 0; j <= secondFactor.Degree; j++)
                {
                    product[i + j] += firstFactor[i] * secondFactor[j];
                }
            }

            product.RemoveRedustantSeniorMembers();
            return product;
        }

        public static void Divede(Polynomial dividend, Polynomial divisor, out Polynomial quotient,
            out Polynomial remainder)
        {
            if (dividend == null)
            {
                throw new ArgumentNullException("dividend");
            }

            if (divisor == null)
            {
                throw new ArgumentNullException("divisor");
            }

            if (dividend.Degree < divisor.Degree)
            {
                quotient = new Polynomial(0);
                remainder = new Polynomial(dividend);
            }
            else
            {
                quotient = new Polynomial(new double[dividend.Degree - divisor.Degree + 1]);
                remainder = new Polynomial(dividend);

                for (int i = 0; i <= quotient.Degree; i++)
                {
                    if (Math.Abs(divisor.coefficients.Last()) < double.Epsilon)
                    {
                        throw new DivideByZeroException();
                    }
                    double coeff = remainder[remainder.Degree - i] /
                                   divisor.coefficients.Last();
                    quotient[quotient.Degree - i] = coeff;
                    for (int j = 0; j <= divisor.Degree; j++)
                    {
                        remainder[remainder.Degree - i - j] -=
                            coeff * divisor[divisor.Degree - j];
                    }
                }
                quotient.RemoveRedustantSeniorMembers();
                remainder.RemoveRedustantSeniorMembers();
            }
        }

        public static Polynomial Add(Polynomial firstSummand, Polynomial secondSummand)
        {
            return firstSummand + secondSummand;
        }

        public static Polynomial Subtract(Polynomial minuend, Polynomial subtrahend)
        {
            return minuend - subtrahend;
        }

        public static Polynomial Multiply(Polynomial firstFactor, Polynomial secondFactor)
        {
            return firstFactor * secondFactor;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            if (Degree > 0)
            {
                result.Append(coefficients[Degree] + "x^" + Degree);
                for (int i = Degree - 1; i > 0; i--)
                {
                    if (Math.Abs(coefficients[i]) >= double.Epsilon)
                    {
                        result.Append((coefficients[i] >= 0 ? "+" : "") + coefficients[i] + "x^" + i);
                    }
                }

                result.Append((coefficients[0] >= 0 ? "+" : "") + coefficients[0]);
            }
            else
            {
                result.Append(coefficients[Degree]);
            }

            return result.ToString();
        }

        public static bool operator ==(Polynomial firstPolynom, Polynomial secondPolynom)
        {
            if (Equals(firstPolynom, null) && Equals(secondPolynom, null))
                return true;

            if (Equals(firstPolynom, null) || Equals(secondPolynom, null))
                return false;

            if (ReferenceEquals(firstPolynom, secondPolynom))
                return true;

            if (firstPolynom.Degree != secondPolynom.Degree) return false;
            for (int i = 0; i < firstPolynom.Degree; i++)
            {
                if (Math.Abs(firstPolynom[i] - secondPolynom[i]) < double.Epsilon)
                    continue;
                return false;
            }

            return true;
        }

        public static bool operator !=(Polynomial firstPolynom, Polynomial secondPolynom)
        {
            if (firstPolynom == null && secondPolynom == null)
                throw new ArgumentNullException("Can't to compare null refferences.");

            if (firstPolynom == null || secondPolynom == null)
                throw new ArgumentNullException("Can't compare polynomial to null.");

            return !(firstPolynom == secondPolynom);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Polynomial)) return false;
            return this == (Polynomial)obj;
        }

        public override int GetHashCode()
        {
            double sum = 0;
            foreach (double coefficient in coefficients)
            {
                sum += coefficient;
            }

            return (int)unchecked(27011 * sum);
        }

        /// <summary>
        /// Remove redustant elements
        /// </summary>
        private void RemoveRedustantSeniorMembers()
        {
            int i = Degree;
            var newCoefficients = new List<double>();
            while (i > 0 && Math.Abs(coefficients[i]) < double.Epsilon)
            {
                i--;
            }

            newCoefficients.AddRange(coefficients.Take(i + 1));
            coefficients = newCoefficients.ToArray();
        }

        private double this[int i]
        {
            get
            {
                if (i > coefficients.Length - 1)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return coefficients[i];
            }
            set
            {
                if (i > coefficients.Length - 1)
                {
                    throw new ArgumentOutOfRangeException();
                }

                coefficients[i] = value;
            }
        }
    }
}
