using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectIcons : MonoBehaviour
{
 [SerializeField] private SpriteRenderer iconRenderer;
    [SerializeField] private Sprite alertIcon;
    [SerializeField] private Sprite confusedIcon;
    [SerializeField] private Sprite parryIcon;

    public void ShowAlertEffectIcon() => ShowEffectIcon(alertIcon);
    public void ShowConfusedEffectIcon() => ShowEffectIcon(confusedIcon);
    public void ShowParryEffectIcon() => ShowEffectIcon(parryIcon);
    public void HideEffectIcon() => iconRenderer.gameObject.SetActive(false);

    private void ShowEffectIcon(Sprite icon)
    {
        iconRenderer.sprite = icon;
        iconRenderer.gameObject.SetActive(true);
    }
}
