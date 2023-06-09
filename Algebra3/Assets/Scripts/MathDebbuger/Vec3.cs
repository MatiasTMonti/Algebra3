﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CustomMath
{
    [System.Serializable]
    public struct Vec3 : IEquatable<Vec3>
    {
        //https://github.com/Unity-Technologies/UnityCsReference/blob/master/Runtime/Export/Math/Vector3.cs

        #region Variables
        public float x;
        public float y;
        public float z;

        public float sqrMagnitude { get { return SqrMagnitude(new Vec3(x, y, z)); } }

        //https://answers.unity.com/questions/1754484/what-is-the-difference-between-normalize-normalize.html
        public Vec3 normalized { get { return new Vec3(x / Mathf.Sqrt(x * x + y * y + z * z), y / Mathf.Sqrt(x * x + y * y + z * z), z / Mathf.Sqrt(x * x + y * y + z * z)); } }
        public float magnitude { get { return Magnitude(new Vec3(x, y, z)); } }
        #endregion

        #region constants
        public const float epsilon = 1e-05f;
        #endregion

        #region Default Values
        public static Vec3 Zero { get { return new Vec3(0.0f, 0.0f, 0.0f); } }
        public static Vec3 One { get { return new Vec3(1.0f, 1.0f, 1.0f); } }
        public static Vec3 Forward { get { return new Vec3(0.0f, 0.0f, 1.0f); } }
        public static Vec3 Back { get { return new Vec3(0.0f, 0.0f, -1.0f); } }
        public static Vec3 Right { get { return new Vec3(1.0f, 0.0f, 0.0f); } }
        public static Vec3 Left { get { return new Vec3(-1.0f, 0.0f, 0.0f); } }
        public static Vec3 Up { get { return new Vec3(0.0f, 1.0f, 0.0f); } }
        public static Vec3 Down { get { return new Vec3(0.0f, -1.0f, 0.0f); } }
        public static Vec3 PositiveInfinity { get { return new Vec3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); } }
        public static Vec3 NegativeInfinity { get { return new Vec3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); } }
        #endregion                                                                                                                                                                               

        #region Constructors
        public Vec3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec3(Vec3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector2 v2)
        {
            this.x = v2.x;
            this.y = v2.y;
            this.z = 0.0f;
        }
        #endregion

        #region Operators
        public static bool operator ==(Vec3 left, Vec3 right)
        {
            float diff_x = left.x - right.x;
            float diff_y = left.y - right.y;
            float diff_z = left.z - right.z;
            float sqrmag = diff_x * diff_x + diff_y * diff_y + diff_z * diff_z;
            return sqrmag < epsilon * epsilon;
        }
        public static bool operator !=(Vec3 left, Vec3 right)
        {
            return !(left == right);
        }

        public static Vec3 operator +(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x + rightV3.x, leftV3.y + rightV3.y, leftV3.z + rightV3.z);
        }
        //X - X
        public static Vec3 operator -(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x - rightV3.x, leftV3.y - rightV3.y, leftV3.z - rightV3.z);
        }
        //Numero negativo
        public static Vec3 operator -(Vec3 v3)
        {
            return new Vec3(-v3.x, -v3.y, -v3.z);
        }
        //Numero * Scalar
        public static Vec3 operator *(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }
        //Scalar * Numero
        public static Vec3 operator *(float scalar, Vec3 v3)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }
        //Division comun
        public static Vec3 operator /(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x / scalar, v3.y / scalar, v3.z / scalar);
        }

        public static implicit operator Vector3(Vec3 v3)
        {
            return new Vector3(v3.x, v3.y, v3.z);
        }
        //Lo mismo que vec3, pero con 2
        public static implicit operator Vector2(Vec3 v2)
        {
            return new Vector2(v2.x, v2.y);
        }
        #endregion

        #region Functions
        public override string ToString()
        {
            return "X = " + x.ToString() + "   Y = " + y.ToString() + "   Z = " + z.ToString();
        }
        //https://answers.unity.com/questions/317648/angle-between-two-vectors.html

        /*
        Se normalizan lo vectores para que sea 1.
        Hace producto punto de los vectores normalizados.
        Ambos vectores son de 1, por lo tanto el resultado es el coseno del angulo.
        Clampeo para evitar errores, ya que acos acepta valores entre -1 y 1.
        Acos calcula el coseno "inverso" en radianes.
        El numero hardcodeado es 180f / PI, para convertir de radianes a grados.
        Esto devuelve un valor entre 0 y 180, ya que 180 es el angulo mas grande posible entre 2 vectores
        */
        public static float Angle(Vec3 from, Vec3 to)
        {
            return Mathf.Acos(Mathf.Clamp(Vec3.Dot(new Vec3(from.normalized.x, from.normalized.y, from.normalized.z), new Vec3(to.normalized.x, to.normalized.y, to.normalized.z)), -1f, 1f)) * 57.29578f;
        }

        //https://forum.unity.com/threads/what-exactly-is-clampmagnitude.336021/
        //Lo camplea hasta la longitud deseada
        public static Vec3 ClampMagnitude(Vec3 vector, float maxLength)
        {
            return new Vec3(vector / Magnitude(vector) * maxLength);
        }

        // v /→  =  √vx2 + vy2 + vz2.
        // Obtiene la distancia exacta entre 2 objetos
        //https://forum.unity.com/threads/sqrmagnitude-or-magnitude.80443/
        public static float Magnitude(Vec3 vector)
        {
            return Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2));
        }
        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            return new Vec3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }
        public static float Distance(Vec3 a, Vec3 b)
        {
            return Mathf.Sqrt(Mathf.Pow((b.x - a.x), 2) + Mathf.Pow((b.y - a.y), 2) + Mathf.Pow((b.z - a.z), 2));
        }
        //Al combinar 2 vectores me devuelve un escalar
        public static float Dot(Vec3 a, Vec3 b)
        {
            return ((a.x * b.x) + (a.y * b.y) + (a.z * b.z));
        }

        //https://medium.com/swlh/youre-using-lerp-wrong-73579052a3c3#:~:text=The%20Lerp%20function%2C%20mathematically%2C%20is,simpler%20than%20the%20Vector3%20version.
        public static Vec3 Lerp(Vec3 a, Vec3 b, float t)
        {
            t = Mathf.Clamp01(t); //Multiplica el valor siempre por 1, lo que hace que no vaya a mas del valor de final
            return new Vec3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        //Lo mismo que el Lerp pero sin clampear el maximo
        public static Vec3 LerpUnclamped(Vec3 a, Vec3 b, float t)
        {
            return new Vec3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        //Gracias a la funcion mathf.max/min puedo determinar cual de los 2 valores es mas grande
        //y gracias a esto hacer el conjunto de vector mas grande que se forme
        //https://docs.unity3d.com/ScriptReference/Vector3.Max.html
        public static Vec3 Max(Vec3 a, Vec3 b)
        {
            return new Vec3(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y), Mathf.Max(a.z, b.z));
        }
        public static Vec3 Min(Vec3 a, Vec3 b)
        {
            return new Vec3(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Min(a.z, b.z));
        }

        //https://docs.unity3d.com/ScriptReference/Vector3-magnitude.html
        //Para comprobar la distancia usar esta misma y elevar al cuadrado el resultado
        //asi usas menos recursos
        public static float SqrMagnitude(Vec3 vector)
        {
            return Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2) + Mathf.Pow(vector.z, 2);
        }
        //primer argumento vector que se desea proyectar, segundo parametro vector sobre el cual se va a proyectar el primero
        public static Vec3 Project(Vec3 vector, Vec3 onNormal)
        {
            return new Vec3(Dot(vector, onNormal) / onNormal.sqrMagnitude * onNormal);
        }
        //La innormal es el plano, la indirection es una flecha direccional que llega al plano, esto nos devuelve un vector de igual
        //magnitud pero con su direccion reflejada
        //Estamos proyectando dos veces para "atras", nos da el reflejo
        //primer parametro, vector a reflejar, segundo parametro vector normal que respresenta donde se va a reflejar
        //https://docs.unity3d.com/ScriptReference/Vector3.Reflect.html
        public static Vec3 Reflect(Vec3 inDirection, Vec3 inNormal)
        {
            return new Vec3(-2F * Dot(inNormal, inDirection) * inNormal + inDirection);
        }
        public void Set(float newX, float newY, float newZ)
        {
            this = new Vec3(newX, newY, newZ);
        }
        public void Scale(Vec3 scale)
        {
            x *= scale.x;
            y *= scale.y;
            z *= scale.z;
        }
        public void Normalize()
        {
            float mag = this.magnitude;
            this.x /= mag;
            this.y /= mag;
            this.z /= mag;
        }
        #endregion

        #region Internals
        public override bool Equals(object other)
        {
            if (!(other is Vec3)) return false;
            return Equals((Vec3)other);
        }

        public bool Equals(Vec3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }
        #endregion
    }
}