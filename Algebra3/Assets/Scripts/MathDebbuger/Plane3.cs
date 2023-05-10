using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CustomMath
{
    public struct Plane3
    {
        #region Variables

        Vec3 m_Normal;
        float m_Distance;

        //Devuelve la normal del plano
        public Vec3 normal { get { return m_Normal; } set { m_Normal = value; } }

        //Devuelve la distancia desde el plano hasta el origen
        public float distance { get { return m_Distance; } set { m_Distance = value; } }

        //Hace que el plano mire a la direccion opuesta
        public Plane3 flipped { get { return new Plane3(-m_Normal, -m_Distance); } }
        #endregion

        #region Constructors

        //Calcula la distancia desde el origen al plano utilizando la ecuación del plano
        public Plane3(Vec3 inNormal, Vec3 inPoint)
        {
            //Normalizo el vector para simplificar el calculo de distancia
            m_Normal = inNormal.normalized;

            //Utilizo para calcular el producto escalar entre el vector normal y el punto
            m_Distance = -Vec3.Dot(m_Normal, inPoint);
        }

        //Este constructor es útil cuando ya conoces el vector normal del plano y la distancia desde el origen al plano.
        public Plane3(Vec3 inNormal, float d)
        {
            m_Normal = inNormal.normalized;

            m_Distance = d;
        }

        //Este constructor es util en caso de que tengas 3 puntos
        //El vector normal y la distancia del plano se calculan automáticamente a partir de los puntos dados
        public Plane3(Vec3 a, Vec3 b, Vec3 c)
        {
            //Primero obtengo los 2 vectores que se encuentran en el plano a partir de a b c
            Vec3 v1 = b - a;
            Vec3 v2 = c - a;

            //Obtengo el vector normal del plano con el producto Cruz de los dos vectores anteriores
            m_Normal = Vec3.Cross(v1, v2).normalized;

            //Obtengo el coeficiente de distancia del plano utilizando uno de los puntos y el vector normal
            //El coeficiente de distancia se calcula como - Ax - By - Cz
            m_Distance = -Vec3.Dot(m_Normal, a);
        }
        #endregion

        #region Functions

        //Devuelve una copia del plano dado que se mueve en el espacio por el translation.
        public static Plane3 Translate(Plane3 plane, Vec3 translation)
        {
            //Obtengo el vector normal del plano
            Vec3 normal = plane.normal;

            //Obtengo la distancia del plano
            float distance = plane.distance;

            //Hace la traslacion del punto del plano
            Vec3 point = -normal * distance + translation;

            //Crea un nuevo objeto plane trasladado
            return new Plane3(normal, point);
        }

        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            throw new NotImplementedException();
        }

        //Invierto la direccion del plano
        public void Flip()
        {
            m_Normal = -m_Normal;
            m_Distance = -m_Distance;
        }

        public float GetDistanceToPoint(Vec3 point)
        {
            throw new NotImplementedException();
        }

        public bool GetSide(Vec3 point)
        {
            throw new NotImplementedException();
        }

        public bool SameSide(Vec3 inPt0, Vec3 inPt1)
        {
            throw new NotImplementedException();
        }

        //La misma funcionalidad que constructor de plane
        public void Set3Point(Vec3 a, Vec3 b, Vec3 c)
        {
            Vec3 v1 = b - a;
            Vec3 v2 = c - a;

            m_Normal = Vec3.Cross(v1, v2).normalized;

            m_Distance = -Vec3.Dot(m_Normal, a);
        }

        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            throw new NotImplementedException();
        }

        public void Translate(Vec3 translation)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
