using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextSpawnerManager : MonoBehaviour
{
    public static DamageTextSpawnerManager Instance;

    [Header("Settings")]
    [SerializeField] private DamageText damageTextPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnDamageText(int damageAmount, Transform parentTransform)
    {
        DamageText text = Instantiate(damageTextPrefab, parentTransform);
       text.transform.position += Vector3.right *0.5f; 
        text.SetDamageText(damageAmount, parentTransform);
    }
}
