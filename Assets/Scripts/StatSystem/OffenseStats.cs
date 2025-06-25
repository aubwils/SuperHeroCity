using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OffenseStats
{

     public Stat attackSpeed;
    //Physical Damage
    public Stat damage;
    public Stat critPower;
    public Stat critChance;
    public Stat armorReduction;

    // Elemental Damage
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;
    public Stat poisonDamage;
    public Stat holyDamage;
    public Stat darkDamage;
}
