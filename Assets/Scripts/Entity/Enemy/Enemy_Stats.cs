using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats :  Entity_Stats
{
    private Enemy_Brain enemyBrain;

    protected override void Start()
    {
        base.Start();
        enemyBrain = GetComponent<Enemy_Brain>();
    }


}