using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

public class ItemController : MonoBehaviour
{
    public event Action<bool> StateChanged;

    [SerializeField]
    private GameObject _model;
    [SerializeField]
    private PlaneAlignment _furnitureType;

    private RectTransform _rt;
    private ContentController _contentController;
    private int _itemId;

    private bool _isChosen = false;

    private GlobalEvents _globalEvents;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
        _contentController = GetComponentInParent<ContentController>();
        StartCoroutine(SetItemIdCoroutine());

        _globalEvents = GlobalEvents.GetInstance();
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
        {
            _isChosen = !_isChosen;
            if (_isChosen)
                _globalEvents.CurrentFurnitureChanged?.Invoke(new Furniture
                {
                    Model = _model,
                    FurnitureType = _furnitureType
                });
            else
                _globalEvents.CurrentFurnitureChanged?.Invoke(null);
        }
        else
            _isChosen = false;

        StateChanged?.Invoke(_isChosen);
    }
}
