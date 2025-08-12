using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Object_Buff : MonoBehaviour
{
    private Player_Stats playerStatsToModify;

    [Header("Buff Details")]
    [SerializeField] private BuffEffectData[] buffs;
    [SerializeField] private string buffName;
    [SerializeField] private float buffDuration = 4f;

    // [Header("Floating Movement")]
    // [SerializeField] private float floatSpeed = 1f;
    // [SerializeField] private float floatRange = 0.1f;
    // private Vector3 startPosition;

    private void Awake()
    {
                //startPosition = transform.position;
    }

    private void Update()
    {
        //ObjectFloat();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerStatsToModify = collision.GetComponent<Player_Stats>();

        if (playerStatsToModify.CanApplyBuffOf(buffName))
        {
            playerStatsToModify.ApplyBuff(buffs, buffDuration, buffName);
            Destroy(gameObject);
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
