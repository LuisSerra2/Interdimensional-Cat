using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Start()
    {
        Cursor.visible = false;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent as RectTransform,
            Input.mousePosition,
            null,
            out mousePosition
        );

        rectTransform.anchoredPosition = mousePosition;
    }
}
