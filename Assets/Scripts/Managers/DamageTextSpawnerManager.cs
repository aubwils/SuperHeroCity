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

    public void SpawnDamageText(int damageAmount, Transform parentTransform, bool isCrit = false)
    {
        DamageText text = Instantiate(damageTextPrefab, parentTransform);

        float randomOffset = Random.Range(-0.5f, 0.5f);
        text.transform.position += Vector3.right * randomOffset;

        Color color = isCrit ? Color.yellow : Color.white;


        text.SetDamageText(damageAmount, parentTransform, color);
    }
}
