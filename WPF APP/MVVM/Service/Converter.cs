using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Service
{
    public class Converter
    {
        public static int[,] ArrayToMatrix(int[] array)
        {

            int[,] matriz = new int[6, 7];
            // int aux = array.Length-1;
            int aux = 0;

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    matriz[i, j] = array[aux];
                    aux++;
                }


            }

            return matriz;
        }

        public static int[] MatrixToArray(int[,] matriz)
        {
            int[] array = new int[42];

            int aux = 0;

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    array[aux] = matriz[i, j];
                    aux++;
                }
            }

            return array;
        }
    }
}
