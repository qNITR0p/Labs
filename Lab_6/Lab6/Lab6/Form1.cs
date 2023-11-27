using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab6
{
    public partial class Form1 : Form
    {
        public abstract class Equation
        {
            public abstract double GetValue(double x);
        }

        public abstract class Integrator
        {
            protected readonly Equation equation;

            public Integrator(Equation equation)
            {
                if (equation == null)
                {
                    throw new ArgumentNullException();
                }
                this.equation = equation;
            }

            public abstract string MethodName { get; }

            public abstract double Integrate(double x1, double x2, int N);
        }

        public class RectangleIntegrator : Integrator
        {
            public RectangleIntegrator(Equation equation) : base(equation)
            {
            }

            public override string MethodName => "Метод прямоугольников";

            public override double Integrate(double x1, double x2, int N)
            {
                double h = (x2 - x1) / N;
                double sum = 0;

                for (int i = 0; i < N; i++)
                {
                    sum = sum + equation.GetValue(x1 + i * h) * h;
                }

                return sum;
            }
        }

        public class TrapezoidIntegrator : Integrator
        {
            public TrapezoidIntegrator(Equation equation) : base(equation)
            {
            }

            public override string MethodName => "Метод трапеций";

            public override double Integrate(double x1, double x2, int N)
            {
                double h = (x2 - x1) / N;
                double sum = 0;

                for (int i = 0; i < N; i++)
                {
                    double x = x1 + i * h;
                    sum = sum + (equation.GetValue(x) + equation.GetValue(x + h)) * h / 2;
                }

                return sum;
            }
        }

        public class QuadEquation : Equation
        {
            private readonly double a;
            private readonly double b;
            private readonly double c;

            public QuadEquation(double a, double b, double c)
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }

            public override double GetValue(double x)
            {
                return a * x * x + b * x + c;
            }
        }

        public class SinusoidalEquation : Equation
        {
            private readonly double a;

            public SinusoidalEquation(double a)
            {
                this.a = a;
            }

            public override double GetValue(double x)
            {
                return Math.Sin(a * x) / x;
            }
        }

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            chart1.Series.Add(new Series());
            chart2.Series.Add(new Series()); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Equation equation1 = new QuadEquation(1, 2, 1);
            RectangleIntegrator i1 = new RectangleIntegrator(equation1);
            double integrValue1 = i1.Integrate(0, 10, 100);

            TrapezoidIntegrator i2 = new TrapezoidIntegrator(new SinusoidalEquation(1));
            double integrValue2 = i2.Integrate(0, 10, 100);

            Console.WriteLine($"Метод интегрирования для первой функции: {i1.MethodName}");
            Console.WriteLine($"Метод интегрирования для второй функции: {i2.MethodName}");

            chart1.ChartAreas[0].AxisX.IsStartedFromZero = false;
            chart2.ChartAreas[0].AxisX.IsStartedFromZero = false;
            DrawFunction(-20, 20, chart1.Series[0], equation1);
            DrawFunction(-20, 20, chart2.Series[0], new SinusoidalEquation(1)); 
        }

        public void DrawFunction(double x1, double x2, Series series, Equation equation)
        {
            double step = (x2 - x1) / 100;
            for (double x = x1; x <= x2; x += step)
            {
                series.Points.AddXY(x, equation.GetValue(x));
            }
        }
    }
}
