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
            this.coefficients = (double[])targetPolynomial.coefficients.Clone();
        }


        public static Polynomial operator +(Polynomial firstSummand, Polynomial secondSummand)
        {
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

            sum.CheckSeniorPolynomialMembers();
            return sum;
        }

        public static Polynomial operator -(Polynomial minuend, Polynomial subtrahend)
        {
            Polynomial difference, smallerPolynomial;
            int sign;
            if (minuend.Degree > subtrahend.Degree)
            {
                smallerPolynomial = subtrahend;
                difference = new Polynomial(minuend);
                sign = 1;
            }
            else
            {
                smallerPolynomial = minuend;
                difference = new Polynomial(subtrahend);
                sign = -1;
            }

            for (int i = 0; i <= smallerPolynomial.Degree; i++)
            {
                difference[i] -= smallerPolynomial[i];
                difference[i] *= sign;
            }
            if (sign < 1)
            {
                for (int i = smallerPolynomial.Degree + 1; i <= difference.Degree; i++)
                {
                    difference[i] *= sign;
                }
            }

            difference.CheckSeniorPolynomialMembers();
            return difference;
        }

        public static Polynomial operator *(Polynomial firstFactor, Polynomial secondFactor)
        {
            var product = new Polynomial(new double[firstFactor.Degree +
                secondFactor.Degree + 1]);
            for (int i = 0; i <= firstFactor.Degree; i++)
            {
                for (int j = 0; j <= secondFactor.Degree; j++)
                {
                    product[i + j] += firstFactor[i] * secondFactor[j];
                }
            }

            product.CheckSeniorPolynomialMembers();
            return product;
        }

        public static void Divede(Polynomial dividend, Polynomial divisor, out Polynomial quotient,
            out Polynomial remainder)
        {
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
                    double coeff = remainder[remainder.Degree - i] /
                                   divisor.coefficients.Last();
                    quotient[quotient.Degree - i] = coeff;
                    for (int j = 0; j <= divisor.Degree; j++)
                    {
                        remainder[remainder.Degree - i - j] -=
                            coeff * divisor[divisor.Degree - j];
                    }
                }
                quotient.CheckSeniorPolynomialMembers();
                remainder.CheckSeniorPolynomialMembers();
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

        /// <summary>
        /// Удаляет нулевые члены, с наибольшей степенью
        /// </summary>
        private void CheckSeniorPolynomialMembers()
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
            get { return coefficients[i]; }
            set { coefficients[i] = value; }
        }
    }
}