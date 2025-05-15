using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat strength;
   public Stat damage;
   public Stat maxHealth;

   [SerializeField] private int currentHealth;
   
   protected virtual void Start()
   {
       currentHealth = maxHealth.GetValue();

   }

    public virtual void DoDamage(CharacterStats targetStats)
    {
         int totalDamage = damage.GetValue() + strength.GetValue();
         targetStats.TakeDamage(totalDamage);
    }


   public virtual void TakeDamage(int damage)
   {
       currentHealth -= damage;
       Debug.Log("Took damage: " + damage + ", Current Health: " + currentHealth);
       if (currentHealth <= 0)
       {
           Die();
       }
   }

   protected virtual void Die()
   {
        
   }


}
