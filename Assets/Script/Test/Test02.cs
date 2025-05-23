using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test02 : MonoBehaviour
{
    // Start is called before the first frame update
    public RaySixDirCollision RaySixDirCollision;
    
    void Start()
    {
        RaySixDirCollision = new RaySixDirCollision(~(1 << 6));
        RaySixDirCollision.AddRayLayer(Vector3.zero,5,Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        RaySixDirCollision.RaySixDirCollisionUpdate(transform);
    }
}
