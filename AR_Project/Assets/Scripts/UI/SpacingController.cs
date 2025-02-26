using UnityEngine;
using UnityEngine.UI;

public class SpacingController : MonoBehaviour
{
    [SerializeField]
    private float _constHeight = 250;

    private RectTransform _rt;

    private void Start()
    {
        _rt = GetComponent<RectTransform>();
        var layoutGroup = GetComponent<HorizontalLayoutGroup>();

        layoutGroup.spacing += _rt.rect.height - _constHeight;
    }

    public float GetScaleCoefficient() => _rt.rect.height / _constHeight;
}
