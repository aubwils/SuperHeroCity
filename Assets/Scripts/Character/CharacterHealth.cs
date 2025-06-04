using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] public  float maxHP = 100f;
    [SerializeField] public float currentHP;
    private bool isDead = false;

    private void Start()
    {
        currentHP = maxHP;
    }

    public virtual bool TakeDamage(float damage, Transform damageSource)
    {
        if (isDead)
            return false; // Indicates that the character is already dead

        ReduceHP(damage);
        return false; // Indicates that the character is still alive
    }

    protected void ReduceHP(float damage)
    {
        currentHP -= damage;
        if (currentHP < 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        isDead = true;
        Debug.Log("Character has died.");
    }
}
