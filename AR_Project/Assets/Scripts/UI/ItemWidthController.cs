using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ItemWidthController : MonoBehaviour
{
    private SpacingController _parentSpacingController;

    private void Start()
    {
        _parentSpacingController = GetComponentInParent<SpacingController>();
    }

    private void ChangeWidthScale(float coefficient)
    {
        GetComponent<RectTransform>().localScale = new Vector2(coefficient, 1);
    }

    private void OnEnable()
    {
        _parentSpacingController.SpacingDistanceChanged += ChangeWidthScale;
    }

    private void OnDisable()
    {
        _parentSpacingController.SpacingDistanceChanged -= ChangeWidthScale;
    }
}
