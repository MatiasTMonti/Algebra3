using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomMath
{
    public struct Quat
    {

        public float x;
        public float y;
        public float z;
        public float w;

        public const float kEpsilon = 1E-06F;

        public static Quat Identity
        {
            get { return new Quat(0, 0, 0, 1); }
        }

        public Quat(float x, float y, float z, float w)
        {
            
        }

        public static bool operator ==(Quat lhs, Quat rhs)
        {

        }

        public static bool operator !=(Quat lhs, Quat rhs)
        {

        };

        public static Quat operator *(Quat lhs, Quat rhs)
        {
            
        }

        public static Vec3 operator *(Quat rotation, Vec3 point)
        {
            
        }

        public static implicit operator Quaternion(Quat quaternion)
        {
            
        }

        public static implicit operator Quat(Quaternion quaternion)
        {
            
        }

        public Vec3 EulerAngles
        {
            
        }

        public Quat Normalized
        {

        }

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
            
        }

        public void Normalize()
        {
            
        }

        public static Quat Lerp(Quat a, Quat b, float t)
        {
            
        }

        public static Quat LerpUnclamped(Quat a, Quat b, float t)
        {
            
        }

        public static Quat Slerp(Quat a, Quat b, float t)
        {

        }


        public static Quat SlerpUnclamped(Quat a, Quat b, float t)
        {
            
        }

        public static float Angle(Quat a, Quat b)
        {
            
        }

        private static bool IsEqualUsingDot(float dot)
        {
            
        }

        public static float Dot(Quat a, Quat b)
        {
            
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

        public override bool Equals(object other)
        {
            
        }

        public bool Equals(Quat other)
        {
            
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2) ^ (w.GetHashCode() >> 1);
        }
    }
}
