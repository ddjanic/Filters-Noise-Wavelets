using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace Diplom
{
    abstract public class wavelet
    {
        protected const short a = 3;            // масштабный коэффициент
        public double b;                        // Делитель для результата дифференцирования по X и по Y
        public Bitmap img;
        private int Xquantity;                  // кол-во столбцов в изображении
        private int Yquantity;                  // кол-во строк в изображении
        private int Xdecomposition;             //Количество уровней разложения по X
        private int Ydecomposition;             //Количество уровней разложения по Y
        Array data;                             //точки по X и Y (яркость)

        public int XQuantity{
            get
            {
                return Xquantity;
            }
            set
            {
                Xquantity = value;
            }
        }

        public int YQuantity
        {
            get
            {
                return Yquantity;
            }
            set
            {
                Yquantity = value;
            }
        }

        public int XDecomposition {
            get
            {
                return Xdecomposition;
            }
            set
            {
                Xdecomposition = value;
            }
        }

        public int YDecomposition
        {
            get
            {
                return Ydecomposition;
            }
            set
            {
                Ydecomposition = value;
            }
        }

                // constructor
        public wavelet(Bitmap src)
        {
            if (src == null)
                throw new System.ArgumentException("В конструктор не передали изображение");
            
            XQuantity = src.Width;
            YQuantity = src.Height;
            XDecomposition = (int)Math.Round((Math.Log(XQuantity) / Math.Log(2)) - 1);
            YDecomposition = (int)Math.Round((Math.Log(YQuantity) / Math.Log(2)) - 1);

            // если изображение цветное, то преобразовываем в серое
            if (src.isGray())
            {
                img = src.Grayscale();
            }
            else
            {
                MessageBox.Show("Ошибка в конструкторе wawelet");
                //throw new System.ArgumentException("В конструктор не передали изображение");
            }

            //img.ImageShowForm("gray");
            

            // Формируем массив из исходного изображения
            data = new Array(XQuantity, YQuantity);
            for (int x = 0; x < XQuantity; x++)
            {
                for (int y = 0; y < YQuantity; y++) {                 
                    Color RGB = img.GetPixel(x,y);
                    data[x, y] = RGB.R;
                }
            }
            data.Show(7, "Исходное изображение");           
        }

        // вейвлет (определяется в производных классах DOG, MHAT, WAVE)
        public abstract double wavelet0(double x);

        // Первая производная вейвлет (определяется в производных классах DOG, MHAT, WAVE)
        public abstract double wavelet1(double x);

        // Вейвлет в дискретной форме 
        private double d_wavelet(int x, double m, double n)
        {
            return Math.Pow(a, -0.5 * m) * wavelet0(Math.Pow(a, -m) * x - n);
        }

        //Первая производная вейвлет в дискретной форме
        private double d_wavelet1(int x, double m, double n)
        {
            return Math.Pow(a, -0.5 * m) * wavelet1(Math.Pow(a, -m) * x - n);
        }

        // Дифференцирование по Х
        private Array dXWAVE()
        {
            // создаем список, состоящий из двумерных массивов
            
                Array[] DWTWAVEXy = new Array[Yquantity];
                try
                {
                    Array DWT = new Array(Xdecomposition, Xquantity);

                    for (int y = 0; y < Yquantity; y++)
                    {
                        for (int m = 0; m < Xdecomposition; m++)
                        {
                            for (int n = 0; n < Xquantity; n++)
                            {
                                for (int x = 0; x < Xquantity; x++)
                                {                                    
                                    DWT[m, n] = d_wavelet(x, Math.Pow(2, m - 1), n) * data[x,y];                                     
                                }
                            }

                        }
                        //DWT.Show(7);
                        DWTWAVEXy[y] = DWT;
                    }                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            // вторая часть dXWAVE
            Array IDWT = new Array(Xquantity, Yquantity);

            for (int y = 0; y < Yquantity; y++)
            {
                Array DWTWy = new Array(Xdecomposition, Xquantity);
                DWTWy = DWTWAVEXy[y];
                
                for (int x = 0; x < Xquantity; x++)
                {

                    double tmp = 0;
                    for (int i = 0; i < Xdecomposition; i++)
                    {
                        for (int j = 0; j < Xquantity; j++)
                        {
                            tmp += d_wavelet1(x, Math.Pow(2, i - 1), j) * DWTWy[i, j];
                        }
                    }
                    
                    IDWT[x, y] = tmp;
                }
            }           
            return IDWT;
        }

        // Дифференцирование по Y
        private Array dYWAVE()
        {
            // создаем список, состоящий из двумерных массивов

            Array[] DWTWAVEkx = new Array[Xquantity];
            try
            {
                Array DWT = new Array(Ydecomposition, Yquantity);

                for (int x = 0; x < Xquantity; x++)
                {
                    for (int m = 0; m < Ydecomposition; m++)
                    {
                        for (int n = 0; n < Yquantity; n++)
                        {
                            for (int y = 0; y < Yquantity; y++)
                            {
                                DWT[m, n] = d_wavelet(y, Math.Pow(2, m - 1), n) * data[x,y];                                
                            }
                        }

                    }                    
                    DWTWAVEkx[x] = DWT;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // вторая часть dXWAVE
            Array IDWT = new Array(Xquantity, Yquantity);

            for (int x = 0; x < Xquantity; x++)
            {
                Array DWTWx = new Array(Xdecomposition, Xquantity);
                DWTWx = DWTWAVEkx[x];

                for (int y = 0; y < Yquantity; y++)
                {

                    double tmp = 0;
                    for (int i = 0; i < Ydecomposition; i++)
                    {
                        for (int j = 0; j < Yquantity; j++)
                        {
                            tmp += d_wavelet1(y, Math.Pow(2, i - 1), j) * DWTWx[i, j];
                        }
                    }

                    IDWT[x, y] = tmp;
                }
            }
            return IDWT;
        }

        // Grad
        public Array Grad(Array DifX, Array DifY) {
            Array result = new Array(Xquantity,Yquantity);
            for (int x = 0; x < DifX.N; x++){
                for (int y = 0; y <DifY.N; y++){
                    result[x,y]= Math.Sqrt(DifX[x,y]*DifX[x,y] + DifX[x,y]*DifX[x,y]);
                }
            }            
            return result;
        }

        // Normal
        public Array Normal(Array A) {
            Array result = new Array(A.N, A.M);
            Double tmp =  254 / (Array.max(A) - Array.min(A));
            return result = (A - Array.min(A)) * tmp;             
        }

        private Array RSchmX(Array A) {
            Array result = new Array(A.N, A.M);
            for (int x = 1; x < A.N; x++) {
                for (int y = 1; y < A.M; y++) {
                    result[y, x] = A[y, x] - A[y, x - 1];
                }
            }
            return result;
        }

        private Array RSchmY(Array A)
        {
            Array result = new Array(A.N, A.M);
            for (int x = 1; x < A.N; x++)
            {
                for (int y = 1; y < A.M; y++)
                {
                    result[y, x] = A[y, x] - A[y - 1, x];
                }
            }
            return result;
        }

        public void Run() {
            Array dX = new Array(dXWAVE());
            //dX.Show(7, "dXWAVE - дифференцирование по Х");

            Array dY = new Array(dYWAVE());
            //dY.Show(7, "dXWAVE - дифференцирование по Y");

            MessageBox.Show("Делим dX & dY на " + b.ToString());

            Array pic = new Array(Grad(b*dXWAVE(),b*dYWAVE()));
            //pic.Show(7, "После обработки Grab");
        }       
        
        
    }
    
    public class DOG : wavelet
    {
        // constructor        
        public DOG (Bitmap src) : base(src)
        {
            b = 1 / 10.5;
        }

        // вейвлет DOG
        public override double wavelet0(double x)
        {
            double kv = x * x;
            return Math.Exp(-0.5 * kv) - 0.5 * Math.Exp(-0.125 * kv);
        }

        // Первая производная вейвлет DOG
        public override double wavelet1(double x)
        {
            double kv = x * x;
            return 0.125 * x * Math.Exp(-0.125 * kv) - x * Math.Exp(-0.5 * kv);
        }
    }

    public class MHAT : wavelet
    {
        // constructor        
        public MHAT(Bitmap src) : base(src)
        {
            b = 1 / 23.0;
        }

        // вейвлет MHAT
        public override double wavelet0(double x)
        {
            double kv = x * x;
            return (2 * Math.Pow(Math.PI, -0.25) / Math.Sqrt(3)) * (1 - kv) * Math.Exp(-0.5 * kv);
        }

        // Первая производная вейвлет MHAT
        public override double wavelet1(double x)
        {
            double kv = x * x;
            return (2 * Math.Sqrt(3) * x * Math.Exp(-0.5 * kv) * (kv - 3)) / (3 * Math.Pow(Math.PI, 0.25));
        }
    }

    public class WAVE : wavelet
    {
        // constructor
        public WAVE(Bitmap src) : base (src)
        {
            b = 1 / 21.0;
        }

        // вейвлет WAVE
        public override double wavelet0(double x)
        {
            return -x * Math.Exp(-0.5 * x * x);
        }

        // Первая производная вейвлет WAVE
        public override double wavelet1(double x)
        {
            double kv = x * x;
            return kv * Math.Exp(-0.5 * kv) - Math.Exp(-0.5 * kv);
        }

    }
}
