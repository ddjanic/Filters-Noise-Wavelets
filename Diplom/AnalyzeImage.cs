using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Diplom
{
    public partial class AnalyzeImage : Form
    {
        Bitmap img1;
        Bitmap img2;
        static Boolean b_img1 = false;         // загружено ли изображение 1
        static Boolean b_img2 = false;         // загружено ли изображение 2
        Boolean LoadingImage = false;
        double SKO;
        double SNRGG1;
        double SNRGG2;
        double SNRF1;
        double SNRF2;

        public AnalyzeImage()
        {
            InitializeComponent();
            SNRF1label.Text = "";
            SNRF2label.Text = "";
            SNRGG1label.Text = "";
            SNRGG2label.Text = "";
        }

        // Расчет и отображение статистики по изображениям
        private void AnalyzeRun()
        {
                // Если загружены обе картинки - расчитываем статистику по каждой и  разницу между ними
                if (LoadingImage)       // если обе картинки загружены                    
                {
                    SKO = ExtendedBitmap.SKO(img1, img2);
                    if (SKO != -1 ) label_SKO.Text = SKO.ToString("F3");
                }            
        }

        // Загрузка изображения 1
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберете файл изображения.";
            ofd.Filter = "BMP|*.bmp|Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(ofd.FileName);
                img1 = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();
                
                // картинка загружена
                b_img1 = true;
                LoadingImage = b_img1 && b_img2;

                // выводим картинку выбранную картинку на экран                
                pb1.Image = img1.resizeImage(new Size(395, 395));

                // расчитываем статистику
                SNRF1 = ExtendedBitmap.SNRF(img1);
                SNRGG1 = ExtendedBitmap.SNRGG(img1);

                SNRF1label.Text = SNRF1.ToString("F3");
                SNRGG1label.Text = SNRGG1.ToString("F3");

                // делаем анализ
                AnalyzeRun();
                
            }
        }

        // Загрузка изображения 2
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберете файл изображения.";
            ofd.Filter = "BMP|*.bmp|Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(ofd.FileName);
                img2 = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();

                // картинка загружена
                b_img2 = true;
                LoadingImage = b_img1 && b_img2;
                // выводим картинку выбранную картинку на экран                
                pb2.Image = img2.resizeImage(new Size(395, 395));

                SNRF2 = ExtendedBitmap.SNRF(img2);
                SNRGG2 = ExtendedBitmap.SNRGG(img2);

                SNRF2label.Text = SNRF2.ToString("F3");
                SNRGG2label.Text = SNRGG2.ToString("F3");

                // делаем анализ
                AnalyzeRun();
            }

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
