using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class Ejercicios : MonoBehaviour
{
    [SerializeField] private enum Ejercicio { Uno, Dos, Tres, Cuatro, Cinco, Seis, Siete, Ocho, Nueve, Diez};
    [SerializeField] private Ejercicio ejercicio;

    [SerializeField] private Color VectorColor = Color.red;

    [SerializeField] private Vec3 A = new Vec3(0, 0, 0);
    [SerializeField] private Vec3 B = new Vec3(0, 0, 0);

    private Vec3 result = new Vec3(0, 0, 0);

    private void Start()
    {
        MathDebbuger.Vector3Debugger.AddVector(transform.position, transform.position + A, Color.black, "A");
        MathDebbuger.Vector3Debugger.AddVector(transform.position, transform.position + B, Color.white, "B");
        MathDebbuger.Vector3Debugger.AddVector(transform.position, transform.position + result, VectorColor, "result");
        MathDebbuger.Vector3Debugger.EnableEditorView();
    }

    private void Update()
    {
        switch (ejercicio)
        {
            case Ejercicio.Uno:
                result = A + B;
                break;
            case Ejercicio.Dos:
                result = B - A;
                break;
            case Ejercicio.Tres:
                result = new Vec3(A.x * B.x, A.y * B.y, A.z * B.z);
                break;
            case Ejercicio.Cuatro:
                result = -Vec3.Cross(A, B);
                break;
            case Ejercicio.Cinco:
                result = Vec3.Lerp(A, B, Time.time % 1);
                break;
            case Ejercicio.Seis:
                result = Vec3.Max(A, B);
                break;
            case Ejercicio.Siete:
                result = Vec3.Project(A, B);
                break;
            case Ejercicio.Ocho:
                result = Vec3.Distance(A, B) * (A + B).normalized;
                break;
            case Ejercicio.Nueve:
                break;
            case Ejercicio.Diez:
                result = Vec3.LerpUnclamped(B, A, Time.time);
                break;
            default:
                break;
        }

        MathDebbuger.Vector3Debugger.UpdatePosition("A", transform.position, A + transform.position);
        MathDebbuger.Vector3Debugger.UpdatePosition("B", transform.position, B + transform.position);
        MathDebbuger.Vector3Debugger.UpdatePosition("result", transform.position, result + transform.position);
    }
}
