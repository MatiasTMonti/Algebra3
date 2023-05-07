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
        public Plane3(Vec3 inNormal, Vec3 inPoint)
        {
            throw new NotImplementedException();
        }
        public Plane3(Vec3 inNormal, float d)
        {
            throw new NotImplementedException();
        }
        public Plane3(Vec3 a, Vec3 b, Vec3 c)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Functions
        public static Plane3 Translate(Plane3 plane, Vec3 translation)
        {
            return new Plane3(plane.m_Normal, -plane.m_Normal * plane.m_Distance + translation);
        }

        public Vec3 ClosestPointOnPlane(Vec3 point)
        {
            throw new NotImplementedException();
        }

        public void Flip()
        {
            throw new NotImplementedException();
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

        public void Set3Point(Vec3 a, Vec3 b, Vec3 c)
        {

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
