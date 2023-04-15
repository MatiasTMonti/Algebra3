using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class teste : MonoBehaviour
{
    public Transform p1;
    public Transform p2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(p1.position, p1.forward * 4, Color.red);
        Debug.DrawRay(p2.position, p2.forward * 4, Color.red);
        Debug.Log(Vec3.Angle(new Vec3(p1.forward.x, p1.forward.y, p1.forward.z), new Vec3(p2.forward.x, p2.forward.y, p2.forward.z)));
    }
}
