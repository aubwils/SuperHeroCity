using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTriggers : MonoBehaviour
{
    private EnemyBrain enemyBrain => GetComponentInParent<EnemyBrain>();

    private void AnimationTrigger()
    {
        enemyBrain.AnimationTrigger();
    }

}
