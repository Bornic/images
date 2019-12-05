using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        /*       private static Bitmap Blur(Bitmap image, Int32 blurSize)
               {
                   return Blur(image, new Rectangle(0, 0, image.Width, image.Height), blurSize);
               }

               private static Bitmap Blur(Bitmap image, Rectangle rectangle, Int32 blurSize)
               {
                   Bitmap blurred = new Bitmap(image.Width, image.Height);

                   // make an exact copy of the bitmap provided
                   using (Graphics graphics = Graphics.FromImage(blurred))
                       graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                           new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

                   // look at every pixel in the blur rectangle
                   for (int xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
                   {
                       for (int yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                       {
                           int avgR = 0, avgG = 0, avgB = 0;
                           int blurPixelCount = 0;

                           // average the color of the red, green and blue for each pixel in the
                           // blur size while making sure you don't go outside the image bounds
                           for (int x = xx; (x < xx + blurSize && x < image.Width); x++)
                           {
                               for (int y = yy; (y < yy + blurSize && y < image.Height); y++)
                               {
                                   Color pixel = blurred.GetPixel(x, y);

                                   avgR += pixel.R;
                                   avgG += pixel.G;
                                   avgB += pixel.B;

                                   blurPixelCount++;
                               }
                           }

                           avgR = avgR / blurPixelCount;
                           avgG = avgG / blurPixelCount;
                           avgB = avgB / blurPixelCount;

                           // now that we know the average for the blur size, set each pixel to that color
                           for (int x = xx; x < xx + blurSize && x < image.Width && x < rectangle.Width; x++)
                               for (int y = yy; y < yy + blurSize && y < image.Height && y < rectangle.Height; y++)
                                   blurred.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                       }
                   }

                   return blurred;
               }

            вывод этого говна
                   Bitmap input = new Bitmap(pictureBox1.Image);
                   Bitmap output = new Bitmap(input.Width, input.Height);
                   output = Blur(output, 10);
                   pictureBox1.Image = output;

               */
        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap image;

            OpenFileDialog open_dialog = new OpenFileDialog(); //создание диалогового окна для выбора файла
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*"; //формат загружаемого файла
            if (open_dialog.ShowDialog() == DialogResult.OK) //если в окне была нажата кнопка "ОК"
            {

                image = new Bitmap(open_dialog.FileName);
                this.pictureBox1.Size = image.Size;
                pictureBox1.Image = image;
                pictureBox1.Invalidate();

            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) // если изображение в pictureBox1 имеется
            {
                // создаём Bitmap из изображения, находящегося в pictureBox1
                Bitmap input = new Bitmap(pictureBox1.Image);
                // создаём Bitmap для черно-белого изображения
                Bitmap output = new Bitmap(input.Width, input.Height);
                // перебираем в циклах все пиксели исходного изображения
                for (int j = 0; j < input.Height; j++)
                    for (int i = 0; i < input.Width; i++)
                    {
                        // получаем (i, j) пиксель
                        UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                        // получаем компоненты цветов пикселя
                        float R = (float)((pixel & 0x00FF0000) >> 16); // красный
                        float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый
                        float B = (float)(pixel & 0x000000FF); // синий
                                                               // делаем цвет черно-белым (оттенки серого) - находим среднее арифметическое
                        R = G = B = (R + G + B) / 3.0f;
                        // собираем новый пиксель по частям (по каналам)
                        UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                        // добавляем его в Bitmap нового изображения
                        output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                    }
                // выводим черно-белый Bitmap в pictureBox2
                pictureBox1.Image = output;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            {
                // 1.
                double treshold = 0.6;

                // 2.
                //int treshold = 150;
                Bitmap input = new Bitmap(pictureBox1.Image);
                Bitmap dst = new Bitmap(input.Width, input.Height);

                for (int i = 0; i < input.Width; i++)
                {
                    for (int j = 0; j < input.Height; j++)
                    {
                        // 1.
                        dst.SetPixel(i, j, input.GetPixel(i, j).GetBrightness() < treshold ? System.Drawing.Color.Black : System.Drawing.Color.White);

                        // 2 (пактически тоже, что 1).
                        //System.Drawing.Color color = src.GetPixel(i, j);
                        //int average = (int)(color.R + color.B + color.G) / 3;
                        //dst.SetPixel(i, j, average < treshold ? System.Drawing.Color.Black : System.Drawing.Color.White);
                    }
                }
                pictureBox1.Image = dst;
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            {
                if (pictureBox1.Image != null) // если изображение в pictureBox1 имеется
                {
                    // создаём Bitmap из изображения, находящегося в pictureBox1
                    Bitmap input = new Bitmap(pictureBox1.Image);
                    // создаём Bitmap для черно-белого изображения
                    Bitmap output = new Bitmap(input.Width, input.Height);
                    // перебираем в циклах все пиксели исходного изображения
                    for (int j = 0; j < input.Height; j++)
                        for (int i = 0; i < input.Width; i++)
                        {
                            // получаем (i, j) пиксель
                            UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                            // получаем компоненты цветов пикселя
                            float R = (float)((pixel & 0x00FF0000) >> 16); // красный
                            float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый
                            float B = (float)(pixel & 0x000000FF); // синий
                                                                   // делаем цвет черно-белым (оттенки серого) - находим среднее арифметическое
                            if (R + 10f <= 255)
                            {
                                R = R + 10f;
                                G = G + 10f;
                                B = B + 10f;
                            }
                            // собираем новый пиксель по частям (по каналам)
                            UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                            // добавляем его в Bitmap нового изображения
                            output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                        }
                    // выводим черно-белый Bitmap в pictureBox2
                    pictureBox1.Image = output;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) // если изображение в pictureBox1 имеется
            {
                // создаём Bitmap из изображения, находящегося в pictureBox1
                Bitmap input = new Bitmap(pictureBox1.Image);
                // создаём Bitmap для черно-белого изображения
                Bitmap output = new Bitmap(input.Width, input.Height);
                // перебираем в циклах все пиксели исходного изображения
                for (int j = 0; j < input.Height; j++)
                    for (int i = 0; i < input.Width; i++)
                    {
                        // получаем (i, j) пиксель
                        UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                        // получаем компоненты цветов пикселя
                        float R = (float)((pixel & 0x00FF0000) >> 16); // красный
                        float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый
                        float B = (float)(pixel & 0x000000FF); // синий
                                                               // делаем цвет черно-белым (оттенки серого) - находим среднее арифметическое
                        if (R + 10f >= 0)
                        {
                            R = R - 10f;
                            G = G - 10f;
                            B = B - 10f;
                        }
                        // собираем новый пиксель по частям (по каналам)
                        UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                        // добавляем его в Bitmap нового изображения
                        output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                    }
                // выводим черно-белый Bitmap в pictureBox2
                pictureBox1.Image = output;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) // если изображение в pictureBox1 имеется
            {
                // создаём Bitmap из изображения, находящегося в pictureBox1
                Bitmap input = new Bitmap(pictureBox1.Image);
                // создаём Bitmap для черно-белого изображения
                Bitmap output = new Bitmap(input.Width, input.Height);
                // перебираем в циклах все пиксели исходного изображения
                for (int j = 0; j < input.Height; j++)
                    for (int i = 0; i < input.Width; i++)
                    {
                        // получаем (i, j) пиксель
                        UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                        // получаем компоненты цветов пикселя
                        float R = (float)((pixel & 0x00FF0000) >> 16); // красный
                        float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый
                        float B = (float)(pixel & 0x000000FF); // синий
                                                               // делаем цвет черно-белым (оттенки серого) - находим среднее арифметическое
                        if (R + 10f >= 0)
                        {
                            R = 255f - R;
                            G = 255f - G;
                            B = 255f - B;
                        }
                        // собираем новый пиксель по частям (по каналам)
                        UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                        // добавляем его в Bitmap нового изображения
                        output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                    }
                // выводим черно-белый Bitmap в pictureBox2
                pictureBox1.Image = output;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null) // если изображение в pictureBox1 имеется
            {
                // создаём Bitmap из изображения, находящегося в pictureBox1
                Bitmap input = new Bitmap(pictureBox1.Image);
                // создаём Bitmap для черно-белого изображения
                Bitmap output = new Bitmap(input.Width, input.Height);
                // перебираем в циклах все пиксели исходного изображения
                for (int j = 0; j < input.Height; j++)
                    for (int i = 0; i < input.Width; i++)
                    {
                        // получаем (i, j) пиксель
                        UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                        // получаем компоненты цветов пикселя
                        float R = (float)((pixel & 0x00FF0000) >> 16); // красный
                        float G = (float)((pixel & 0x0000FF00) >> 8); // зеленый
                        float B = (float)(pixel & 0x000000FF); // синий
                        float S;// делаем цвет черно-белым (оттенки серого) - находим среднее арифметическое
                        S = (R + G + B) / 3f;

                        if (S <= 85f)
                        {
                            R = R + 70f;
                            //G = 0;
                            //B = 0;
                        }
                        if (S >= 85 && S <= 170f)
                        {
                            //R = 0;
                            G = G + 70f;
                            //B = 0;
                        }
                        if (S >= 170 && S <= 255f)
                        {
                            //R = 0;
                            //G = 0;
                            B = B + 70f;

                        }
                        // собираем новый пиксель по частям (по каналам)
                        UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                        // добавляем его в Bitmap нового изображения
                        output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                    }
                // выводим черно-белый Bitmap в pictureBox2
                pictureBox1.Image = output;
            }


        }

        private void button9_Click(object sender, EventArgs e)
        {
            Bitmap bm = (Bitmap)pictureBox1.Image;

            int w = bm.Width;
            int h = bm.Height;

            // размыте горизонт
            for (int i = 1; i < w - 1; i++)
                for (int j = 0; j < h; j++)
                {
                    Color c1 = bm.GetPixel(i - 1, j);
                    Color c2 = bm.GetPixel(i, j);
                    Color c3 = bm.GetPixel(i + 1, j);


                    byte bR = (byte)((c1.R + c2.R + c3.R) / 3);
                    byte bG = (byte)((c1.G + c2.G + c3.G) / 3);
                    byte bB = (byte)((c1.B + c2.B + c3.B) / 3);


                    Color cBlured = Color.FromArgb(bR, bG, bB);

                    bm.SetPixel(i, j, cBlured);

                }

            //размытие вертикаль
            for (int i = 0; i < w; i++)
                for (int j = 1; j < h - 1; j++)
                {
                    Color c1 = bm.GetPixel(i, j - 1);
                    Color c2 = bm.GetPixel(i, j);
                    Color c3 = bm.GetPixel(i, j + 1);


                    byte bR = (byte)((c1.R + c2.R + c3.R) / 3);
                    byte bG = (byte)((c1.G + c2.G + c3.G) / 3);
                    byte bB = (byte)((c1.B + c2.B + c3.B) / 3);


                    Color cBlured = Color.FromArgb(bR, bG, bB);

                    bm.SetPixel(i, j, cBlured);

                }


            pictureBox1.Refresh();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Graphics graph = Graphics.FromImage(pictureBox1.Image);
            graph.DrawEllipse(Pens.Blue, ((pictureBox1.Width) / 2)-50, ((pictureBox1.Height) / 2)-50, 100, 100);
            pictureBox1.Refresh();
        }
    }
}

