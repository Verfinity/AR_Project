using System;
using System.Collections;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public event Action<bool> StateChanged;

    [SerializeField]
    private GameObject _model;
    [SerializeField]
    private FurnitureType _furnitureType;

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

        _itemId = (int)Mathf.Floor(_rt.anchoredPosition.x / _contentController.FullItemWidth);
    }

    private void OnEnable()
    {
        _contentController.ItemPressed += OnItemPressed;
    }

    private void OnDisable()
    {
        _contentController.ItemPressed -= OnItemPressed;
    }

    private void OnItemPressed(int pressedItemId)
    {
        if (_itemId == pressedItemId)
            _isChosen = !_isChosen;
        else
            _isChosen = false;

        StateChanged?.Invoke(_isChosen);
    }
}
