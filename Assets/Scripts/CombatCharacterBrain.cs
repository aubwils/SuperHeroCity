using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCharacterBrain : CharacterBrainBase
{
    [Header("Combat")]
    public float attackDamage = 1f;
    public Transform attackCheck;
    public float attackCheckRadius = 0.5f;
    public CharacterFX characterFX { get; protected set; }

    public override void Awake()
    {
        base.Awake(); 
        characterFX = GetComponentInChildren<CharacterFX>();
    }

    public virtual void TakeDamage(float damage)
    {
        Debug.Log($"{gameObject.name} took {damage} damage.");
        if (characterFX != null)
            characterFX.StartCoroutine("FlashFX");
    }

    protected virtual void OnDrawGizmosSelected()
    {
        if (attackCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
        }
    }

}
