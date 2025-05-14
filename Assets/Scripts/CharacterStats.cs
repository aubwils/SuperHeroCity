using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
   public int damage;
   public int maxHealth;

   [SerializeField] private int currentHealth;
   
   private void Start()
   {
       currentHealth = maxHealth;
   }

   public void TakeDamage(int damage)
   {
       currentHealth -= damage;
       if (currentHealth <= 0)
       {
           //Die();
           Debug.Log("Die");
       }
   }


}
