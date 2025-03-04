using UnityEngine;

public class ItemWidthController : MonoBehaviour
{
    [SerializeField]
    private ItemController _itemController;
    [SerializeField]
    private float _pressPersent = 0.8f;

    private RectTransform _rt;
    private float size = 0;

    private void Start()
    {
        _rt = GetComponent<RectTransform>();
        size = transform.GetComponentInParent<ContentController>().ItemWidth;

        _rt.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal, 
            size
            );
    }

    private void OnEnable()
    {
        _itemController.StateChanged += SetWidthByState;
    }

    private void OnDisable()
    {
        _itemController.StateChanged -= SetWidthByState;
    }

    private void SetWidthByState(bool state)
    {
        float newSize = size;
        if (state)
            newSize *= _pressPersent;
        _rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newSize);
        _rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newSize);
    }
}
