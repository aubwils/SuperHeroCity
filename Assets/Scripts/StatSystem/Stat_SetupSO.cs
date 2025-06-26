using UnityEngine;

[CreateAssetMenu(fileName = "Default Stat Setup", menuName = "Game Setup/Stat Setup")]
public class Stat_SetupSO : ScriptableObject
{
[Header("Resources")]
    public float maxHealth = 100f;
    public float healthRegen;

    public float maxMana = 50f;
    public float manaRegen;

    public float maxStamina = 100f;
    public float staminaRegen;


    [Header("Offense")]
    public float attackSpeed = 1f;
    public float damage = 5f;
    public float critPower;
    public float critChance;
    public float armorReduction;

    [Header("Elemental Damage")]
    public float fireDamage;
    public float iceDamage;
    public float lightningDamage;
    public float poisonDamage;
    public float holyDamage;
    public float darkDamage;

    [Header("Defense")]
    public float armor;
    public float evasion;
    public float suspisionResistance;

    [Header("Elemental Resistance")]
    public float fireResistance;
    public float iceResistance;
    public float lightningResistance;
    public float poisonResistance;
    public float holyResistance;
    public float darkResistance;
  
    [Header("Major Stats")]
    [Tooltip("1 Physical Damage, .5% Crit Power per point")]
    public float strength;

    [Tooltip(".5% Evasion, .3% Crit Chance per point")]
    public float dexterity;

    [Tooltip("1 Magical Damage, .5% Elemental resistance per point")]
    public float intelligence;

    [Tooltip("5 Max Health, 1 Armor per point")]
    public float constitution;
}
