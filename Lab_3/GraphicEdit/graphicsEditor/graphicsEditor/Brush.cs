using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace graphicsEditor
{
    abstract class Brush
    {
        public Color BrushColor { get; set; }
        public int Size { get; set; }
        public Brush(Color brushColor, int size)
        {
            BrushColor = brushColor;
            Size = size;
        }
        public abstract void Draw(Bitmap image, int x, int y);

        public void ChangeColor(Color newColor)
        {
            BrushColor = newColor;
        }
    }
    class QuadBrush : Brush
    {
        public QuadBrush(Color brushColor, int size)
            : base(brushColor, size)
        {
        }

        public override void Draw(Bitmap image, int x, int y)
        {
            // Уменьшаем размер в два раза
            int size = Size / 2;

            for (int y0 = y - size; y0 < y + size; ++y0)
            {
                for (int x0 = x - size; x0 < x + size; ++x0)
                {
                    // Проверяем, находится ли текущий пиксель в пределах изображения
                    if (x0 >= 0 && x0 < image.Width && y0 >= 0 && y0 < image.Height)
                    {
                        image.SetPixel(x0, y0, BrushColor);
                    }
                }
            }
        }
        public void ChangeColor(Color newColor)
        {
            BrushColor = newColor;
        }
    }

    class CircleBrush : Brush
    {
        public CircleBrush(Color brushColor, int size)
            : base(brushColor, size)
        {

        }
        public override void Draw(Bitmap image, int x, int y)
        {
            for (int y0 = y - Size; y0 <= y + Size; ++y0)
            {
                for (int x0 = x - Size; x0 <= x + Size; ++x0)
                {
                    // Проверяем, находится ли текущий пиксель в пределах изображения
                    if (x0 >= 0 && x0 < image.Width && y0 >= 0 && y0 < image.Height)
                    {
                        // Вычисляем расстояние от центра круга до текущего пикселя
                        int dx = x - x0;
                        int dy = y - y0;
                        int distance = (int)Math.Sqrt(dx * dx + dy * dy);

                        // Если пиксель находится внутри круга, то рисуем его
                        if (distance <= Size / 2)
                        {
                            image.SetPixel(x0, y0, BrushColor);
                        }
                    }
                }
            }
        }
        public void ChangeColor(Color newColor)
        {
            BrushColor = newColor;
        }
    }

    class TriangleBrush : Brush
    {
        public TriangleBrush(Color brushColor, int size)
            : base(brushColor, size)
        {

        }
        public override void Draw(Bitmap image, int x, int y)
        {
            for (int y0 = y - Size; y0 <= y + Size; ++y0)
            {
                for (int x0 = x - Size; x0 <= x + Size; ++x0)
                {
                    // Проверяем, находится ли текущий пиксель в пределах изображения
                    if (x0 >= 0 && x0 < image.Width && y0 >= 0 && y0 < image.Height)
                    {
                        // Вычисляем координаты треугольника
                        int x1 = x - Size / 2;
                        int y1 = y + Size / 2;
                        int x2 = x + Size / 2;
                        int y2 = y + Size / 2;
                        int x3 = x;
                        int y3 = y - Size / 2;

                        // Проверяем, находится ли текущий пиксель внутри треугольника
                        if (IsInsideTriangle(x0, y0, x1, y1, x2, y2, x3, y3))
                        {
                            image.SetPixel(x0, y0, BrushColor);
                        }
                    }
                }
            }
        }

        private bool IsInsideTriangle(int x, int y, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            // Формула для проверки, находится ли точка внутри треугольника
            bool b1 = ((y1 - y) * (x2 - x) - (x1 - x) * (y2 - y)) < 0;
            bool b2 = ((y2 - y) * (x3 - x) - (x2 - x) * (y3 - y)) < 0;
            bool b3 = ((y3 - y) * (x1 - x) - (x3 - x) * (y1 - y)) < 0;

            return ((b1 == b2) && (b2 == b3));
        }
        public void ChangeColor(Color newColor)
        {
            BrushColor = newColor;
        }
    }

    class StarBrush : Brush
    {
        public StarBrush(Color brushColor, int size)
            : base(brushColor, size)
        {

        }

        public override void Draw(Bitmap image, int x, int y)
        {
            // Проверяем, находится ли центр звезды в пределах изображения
            if (x >= 0 && x < image.Width && y >= 0 && y < image.Height)
            {
                int n = 5;          // число вершин
                double R = 25 * Size / 100.0, r = 50 * Size / 100.0; // радиусы
                double alpha = 0;   // поворот
                double x0 = x, y0 = y; // центр
                Graphics g = Graphics.FromImage(image);
                PointF[] points = new PointF[2 * n + 1];
                double a = alpha, da = Math.PI / n, l;
                for (int k = 0; k < 2 * n + 1; k++)
                {
                    l = k % 2 == 0 ? r : R;
                    points[k] = new PointF((float)(x0 + l * Math.Cos(a)), (float)(y0 + l * Math.Sin(a)));
                    a += da;
                }

                g.FillPolygon(new SolidBrush(BrushColor), points);
            }
        }
        public void ChangeColor(Color newColor)
        {
            BrushColor = newColor;
        }
    }
    class EraserBrush : Brush
    {
        public EraserBrush(Color brushColor, int size)
            : base(brushColor, size)
        {
        }

        public override void Draw(Bitmap image, int x, int y)
        {
            // Уменьшаем размер в два раза
            int size = Size / 2;

            for (int y0 = y - size; y0 < y + size; ++y0)
            {
                for (int x0 = x - size; x0 < x + size; ++x0)
                {
                    // Проверяем, находится ли текущий пиксель в пределах изображения
                    if (x0 >= 0 && x0 < image.Width && y0 >= 0 && y0 < image.Height)
                    {
                        // Заполняем пиксель цветом фона
                        image.SetPixel(x0, y0, Color.White);
                    }
                }
            }
        }
    }


}
