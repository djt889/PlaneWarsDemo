using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyL1 : Enemy
{
    private new void Start()
    {
        Init(100, 10, 2f);
        base.Start();
        
    }
}

