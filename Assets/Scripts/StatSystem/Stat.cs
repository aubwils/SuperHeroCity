using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private float baseValue;
    [SerializeField] private List<StatModifier> modifiers = new List<StatModifier>();

    private bool wasModifiedNeedstoBeRecalculated = true;
    private float finalValue;

    public float GetValue()
    {
        if (wasModifiedNeedstoBeRecalculated)
        {
            finalValue = GetFinalValue();
            wasModifiedNeedstoBeRecalculated = false;
        }

        return finalValue;
        // This method checks if the modifiers need to be recalculated. If they do, it calculates the final value by summing the base value and all the modifiers' values, and then returns the final value.
        // If the modifiers do not need to be recalculated, it simply returns the final value this is so we do not have to recalculate the final value every time we call GetValue
        // which if we had a lot of enemies or objects with stats it would be very expensive this is more efficient as we only recalculate the final value when we add or remove a modifier or when we first call GetValue
    }

    public void AddModifier(float value, string source)
    {
        StatModifier modifierToAdd = new StatModifier(value, source);
        modifiers.Add(modifierToAdd);
        wasModifiedNeedstoBeRecalculated = true;
    }

    public void RemoveModifier(string source)
    {
        modifiers.RemoveAll(modifier => modifier.source == source);
        // this is similar to a for each loop that checks each modifier
        // we are saying the list of modifiers remove all modifiers where the source matches the source we want to remove
        // using a name to get access to the modifier elemtents can be named anything and then use +> expression to get access to the element and we will get the source of it
        // then we compaire the souce of the modifier to the source we want to remove and if it matches we remove it
    }

    private float GetFinalValue()
    {
        finalValue = baseValue;
        foreach (var modifier in modifiers)
        {
            finalValue = finalValue + modifier.value;
        }
        return finalValue;
    }

}

[Serializable]
public class StatModifier
{
    public float value;
    public string source;

    public StatModifier(float value, string source)
    {
        this.value = value;
        this.source = source;
    }
}
