using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class teste : MonoBehaviour
{
    private Vec3 endPos = new Vec3(5, -2, 0);
    private Vec3 startPos;
    private float desireDur = 3f;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vec3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentage = elapsedTime / desireDur;

        transform.position = Vec3.Lerp(startPos, endPos, percentage);
    }
}
