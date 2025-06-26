using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [Header("Damage Text Settings")]
    [SerializeField] private TextMeshProUGUI damageTMP;

    public void SetDamageText(int damage, Transform parent, Color color)
    {
        damageTMP.text = damage.ToString();
        damageTMP.color = color; 
        transform.SetParent(parent);
    }

    public void DestroyDamageText()
    {
        Destroy(gameObject);
    }

}
