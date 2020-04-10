using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    public  class Array
    {
        // Поля класса
        private int m, n;                   // размерность массива
        private double[,] mass;             // сам массив
        

       // конструктор  
       public Array(int N, int M)
        {
            m = M;
            n = N;
            mass = new double[N, M];
        }

       // конструктор 
       public Array(Array source)
        {
            this.m = source.m;
            this.n = source.n;
            this.mass = new double[source.n, source.m];
            this.mass = (double[,])source.mass.Clone();
        }

        // деструктор
        ~Array()
        {
            m = 0;
            n = 0;
            mass = null;
        }

        public int N
        {
            get
            {
                if (n > 0)
                    return n;
                else
                    return -1;
            }
            set {
                if (n > 0)
                    n = (int)value;            
            }
        }

        public int M
        {
            get
            {
                if (m > 0)
                    return m;
                else
                    return -1;
            }
            set{
                if (m > 0)
                    m = (int)value;            
            }
        }

        public double this[int i, int j]
        {
            get
            {
                if (n > 0 && m > 0)
                    if (i > -1 && j > -1)
                        return mass[i, j];
                    else
                        Console.WriteLine("Неверный индексы");
                else
                    Console.WriteLine("Не задана матрица");
                return -1;
            }
            set
            {
                if (n > 0 && m > 0)
                    if (i > -1 && j > -1)
                        mass[i, j] = value;
                    else
                        Console.WriteLine("Неверный индексы");
                else
                    Console.WriteLine("Не задана матрица");
            }
        }

        // Умножение матриц. Перегрузка оператора умножения
        public static Array operator *(Array A, Array B)
        {
            if (A.m != B.n)
                throw new System.ArgumentException("Не совпадают размерности матриц");

            Array C = new Array(A.n, B.m); //Столько же строк, сколько в А; столько столбцов, сколько в B 
            for (int i = 0; i < A.n; ++i)
            {
                for (int j = 0; j < B.m; ++j)
                {
                    C[i, j] = 0;
                    for (int k = 0; k < A.m; ++k)
                    { //ТРЕТИЙ цикл, до A.m=B.n
                        C[i, j] += A[i, k] * B[k, j]; //Собираем сумму произведений
                    }
                }
            }
            return C;
        }

        // Умножение матрицы на число. Перегрузка оператора умножения
        public static Array operator *(double N, Array A)
        {
            Array C = new Array(A.n, A.m); 
            for (int i = 0; i < A.n; ++i)
            {
                for (int j = 0; j < A.m; ++j)
                {
                    C[i, j] = N * A[i, j];
                }
            }
            return C;
        }
        // Умножение матрицы на число. Перегрузка оператора умножения
        public static Array operator *(Array A, double N)
        {
            Array C = new Array(A.n, A.m);
            for (int i = 0; i < A.n; ++i)
            {
                for (int j = 0; j < A.m; ++j)
                {
                    C[i, j] = N * A[i, j];
                }
            }
            return C;
        }

        // Сложение матриц. Перегрузка оператора сложения
        public static Array operator +(Array A, Array B)
        {
            if ((A.m != B.m) && (A.n != B.n)) 
                throw new System.ArgumentException("Не совпадают размерности матриц. Должны быть одного размера");

            Array C = new Array(A.n, B.m); //Столько же строк, сколько в А; столько столбцов, сколько в B 
            for (int i = 0; i < A.n; ++i)
            {
                for (int j = 0; j < B.m; ++j)
                {
                    C[i, j] = 0;
                    for (int k = 0; k < A.m; ++k)
                    { //ТРЕТИЙ цикл, до A.m=B.n
                        C[i, j] += A[i, k] * B[k, j]; //Собираем сумму произведений
                    }
                }
            }
            return C;
        }

        // Сложение матрицы и числа. Перегрузка оператора сложения
        public static Array operator +(double K, Array A)
        {
            Array C = new Array(A.N, A.M);
            for (int i = 0; i < A.N; ++i)
            {
                for (int j = 0; j < A.M; ++j)
                {
                    C[i, j] = K + A[i, j];
                }
            }
            return C;
        }

        // Сложение матрицы и числа. Перегрузка оператора сложения
        public static Array operator +(Array A, double K)
        {
            Array C = new Array(A.N, A.M);
            for (int i = 0; i < A.N; ++i)
            {
                for (int j = 0; j < A.M; ++j)
                {
                    C[i, j] = K + A[i, j];
                }
            }
            return C;
        }

        // Вычитание матрицы и числа. Перегрузка оператора вычитания
        public static Array operator -(Array A, double K)
        {
            Array C = new Array(A.N, A.M);
            for (int i = 0; i < A.N; ++i)
            {
                for (int j = 0; j < A.M; ++j)
                {
                    C[i, j] = A[i, j] - K;
                }
            }
            return C;
        }

        // Вычитание матрицы и числа. Перегрузка оператора вычитания
        public static Array operator -(double K, Array A)
        {
            Array C = new Array(A.N, A.M);
            for (int i = 0; i < A.N; ++i)
            {
                for (int j = 0; j < A.M; ++j)
                {
                    C[i, j] = K - A[i, j];
                }
            }
            return C;
        }

        // Нахождение минимального значения матрицы
        public static double min(Array A)
        {   
            double min = A[0,0];
            
            for (int i = 0; i < A.n; ++i)
            {
                for (int j = 0; j < A.m; ++j)
                {
                    if (min > A[i, j])
                        min = A[i, j];
                }
            }
            return min;
        }

        // Нахождение минимального значения матрицы
        public static double max(Array A)
        {
            double max = A[0, 0];

            for (int i = 0; i < A.n; ++i)
            {
                for (int j = 0; j < A.m; ++j)
                {
                    if (max < A[i, j])
                        max = A[i, j];
                }
            }
            return max;
        }

        // Отображением матрицы. N строк и столбцов. Для диагностики.
        public void Show(int col, string title )
        {            
            string str = "Размерность матрицы: " + n.ToString() + "х" + m.ToString() + "\n";
                
            int count;
            
            if (m > n)
                count = n;
            else
                count = m;
            
            if (col < count )
                count = col;

            str = "pic[0,1]=" + this[0, 1].ToString() + "\n";
            for (int i = 0; i < count; ++i)
            {
                for (int j = 0; j < count; ++j)
                {
                    str += this[i, j].ToString("F3") + "\t";
                }
                str += "\n";
            }
            MessageBox.Show(str, title);            
        }

        public Array Copy()
        {
            return new Array(this);
        }

    }
}
