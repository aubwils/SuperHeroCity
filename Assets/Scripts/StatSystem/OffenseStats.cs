using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OffenseStats
{
    //Physical Damage
    public Stat damage;
    public Stat critPower;
    public Stat critChance;

    // Elemental Damage
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
    public Stat poisonDamage;
    public Stat holyDamage;
    public Stat darkDamage;
}
