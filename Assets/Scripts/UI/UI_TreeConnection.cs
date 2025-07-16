using UnityEngine;
using UnityEngine.UI;

public class UI_TreeConnection : MonoBehaviour
{
    [SerializeField] private RectTransform rotationPoint;
    [SerializeField] private RectTransform connecttionLength;
    [SerializeField] private RectTransform childNodeConnectionPoint;

    public void DirectConnection(NodeDirectionType direction, float length)
    {
        bool shouldBeActive = direction != NodeDirectionType.None;
        float finalLength = shouldBeActive ? length : 0f;
        float angle = GetDirectionAngle(direction);

        rotationPoint.localRotation = Quaternion.Euler(0, 0, angle);
        connecttionLength.sizeDelta = new Vector2(finalLength, connecttionLength.sizeDelta.y);
    }

    public Image GetConnectionImage() => connecttionLength.GetComponent<Image>();

    public Vector2 GetConnectionPoint(RectTransform rect) // This method converts the world position of the childNodeConnectionPoint to a local position relative to the parent RectTransform.
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (
         rect.parent as RectTransform,
         childNodeConnectionPoint.position, //taking world position of this object and converting it to the local position
         null,
         out Vector2 localPosition
        );

        return localPosition;
    }


 private float GetDirectionAngle(NodeDirectionType direction)
    {
        switch (direction)
        {
            case NodeDirectionType.UpLeft: return 135f;
            case NodeDirectionType.Up: return 90f;
            case NodeDirectionType.UpRight: return 45f;
            case NodeDirectionType.Left: return 180f;
            case NodeDirectionType.Right: return 0f;
            case NodeDirectionType.DownLeft: return -135f;
            case NodeDirectionType.Down: return -90f;
            case NodeDirectionType.DownRight: return -45f;
            default: return 0f;
        }
    }
}


public enum NodeDirectionType
{
    None,
    UpLeft,
    Up,
    UpRight,
    Left,
    Right,
    DownLeft,
    Down,
    DownRight
}
