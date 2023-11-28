namespace MathTaskClassLibrary
{
    public class Geometry
    {
        public int CalculateArea(int a, int b)
        {
            if (a < 0 || b < 0) throw new System.ArgumentException();
            return a * b;
        }

        public static void Main(string[] args)
        {

        }
    }
}
