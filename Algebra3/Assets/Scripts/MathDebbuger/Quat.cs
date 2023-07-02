using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomMath
{
    public struct Quat
    {
        #region variables
        public float x;
        public float y;
        public float z;
        public float w;

        public const float kEpsilon = 1E-06F;

        //Crea un objeto y se inicializa en el 0 0 0 1, donde los 0 son la parte imaginaria y el 1 la real
        public static Quat Identity
        {
            get { return new Quat(0, 0, 0, 1); }
        }

        //Seteo de variables
        public Quat(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        #endregion

        #region Operators
        public static bool operator ==(Quat lhs, Quat rhs)
        {
            return lhs.x == rhs.x && lhs.w == rhs.w && lhs.z == rhs.z && lhs.w == rhs.w;
        }

        public static bool operator !=(Quat lhs, Quat rhs)
        {
            return !(lhs == rhs);
        }

        public static Quat operator *(Quat lhs, Quat rhs)
        {
            float newX = lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y;
            float newY = lhs.w * rhs.y - lhs.x * rhs.z + lhs.y * rhs.w + lhs.z * rhs.x;
            float newZ = lhs.w * rhs.z + lhs.x * rhs.y - lhs.y * rhs.x + lhs.z * rhs.w;
            float newW = lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z;

            return new Quat(newX, newY, newZ, newW);
        }

        //Este operador se utiliza para aplicar una rotación representada por el cuaternión a un punto/vector en el espacio tridimensional
        public static Vec3 operator *(Quat rotation, Vec3 point)
        {
            //Creo un nuevo quaternion agregando el componente W = 0, a partir de un vector3
            Quat pointQuat = new Quat(point.x, point.y, point.z, 0);

            //Hago la multiplicacion de quaterniones para aplicar la rotacion
            Quat resultQuat = rotation * pointQuat;

            //Retorno la multiplicacion del vector
            return new Vec3(resultQuat.x, resultQuat.y, resultQuat.z);
        }

        //Este operador permite convertir un objeto Quat a un objeto Quaternion de otro tipo o clase
        //La palabra implicita nos dice que el compilador lo hara automaticamente sin necesidad de hacerlo nosotros
        public static implicit operator Quaternion(Quat quaternion)
        {
            return new Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }

        //Es lo mismo pero de un objeto Quaternion a Quat
        public static implicit operator Quat(Quaternion quaternion)
        {
            return new Quat(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }

        #endregion

        public Vec3 EulerAngles
        {

        }

        //Normaliza el quaternion
        public Quat Normalized => Normalize(this);

        public static Quat Euler(float x, float y, float z)
        {

        }

        public static Quat Euler(Vec3 euler)
        {

        }

        private static Quat ToQuaternion(Vec3 vec3)
        {

        }

        private static Vec3 ToEulerAngles(Quat quat)
        {

        }


        public static Quat Inverse(Quat rotation)
        {

        }

        public static Quat Normalize(Quat quat)
        {
            //Saco la magnitud
            float magnitud = Mathf.Sqrt(quat.x * quat.x + quat.y * quat.y + quat.z * quat.z + quat.w * quat.w);

            //Compruebo que su magnitud sea mayor que 0
            if (magnitud == 0)
                return quat;

            //Calculo el inverso de la magnitud
            float invMagnitud = 1f / magnitud;

            //Multiplico cada componente para normalizarlo
            return new Quat(quat.x * invMagnitud, quat.y * invMagnitud, quat.z * invMagnitud, quat.w * invMagnitud);
        }

        public void Normalize()
        {
            //Saco la magnitud
            float magnitude = Mathf.Sqrt(x * x + y * y + z * z + w * w);

            //Compruebo que su magnitud sea mayor que 0
            if (magnitude == 0)
                return;

            //Calculo el inverso de la magnitud
            float invMagnitud = 1f / magnitude;


            //Multiplico cada componente para normalizarlo
            x *= invMagnitud;
            y *= invMagnitud;
            z *= invMagnitud;
            w *= invMagnitud;
        }

        public static Quat Lerp(Quat a, Quat b, float t)
        {
            //Campleo t entre 0 y 1
            t = Mathf.Clamp01(t);

            //Utilizo LerpUnclamped ya que hace lo mismo con la diferencia de que clampea
            return LerpUnclamped(a, b, t);
        }

        //Permite realizar una interpolación lineal sin restricciones entre los cuaterniones a y b con un parámetro de interpolación t
        public static Quat LerpUnclamped(Quat a, Quat b, float t)
        {
            //Saco el inverso de t
            //Reduce gradualmente la influencia de A a medida que T aumenta y la influencia de B se incrementa proporcionalmente
            float invT = 1f - t;

            Quat result = new Quat(
                invT * a.x + t * b.x,
                invT * a.y + t * b.y,
                invT * a.z + t * b.z,
                invT * a.w + t * b.w
                );

            result.Normalize();

            return result;
        }

        public static Quat Slerp(Quat a, Quat b, float t)
        {

        }


        public static Quat SlerpUnclamped(Quat a, Quat b, float t)
        {

        }


        //Nos permite obtener la diferencia de orientacion en grados entre 2 quaterniones
        public static float Angle(Quat a, Quat b)
        {
            //Normalizo los quaterniones
            Quat normalizedA = a.Normalized;
            Quat normalizedB = b.Normalized;

            float dotProduct = Quat.Dot(normalizedA, normalizedB);

            //Mantengo el valor del escalar entre -1 y 1
            dotProduct = Mathf.Clamp(dotProduct, -1, 1);

            //Calculo el angulo en radianes
            //Me devuelve el arcoseno
            //Me devuelve radianes
            float angleRadians = Mathf.Acos(dotProduct);

            //Lo convierto de radianes a grados;
            float angleToDegrees = angleRadians * Mathf.Rad2Deg;

            return angleToDegrees;
        }

        private static bool IsEqualUsingDot(float dot)
        {

        }

        //Nos devuelve el escalar
        public static float Dot(Quat a, Quat b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static Quat AngleAxis(float angle, Vec3 axis)
        {

        }

        public static Quat FromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {

        }

        public static Quat LookRotation(Vec3 forward, Vec3 upwards)
        {

        }

        public static Quat LookRotation(Vec3 forward)
        {

        }

        public static Quat RotateTowards(Quat from, Quat to, float maxDegreesDelta)
        {

        }

        //La diferencia radica en que una de las dos nos permite comparar objetos de forma mas segura, teniendo en cuenta que es de tipo QUAT
        public override bool Equals(object other)
        {
            //is nos permite verificar si un objeto es del tipo especificado.
            if (other == null || other is Quat)
                return false;

            //1. Realizo una conversion explicita del parametro other a QUAT
            return !Equals((Quat)other);
        }

        //Esta funcion compara si 2 objetos de tipo quaternion son iguales, lo cual pregunta por this y other.
        public bool Equals(Quat other)
        {
            if (other == null)
                return false;

            //Si todos los componentes son iguales, devuelve true, si no false.
            return this.x == other.x && this.y == other.y && this.z == other.z && this.w == other.w;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2) ^ (w.GetHashCode() >> 1);
        }
    }
}
