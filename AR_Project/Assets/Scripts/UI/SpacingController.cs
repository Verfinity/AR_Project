using System;
using UnityEngine;
using UnityEngine.UI;

public class SpacingController : MonoBehaviour
{
    public event Action<float> SpacingDistanceChanged;

    [SerializeField]
    private float _constHeight = 250;

    private RectTransform _rt;

    private void Start()
    {
        _rt = GetComponent<RectTransform>();
        var layoutGroup = GetComponent<HorizontalLayoutGroup>();
        layoutGroup.spacing += _rt.rect.height - _constHeight;

        SpacingDistanceChanged?.Invoke(_rt.rect.height / _constHeight);
    }
}
