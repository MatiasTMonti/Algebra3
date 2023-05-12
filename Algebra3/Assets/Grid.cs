using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private Vector3[,,] grid = new Vector3[100, 100, 100];

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for(int z = 0; z < grid.GetLength(2); z++)
                {
                    grid[x, y, z] = new Vector3(x, y, z);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int z = 0; z < grid.GetLength(2); z++)
                {
                    Gizmos.DrawSphere(grid[x, y, z], 0.1f);
                }
            }
        }
    }
}
