using UnityEngine;

public class UI_ToolTip : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void ShowToolTip(bool show, RectTransform targetRect)
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
        rectTransform.position = targetRect.position;
    }
}
