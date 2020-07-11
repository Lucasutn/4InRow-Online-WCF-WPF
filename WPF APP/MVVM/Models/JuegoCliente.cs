using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Service
{
    public class JuegoCliente
    {
        public int[,] Tablero { get; set; }

        public int[] Contador { get; set; }
        public JugadorCliente[] Jugadores { get; set; }

        public JuegoCliente()
        {
            Tablero = new int[6, 7];

            Contador = new int[] { 5, 5, 5, 5, 5, 5, 5 };
        }

        public void Anotacion(JugadorCliente jugador, int column)
        {
            if (Contador[column] >= 0)
            {
                Tablero[Contador[column], column] = jugador.Id;


                Contador[column] -= 1;
            }
            else
            {

                //no cambia de turno
            }
        }

        /// <summary>
        /// Verifica si hay un ganador, llamando a las dos funciones encargadas de realizar la verificacion en filas y diagonales
        /// </summary>
        /// <returns></returns>
        public bool VerificarGanador()
        {
            return VerificarFilasMatriz(Tablero) || VerificarFilasMatriz(MatrizTranspuesta(Tablero)) || VerificarDiagonalesMatriz(Tablero) || VerificarDiagonalesMatriz(PermutarMatriz(Tablero));
        }

        public bool VerificarEmpate()
        {
            return !Converter.MatrixToArray(Tablero).Contains(0);
        }


        /// <summary>
        /// utilizada para la verificacion de filas en la comprobacion de diagonales de una matriz
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public bool VerificarFila(List<int> array)
        {
            return VerificarFila(array.ToArray());
        }

        /// <summary>
        /// Verifica si un arreglo en un arreglo de una dimension se encuentra un determinado valor repetido consecutivamente 4 veces
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public bool VerificarFila(int[] array)
        {
            int aux = array[0];
            int cuatro = 0;

            foreach (var num in array)
            {
                if (aux == num && num != 0)
                {
                    cuatro += 1;

                    if (cuatro >= 4)
                    {
                        return true;
                    }
                }
                else
                {
                    cuatro = 0;

                    if (num != 0)
                    {
                        cuatro += 1;
                    }

                    aux = num;
                }
            }

            return false;
        }


        /// <summary>
        /// Verifica Filas y Columnas en una matriz buscando Ganador (4 aciertos)
        /// </summary>
        /// <param name="matriz"></param>
        /// <returns></returns>
        public bool VerificarFilasMatriz(int[,] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                int[] array = new int[matriz.GetLength(1)];

                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    array[j] = matriz[i, j];
                }

                // if (!VerificarFila(array)) continue;
                // return true;

                if (VerificarFila(array))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matriz"></param>
        /// <returns></returns>
        public int[,] MatrizTranspuesta(int[,] matriz)
        {
            int[,] transpuesta = new int[matriz.GetLength(1), matriz.GetLength(0)];

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    transpuesta[j, i] = matriz[i, j];
                }
            }

            return transpuesta;
        }

        /// <summary>
        /// Genera arreglos con las diagonales que se forman en una matriz con una longitud mayor e igual que 4.
        /// Luego utiliza la funcion para VerificarFilas
        /// </summary>
        /// <param name="matriz"></param>
        /// <returns></returns>
        public bool VerificarDiagonalesMatriz(int[,] matriz)
        {
            int aux = 7;
            int cont = 0;


            for (int i = 0; i < 4; i++)
            {
                List<int> array = new List<int>();

                int otroCont = 0;

                for (int j = cont; j < aux; j++)
                {
                    if (array.Count < 6)
                    {
                        array.Add(matriz[otroCont, j]);
                    }

                    otroCont += 1;
                }


                if (VerificarFila(array) == true)
                {
                    return true;
                }


                cont += 1;
            }

            int otroAux = 5;
            cont = 0;


            //itera solo dos veces, pq se necesita solo las dos ultimas diagonales
            for (int i = 0; i < 2; i++)
            {
                List<int> array = new List<int>();

                int otroCont = 1 + i;

                for (int j = cont; j < otroAux; j++)
                {
                    if (array.Count < 6)
                    {
                        array.Add(matriz[otroCont, j]);
                    }

                    otroCont += 1;
                }


                if (VerificarFila(array))
                {
                    return true;
                }

                otroAux -= 1;

            }


            return false;
        }

        /// <summary>
        /// permuto una matriz moviendo las columnas que se encuentra a la izquierda a derecha, la primer columna por la ultima,
        /// la segunda por la ante ultima..
        /// </summary>
        /// <param name="matriz"></param>
        /// <returns></returns>
        public int[,] PermutarMatriz(int[,] matriz)
        {
            int[,] array = new int[matriz.GetLength(0), matriz.GetLength(1)];
            int[,] array2 = new int[matriz.GetLength(0), matriz.GetLength(1)];

            int rowNum = matriz.GetLength(0) - 1;
            int columnNum = matriz.GetLength(1) - 1;


            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {

                    array[i, j] = matriz[rowNum - i, columnNum - j];
                }
            }

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    array2[rowNum - i, j] = array[i, j];
                }
            }


            return array2;
        }
    }
}
