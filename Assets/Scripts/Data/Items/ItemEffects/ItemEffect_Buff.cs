using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Effect data - Buff ", menuName = "Game Setup/Item Data/Item Effect/Buff Effect")]
public class ItemEffect_Buff : ItemEffect_DataSO
{
    [SerializeField] private BuffEffectData[] buffsToApply;
    [SerializeField] private float duration;
    [SerializeField] private string source = Guid.NewGuid().ToString();

    Player_Stats playerStats;

    public override bool CanBeUsed()
    {
        if (playerStats == null)
            playerStats = FindAnyObjectByType<Player_Stats>();

        if (playerStats.CanApplyBuffOf(source))
        {
            return true;
        }
        else
        {
            Debug.Log("Same buff effect cannot be applied twice!");
            return false;
        }
        // Will this let me update the buff duration to the new one if longer than current? 


    }

    public override void ExecuteEffect()
    {
        playerStats.ApplyBuff(buffsToApply, duration, source);
    }
}
