using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefenseStats
{
    // Physical Defense
    public Stat armor;
    public Stat evasion;
    public Stat suspisionResistance;

    // Elemental Resistance
    public Stat fireResistance;
    public Stat iceResistance;
    public Stat lightningResistance;
    public Stat poisonResistance;
    public Stat holyResistance;
    public Stat darkResistance;

}
