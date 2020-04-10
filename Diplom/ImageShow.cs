using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AForge.Imaging;
using AForge.Math;
using OpenCvSharp;



namespace Diplom
{
    public partial class ImageShowForm : Form
    {
        private ImageStatistics stat;        
        
        private Histogram activeHistogram;
        private int currentImageHash = 0;
        private Bitmap src = null;    

        public ImageShowForm()
        {
            InitializeComponent();
        }
        
        public ImageShowForm(Bitmap src, String str)
            // переопределим конструктор класса для передачи параметров из родительской формы
        {
            InitializeComponent();
            // задаем сдвиг изображения вправо относительно правой границы окна
            int shift = 240;

            // Определяем размеры изображения
            int W = src.Width;
            int H = src.Height;

            //Меняем размеры экрана 
            this.Size = new Size(W + shift + 4, H + 4);
            this.Text = str;
                        

            //pb.Width = Width;
            //pb.Height = Height;
            pb.Location = new Point(shift + 4, 2);
            pb.Size = new Size(W, H);
            
            this.src = src;
            
            pb.Image = src;          
            
            // Инициализируем параметры отображения графика гистограммы
            // Отключаем отображение легенды
            chart1.Series["Series1"].IsVisibleInLegend = false;          
                      
            // Собираем статистические данные по изображению.
            GatherStatistics(src);
            
        }

        private void ImageShowForm_Load(object sender, EventArgs e)
        {

        }

        // selection changed in channels combo
        private void channelCombo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (stat != null)
            {
                SwitchChannel((stat.IsGrayscale) ? 3 : channelCombo.SelectedIndex);
            }
        }
        // Gather image statistics
        public void GatherStatistics(Bitmap image)
        {
            // avoid calculation in the case of the same image
            if (image != null)
            {
                if (currentImageHash == image.GetHashCode())
                    return;
                currentImageHash = image.GetHashCode();
            }

            
            // busy
            Capture = true;
            Cursor = Cursors.WaitCursor;

            // get statistics
            stat = (image == null) ? null : new ImageStatistics(image);

            // free
            Cursor = Cursors.Arrow;
            Capture = false;

            // clean combo
            channelCombo.Items.Clear();
            channelCombo.Enabled = false;

            if (stat != null)
            {
                if (!stat.IsGrayscale)
                {
                    // RGB picture
                    channelCombo.Items.AddRange(new object[] { "Red", "Green", "Blue" });
                    channelCombo.Enabled = true;
                }
                else
                {
                    // grayscale picture
                    channelCombo.Items.Add("Gray");
                }
                channelCombo.SelectedIndex = 0;
            }
            else
            {
                //histogram.Values.
                //histogram.Values = null;
                meanLabel.Text = String.Empty;
                stdDevLabel.Text = String.Empty;
                medianLabel.Text = String.Empty;
                minLabel.Text = String.Empty;
                maxLabel.Text = String.Empty;                
            }
        }

        // Switch channel
        public void SwitchChannel( int channel )
        {
            if ( ( channel >= 0 ) && ( channel <= 2 ) )
            {
                if ( !stat.IsGrayscale )
                {
                    //histogram.Color = colors[channel];
                    activeHistogram = ( channel == 0 ) ? stat.Red : ( channel == 1 ) ? stat.Green : stat.Blue;
                }
            }
            else if ( channel == 3 )
            {
                if ( stat.IsGrayscale )
                {
                    //histogram.Color = colors[3];
                    activeHistogram = stat.Gray;
                }
            }

            if ( activeHistogram != null )
            {
                //histogram.Values = activeHistogram.Values;
                // Собираем статистику по изображению
                int[] yval = activeHistogram.Values;
                
                try
                {
                    // очищаем Сhart для дальшейшей отрисовки
                    foreach (var series in chart1.Series)
                    {
                        series.Points.Clear();
                    }
                    // Заполняем Chart значениями гистограммы                  
                    chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
                    chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Enabled = false;
                    for (int i = 0; i < yval.Length; i++)
                    {
                        chart1.Series["Series1"].Points.AddXY(i, yval[i]);
                    }
                }
                catch (Exception e){
                    MessageBox.Show(e.Message);
                }
                meanLabel.Text = activeHistogram.Mean.ToString( "F2" );     // среднее значение                
                stdDevLabel.Text = activeHistogram.StdDev.ToString( "F2" ); // стандартное отклонение                
                medianLabel.Text = activeHistogram.Median.ToString( );      // медиана                
                minLabel.Text = activeHistogram.Min.ToString( );            // минимальное значение                
                maxLabel.Text = activeHistogram.Max.ToString( );            // максимальное значение  
                                               
                double MSE = ExtendedBitmap.SKO(src, Diplom.MainForm.getOriginalBitmap());
                MSELabel.Text = MSE.ToString("F3");

                double SNR = ExtendedBitmap.SNR(src, Diplom.MainForm.getOriginalBitmap());
                SNRlabel.Text = SNR.ToString("F3");

                double SNRGG = ExtendedBitmap.SNRGG(src);
                SNRGGlabel.Text = SNRGG.ToString("F3");
                
                
            }
        }

        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {

            if (src != null)
            {                 
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Сохранить изображение";
                sfd.Filter = "Bitmap Images(*.bmp)|*.bmp|Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";                

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(sfd.FileName).ToUpper();
                    ImageFormat imgFormat = ImageFormat.Png;

                    if (fileExtension == "BMP")
                    {
                        imgFormat = ImageFormat.Bmp;
                    }
                    else if (fileExtension == "JPG")
                    {
                        imgFormat = ImageFormat.Jpeg;
                    }

                    StreamWriter streamWriter = new StreamWriter(sfd.FileName, false);
                    src.Save(streamWriter.BaseStream, imgFormat);
                    streamWriter.Flush();
                    streamWriter.Close();                    
                }
            }
        }        
    }
}
