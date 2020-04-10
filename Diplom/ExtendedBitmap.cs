using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using AForge.Imaging.Filters;


namespace Diplom
{
    public static class ExtendedBitmap

    {     

        public enum BlurType
        {
            Mean3x3,
            Mean5x5,
            Mean7x7,
            Mean9x9,
            GaussianBlur3x3,
            GaussianBlur5x5,
            MotionBlur5x5,
            MotionBlur5x5At45Degrees,
            MotionBlur5x5At135Degrees,
            MotionBlur7x7,
            MotionBlur7x7At45Degrees,
            MotionBlur7x7At135Degrees,
            MotionBlur9x9,
            MotionBlur9x9At45Degrees,
            MotionBlur9x9At135Degrees,
            Median3x3,
            Median5x5,
            Median7x7,
            Median9x9,
            Median11x11
        }

        public static Bitmap CopyToSquareCanvas(this Bitmap sourceBitmap, int canvasWidthLenght)
        {
            float ratio = 1.0f;
            int maxSide = sourceBitmap.Width > sourceBitmap.Height ?
                          sourceBitmap.Width : sourceBitmap.Height;

            ratio = (float)maxSide / (float)canvasWidthLenght;

            Bitmap bitmapResult = (sourceBitmap.Width > sourceBitmap.Height ?
                                    new Bitmap(canvasWidthLenght, (int)(sourceBitmap.Height / ratio))
                                    : new Bitmap((int)(sourceBitmap.Width / ratio), canvasWidthLenght));

            using (Graphics graphicsResult = Graphics.FromImage(bitmapResult))
            {
                graphicsResult.CompositingQuality = CompositingQuality.HighQuality;
                graphicsResult.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsResult.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphicsResult.DrawImage(sourceBitmap,
                                        new Rectangle(0, 0,
                                            bitmapResult.Width, bitmapResult.Height),
                                        new Rectangle(0, 0,
                                            sourceBitmap.Width, sourceBitmap.Height),
                                            GraphicsUnit.Pixel);
                graphicsResult.Flush();
            }

            return bitmapResult;
        }

        private static Bitmap ConvolutionFilter(Bitmap sourceBitmap,
                                             double[,] filterMatrix,
                                                  double factor = 1,
                                                       int bias = 0,
                                             bool grayscale = false)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                     sourceBitmap.Width, sourceBitmap.Height),
                                                       ImageLockMode.ReadOnly,
                                                 PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            if (grayscale == true)
            {
                float rgb = 0;

                for (int k = 0; k < pixelBuffer.Length; k += 4)
                {
                    rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;


                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }

            double blue = 0.0;
            double green = 0.0;
            double red = 0.0;

            int filterWidth = filterMatrix.GetLength(1);
            int filterHeight = filterMatrix.GetLength(0);

            int filterOffset = (filterWidth - 1) / 2;
            int calcOffset = 0;

            int byteOffset = 0;

            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blue = 0;
                    green = 0;
                    red = 0;

                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;

                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {

                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);

                            blue += (double)(pixelBuffer[calcOffset]) *
                                    filterMatrix[filterY + filterOffset,
                                                        filterX + filterOffset];

                            green += (double)(pixelBuffer[calcOffset + 1]) *
                                     filterMatrix[filterY + filterOffset,
                                                        filterX + filterOffset];

                            red += (double)(pixelBuffer[calcOffset + 2]) *
                                   filterMatrix[filterY + filterOffset,
                                                      filterX + filterOffset];
                        }
                    }

                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;

                    if (blue > 255)
                    { blue = 255; }
                    else if (blue < 0)
                    { blue = 0; }

                    if (green > 255)
                    { green = 255; }
                    else if (green < 0)
                    { green = 0; }

                    if (red > 255)
                    { red = 255; }
                    else if (red < 0)
                    { red = 0; }

                    resultBuffer[byteOffset] = (byte)(blue);
                    resultBuffer[byteOffset + 1] = (byte)(green);
                    resultBuffer[byteOffset + 2] = (byte)(red);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                     resultBitmap.Width, resultBitmap.Height),
                                                      ImageLockMode.WriteOnly,
                                                 PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        public static Bitmap ConvolutionFilter(this Bitmap sourceBitmap,
                                                double[,] xFilterMatrix,
                                                double[,] yFilterMatrix,
                                                      double factor = 1,
                                                           int bias = 0,
                                                 bool grayscale = false)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                     sourceBitmap.Width, sourceBitmap.Height),
                                                       ImageLockMode.ReadOnly,
                                                  PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            if (grayscale == true)
            {
                float rgb = 0;

                for (int k = 0; k < pixelBuffer.Length; k += 4)
                {
                    rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;

                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }

            double blueX = 0.0;
            double greenX = 0.0;
            double redX = 0.0;

            double blueY = 0.0;
            double greenY = 0.0;
            double redY = 0.0;

            double blueTotal = 0.0;
            double greenTotal = 0.0;
            double redTotal = 0.0;

            int filterOffset = 1;
            int calcOffset = 0;

            int byteOffset = 0;

            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blueX = greenX = redX = 0;
                    blueY = greenY = redY = 0;

                    blueTotal = greenTotal = redTotal = 0.0;

                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;

                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);

                            blueX += (double)(pixelBuffer[calcOffset]) *
                                      xFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            greenX += (double)(pixelBuffer[calcOffset + 1]) *
                                      xFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            redX += (double)(pixelBuffer[calcOffset + 2]) *
                                      xFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            blueY += (double)(pixelBuffer[calcOffset]) *
                                      yFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            greenY += (double)(pixelBuffer[calcOffset + 1]) *
                                      yFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            redY += (double)(pixelBuffer[calcOffset + 2]) *
                                      yFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];
                        }
                    }

                    blueTotal = Math.Sqrt((blueX * blueX) + (blueY * blueY));
                    greenTotal = Math.Sqrt((greenX * greenX) + (greenY * greenY));
                    redTotal = Math.Sqrt((redX * redX) + (redY * redY));

                    if (blueTotal > 255)
                    { blueTotal = 255; }
                    else if (blueTotal < 0)
                    { blueTotal = 0; }

                    if (greenTotal > 255)
                    { greenTotal = 255; }
                    else if (greenTotal < 0)
                    { greenTotal = 0; }

                    if (redTotal > 255)
                    { redTotal = 255; }
                    else if (redTotal < 0)
                    { redTotal = 0; }

                    resultBuffer[byteOffset] = (byte)(blueTotal);
                    resultBuffer[byteOffset + 1] = (byte)(greenTotal);
                    resultBuffer[byteOffset + 2] = (byte)(redTotal);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                     resultBitmap.Width, resultBitmap.Height),
                                                      ImageLockMode.WriteOnly,
                                                  PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        private static Bitmap ConvolutionFilterBlur(this Bitmap sourceBitmap,
                                                  double[,] filterMatrix,
                                                       double factor = 1,
                                                            int bias = 0)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                     sourceBitmap.Width, sourceBitmap.Height),
                                                       ImageLockMode.ReadOnly,
                                                 PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            double blue = 0.0;
            double green = 0.0;
            double red = 0.0;

            int filterWidth = filterMatrix.GetLength(1);
            int filterHeight = filterMatrix.GetLength(0);

            int filterOffset = (filterWidth - 1) / 2;
            int calcOffset = 0;

            int byteOffset = 0;

            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blue = 0;
                    green = 0;
                    red = 0;

                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;

                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);

                            blue += (double)(pixelBuffer[calcOffset]) *
                                    filterMatrix[filterY + filterOffset,
                                                        filterX + filterOffset];

                            green += (double)(pixelBuffer[calcOffset + 1]) *
                                     filterMatrix[filterY + filterOffset,
                                                        filterX + filterOffset];

                            red += (double)(pixelBuffer[calcOffset + 2]) *
                                   filterMatrix[filterY + filterOffset,
                                                      filterX + filterOffset];
                        }
                    }

                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;

                    blue = (blue > 255 ? 255 :
                           (blue < 0 ? 0 :
                            blue));

                    green = (green > 255 ? 255 :
                            (green < 0 ? 0 :
                             green));

                    red = (red > 255 ? 255 :
                          (red < 0 ? 0 :
                           red));

                    resultBuffer[byteOffset] = (byte)(blue);
                    resultBuffer[byteOffset + 1] = (byte)(green);
                    resultBuffer[byteOffset + 2] = (byte)(red);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                     resultBitmap.Width, resultBitmap.Height),
                                                      ImageLockMode.WriteOnly,
                                                 PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }
   /*    public static class GaussianCalculator
        {
            public static double[,] Calculate(int lenght, double weight)
            {
                double[,] Kernel = new double[lenght, lenght];
                double sumTotal = 0;

                int kernelRadius = lenght / 2;
                double distance = 0;

                double calculatedEuler = 1.0 /
                (2.0 * Math.PI * Math.Pow(weight, 2));

                for (int filterY = -kernelRadius;
                     filterY <= kernelRadius; filterY++)
                {
                    for (int filterX = -kernelRadius;
                        filterX <= kernelRadius; filterX++)
                    {
                        distance = ((filterX * filterX) +
                                   (filterY * filterY)) /
                                   (2 * (weight * weight));

                        Kernel[filterY + kernelRadius,
                               filterX + kernelRadius] =
                               calculatedEuler * Math.Exp(-distance);

                        sumTotal += Kernel[filterY + kernelRadius,
                                           filterX + kernelRadius];
                    }
                }

                for (int y = 0; y < lenght; y++)
                {
                    for (int x = 0; x < lenght; x++)
                    {
                        Kernel[y, x] = Kernel[y, x] *
                                       (1.0 / sumTotal);
                    }
                }

                return Kernel;
            }
        }
*/
        public static Bitmap DifferenceOfGaussianFilter(this Bitmap sourceBitmap, 
                                                        int matrixSize, double weight1,
                                                        double weight2)
        {
            double[,] kernel1 = 
            GaussianCalculator.Calculate(matrixSize, 
            (weight1 > weight2 ? weight1 : weight2));

            double[,] kernel2 = 
            GaussianCalculator.Calculate(matrixSize, 
            (weight1 > weight2 ? weight2 : weight1));

            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                     sourceBitmap.Width, sourceBitmap.Height),
                                                       ImageLockMode.ReadOnly,
                                                 PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] grayscaleBuffer = new byte[sourceData.Width * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            double rgb = 0;

            for (int source = 0, dst = 0; 
                 source < pixelBuffer.Length && dst < grayscaleBuffer.Length; 
                 source += 4, dst++)
            {
                rgb = pixelBuffer[source] * 0.11f;
                rgb += pixelBuffer[source + 1] * 0.59f;
                rgb += pixelBuffer[source + 2] * 0.3f;

                grayscaleBuffer[dst] = (byte)rgb;
            }

            double color1 = 0.0;
            double color2 = 0.0;

            int filterOffset = (matrixSize - 1) / 2;
            int calcOffset = 0;

            for (int source = 0, dst = 0; 
                 source < grayscaleBuffer.Length && dst + 4 < resultBuffer.Length; 
                 source++, dst += 4)
            {
                color1 = 0;
                color2 = 0;
                
                for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                {
                    for (int filterX = -filterOffset;
                        filterX <= filterOffset; filterX++)
                    {

                        calcOffset = source + (filterX) +
                                     (filterY * sourceBitmap.Width);

                        calcOffset = (calcOffset < 0 ? 0 : 
                                     (calcOffset >= grayscaleBuffer.Length ? 
                                     grayscaleBuffer.Length - 1 : calcOffset));

                        color1 += (grayscaleBuffer[calcOffset]) *
                                   kernel1[filterY + filterOffset,
                                   filterX + filterOffset];

                        color2 += (grayscaleBuffer[calcOffset]) *
                                   kernel2[filterY + filterOffset,
                                   filterX + filterOffset];
                    }
                }

                color1 = color1 - color2;
                color1 = (color1 >= weight1 - weight2 ? 255 : 0);

                resultBuffer[dst] = (byte)color1;
                resultBuffer[dst + 1] = (byte)color1;
                resultBuffer[dst + 2] = (byte)color1;
                resultBuffer[dst + 3] = 255;
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                     resultBitmap.Width, resultBitmap.Height),
                                                      ImageLockMode.WriteOnly,
                                                 PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }
      

        public static Bitmap MedianFilter(this Bitmap sourceBitmap, int matrixSize)
        {
            BitmapData sourceData =
                       sourceBitmap.LockBits(new Rectangle(0, 0,
                       sourceBitmap.Width, sourceBitmap.Height),
                       ImageLockMode.ReadOnly,
                       PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];

            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            int filterOffset = (matrixSize - 1) / 2;
            int calcOffset = 0;

            int byteOffset = 0;

            List<int> neighbourPixels = new List<int>();
            byte[] middlePixel;

            for (int offsetY = filterOffset; offsetY < sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX < sourceBitmap.Width - filterOffset; offsetX++)
                {
                    byteOffset = offsetY * sourceData.Stride + offsetX * 4;

                    neighbourPixels.Clear();

                    for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset + (filterX * 4) + (filterY * sourceData.Stride);

                            neighbourPixels.Add(BitConverter.ToInt32(pixelBuffer, calcOffset));
                        }
                    }

                    neighbourPixels.Sort();

                    middlePixel = BitConverter.GetBytes(neighbourPixels[filterOffset]);

                    resultBuffer[byteOffset] = middlePixel[0];
                    resultBuffer[byteOffset + 1] = middlePixel[1];
                    resultBuffer[byteOffset + 2] = middlePixel[2];
                    resultBuffer[byteOffset + 3] = middlePixel[3];
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                       ImageLockMode.WriteOnly,
                       PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                                       resultBuffer.Length);

            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        // http://www.aforgenet.com/framework/docs/html/d7196dc6-8176-4344-a505-e7ade35c1741.htm
    /*    public static Bitmap Grayscale(this Bitmap original)
        {
            // create grayscale filter (BT709)
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            
            // apply the filter
            return filter.Apply(original);
        }*/

        // tech.pro/tutorial/660/csharp-tutorial-convert-a-color-image-to-grayscale        
        public static Bitmap Grayscale(this Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][] 
                {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }
        
       
        /* public static Bitmap Grayscale(this Bitmap original)
        {
            //Создаем пустой bitmap c размерами загруженного изображения
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    // берем пиксел из исходного изображения
                    Color originalColor = original.GetPixel(i, j);

                    //создаем grayscale версию пикселя 
                    int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
                        + (originalColor.B * .11));

                    //создаем color object
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    //заносим новый пиксей в новый файл bitmap 
                    newBitmap.SetPixel(i, j, newColor);
                }
            }

            return newBitmap;
        }
*/
        // Метод для изменения размера изображения
        public static Image resizeImage(this Image imgToResize, Size size)        
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;

        }

        // отображение изображения в отдельном окне
        public static void ImageShow(this Image src, String Capture)        
        {
            try
            {
                int Height = src.Height;
                int Width = src.Width;

                Form form1 = new Form();
                PictureBox pb = new PictureBox();

                form1.Text = Capture;
                form1.Size = new Size(Width + 4, Height + 4);
                form1.HelpButton = true;
                form1.StartPosition = FormStartPosition.CenterScreen;

                pb.Location = new Point(0, 0);
                pb.Size = new System.Drawing.Size(Width, Height);
                pb.Image = new Bitmap(src);

                form1.Controls.Add(pb);
                form1.StartPosition = FormStartPosition.WindowsDefaultLocation;
                form1.HelpButton = true;
                form1.Show();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

        }

        // отображение изображения в отдельной форме.
        public static void ImageShowForm(this Bitmap src, String Capture)         
        {
            Form form = new ImageShowForm(src,Capture);
            form.StartPosition = FormStartPosition.WindowsDefaultLocation;
            form.HelpButton = true;
            form.Show();
        }

        // Метод для наложения шума на изображения
        public static Bitmap ImageBlurFilter(this Bitmap sourceBitmap, BlurType blurType)            
        {
            Bitmap resultBitmap = null;

            switch (blurType)
            {
                case BlurType.Mean3x3:
                    {
                        
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                                       Matrix.Mean3x3, 1.0 / 9.0, 0);
                    } break;
                case BlurType.Mean5x5:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                                       Matrix.Mean5x5, 1.0 / 25.0, 0);
                    } break;
                case BlurType.Mean7x7:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                                       Matrix.Mean7x7, 1.0 / 49.0, 0);
                    } break;
                case BlurType.Mean9x9:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                                       Matrix.Mean9x9, 1.0 / 81.0, 0);
                    } break;
                case BlurType.GaussianBlur3x3:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                                Matrix.GaussianBlur3x3, 1.0 / 16.0, 0);
                    } break;
                case BlurType.GaussianBlur5x5:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                               Matrix.GaussianBlur5x5, 1.0 / 159.0, 0);
                    } break;
                case BlurType.MotionBlur5x5:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                                   Matrix.MotionBlur5x5, 1.0 / 10.0, 0);
                    } break;
                case BlurType.MotionBlur5x5At45Degrees:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                        Matrix.MotionBlur5x5At45Degrees, 1.0 / 5.0, 0);
                    } break;
                case BlurType.MotionBlur5x5At135Degrees:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                        Matrix.MotionBlur5x5At135Degrees, 1.0 / 5.0, 0);
                    } break;
                case BlurType.MotionBlur7x7:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                        Matrix.MotionBlur7x7, 1.0 / 14.0, 0);
                    } break;
                case BlurType.MotionBlur7x7At45Degrees:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                        Matrix.MotionBlur7x7At45Degrees, 1.0 / 7.0, 0);
                    } break;
                case BlurType.MotionBlur7x7At135Degrees:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                        Matrix.MotionBlur7x7At135Degrees, 1.0 / 7.0, 0);
                    } break;
                case BlurType.MotionBlur9x9:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                        Matrix.MotionBlur9x9, 1.0 / 18.0, 0);
                    } break;
                case BlurType.MotionBlur9x9At45Degrees:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                        Matrix.MotionBlur9x9At45Degrees, 1.0 / 9.0, 0);
                    } break;
                case BlurType.MotionBlur9x9At135Degrees:
                    {
                        resultBitmap = sourceBitmap.ConvolutionFilterBlur(
                        Matrix.MotionBlur9x9At135Degrees, 1.0 / 9.0, 0);
                    } break;
                case BlurType.Median3x3:
                    {
                        resultBitmap = sourceBitmap.MedianFilter(3);
                    } break;
                case BlurType.Median5x5:
                    {
                        resultBitmap = sourceBitmap.MedianFilter(5);
                    } break;
                case BlurType.Median7x7:
                    {
                        resultBitmap = sourceBitmap.MedianFilter(7);
                    } break;
                case BlurType.Median9x9:
                    {
                        resultBitmap = sourceBitmap.MedianFilter(9);
                    } break;
                case BlurType.Median11x11:
                    {
                        resultBitmap = sourceBitmap.MedianFilter(11);
                    } break;
            }

            return resultBitmap;
        }

        public static Bitmap Laplacian3x3Filter(this Bitmap sourceBitmap, bool grayscale = true)
        {
            Bitmap resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.Laplacian3x3, 1.0, 0, grayscale);
            return resultBitmap;
        }

        public static Bitmap Laplacian5x5Filter(this Bitmap sourceBitmap, bool grayscale = true)
        {
            Bitmap resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.Laplacian5x5, 1.0, 0, grayscale);
            return resultBitmap;
        }

        public static Bitmap LaplacianOfGaussianFilter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.LaplacianOfGaussian, 1.0, 0, true);
            return resultBitmap;
        }

        public static Bitmap Laplacian3x3OfGaussian3x3Filter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.Gaussian3x3, 1.0 / 16.0, 0, true);
            resultBitmap = ConvolutionFilter(resultBitmap, Matrix.Laplacian3x3, 1.0, 0, false);
            return resultBitmap;
        }

        public static Bitmap Laplacian3x3OfGaussian5x5Filter1(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.Gaussian5x5Type1, 1.0 / 159.0, 0, true);
            resultBitmap = ConvolutionFilter(resultBitmap, Matrix.Laplacian3x3, 1.0, 0, false);
            return resultBitmap;
        }

        public static Bitmap Laplacian3x3OfGaussian5x5Filter2(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.Gaussian5x5Type2, 1.0 / 256.0, 0, true);
            resultBitmap = ConvolutionFilter(resultBitmap, Matrix.Laplacian3x3, 1.0, 0, false);
            return resultBitmap;
        }

        public static Bitmap Laplacian5x5OfGaussian3x3Filter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.Gaussian3x3, 1.0 / 16.0, 0, true);
            resultBitmap = ConvolutionFilter(resultBitmap, Matrix.Laplacian5x5, 1.0, 0, false);
            return resultBitmap;
        }

        public static Bitmap Laplacian5x5OfGaussian5x5Filter1(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.Gaussian5x5Type1, 1.0 / 159.0, 0, true);
            resultBitmap = ConvolutionFilter(resultBitmap, Matrix.Laplacian5x5, 1.0, 0, false);
            return resultBitmap;
        }

        public static Bitmap Laplacian5x5OfGaussian5x5Filter2(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.Gaussian5x5Type2, 1.0 / 256.0, 0, true);
            resultBitmap = ConvolutionFilter(resultBitmap, Matrix.Laplacian5x5, 1.0, 0, false);
            return resultBitmap;
        }

        public static Bitmap Sobel3x3Filter(this Bitmap sourceBitmap, bool grayscale = true)
        {
            Bitmap resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.Sobel3x3Horizontal, Matrix.Sobel3x3Vertical, 1.0, 0, grayscale);
            return resultBitmap;
        }

        public static Bitmap PrewittFilter(this Bitmap sourceBitmap, bool grayscale = true)
        {
            Bitmap resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.Prewitt3x3Horizontal, Matrix.Prewitt3x3Vertical, 1.0, 0, grayscale);
            return resultBitmap;
        }

        public static Bitmap KirschFilter(this Bitmap sourceBitmap, bool grayscale = true)
        {
            Bitmap resultBitmap = ConvolutionFilter(sourceBitmap, Matrix.Kirsch3x3Horizontal, Matrix.Kirsch3x3Vertical, 1.0, 0, grayscale);
            return resultBitmap;
        }

        // Цветное изображение или нет
        public static Boolean isGray(this Bitmap img) {
            Boolean result = true;
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++) 
                {
                    Color point = img.GetPixel(x, y);
                    if ((point.R == point.G) && (point.G == point.B)) {
                        result = true;
                        break;                        
                    } else {
                        result = false;
                        break;
                    }
                }                
            }
            return result;
        }

        // Среднеквадратичное отклонение        
        public static double SKO(Bitmap img1, Bitmap img2)        
        {
            int H = img1.Height; 
            int W = img1.Width; 

            if ((H != img2.Height) || (W != img2.Width))
            {
                MessageBox.Show("Изображения не совпадают по размеру!", "Ошибка!");
                return -1;
            }
            double Sum_R;

            Sum_R = 0;                         
            
            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    double Rdif;
                    Color RGB1 = img1.GetPixel(x, y);
                    Color RGB2 = img2.GetPixel(x, y);
                    
                    Rdif = Math.Pow(Math.Abs(RGB1.R - RGB2.R), 2);
                    Sum_R += Rdif;                    
                }                
            }

            double tmp = Math.Sqrt(Sum_R / ((H - 1) * (W - 1)));            
            return Math.Sqrt(Sum_R / ((H - 1) * (W - 1)));
        }            

        // Среднеквадратичное отклонение
        public static double SNR(Bitmap img1, Bitmap img2)
        {                    
            int H = img1.Height; 
            int W = img1.Width; 

            if ((H != img2.Height) || (W != img2.Width))
            {
                MessageBox.Show("Изображения не совпадают по размеру!", "Ошибка!");
                return -1.0;
            }
            double Sum_R = 0;                                  
            
            double SIG = 0;

            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    double Rdif;
                    Color RGB1 = img1.GetPixel(x, y);
                    Color RGB2 = img2.GetPixel(x, y);
                    
                    Rdif = Math.Pow(Math.Abs(RGB1.R - RGB2.R), 2);
                    Sum_R += Rdif;

                    SIG += RGB1.R * RGB1.R;                   
                }                
            }
            double SKO = Sum_R / ((H - 1) * (W - 1));

            return SIG / SKO;
        }

        // Отношение пиковый сигнал/шум
        public static double SNRGG(Bitmap img1)
        {
            int Height = img1.Height; 
            int Width = img1.Width; 
            
            double SS=0;
            double V = 0;
            
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Color RGB1 = img1.GetPixel(x, y);
                    SS += RGB1.R;
                }
            }
            
            SS = SS / ((Height - 1) * (Width - 1));

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Color RGB1 = img1.GetPixel(x, y);
                    V += Math.Pow((RGB1.R - SS),2);
                }
            }
            
            return (255 - SS) / Math.Sqrt(V / ((Height - 1) * (Width - 1)));
        }

        // Отношение пиковый сигнал/шум по СКО фона
        public static double SNRF(Bitmap img1, int i1 = 5, int j1 = 7, int nn = 5)
        {
            int Height = img1.Height;
            int Width = img1.Width;
            int n = i1 + nn;
            int m = j1 + nn;

            double SS = 0;
            double SSF = 0;
            double VF = 0;
            try
            {
                for (int i = 0; i < Width; i++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        Color RGB1 = img1.GetPixel(i, j);
                        SS += RGB1.R;
                    }
                }
                SS = SS / ((Width - 1) * (Height - 1));

                for (int i = i1; i <= n; i++)
                {
                    for (int j = j1; j <= m; j++)
                    {
                        Color RGB1 = img1.GetPixel(j, i);
                        SSF += RGB1.R;
                    }
                }
                SSF = SSF / (nn * nn);

                for (int i = i1; i <= n; i++)
                {
                    for (int j = j1; j <= m; j++)
                    {
                        Color RGB1 = img1.GetPixel(j, i);
                        VF += Math.Pow((RGB1.R - SSF), 2);
                    }
                }
                VF = VF / (nn * nn);
            }
            catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            return (255-SS)/Math.Sqrt(VF);
        }

        // Возвращает минимальное значение яркости по Color.R
        public static int min(this Bitmap img)
        {
            int result = 255;
            for (int x = 0; x < img.Height; x++)
            {
                for (int y = 0; y < img.Width; y++)
                {
                    Color point = img.GetPixel(x, y);
                    if (point.R < result)
                        result = point.R;
                }
            }
            return result;
        }

        // Возвращает максимальное значение яркости по Color.R
        public static int max(this Bitmap img)
        {
            int result = 0;
            for (int x = 0; x < img.Height; x++)
            {
                for (int y = 0; y < img.Width; y++)
                {
                    Color point = img.GetPixel(x, y);
                    if (point.R > result)
                        result = point.R;
                }
            }
            return result;
        }
    }
}
