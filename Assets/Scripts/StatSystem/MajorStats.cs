using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MajorStats
{
    [Header("Major Stats for Player Only, Leave Blank on Enemies")]
    [Tooltip("1 Physical Damage, .5% Crit Power per point")]
    public Stat strength;

    [Tooltip(".5% Evasion, .3% Crit Chance per point")]
    public Stat dexterity;

    [Tooltip("1 Magical Damage, .5% Elemental resistance per point")]
    public Stat intelligence;

    [Tooltip("5 Max Health, 1 Armor per point")]
    public Stat constitution;  
}
