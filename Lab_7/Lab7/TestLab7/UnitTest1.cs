using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab7;
using System;
using System.Text.RegularExpressions;

namespace TestLab7
{
    [TestClass]
    public class UnitTest1
    {
        private Program program = new Program();

        [TestClass]
        public class TestAlphabetString : UnitTest1
        {
            [TestMethod]
            public void TestValidInputs()
            {
                var result = program.GenerateAlphabetString(5);
                Assert.AreEqual("ABCDE", result);
            }
        }

        [TestClass]
        public class TestQuadraticEquation : UnitTest1
        {
            [TestMethod]
            public void TestValidInputs()
            {
                var roots = program.SolveQuadraticEquation(1, -3, 2);
                Assert.AreEqual(2, roots[0]);
                Assert.AreEqual(1, roots[1]);
            }

            [TestMethod]
            public void TestInvalidInputs()
            {
                var roots = program.SolveQuadraticEquation(0, -3, 2);
                Assert.AreEqual(0, roots.Length);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void TestException()
            {
                program.SolveQuadraticEquation(0, -3, 2);
            }
        }

        [TestClass]
        public class TestLeapYear : UnitTest1
        {
            [TestMethod]
            public void TestValidInputs()
            {
                var days = program.GetNumberOfDaysInYear(2000);
                Assert.AreEqual(366, days);
                days = program.GetNumberOfDaysInYear(2001);
                Assert.AreEqual(365, days);
            }

            [TestMethod]
            public void TestInvalidInputs()
            {
                var days = program.GetNumberOfDaysInYear(0);
                Assert.AreEqual(0, days);
            }
        }

        [TestClass]
        public class TestEmailRegex : UnitTest1
        {
            [TestMethod]
            public void TestValidInputs()
            {
                var isValid = Regex.IsMatch("test@test.test", program.EmailRegex());
                if (!isValid)
                {
                    throw new Exception("Invalid email address");
                }
            }

            [TestMethod]
            public void TestInvalidInputs()
            {
                var isValid = Regex.IsMatch("test.test", program.EmailRegex());
                if (isValid)
                {
                    throw new Exception("Invalid email address");
                }
            }
        }


        [TestClass]
        public class TestSumOfDigits : UnitTest1
        {
            [TestMethod]
            public void TestValidInputs()
            {
                var sum = program.SumOfDigitsInString("12345");
                Assert.AreEqual(15, sum);
            }
        }
    }
}
