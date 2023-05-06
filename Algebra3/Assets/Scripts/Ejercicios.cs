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
        
    }

    private void Update()
    {
        switch (ejercicio)
        {
            case Ejercicio.Uno:
                break;
            case Ejercicio.Dos:
                break;
            case Ejercicio.Tres:
                break;
            case Ejercicio.Cuatro:
                break;
            case Ejercicio.Cinco:
                break;
            case Ejercicio.Seis:
                break;
            case Ejercicio.Siete:
                break;
            case Ejercicio.Ocho:
                break;
            case Ejercicio.Nueve:
                break;
            case Ejercicio.Diez:
                break;
            default:
                break;
        }
    }
}
