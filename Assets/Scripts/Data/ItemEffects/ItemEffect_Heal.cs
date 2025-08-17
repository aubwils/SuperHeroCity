using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Effect data - Heal ", menuName = "Game Setup/Item Data/Item Effect/Heal Effect")]
public class ItemEffect_Heal : ItemEffect_DataSO
{
    [SerializeField] private float healPercent = .1f;

    public override void ExecuteEffect()
    {
        Player_Brain playerBrain = FindFirstObjectByType<Player_Brain>();

        float healAmount = playerBrain.entityStats.GetMaxHealth() * healPercent;

        playerBrain.health.IncreaseHealth(healAmount);

    }

}
