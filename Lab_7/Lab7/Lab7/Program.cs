using System;

namespace Lab7
{
    public class Program
    {
        static void Main(string[] args)
        {
            
        }
        public string GenerateAlphabetString(int N)
        {
            if (N < 1 || N > 26)
            {
                throw new ArgumentException("N must be between 1 and 26");
            }

            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return alphabet.Substring(0, N);
        }

        public double[] SolveQuadraticEquation(double a, double b, double c)
        {
            if (a == 0)
            {
                throw new ArgumentException("a must not be zero");
            }

            double discriminant = b * b - 4 * a * c;
            double sqrtDiscriminant = Math.Sqrt(discriminant);

            double[] roots = new double[2];

            if (discriminant > 0)
            {
                roots[0] = (-b + sqrtDiscriminant) / (2 * a);
                roots[1] = (-b - sqrtDiscriminant) / (2 * a);
            }
            else if (discriminant == 0)
            {
                roots[0] = -b / (2 * a);
            }

            return roots;
        }

        public int GetNumberOfDaysInYear(int year)
        {
            if (year < 1)
            {
                throw new ArgumentException("Year must be a positive integer");
            }

            if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0))
            {
                return 366;
            }
            else
            {
                return 365;
            }
        }

        public string EmailRegex()
        {
            return @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,})+$";
        }





        public int SumOfDigitsInString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("Input string must not be null or empty");
            }

            int sum = 0;
            foreach (char c in str)
            {
                if (char.IsDigit(c))
                {
                    sum += int.Parse(c.ToString());
                }
            }

            return sum;
        }

    }
}
