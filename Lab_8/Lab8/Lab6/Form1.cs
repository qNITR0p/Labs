using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

            public event EventHandler<IntegratorEventArgs> OnStep;
            public event EventHandler<IntegratorEventArgs> OnFinish;

            public abstract double Integrate(double x1, double x2, int N);

            protected virtual void RaiseStepEvent(double x, double f, double sum)
            {
                OnStep?.Invoke(this, new IntegratorEventArgs { X = x, F = f, Integr = sum });
            }

            protected virtual void RaiseFinishEvent(double sum)
            {
                OnFinish?.Invoke(this, new IntegratorEventArgs { Integr = sum });
            }
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
                    double x = x1 + i * h;
                    double f = equation.GetValue(x);
                    sum = sum + f * h;
                    RaiseStepEvent(x, f, sum);
                }
                RaiseFinishEvent(sum);

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
                    double f = equation.GetValue(x);
                    sum = sum + (f + equation.GetValue(x + h)) * h / 2;
                    RaiseStepEvent(x, f, sum);
                }
                RaiseFinishEvent(sum);

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
            i1.OnStep += (s, args) => WriteToFile(args);
            i1.OnStep += (s, args) => listBox1.Items.Add($"X: {args.X}, F: {args.F}, Integr: {args.Integr}");
            i1.OnFinish += (s, args) => MessageBox.Show($"Integral value: {args.Integr}");
            i1.OnFinish += (s, args) => Console.WriteLine($"Integral value: {args.Integr}");
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

        private void WriteToFile(IntegratorEventArgs args)
        {
            using (StreamWriter sw = new StreamWriter("output.txt", true))
            {
                sw.WriteLine($"X: {args.X}, F: {args.F}, Integr: {args.Integr}");
            }
        }
    }

    public class IntegratorEventArgs : EventArgs
    {
        public double X { get; set; }
        public double F { get; set; }
        public double Integr { get; set; }
    }
}