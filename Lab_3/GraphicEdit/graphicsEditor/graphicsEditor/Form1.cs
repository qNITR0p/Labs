using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;


namespace graphicsEditor
{
    public partial class simpEditor : Form
    {
        
        private string fileName;
        Color DefaultColor
        {
            get { return Color.White; }
        }
        public simpEditor()
        {
            InitializeComponent();
            CreateBlank(800, 500);
            trackBar1.ValueChanged += TrckBrSize_ValueChanged;
            button1.BackColor = colors[0];
            button2.BackColor = colors[1];
            button3.BackColor = colors[2];
            button4.BackColor = colors[3];
            button5.BackColor = colors[4];
            button6.BackColor = colors[5];
            button7.BackColor = colors[6];
            button8.BackColor = colors[7];
            button9.BackColor = colors[8];
            button10.BackColor = colors[9];
            button11.BackColor = colors[10];
            button12.BackColor = colors[11];
            button13.BackColor = colors[12];
            button14.BackColor = colors[13];
            button15.BackColor = colors[14];
            button16.BackColor = colors[15];
            button17.BackColor = colors[16];
            button18.BackColor = colors[17];
            button19.BackColor = colors[18];
            button20.BackColor = colors[19];
            btnColor.BackColorChanged += BtnColor_BackColorChanged;
        }
        private void BtnColor_BackColorChanged(object sender, EventArgs e)
        {
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(btnColor.BackColor);
            }
        }

        private Brush _currentBrush;
        private void TrckBrSize_ValueChanged(object sender, EventArgs e)
        {
            // Проверяем, инициализирована ли кисть
            if (_selectedBrush != null)
            {
                // Обновляем размер кисти
                _selectedBrush.Size = trackBar1.Value;
            }
        }



        Color[] colors = new Color[]
       {
           Color.Red,
           Color.Green,
           Color.Blue,
           Color.Yellow,
           Color.Purple,
           Color.Orange,
           Color.Cyan,
           Color.Magenta,
           Color.Lime,
           Color.Pink,
           Color.Teal,
           Color.Brown,
           Color.Indigo,
           Color.Maroon,
           Color.Navy,
           Color.Olive,
           Color.Gray,
           Color.Silver,
           Color.Black,
           Color.White,
           Color.Aqua
       };
   
        void CreateBlank(int width, int height)
        {
            var oldImage = pictureBox1.Image;
            var bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    bmp.SetPixel(i, j, DefaultColor);
                }
            }
            pictureBox1.Image = bmp;
            if (oldImage != null)
            {
                oldImage.Dispose();
            }

            // Устанавливаем размер pictureBox1 в соответствии с размером изображения
            pictureBox1.Width = width;
            pictureBox1.Height = height;
        }


        int _x;
        int _y;
        bool _mouseClicked = false;
        Color SelectedColor
        {
            get { return colorDialog1.Color; }
        }
        int SelectedSize
        {
            get { return trackBar1.Value; }
        }
        Brush _selectedBrush;


        private void btnSquare_Click(object sender, EventArgs e)
        {
            _selectedBrush = new QuadBrush(btnColor.BackColor, SelectedSize);

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (_selectedBrush == null)
            {
                return;
            }
            _x = e.X > 0 ? e.X : 0;
            _y = e.Y > 0 ? e.Y : 0;
            _selectedBrush.Draw(pictureBox1.Image as Bitmap, _x, _y);
            pictureBox1.Refresh();
            _mouseClicked = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseClicked = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            _x = e.X > 0 && e.X < pictureBox1.Width ? e.X : 0;
            _y = e.Y > 0 && e.Y < pictureBox1.Height ? e.Y : 0;
            if (_mouseClicked && _x > 0 && _y > 0)
            {
                // Получаем Bitmap из pictureBox1
                Bitmap bitmap = (Bitmap)pictureBox1.Image;

                // Рисуем на Bitmap
                _selectedBrush.Draw(bitmap, _x, _y);

                // Обновляем pictureBox1
                pictureBox1.Refresh();
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png|Tiff Image|*.tiff|All files|*.*";
            saveFileDialog.Title = "Save an Image File";
            saveFileDialog.ShowDialog();

            // Если пользователь выбрал место сохранения
            if (saveFileDialog.FileName != "")
            {
                // Сохраняем изображение в выбранном формате
                ImageFormat imageFormat = ImageFormat.Png; // По умолчанию
                switch (Path.GetExtension(saveFileDialog.FileName).ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        imageFormat = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        imageFormat = ImageFormat.Bmp;
                        break;
                    case ".gif":
                        imageFormat = ImageFormat.Gif;
                        break;
                    case ".png":
                        imageFormat = ImageFormat.Png;
                        break;
                    case ".tiff":
                        imageFormat = ImageFormat.Tiff;
                        break;
                }
                ((Bitmap)pictureBox1.Image).Save(saveFileDialog.FileName, imageFormat);

                // Сохраняем имя файла
                this.fileName = saveFileDialog.FileName;

                MessageBox.Show("Сохранено");
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Предлагаем пользователю сохранить старое изображение
            DialogResult result = MessageBox.Show("Сохранить текущее изображение?", "Сохранение изображения", MessageBoxButtons.YesNo);

            // Если пользователь выбрал "Да"
            if (result == DialogResult.Yes)
            {
                // Сохраняем старое изображение
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png|Tiff Image|*.tiff|All files|*.*";
                saveFileDialog.Title = "Save an Image File";
                saveFileDialog.ShowDialog();

                // Если пользователь выбрал место сохранения
                if (saveFileDialog.FileName != "")
                {
                    // Сохраняем старое изображение в выбранном формате
                    ImageFormat imageFormat = ImageFormat.Png; // По умолчанию
                    switch (Path.GetExtension(saveFileDialog.FileName).ToLower())
                    {
                        case ".jpg":
                        case ".jpeg":
                            imageFormat = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            imageFormat = ImageFormat.Bmp;
                            break;
                        case ".gif":
                            imageFormat = ImageFormat.Gif;
                            break;
                        case ".png":
                            imageFormat = ImageFormat.Png;
                            break;
                        case ".tiff":
                            imageFormat = ImageFormat.Tiff;
                            break;
                    }
                    ((Bitmap)pictureBox1.Image).Save(saveFileDialog.FileName, imageFormat);
                    MessageBox.Show("Сохранено");
                }
            }

            // Создаем новое пространство для рисования
            // Создаем новый экземпляр InputDialog
            InputDialog inputDialog = new InputDialog();

            // Показываем диалог и получаем результат
            DialogResult dialogResult = inputDialog.ShowDialog();

            // Если пользователь нажал "ОК"
            if (dialogResult == DialogResult.OK)
            {
                // Получаем значения из InputDialog
                int newWidth = inputDialog.W;
                int newHeight = inputDialog.H;
                string newFileName = inputDialog.fileName;


                // Создаем новое пространство для рисования
                Console.WriteLine($"Creating new image with width {newWidth} and height {newHeight}");
                CreateBlank(newWidth, newHeight);

                // Сохраняем имя файла
                this.fileName = newFileName;
            }
        }


        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png|Tiff Image|*.tiff|All files|*.*";
            openFileDialog.Title = "Open an Image File";
            openFileDialog.ShowDialog();

            // Если пользователь выбрал файл
            if (openFileDialog.FileName != "")
            {
                try
                {
                    // Загружаем изображение
                    Image originalImage = Image.FromFile(openFileDialog.FileName);

                    // Создаем уменьшенную версию изображения
                    Image thumbnailImage = originalImage.GetThumbnailImage(pictureBox1.Width, pictureBox1.Height, null, IntPtr.Zero);

                    // Загружаем уменьшенную версию изображения в pictureBox1
                    pictureBox1.Image = thumbnailImage;

                    MessageBox.Show("Image opened successfully!");
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("The selected file is not an image!");
                }
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Предлагаем пользователю сохранить свое изображение
            DialogResult result = MessageBox.Show("Сохранить текущее изображение?", "Сохранение изображения", MessageBoxButtons.YesNo);

            // Если пользователь выбрал "Да"
            if (result == DialogResult.Yes)
            {
                // Сохраняем изображение
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png|Tiff Image|*.tiff|All files|*.*";
                saveFileDialog.Title = "Save an Image File";
                saveFileDialog.ShowDialog();

                // Если пользователь выбрал место сохранения
                if (saveFileDialog.FileName != "")
                {
                    // Сохраняем изображение в выбранном формате
                    ImageFormat imageFormat = ImageFormat.Png; // По умолчанию
                    switch (Path.GetExtension(saveFileDialog.FileName).ToLower())
                    {
                        case ".jpg":
                        case ".jpeg":
                            imageFormat = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            imageFormat = ImageFormat.Bmp;
                            break;
                        case ".gif":
                            imageFormat = ImageFormat.Gif;
                            break;
                        case ".png":
                            imageFormat = ImageFormat.Png;
                            break;
                        case ".tiff":
                            imageFormat = ImageFormat.Tiff;
                            break;
                    }
                    ((Bitmap)pictureBox1.Image).Save(saveFileDialog.FileName, imageFormat);
                    MessageBox.Show("Сохранено");
                }
            }

            // Выходим из приложения
            this.Close();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Имя программы: PaintLess\n" +
                           "Версия: 0.1\n" +
                           "Авторы: Данил Лущуков\n" +
                           "Лицензия: Отсутствует");
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            _selectedBrush = new CircleBrush(btnColor.BackColor, SelectedSize);

        }

        private void btnTriangle_Click(object sender, EventArgs e)
        {
            _selectedBrush = new TriangleBrush(btnColor.BackColor, SelectedSize);

        }

        private void btnStar_Click(object sender, EventArgs e)
        {
            _selectedBrush = new StarBrush(btnColor.BackColor, SelectedSize);

        }

        private void btnEraser_Click(object sender, EventArgs e)
        {
            _selectedBrush = new EraserBrush(SelectedColor, SelectedSize);

        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btnColor.BackColor = colorDialog1.Color;
                _selectedBrush.ChangeColor(colorDialog1.Color);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[0];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[0]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[1];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[1]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[2];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[2]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[3];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[3]);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[4];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[4]);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[5];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[5]);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[6];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[6]);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[7];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[7]);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[8];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[8]);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[9];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[9]);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[10];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[10]);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[11];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[11]);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[12];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[12]);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[13];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[13]);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[14];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[14]);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[15];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[15]);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[16];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[16]);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[17];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[17]);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[18];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[18]);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            btnColor.BackColor = colors[19];
            if (_selectedBrush != null)
            {
                _selectedBrush.ChangeColor(colors[19]);
            }
        }



    }

}

