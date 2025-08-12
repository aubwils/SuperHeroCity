using UnityEngine;

public class UI_ToolTip : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private Vector2 offset = new Vector2(300, 20);

    protected virtual void Awake()
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
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, targetRect.position);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            screenPoint,
            null,
            out localPoint
        );

        // Horizontal offset logic
        localPoint.x += (screenPoint.x < Screen.width / 2) ? offset.x : -offset.x;
        localPoint.y += offset.y;

        // --- Clamp vertically so tooltip stays on screen ---
        float tooltipHeight = rectTransform.rect.height;
        float canvasHeight = (rectTransform.parent as RectTransform).rect.height;

        float halfHeight = tooltipHeight / 2f;

        // Prevent top overflow
        if (localPoint.y + halfHeight > canvasHeight / 2f)
            localPoint.y = (canvasHeight / 2f) - halfHeight;

        // Prevent bottom overflow
        if (localPoint.y - halfHeight < -(canvasHeight / 2f))
            localPoint.y = -(canvasHeight / 2f) + halfHeight;

        rectTransform.localPosition = localPoint;
    }
    
    protected string GetColoredText(string color, string text)
    {
        return $"<color={color}>{text}</color>";
    }
}
