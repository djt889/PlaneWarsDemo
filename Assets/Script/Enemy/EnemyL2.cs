using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyL2 : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        Init(100, 10, 2f);
        base.Start();
    }

    // Update is called once per frame
    public override void Shoot()
    {
        
        base.Shoot();
    }
}
