using UnityEngine;

public class ItemWidthController : MonoBehaviour
{
    private void Start()
    {
        var parentRectTransform = transform.parent.GetComponent<RectTransform>();
        var rectTransform = GetComponent<RectTransform>();

        rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal, 
            parentRectTransform.rect.height
            );
    }
}
