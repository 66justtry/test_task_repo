using System;

namespace matrix_app
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //создание матрицы
            int n = 9, m = 9;
            int[,] matrix = new int[n, m];
            var rand = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix[i, j] = rand.Next(4);
                }
            }

            Console.WriteLine("Matrix:");
            Print(matrix, n, m);

            //проверка матрицы
            bool deleted = true;

            while (deleted)
            {
                //если в течение проверок было удаление - повторяем проверки
                deleted = false;
                for (int i = 0; i < m; i++)
                {
                    if (CheckRow(matrix, n, i))
                    {
                        //если удаляли элементы в строке
                        deleted = true;
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    if (CheckColumn(matrix, m, i))
                    {
                        //если удаляли элементы в столбце
                        deleted = true;
                    }
                }
            }

            //результат
            Console.WriteLine("\nResult:");
            Print(matrix, n, m);


        }

        static bool CheckRow(int[,] matrix, int n, int row)
        {
            //проверка для одной строки
            int cur = 0;
            while (cur < n)
            {
                int posx = cur;
                int count = 0;
                while ((posx < n) && (matrix[row, cur] == matrix[row, posx]))
                {
                    count++;
                    posx++;
                }
                if (count >= 3)
                {
                    //если найдено 3 в ряд - удаляем, начинаем проверку заново
                    RemoveRow(matrix, row, cur, cur + count - 1);
                    CheckRow(matrix, n, row);
                    return true;
                }
                cur++;
            }
            return false;
        }

        static void RemoveRow(int[,] matrix, int row, int columnFrom, int columnTo)
        {
            var rand = new Random();
            for (int i = columnFrom; i <= columnTo; i++)
            {
                for (int j = row; j > 0; j--)
                {
                    //сдвиг столбца вниз, вместо удаленного элемента
                    matrix[j, i] = matrix[j - 1, i];
                }
                //добавляем новый элемент сверху
                matrix[0, i] = rand.Next(4);
            }
        }

        static bool CheckColumn(int[,] matrix, int m, int col)
        {
            //проверка для одного столбца
            int cur = 0;
            while (cur < m)
            {
                int posy = cur;
                int count = 0;
                while ((posy < m) && (matrix[cur, col] == matrix[posy, col]))
                {
                    count++;
                    posy++;
                }
                if (count >= 3)
                {
                    //если найдено 3 подряд - удаление и проверка заново
                    RemoveColumn(matrix, col, cur, cur + count - 1);
                    CheckColumn(matrix, m, col);
                    return true;
                }
                cur++;
            }
            return false;
        }
        static void RemoveColumn(int[,] matrix, int col, int rowFrom, int rowTo)
        {
            var rand = new Random();
            for (int i = rowTo; i >= rowFrom; i--)
            {
                //сдвигаем элементы, начиная с последнего заданого, повторяем для элементов выше
                for (int j = rowTo; j > 0; j--)
                {
                    matrix[j, col] = matrix[j - 1, col];
                }
                matrix[0, col] = rand.Next(4);
            }
        }

        static void Print(int[,] matrix, int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}