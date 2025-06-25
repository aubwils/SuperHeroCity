using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Buff
{
    public StatType statType;
    public float value;
}

public class Object_Buff : MonoBehaviour
{
    private SpriteRenderer sr;
    private Entity_Stats entityStatsToModify;

    [Header("Buff Details")]
    [SerializeField] private Buff[] buffs;
    [SerializeField] private string buffName;
    [SerializeField] private float buffDuration = 4f;
    [SerializeField] private bool canBeUsed = true;

    // [Header("Floating Movement")]
    // [SerializeField] private float floatSpeed = 1f;
    // [SerializeField] private float floatRange = 0.1f;
    // private Vector3 startPosition;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        //startPosition = transform.position;
    }

    private void Update()
    {
        //ObjectFloat();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeUsed)
            return;

        entityStatsToModify = collision.GetComponent<Entity_Stats>();
        StartCoroutine(BuffRoutine(buffDuration));
    }

    private IEnumerator BuffRoutine(float duration)
    {
        canBeUsed = false;
        sr.color = Color.clear;
        ApplyBuff(true);

        yield return new WaitForSeconds(duration);

        ApplyBuff(false);
        Destroy(gameObject);
    }

    private void ApplyBuff(bool apply)
    {
        foreach (var buff in buffs)
        {
            if (apply)
                entityStatsToModify.GetStatByType(buff.statType).AddModifier(buff.value, buffName);
            else
                entityStatsToModify.GetStatByType(buff.statType).RemoveModifier(buffName);
        }
    }

    // private void ObjectFloat()
    // {
    //     float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
    //     //Mathf.Sin returns a value between -1 and 1, so we multiply it by floatRange to get the desired floating effect.
    //     transform.position = startPosition + new Vector3(0, yOffset);
    //     //FUTURE IDK If I will use this.... tbd. I don't think I will use it on my objects (unless i have balloons or a floating thing
    //     // I do like it but cant think of a use for it right now.
    // }
}
