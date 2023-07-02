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

        #region Properties
        public Vec3 EulerAngles
        {
            get { return ToEulerAngles(this) * Mathf.Rad2Deg; } //Devuelve un vec3 que representa los angulos Euler de un quaternion y luego los paso de radianes a angulos
            set { this = ToQuaternion(value * Mathf.Deg2Rad); } //Seteo el valor de del quaternion de vec3 y lo paso de grados a radianes
        }

        //Normaliza el quaternion
        public Quat Normalized => Normalize(this);

        #endregion

        #region Functions

        //Nos sirve para crear un nuevo quaterion a partir de los angulos de euler. 
        public static Quat Euler(float x, float y, float z)
        {
            //Convierte los angulos de grados a radianes (Necesario porque las funciones trig en C# esperan angulos en radianes)
            float radianX = x * Mathf.Deg2Rad;
            float radianY = y * Mathf.Deg2Rad;
            float radianZ = z * Mathf.Deg2Rad;

            //Calculo los valores trig de la mitad de los angulos utilizando la formula para la conversion de angulos euler a quaterniones.
            float halfX = radianX * 0.5f;
            float halfY = radianY * 0.5f;
            float halfZ = radianZ * 0.5f;

            //Saco el seno y coseno de los angulos euler
            //El seno lo uso para los componentes imaginarios, relacionado con la magnitud y rotacion en torno a los ejes X Y Z.
            //El coseno lo uso para el componente real(W), multiplicado representa la rotacion total, escalar.
            float sinX = Mathf.Sin(halfX);
            float cosX = Mathf.Cos(halfX);
            float sinY = Mathf.Sin(halfY);
            float cosY = Mathf.Cos(halfY);
            float sinZ = Mathf.Sin(halfZ);
            float cosZ = Mathf.Cos(halfZ);

            // Calcula los componentes del cuaternion utilizando la fórmula de Euler para la conversión. (La foto definitva)
            float qx = sinX * cosY * cosZ - cosX * sinY * sinZ;
            float qy = cosX * sinY * cosZ + sinX * cosY * sinZ;
            float qz = cosX * cosY * sinZ - sinX * sinY * cosZ;
            float qw = cosX * cosY * cosZ + sinX * sinY * sinZ;

            //Creo el quaternion a partir de estos componentes.
            Quat quaternion = new Quat(qx, qy, qz, qw);

            return quaternion;
        }

        //Si bien ambas nos devuelven los mismo, la diferencia radica en los parametros que le pasamos.
        //Ya que una nos pide los parametros por separados en grados y la otra como un objeto vec3.
        public static Quat Euler(Vec3 euler)
        {
            float halfX = euler.x * 0.5f;
            float halfY = euler.y * 0.5f;
            float halfZ = euler.z * 0.5f;

            float sinX = Mathf.Sin(halfX);
            float cosX = Mathf.Cos(halfX);
            float sinY = Mathf.Sin(halfY);
            float cosY = Mathf.Cos(halfY);
            float sinZ = Mathf.Sin(halfZ);
            float cosZ = Mathf.Cos(halfZ);

            float x = sinX * cosY * cosZ + cosX * sinY * sinZ;
            float y = cosX * sinY * cosZ - sinX * cosY * sinZ;
            float z = cosX * cosY * sinZ - sinX * sinY * cosZ;
            float w = cosX * cosY * cosZ + sinX * sinY * sinZ;

            return new Quat(x, y, z, w);
        }

        private static Quat ToQuaternion(Vec3 vec3)
        {
            /*
            // Calcula los componentes del cuaternion a partir de los valores del objeto Vec3
            float x = vec3.x;
            float y = vec3.y;
            float z = vec3.z;
            float w = 0f; // El componente w se establece en 0, ya que no se proporciona en el objeto Vec3

            return new Quat(x, y, z, w);
            */

            float cz = Mathf.Cos(Mathf.Deg2Rad * vec3.z / 2);
            float sz = Mathf.Sin(Mathf.Deg2Rad * vec3.z / 2);
            float cy = Mathf.Cos(Mathf.Deg2Rad * vec3.y / 2);
            float sy = Mathf.Sin(Mathf.Deg2Rad * vec3.y / 2);
            float cx = Mathf.Cos(Mathf.Deg2Rad * vec3.x / 2);
            float sx = Mathf.Sin(Mathf.Deg2Rad * vec3.x / 2);

            Quat quat = new Quat();

            quat.w = cx * cy * cz + sx * sy * sz;
            quat.x = sx * cy * cz - cx * sy * sz;
            quat.y = cx * sy * cz + sx * cy * sz;
            quat.z = cx * cy * sz - sx * sy * cz;

            return quat;
        }

        //Esta funcion convierte un cuaterion en angulos euler
        private static Vec3 ToEulerAngles(Quat quat)
        {
            //Los paso a algo mas simple de leer
            float qw = quat.w;
            float qx = quat.x;
            float qy = quat.y;
            float qz = quat.z;

            //Arcotangente y Arcoseno
            //La formulas son usadas para convertir los componentes de un cuaternion en angulos euler
            //Se basan en la representacion de cuaternion conocida como "Euler de navegacion"
            float roll = Mathf.Atan2(2f * (qw * qx + qy * qz), 1f - 2f * (qx * qx + qy * qy)); //Angulo de rotacion alrededor del eje X
            float pitch = Mathf.Asin(2f * (qw * qy - qz * qx));                                //Angulo de rotacion alrededor del eje Y
            float yaw = Mathf.Atan2(2f * (qw * qz + qx * qy), 1f - 2f * (qy * qy + qz * qz));  //Angulo de rotacion alrededor del eje Z

            Vec3 euler = new Vec3(roll, pitch, yaw);

            return euler;
        }

        //Se encarga de calcular y devolver el quaternion inverso
        public static Quat Inverse(Quat rotation)
        {
            //Calculo el conjugado del quaternion
            Quat conjugado = new Quat(-rotation.x, -rotation.y, -rotation.z, rotation.w);

            //Lo normalizo para que la logitud no sea mayor a 1
            Quat inverse = conjugado.Normalized;

            return inverse;
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
            t = Mathf.Clamp01(t);

            return SlerpUnclamped(a, b, t);
        }

        public static Quat SlerpUnclamped(Quat a, Quat b, float t)
        {
            //Creo una instancia del quaternion
            Quat r;

            //Calculo el valor de time
            float time = 1 - t;

            float wa, wb;
            
            //Calculo el angulo entre los cuaterniones A y B
            float angle = Mathf.Acos(Dot(a, b));

            //Uso abs para obtener el valor absoluto(obtener el positivo)
            angle = Mathf.Abs(angle);

            //Calculo el valor del seno
            float sn = Mathf.Sin(angle);

            //Calculo los factores de interpolacion dividiendo el seno del angulo, osea los normalizo
            wa = Mathf.Sin(time * angle) / sn;
            wb = Mathf.Sin(t * angle) / sn;

            //Realizo la interpolacion lineal usando wa y wb, los asignamos a r
            r.x = wa * a.x + wb * b.x;
            r.y = wa * a.y + wb * b.y;
            r.z = wa * a.z + wb * b.z;
            r.w = wa * a.w + wb * b.w;

            //Normalizo r
            r.Normalize();

            return r;
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

        //Compara 2 vectores o direcciones usando el producto escalar y determina si son aproximadamente iguales.
        private static bool IsEqualUsingDot(float dot)
        {
            float dotThreshold = 0.9999f; // Valor de referencia para la igualdad

            bool isEqual = dot >= dotThreshold; // Compara el producto escalar con el umbral

            return isEqual;
        }

        //Nos devuelve el escalar
        //El producto escalar es una operacion que nos devuelve un valor numero al multiplicar los componentes correspondientes de 2 vectores y sumarlos.
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

        #endregion
    }
}
