using MathTaskClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MathTaskClassLibraryTests
{
    [TestClass]
    public class GeometryTest
    {
        [TestMethod]
        public void CalculateAreaInvalidDataTest1()
        {
            bool catched = false;
            try
            {
                int a = -4;
                int b = 10;

                Geometry g = new Geometry();
                g.CalculateArea(a, b);
            }
            catch (ArgumentException e)
            {
                catched = true;
            }
            Assert.IsTrue(catched, "не обработаны допустимые данные");
        }

        [TestMethod]
        public void CalculateAreaInvalidDataTest2()
        {
            int a = -4;
            int b = 10;
            Geometry g = new Geometry();
            Assert.ThrowsException<ArgumentException>(() => g.CalculateArea(a, b),
                "не обработаны отрицательные длины сторон прямоуголника");

        }


    }
}
