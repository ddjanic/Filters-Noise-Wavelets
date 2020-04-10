using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
//using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.IO;
using Accord.Imaging.Filters;
using Accord.Math.Wavelets;




namespace Diplom
{
    
    public partial class MainForm : Form
    {
        public  static Bitmap OriginalBitmap    = null;
        private  Image OriginalImage      = null;
        private String Path = null;      
        
        
        public MainForm()
        {
            InitializeComponent();
            Path = Application.StartupPath;
            OrigImg = (Bitmap)pictureBox1.Image;

            try
            {
                OriginalImage = new Bitmap(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("Diplom.Resources.TestImage1.bmp"));
                OriginalBitmap = new Bitmap(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("Diplom.Resources.TestImage1.bmp"));

                // Отобразить картинку в Picture Box
                pb1.Image = OriginalImage.resizeImage(new Size(191,150));
                if (OriginalBitmap != null)
                {
                    // Активизировать кнопки                    
                    btnDOG.Enabled = true;                    
                    btnSobel.Enabled = true;
                    btnLaplace.Enabled = true;
                    
                    btRunAllFilters.Enabled = true;
                    btnMean.Enabled = true;
                    btnMotionBlur.Enabled = true;
                }

                cmbLaplace.SelectedIndex = 0;
                cmbLoG.SelectedIndex = 0;
                cmbKernelLenght.SelectedIndex = 0;
                cmbFileExtension.SelectedIndex = 0;

                textBoxDirectory.Text = Application.StartupPath;

                cmbMean.Items.Add(ExtendedBitmap.BlurType.Mean3x3);
                cmbMean.Items.Add(ExtendedBitmap.BlurType.Mean5x5);
                cmbMean.Items.Add(ExtendedBitmap.BlurType.Mean7x7);
                cmbMean.Items.Add(ExtendedBitmap.BlurType.Mean9x9);
                cmbMean.SelectedIndex = 0;

                cmbMotionBlur.Items.Add(ExtendedBitmap.BlurType.MotionBlur5x5);
                cmbMotionBlur.Items.Add(ExtendedBitmap.BlurType.MotionBlur5x5At45Degrees);
                cmbMotionBlur.Items.Add(ExtendedBitmap.BlurType.MotionBlur5x5At135Degrees);
                cmbMotionBlur.Items.Add(ExtendedBitmap.BlurType.MotionBlur7x7);
                cmbMotionBlur.Items.Add(ExtendedBitmap.BlurType.MotionBlur7x7At45Degrees);
                cmbMotionBlur.Items.Add(ExtendedBitmap.BlurType.MotionBlur7x7At135Degrees);
                cmbMotionBlur.Items.Add(ExtendedBitmap.BlurType.MotionBlur9x9);
                cmbMotionBlur.Items.Add(ExtendedBitmap.BlurType.MotionBlur9x9At45Degrees);
                cmbMotionBlur.Items.Add(ExtendedBitmap.BlurType.MotionBlur9x9At135Degrees);
                cmbMotionBlur.SelectedIndex = 0;


                cmbMedian.Items.Add(ExtendedBitmap.BlurType.Median3x3);
                cmbMedian.Items.Add(ExtendedBitmap.BlurType.Median5x5);
                cmbMedian.Items.Add(ExtendedBitmap.BlurType.Median7x7);
                cmbMedian.Items.Add(ExtendedBitmap.BlurType.Median9x9);
                cmbMedian.Items.Add(ExtendedBitmap.BlurType.Median11x11);
                cmbMedian.SelectedIndex = 0;

                cmbGaussian.Items.Add(ExtendedBitmap.BlurType.GaussianBlur3x3);
                cmbGaussian.Items.Add(ExtendedBitmap.BlurType.GaussianBlur5x5);
                cmbGaussian.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }             

        public static Bitmap getOriginalBitmap(){
            return OriginalBitmap;
        }
        
        private void btLoadImage_Click(object sender, EventArgs e)
            // обработки события при нажатии на кнопку "Загрузите изображение"
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберете файл изображения.";
            ofd.Filter = "BMP|*.bmp|Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            //ofd.Filter = "Bitmap Images(*.bmp)|*.bmp|Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(ofd.FileName);
                OriginalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();
                OriginalImage = OriginalBitmap;
                
                FileInfo file_info = new FileInfo(ofd.FileName);                
                long lFileSize = file_info.Length / 1023;

                double SNRF = ExtendedBitmap.SNRF(OriginalBitmap,50,70,50);
                // Выводим информацию о файле в статус бар     
                toolStripStatusLabel1.Text = ofd.SafeFileName + ", " + lFileSize.ToString() + "КБ, " + OriginalImage.Size.ToString() + "SNRGG=" + SNRF.ToString(); //"0.###E+0"
                    
                // выводим картинку выбранную картинку на экран                
                pb1.Image = OriginalImage.resizeImage(new Size(191, 150));                           
                
                // Исходное изображение загружено -> включаем кнопки по обработке изображения
                btnDOG.Enabled = true;                
                btnSobel.Enabled = true;
                btnLaplace.Enabled = true;                
                btRunAllFilters.Enabled = true;           
                                
            }
        }

        public void SaveToFile(Bitmap resultBitmap, String fileName)
        {
            string fileExtension = cmbFileExtension.SelectedItem.ToString();            
            Path = textBoxDirectory.Text;
            
            fileName = fileName + "." + fileExtension;            
            
            String FullFileName = Path + "\\" + fileName;            

            ImageFormat imgFormat = ImageFormat.Bmp;

            if (fileExtension == "PNG")
            {
                imgFormat = ImageFormat.Png;
            }
            else if (fileExtension == "JPG")
            {
                imgFormat = ImageFormat.Jpeg;
            }
            try
            {
                StreamWriter streamWriter = new StreamWriter(FullFileName, false);
                resultBitmap.Save(streamWriter.BaseStream, imgFormat);
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (Exception em)
            {
                MessageBox.Show(em.Message);
            }

        }

        private void chbSavePath_CheckedChanged(object sender, EventArgs e)
            // Реакция на checkbox по выбору пути (откуда запускалось приложение или дать выбрать новый)
        {
            if (chbSavePath.Checked)            // если откуда запускалось приложение, то определяем путь и воводим его в текстовое поле
            {
                Path = Application.StartupPath;
                textBoxDirectory.Text = Path;
            }
            // инвертируем состояние кнопок
            // textBoxDirectory.Enabled = !textBoxDirectory.Enabled;
            btnChangeDir.Enabled = !btnChangeDir.Enabled;
        }
        

        private void btnChangeDir_Click(object sender, EventArgs e)
            // Выбрать каталог куда сохранять изображение
        {
            using (var dialog = new FolderBrowserDialog())
                if (dialog.ShowDialog() == DialogResult.OK)
                    Path = dialog.SelectedPath;
            textBoxDirectory.Text = Path;
        }       

        private void btnImageShow_Click(object sender, EventArgs e)
        // Показать оригинальное изображение
        {
            Bitmap resultBitmap = null;
            string filename = "Оригинальное изображение ";
            if (chbGray.Checked)
            {
                filename += " grey";
                resultBitmap = OriginalBitmap.Grayscale();                
            }
            else {
                resultBitmap = OriginalBitmap;
            }
            if (resultBitmap != null) resultBitmap.ImageShowForm(filename);
        }    
   
                
        private void button1_Click_1(object sender, EventArgs e)
        {            
            try {                
                Help.ShowHelp(this, "Russ White - Practical Bgp - 2005.chm", "0321127005/ch01lev1sec5.html");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

            private void btDOG_Click(object sender, EventArgs e)
        {
            /*DOG result = new DOG(OriginalBitmap);
            result.Run();*/



            String fileName = null;
            Bitmap selectedSource = null;
            Bitmap resultBitmap = null;

            int lenght = Int32.Parse(cmbKernelLenght.SelectedItem.ToString());

            if (OriginalBitmap != null) {

                selectedSource = OriginalBitmap;

                if (selectedSource != null)
                {
                    fileName = "DOG" + "_" + lenght + "_" + numWeight1.Value.ToString() + "_" + numWeight2.Value.ToString();
                    resultBitmap = selectedSource.DifferenceOfGaussianFilter(lenght, (double)numWeight1.Value, (double)numWeight2.Value);
                    resultBitmap.ImageShowForm("Метод " + fileName);
                    if (chb_SaveToFile.Checked)
                    {
                        SaveToFile(resultBitmap, fileName);
                    } 
                }
                                 
            }
        }

        private void btMATH_Click(object sender, EventArgs e)
        {
            MHAT result = new MHAT(OriginalBitmap);
            result.Run();
        }             
         
        private void button1_Click(object sender, EventArgs e)  
        // Метод Собеля
        {
            if (OriginalBitmap != null)
            {
                String fileName = null;
                Bitmap resultBitmap = null;

                if (chbGray.Checked)
                {
                    fileName = "Sobel__grey";
                    resultBitmap = OriginalBitmap.Sobel3x3Filter();
                    resultBitmap.ImageShowForm("Метод Собеля 3х3 (серый)");
                }
                else
                {
                    fileName = "Sobel_color";
                    resultBitmap = OriginalBitmap.Sobel3x3Filter(false);
                    resultBitmap.ImageShowForm("Метод Собеля 3х3");
                }
                if (chb_SaveToFile.Checked)
                {
                    SaveToFile(resultBitmap, fileName);
                }

            }
            else
            {
                MessageBox.Show("OriginalBitmap is null");
            }     
        }

 
        private void btLaplace_Click(object sender, EventArgs e)
        // Метод Лапласа
        {
            if (OriginalBitmap != null)
            {
                String fileName = null;
                Bitmap resultBitmap = null;
                try
                {
                    if (chbGray.Checked)
                    {
                        if (cmbLaplace.SelectedItem.ToString() == "3x3")
                        {
                            fileName = "Laplace_3x3_grey";
                            resultBitmap = OriginalBitmap.Laplacian3x3Filter();
                            resultBitmap.ImageShowForm("Метод Лапласа 3х3");
                        }
                        else if (cmbLaplace.SelectedItem.ToString() == "5x5")
                        {
                            fileName = "Laplace_5x5_grey";
                            resultBitmap = OriginalBitmap.Laplacian5x5Filter();
                            resultBitmap.ImageShowForm("Метод Лапласа 5х5 (серый)");
                        }
                    }
                    else
                    {  // если цветной
                        if (cmbLaplace.SelectedItem.ToString() == "3x3")
                        {
                            fileName = "Laplace_3x3_color";
                            resultBitmap = OriginalBitmap.Laplacian5x5Filter(false);
                            resultBitmap.ImageShowForm("Метод Лапласа 3х3");
                        }
                        else if (cmbLaplace.SelectedItem.ToString() == "5x5")
                        {
                            fileName = "Laplace_5x5_color";
                            resultBitmap = OriginalBitmap.Laplacian5x5Filter(false);
                            resultBitmap.ImageShowForm("Метод Лапласа 5х5");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                if (chb_SaveToFile.Checked)
                {
                    SaveToFile(resultBitmap, fileName);
                }

            }
            else
            {
                MessageBox.Show("OriginalBitmap is null");
            }
        }

        private void btCanny_Click(object sender, EventArgs e)
        {
            MessageBox.Show("еще не готово");
            if (OriginalBitmap != null) { }                      
        }

        private void btnPrewitt_Click(object sender, EventArgs e)
        {           

            if (OriginalBitmap != null)
            {
                String fileName = null;
                Bitmap resultBitmap = null;

                if (chbGray.Checked)
                {
                    fileName = "Prewitt_grey";
                    resultBitmap = OriginalBitmap.PrewittFilter();
                    resultBitmap.ImageShowForm("Метод Превитта (серый)");
                }
                else
                {
                    fileName = "Prewitt_color";
                    resultBitmap = OriginalBitmap.PrewittFilter(false);
                    resultBitmap.ImageShowForm("Метод Превитта");
                }
                if (chb_SaveToFile.Checked)
                {
                    SaveToFile(resultBitmap, fileName);
                }

            }
            else { 
                MessageBox.Show("OriginalBitmap is null"); 
            }

        }

        private void btnKirsch_Click(object sender, EventArgs e)
        {
            if (OriginalBitmap != null)
            {
                String fileName = null;
                Bitmap resultBitmap = null;

                if (chbGray.Checked)
                {
                    fileName = "Kirsch_grey";
                    resultBitmap = OriginalBitmap.KirschFilter();
                    resultBitmap.ImageShowForm("Метод Кирша (серый)");
                }
                else
                {
                    fileName = "Kirsch_color";
                    resultBitmap = OriginalBitmap.KirschFilter(false);
                    resultBitmap.ImageShowForm("Метод Кирша");
                }
                if (chb_SaveToFile.Checked)
                {
                    SaveToFile(resultBitmap, fileName);
                }

            }
            else
            {
                MessageBox.Show("OriginalBitmap is null");
            }

        }

        private void btnLoG_Click(object sender, EventArgs e)
        {
            if (OriginalBitmap != null)
            {
                String fileName = null;
                Bitmap resultBitmap = null;
                try
                {
                    if (cmbLoG.SelectedItem.ToString() == "LaplacianOfGaussian")
                    {
                        fileName = "LaplacianOfGaussian";
                        resultBitmap = OriginalBitmap.LaplacianOfGaussianFilter();
                        resultBitmap.ImageShowForm("Laplacian Of Gaussian (LoG)");
                    }
                    else if (cmbLoG.SelectedItem.ToString() == "Laplacian3x3OfGaussian3x3 F")
                    {
                        fileName = "Laplacian3x3OfGaussian3x3";
                        resultBitmap = OriginalBitmap.Laplacian3x3OfGaussian3x3Filter();
                        resultBitmap.ImageShowForm("Метод LoG 3x3 3x3");
                    }
                    else if (cmbLoG.SelectedItem.ToString() == "Laplacian3x3OfGaussian5x5 F1")
                    {
                        fileName = "Laplacian3x3OfGaussian5x5_1";
                        resultBitmap = OriginalBitmap.Laplacian3x3OfGaussian5x5Filter1();
                        resultBitmap.ImageShowForm("Метод LoG 3x3 5x5 1");
                    }
                    else if (cmbLoG.SelectedItem.ToString() == "Laplacian3x3OfGaussian5x5 F2")
                    {
                        fileName = "Laplacian3x3OfGaussian5x5_2";
                        resultBitmap = OriginalBitmap.Laplacian3x3OfGaussian5x5Filter2();
                        resultBitmap.ImageShowForm("Метод LoG 3x3 5x5 2");
                    }
                    else if (cmbLoG.SelectedItem.ToString() == "Laplacian5x5OfGaussian3x3 F")
                    {
                        fileName = "Laplacian5x5OfGaussian3x3";
                        resultBitmap = OriginalBitmap.Laplacian5x5OfGaussian3x3Filter();
                        resultBitmap.ImageShowForm("Метод LoG 5x5 3x3");
                    }
                    else if (cmbLoG.SelectedItem.ToString() == "Laplacian5x5OfGaussian5x5 F1")
                    {
                        fileName = "Laplacian5x5OfGaussian5x5_1";
                        resultBitmap = OriginalBitmap.Laplacian5x5OfGaussian5x5Filter1();
                        resultBitmap.ImageShowForm("Метод LoG 5x5 5x5 1");
                    }
                    else if (cmbLoG.SelectedItem.ToString() == "Laplacian5x5OfGaussian5x5 F2")
                    {
                        fileName = "Laplacian5x5OfGaussian5x5_2";
                        resultBitmap = OriginalBitmap.Laplacian5x5OfGaussian5x5Filter2();
                        resultBitmap.ImageShowForm("Метод LoG 5x5 5x5 2");
                    }
                    if (chb_SaveToFile.Checked)
                    {
                        SaveToFile(resultBitmap, fileName);
                    }
                                                            
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }               

            }
            else
            {
                MessageBox.Show("OriginalBitmap is null");
            }
        }

        private void btnMean_Click(object sender, EventArgs e)
        {
            if (OriginalBitmap != null)
            {
                String fileName = null;
                Bitmap resultBitmap = null;
                try
                {
                    ExtendedBitmap.BlurType blurType = ((ExtendedBitmap.BlurType)cmbMean.SelectedItem);
                    fileName = cmbMean.SelectedItem.ToString();
                    
                    if (chbGray.Checked)
                    {
                        fileName += " grey";                        
                        Bitmap tmp = OriginalBitmap.ImageBlurFilter(blurType);
                        resultBitmap = tmp.Grayscale();
                        resultBitmap.ImageShowForm("Метод "+ fileName);
                    }
                    else
                    {
                        resultBitmap = OriginalBitmap.ImageBlurFilter(blurType);
                        resultBitmap.ImageShowForm("Метод " + fileName);
                    }
                    if (chb_SaveToFile.Checked)                    
                    {
                        SaveToFile(resultBitmap, fileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }                
            }
            else
            {
                MessageBox.Show("OriginalBitmap is null");
            }
        }           

        private void btnMotionBlur_Click(object sender, EventArgs e)
        {
            if (OriginalBitmap != null)
            {
                String fileName = null;
                Bitmap resultBitmap = null;
                Bitmap tmp = null;
                try
                {
                    ExtendedBitmap.BlurType blurType = ((ExtendedBitmap.BlurType)cmbMotionBlur.SelectedItem);
                    fileName = cmbMotionBlur.SelectedItem.ToString();
                    if (chbGray.Checked)
                    {
                        fileName += " grey";
                        tmp = OriginalBitmap.ImageBlurFilter(blurType);
                        resultBitmap = tmp.Grayscale();
                        resultBitmap.ImageShowForm("Метод " + fileName);
                    }
                    else
                    {                        
                        resultBitmap = OriginalBitmap.ImageBlurFilter(blurType);
                        resultBitmap.ImageShowForm("Метод " + fileName);
                    }
                    if (chb_SaveToFile.Checked)
                    {
                        SaveToFile(resultBitmap, fileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("OriginalBitmap is null");
            }

        }       

        private void btnMedian_Click(object sender, EventArgs e)
        {
            if (OriginalBitmap != null)
            {
                String fileName = null;
                Bitmap resultBitmap = null;
                try
                {
                    ExtendedBitmap.BlurType blurType = ((ExtendedBitmap.BlurType)cmbMedian.SelectedItem);
                    fileName = cmbMedian.SelectedItem.ToString();
                    resultBitmap = OriginalBitmap.ImageBlurFilter(blurType);
                    resultBitmap.ImageShowForm("Метод " + fileName);
                    if (chb_SaveToFile.Checked)
                    {
                        SaveToFile(resultBitmap, fileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("OriginalBitmap is null");
            }

        }           

        private void btnGaussian_Click(object sender, EventArgs e)
        {
            if (OriginalBitmap != null)
            {
                String fileName = null;
                Bitmap resultBitmap = null;
                try
                {
                    ExtendedBitmap.BlurType blurType = ((ExtendedBitmap.BlurType)cmbGaussian.SelectedItem);
                    fileName = cmbGaussian.SelectedItem.ToString();
                    resultBitmap = OriginalBitmap.ImageBlurFilter(blurType);
                    resultBitmap.ImageShowForm("Метод " + fileName);
                    if (chb_SaveToFile.Checked)
                    {
                        SaveToFile(resultBitmap, fileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("OriginalBitmap is null");
            }

        }        

        private void btRunAllFilters_Click(object sender, EventArgs e)
        {
            btnPrewitt.PerformClick();
            btnKirsch.PerformClick();
            btnSobel.PerformClick();
            btnLaplace.PerformClick();
            btnLoG.PerformClick();
            btnDOG.PerformClick();

        }

        private void btnRunAllHum_Click(object sender, EventArgs e)
        {
            Bitmap bitmapResult = null;

            if (OriginalBitmap != null)
            {                
                try
                {
                    foreach(ExtendedBitmap.BlurType blurType in Enum.GetValues(typeof(ExtendedBitmap.BlurType))){
                        bitmapResult = OriginalBitmap.ImageBlurFilter(blurType);
                        bitmapResult.ImageShowForm("Метод " + blurType.ToString());
                    }                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("OriginalBitmap is null");
            }
            

        }

        
        // Анализ изображений
        private void button1_Click_2(object sender, EventArgs e)
        {          
            Form form = new AnalyzeImage();
            form.StartPosition = FormStartPosition.WindowsDefaultLocation;
            form.HelpButton = true;
            form.Show();
        }


        /////////////////// 
        // Хаар
        ///////////////////
        /// 

        public Bitmap OrigImg { get; set; }
        public Bitmap TransfImg { get; set; }

        private const double w0 = 0.5;
        private const double w1 = -0.5;
        private const double s0 = 0.5;
        private const double s1 = 0.5;

        /// <summary>
        ///   Вейвлет дискретной трансформации Хаара
        /// </summary>
        /// 
        public void FWT(double[] data)
        {
            double[] temp = new double[data.Length];

            int h = data.Length >> 1;
            for (int i = 0; i < h; i++)
            {
                int k = (i << 1);
                temp[i] = data[k] * s0 + data[k + 1] * s1;
                temp[i + h] = data[k] * w0 + data[k + 1] * w1;
            }

            for (int i = 0; i < data.Length; i++)
                data[i] = temp[i];
        }

        /// <summary>
        ///   Вейвлет дискретной трансформации Хаара 2D
        /// </summary>
        /// 
        public void FWT(double[,] data, int iterations)
        {
            int rows = data.GetLength(0);
            int cols = data.GetLength(1);

            double[] row = new double[cols];
            double[] col = new double[rows];

            for (int k = 0; k < iterations; k++)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < row.Length; j++)
                        row[j] = data[i, j];

                    FWT(row);

                    for (int j = 0; j < row.Length; j++)
                        data[i, j] = row[j];
                }

                for (int j = 0; j < cols; j++)
                {
                    for (int i = 0; i < col.Length; i++)
                        col[i] = data[i, j];

                    FWT(col);

                    for (int i = 0; i < col.Length; i++)
                        data[i, j] = col[i];
                }
            }
        }

        /// <summary>
        ///   Инверсия вейвлета трансформации Хаара
        /// </summary>
        /// 
        public void IWT(double[] data)
        {
            double[] temp = new double[data.Length];

            int h = data.Length >> 1;
            for (int i = 0; i < h; i++)
            {
                int k = (i << 1);
                temp[k] = (data[i] * s0 + data[i + h] * w0) / w0;
                temp[k + 1] = (data[i] * s1 + data[i + h] * w1) / s0;
            }

            for (int i = 0; i < data.Length; i++)
                data[i] = temp[i];
        }

        /// <summary>
        ///  Инверсия вейвлета трансформации Хаара 2D
        /// </summary>
        /// 
        public void IWT(double[,] data, int iterations)
        {
            int rows = data.GetLength(0);
            int cols = data.GetLength(1);

            double[] col = new double[rows];
            double[] row = new double[cols];

            for (int l = 0; l < iterations; l++)
            {
                for (int j = 0; j < cols; j++)
                {
                    for (int i = 0; i < row.Length; i++)
                        col[i] = data[i, j];

                    IWT(col);

                    for (int i = 0; i < col.Length; i++)
                        data[i, j] = col[i];
                }

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < row.Length; j++)
                        row[j] = data[i, j];

                    IWT(row);

                    for (int j = 0; j < row.Length; j++)
                        data[i, j] = row[j];
                }
            }
        }

        public double Scale(double fromMin, double fromMax, double toMin, double toMax, double x)
        {
            if (fromMax - fromMin == 0) return 0;
            double value = (toMax - toMin) * (x - fromMin) / (fromMax - fromMin) + toMin;
            if (value > toMax)
            {
                value = toMax;
            }
            if (value < toMin)
            {
                value = toMin;
            }
            return value;
        }

        public void ApplyHaarTransform(bool Forward, bool Safe, string sIterations)
        {
            Bitmap bmp = Forward ? new Bitmap(OrigImg) : new Bitmap(TransfImg);

            int Iterations = 0;
            int.TryParse(sIterations, out Iterations);

            int maxScale = (int)(Math.Log(bmp.Width < bmp.Height ? bmp.Width : bmp.Height) / Math.Log(2));
            if (Iterations < 1 || Iterations > maxScale)
            {
                MessageBox.Show("Итерация должна быть целым числом от 1 до " + maxScale);
                return;
            }

            int time = Environment.TickCount;

            double[,] Red = new double[bmp.Width, bmp.Height];
            double[,] Green = new double[bmp.Width, bmp.Height];
            double[,] Blue = new double[bmp.Width, bmp.Height];

            int PixelSize = 3;
            BitmapData bmData = null;

            if (Safe)
            {
                Color c;

                for (int j = 0; j < bmp.Height; j++)
                {
                    for (int i = 0; i < bmp.Width; i++)
                    {
                        c = bmp.GetPixel(i, j);
                        Red[i, j] = (double)Scale(0, 255, -1, 1, c.R);
                        Green[i, j] = (double)Scale(0, 255, -1, 1, c.G);
                        Blue[i, j] = (double)Scale(0, 255, -1, 1, c.B);
                    }
                }
            }
            else
            {
                bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                unsafe
                {
                    for (int j = 0; j < bmData.Height; j++)
                    {
                        byte* row = (byte*)bmData.Scan0 + (j * bmData.Stride);
                        for (int i = 0; i < bmData.Width; i++)
                        {
                            Red[i, j] = (double)Scale(0, 255, -1, 1, row[i * PixelSize + 2]);
                            Green[i, j] = (double)Scale(0, 255, -1, 1, row[i * PixelSize + 1]);
                            Blue[i, j] = (double)Scale(0, 255, -1, 1, row[i * PixelSize]);
                        }
                    }
                }
            }

            if (Forward)
            {
                FWT(Red, Iterations);
                FWT(Green, Iterations);
                FWT(Blue, Iterations);
            }
            else
            {
                IWT(Red, Iterations);
                IWT(Green, Iterations);
                IWT(Blue, Iterations);
            }

            if (Safe)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    for (int i = 0; i < bmp.Width; i++)
                    {
                        bmp.SetPixel(i, j, Color.FromArgb((int)Scale(-1, 1, 0, 255, Red[i, j]), (int)Scale(-1, 1, 0, 255, Green[i, j]), (int)Scale(-1, 1, 0, 255, Blue[i, j])));
                    }
                }
            }
            else
            {
                unsafe
                {
                    for (int j = 0; j < bmData.Height; j++)
                    {
                        byte* row = (byte*)bmData.Scan0 + (j * bmData.Stride);
                        for (int i = 0; i < bmData.Width; i++)
                        {
                            row[i * PixelSize + 2] = (byte)Scale(-1, 1, 0, 255, Red[i, j]);
                            row[i * PixelSize + 1] = (byte)Scale(-1, 1, 0, 255, Green[i, j]);
                            row[i * PixelSize] = (byte)Scale(-1, 1, 0, 255, Blue[i, j]);
                        }
                    }
                }

                bmp.UnlockBits(bmData);
            }

            if (Forward)
            {
                TransfImg = new Bitmap(bmp);
            }

            pictureBox1.Image = bmp;
            lblDirection.Text = Forward ? "Действие вперед" : "Инверсия действия";
            lblTransformTime.Text = ((int)(Environment.TickCount - time)).ToString() + " мсек.";
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Файлы изображений(*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.tif)|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.tif";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Bitmap tempbitmap = new Bitmap(open.FileName);
                if (((tempbitmap.Width & (tempbitmap.Width - 1)) != 0) ||
                    ((tempbitmap.Height & (tempbitmap.Height - 1)) != 0))
                {
                    MessageBox.Show("Ширина и высота изображения должны быть степенью 2!");
                    return;
                }
                OrigImg = tempbitmap;
                pictureBox1.Image = OrigImg;
                lblWidth.Text = OrigImg.Width.ToString();
                lblHeight.Text = OrigImg.Height.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Изображения|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.tif";
            ImageFormat format = ImageFormat.Png;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                    case ".jpeg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                    case ".gif":
                        format = ImageFormat.Gif;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                    case ".tif":
                        format = ImageFormat.Tiff;
                        break;
                }
                pictureBox1.Image.Save(sfd.FileName, format);
            }
        }

        private void btnForwardSafe_Click(object sender, EventArgs e)
        {
            ApplyHaarTransform(true, true, txtIterations.Text);
        }

        private void btnForwardUnsafe_Click(object sender, EventArgs e)
        {
            ApplyHaarTransform(true, false, txtIterations.Text);
        }

        private void btnInverseSafe_Click(object sender, EventArgs e)
        {
            ApplyHaarTransform(false, true, txtIterations.Text);
        }

        private void btnInverseUnsafe_Click(object sender, EventArgs e)
        {
            ApplyHaarTransform(false, false, txtIterations.Text);
        }

        private void toolStripHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Дипломная работа, версия 16.7.2.3",
                "О программе..");
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms приложение
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // консольное
                System.Environment.Exit(1);
            }
        }
    }
}
       