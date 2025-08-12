using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : Entity_Stats
{
    private Player_Brain playerBrain;
    private List<string> activeBuff = new List<string>();
    private Inventory_Player playerInventory;

    protected override void Awake()
    {
        base.Awake();
        playerInventory = GetComponent<Inventory_Player>();
    }

    protected override void Start()
    {
        base.Start();
        playerBrain = GetComponent<Player_Brain>();
    }

    public bool CanApplyBuffOf(string source)
    {
        return activeBuff.Contains(source) == false;
    }

    public void ApplyBuff(BuffEffectData[] buffsToApply, float duration, string source)
    {
        StartCoroutine(BuffRoutine(buffsToApply, duration, source));
    }

    private IEnumerator BuffRoutine(BuffEffectData[] buffsToApply, float duration, string source)
    {
        activeBuff.Add(source);

        foreach (var buff in buffsToApply)
        {
            GetStatByType(buff.statType).AddModifier(buff.value, source);
        }

        yield return new WaitForSeconds(duration);

        foreach (var buff in buffsToApply)
        {
            GetStatByType(buff.statType).RemoveModifier(source);
        }

        playerInventory.TriggerUpdateUI();
        activeBuff.Remove(source);
    }

}