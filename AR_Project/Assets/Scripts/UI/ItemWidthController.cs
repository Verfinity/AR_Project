using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ItemWidthController : MonoBehaviour
{
    [SerializeField]
    private SpacingController _parentSpacingController;

    private void Start()
    {
        var rt = GetComponent<RectTransform>();
        rt.localScale = new Vector2(_parentSpacingController.GetScaleCoefficient(), 1);
    }
}
