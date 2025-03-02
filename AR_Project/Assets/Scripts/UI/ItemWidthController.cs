using UnityEngine;

public class ItemWidthController : MonoBehaviour
{
    private void Start()
    {
        var rectTransform = GetComponent<RectTransform>();
        float height = transform.parent.GetComponent<RectTransform>().rect.height;

        rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal, 
            height
            );
    }
}
