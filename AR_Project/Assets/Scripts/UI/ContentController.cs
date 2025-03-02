using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContentController : EventTrigger
{
    public event Action<int> ItemPressed;
    public float FullItemWidth => _rt.rect.height + _horLayoutGroup.spacing;
    public float ItemWidth => _rt.rect.height;

    private HorizontalLayoutGroup _horLayoutGroup;

    private RectTransform _rt;
    private int? _pointerId = null;
    private int? _itemId = null;

    private void Awake()
    {
        _horLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        _rt = GetComponent<RectTransform>();
        _rt.anchoredPosition = new Vector2(
            0,
            _rt.anchoredPosition.y
            );
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (_pointerId == null)
            _pointerId = eventData.pointerId;

        if (eventData.pointerId != _pointerId)
            return;

        _itemId = GetItemId(eventData.position.x);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != _pointerId)
            return;

        _itemId = null;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId != _pointerId)
            return;

        _rt.anchoredPosition += new Vector2(eventData.delta.x, 0);
        SetBorders();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId != _pointerId)
            return;

        _pointerId = null;

        if (_itemId == null)
            return;

        ItemPressed?.Invoke((int)_itemId);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerId != _pointerId)
            return;

        _pointerId = null;
    }

    private void SetBorders()
    {
        if (_rt.anchoredPosition.x > 0)
            _rt.anchoredPosition = new Vector2(
                0,
                _rt.anchoredPosition.y
                );

        float deltaX = -(_rt.rect.width - Screen.width);
        if (deltaX > 0)
            deltaX = 0;
        if (_rt.anchoredPosition.x < deltaX)
        {
            _rt.anchoredPosition = new Vector2(
                deltaX,
                _rt.anchoredPosition.y
                );
        }
    }

    private int GetItemId(float positionX)
    {
        float absolutePosition = positionX - _rt.anchoredPosition.x;
        return (int)(absolutePosition / FullItemWidth);
    }
}
