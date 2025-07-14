using UnityEngine;

public class UI_ToolTip : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private Vector2 offset = new Vector2(300, 20);

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public virtual void ShowToolTip(bool show, RectTransform targetRect)
    {
        if (show == false)
        {
            rectTransform.position = new Vector3(9999, 9999, 0);
            return;
        }
        UpdatePosition(targetRect);
    }
    private void UpdatePosition(RectTransform targetRect)
    {
        float screenCenterX = Screen.width / 2;
        float screenTop = Screen.height;
        float screenBottom = 0;

        Vector2 targetPosition = targetRect.position;
        targetPosition.x = targetPosition.x < screenCenterX ? targetPosition.x + offset.x : targetPosition.x - offset.x;

        float screenVerticalHalf = rectTransform.sizeDelta.y / 2f;
        float topY = targetPosition.y + screenVerticalHalf;
        float bottomY = targetPosition.y - screenVerticalHalf;

        if (topY > screenTop)
            targetPosition.y = screenTop - screenVerticalHalf - offset.y;
        else if (bottomY < screenBottom)
            targetPosition.y = screenBottom + screenVerticalHalf + offset.y;
    
        rectTransform.position = targetPosition;
    }
}
