using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomMath
{
    public struct Matrix4x4
    {
        #region Variables
        public float m00;
        public float m10;
        public float m20;
        public float m30;

        public float m01;
        public float m11;
        public float m21;
        public float m31;

        public float m02;
        public float m12;
        public float m22;
        public float m32;

        public float m03;
        public float m13;
        public float m23;
        public float m33;
        
        //Inicializo una matriz en 0
        private static readonly Matrix4x4 Zero = new Matrix4x4(new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 0));

        //Los elementos diagonales representan la escala y rotacion de identidad
        private static readonly Matrix4x4 Identity = new Matrix4x4(new Vector4(1f, 0, 0, 0), new Vector4(0, 1f, 0, 0), new Vector4(0, 0, 1f, 0), new Vector4(0, 0, 0, 1f));
        #endregion

        #region Properties
        public Vec3 lossyScale => GetLossyScale();

        //Tiene como proposito calcular y devolver la escala "lossyScale"
        //La escala lossyScale se refiere a la escala resultante de aplicar las transformaciones
        private Vec3 GetLossyScale()
        {
            //Representan los valores en diagonal de la matriz (X Y Z)
            return new Vec3(m00, m11, m22);
        }

        public Quat rotation => GetRotation();

        private Quat GetRotation()
        {
            //Se calcula la suma de estos 3 componentes (X Y Z diagonales)
            float trace = m00 + m11 + m22;
            float w, x, y, z;

            //Rotacion general
            if (trace > 0)
            {
                //s = valor inverso multiplicativo de 2 * Raiz cuadrada del real + X Y Z
                float s = 0.5f / Mathf.Sqrt(trace + 1);

                //Calculo los componentes del cuaternion de rotacion
                w = 0.25f / s;     //Esto se hace para normalizar, se divide por el factor escala
                x = (m21 - m12) * s;
                y = (m02 - m20) * s;
                z = (m10 - m01) * s;
            }
            else if (m00 > m11 && m00 > m22) //Se hace para saber que componente de la matriz tiene el valor mas alto
            {
                float s = 2 * Mathf.Sqrt(1 + m00 - m11 - m22);
                w = (m21 - m12) / s;
                x = 0.25f * s;
                y = (m01 + m10) / s;
                z = (m02 + m20) / s;
            }
            else if (m11 > m22)
            {
                float s = 2 * Mathf.Sqrt(1 + m11 - m00 - m22);
                w = (m02 - m20) / s;
                x = (m01 + m10) / s;
                y = 0.25f * s;
                z = (m12 + m21) / s;
            }
            else
            {
                float s = 2 * Mathf.Sqrt(1 + m22 - m00 - m11);
                w = (m10 - m01) / s;
                x = (m02 + m20) / s;
                y = (m12 + m21) / s;
                z = 0.25f * s;
            }

            return new Quaternion(x, y, z, w);
        }

        //Indexador nos permite acceder como si fuera un array
        public float this[int index]
        {
            get
            {
                return index switch
                {
                    0 => m00,
                    1 => m10,
                    2 => m20,
                    3 => m30,
                    4 => m01,
                    5 => m11,
                    6 => m21,
                    7 => m31,
                    8 => m02,
                    9 => m12,
                    10 => m22,
                    11 => m32,
                    12 => m03,
                    13 => m13,
                    14 => m23,
                    15 => m33,
                    _ => throw new IndexOutOfRangeException("Invalid matrix index!"),
                };
            }
            set
            {
                switch (index)
                {
                    case 0:
                        m00 = value;
                        break;
                    case 1:
                        m10 = value;
                        break;
                    case 2:
                        m20 = value;
                        break;
                    case 3:
                        m30 = value;
                        break;
                    case 4:
                        m01 = value;
                        break;
                    case 5:
                        m11 = value;
                        break;
                    case 6:
                        m21 = value;
                        break;
                    case 7:
                        m31 = value;
                        break;
                    case 8:
                        m02 = value;
                        break;
                    case 9:
                        m12 = value;
                        break;
                    case 10:
                        m22 = value;
                        break;
                    case 11:
                        m32 = value;
                        break;
                    case 12:
                        m03 = value;
                        break;
                    case 13:
                        m13 = value;
                        break;
                    case 14:
                        m23 = value;
                        break;
                    case 15:
                        m33 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
        }

        public float this[int row, int column]
        {
            get
            {
                return this[row + column * 4];
            }
            set
            {
                this[row + column * 4] = value;
            }
        }

        public Matrix4x4 transpose => Transpose(this);

        /*Crea una matriz donde se intercambian las filas y columnas, nos sirve para transformacion o calculos de orientacion.
          Si bien cambia todo, las bases se siguen respetando es decir.
          Original
        
         m00  m01  m02  m03
         m10  m11  m12  m13
         m20  m21  m22  m23
         m30  m31  m32  m33
         

          Traspuesta
        
         m00  m10  m20  m30
         m01  m11  m21  m31
         m02  m12  m22  m32
         m03  m13  m23  m33
         */
        public static Matrix4x4 Transpose(Matrix4x4 m)
        {
            //Creo una instancia de una matriz para almacenar la traspuesta
            Matrix4x4 result = new Matrix4x4();

            //Bucle anidado para recorrer filas y columnas
            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    result[column, row] = m[row, column]; //En cada iteracion asigno i j de la matriz original a j i de la traspuesta
                }
            }

            return result;
        }

        //Se utiliza para crear una matriz a partir de 4 vectores de columna
        public Matrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
        {
            m00 = column0.x;
            m01 = column1.x;
            m02 = column2.x;
            m03 = column3.x;

            m10 = column0.y;
            m11 = column1.y;
            m12 = column2.y;
            m13 = column3.y;

            m20 = column0.z;
            m21 = column1.z;
            m22 = column2.z;
            m23 = column3.z;

            m30 = column0.w;
            m31 = column1.w;
            m32 = column2.w;
            m33 = column3.w;
        }
        #endregion

        #region Operators
        public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            Matrix4x4 result = new Matrix4x4();

            //Multiplico todas las filas por todas las columnas :S
            result.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
            result.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
            result.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
            result.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;

            result.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
            result.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
            result.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
            result.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;

            result.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
            result.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
            result.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
            result.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;

            result.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
            result.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
            result.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
            result.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;

            return result;
        }

        //La diferencia entre los 2 es que uno multiplica una matriz x matriz y el otro matriz x vector
        //En resumidas cuentas uno nos devuelve una matrix4x4 y el otro un vector4D
        public static Vector4 operator *(Matrix4x4 lhs, Vector4 vector)
        {
            float x = lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z + lhs.m03 * vector.w;
            float y = lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z + lhs.m13 * vector.w;
            float z = lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z + lhs.m23 * vector.w;
            float w = lhs.m30 * vector.x + lhs.m31 * vector.y + lhs.m32 * vector.z + lhs.m33 * vector.w;

            return new Vector4(x, y, z, w);
        }

        public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            //Pregunto en cada vuelta si son iguales o no, en caso de que no retorno falso
            for (int i = 0; i < 16; i++)
            {
                if (lhs[i] != rhs[i])
                    return false;
            }

            return true;
        }

        //Si las matrices no son iguales entonces retorno true
        public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs) => !(lhs == rhs);
        #endregion

        #region Functions

        //Retorna el valor de una columna en especifico
        public Vector4 GetColumn(int number)
        {
            //Compruebo que este dentro del rango de columnas
            if (number < 0 || number >= 4)
            {
                throw new IndexOutOfRangeException("Invalid column number!");
            }

            //Creo un nuevo vector usando los index correspondientes
            //Los numeros X, hacen referencia al desplasamiento de cada posicion
            //Por ejemplo para number = 0 + X (m00, m01, m02, m03)
            //                 number = 1 + X (m10, m11, m12, m13)
            return new Vector4(this[number], this[number + 4], this[number + 8], this[number + 12]);
        }

        public static Matrix4x4 Rotate(Quat q)
        {
            
        }

        //Esta funcion se utiliza para crear una matriz de escala
        public static Matrix4x4 Scale(Vec3 vector)
        {
            //Se crea una instancia de matriz
            Matrix4x4 result = new Matrix4x4();

            //Se asignan a los componentes X Y Z respecivamente, ya que cada uno representa una escala
            result.m00 = vector.x;
            result.m11 = vector.y;
            result.m22 = vector.z;
            result.m33 = 1f; //Se le asigna 1 ya que no hay cambio en su escala

            return result;
        }

        public static Matrix4x4 Translate(Vec3 vector)
        {
            
        }

        public static Matrix4x4 TRS(Vec3 translation, Quat rotation, Vec3 scale)
        {
            
        }
        #endregion
    }
}
