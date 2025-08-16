using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemModifier
{
    public StatType statType; // which stat to affect
    public float value;       // flat amount (treat as % in UI if that stat is percentage-based)

}