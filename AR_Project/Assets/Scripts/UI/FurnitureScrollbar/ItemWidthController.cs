using UnityEngine;

public class ItemWidthController : MonoBehaviour
{
    [SerializeField]
    private ItemController _itemController;
    [SerializeField]
    private float _pressPersent = 0.85f;

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
        float scale = 1;
        if (state)
            scale = _pressPersent;
        _rt.localScale = new Vector2(scale, scale);
    }
}
