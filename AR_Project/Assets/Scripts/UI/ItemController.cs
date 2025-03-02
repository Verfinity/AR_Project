using System.Collections;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private float _persentOnPress = 0.8f;

    private RectTransform _rt;
    private ContentController _contentController;
    private int _itemId;

    private bool _isChosen = false;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _contentController = GetComponentInParent<ContentController>();
        StartCoroutine(SetItemIdCoroutine());
    }

    private IEnumerator SetItemIdCoroutine()
    {
        yield return new WaitForEndOfFrame();

        float leftSidePosition = _rt.anchoredPosition.x - _contentController.ItemWidth / 2;
        _itemId = (int)Mathf.Round(leftSidePosition / _contentController.FullItemWidth);
    }

    private void OnEnable()
    {
        _contentController.ItemPressed += OnItemPressed;
    }

    private void OnDisable()
    {
        _contentController.ItemPressed += OnItemPressed;
    }

    private void OnItemPressed(int pressedItemId)
    {
        if (_itemId == pressedItemId)
            _isChosen = !_isChosen;
        else
            _isChosen = false;
        SetWidthByState(_isChosen);
    }

    private void SetWidthByState(bool state)
    {
        float size = _contentController.ItemWidth;
        if (state)
            size *= _persentOnPress;
        _rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        _rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
    }
}
