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
        #endregion

        #region Properties
        private static readonly Matrix4x4 Zero = ;

        private static readonly Matrix4x4 Identity = ;

        public Vec3 lossyScale
        {

        }
        private Vec3 GetLossyScale()
        {
            
        }

        public Quat rotation;

        private Quat GetRotation()
        {
            
        }

        public float this[int index]
        {
            
        }

        public float this[int row, int column]
        {
            
        }

        public Matrix4x4 transpose;

        public static Matrix4x4 Transpose(Matrix4x4 m)
        {
            
        }


        public Matrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
        {
            
        }
        #endregion

        #region Operators
        public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            
        }

        public static Vector4 operator *(Matrix4x4 lhs, Vector4 vector)
        {
            
        }

        public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            
        }

        public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs);
        #endregion

        #region Functions
        public Vector4 GetColumn(int number)
        {
            
        }



        public static Matrix4x4 Rotate(Quat q)
        {
            
        }

        public static Matrix4x4 Scale(Vec3 vector)
        {
            
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
