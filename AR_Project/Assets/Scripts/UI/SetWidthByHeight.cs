using System.Collections;
using UnityEngine;

public class SetWidthByHeight : MonoBehaviour
{
    private RectTransform _rt;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
        StartCoroutine(SetWidthByHeightCoroutine());
    }

    private IEnumerator SetWidthByHeightCoroutine()
    {
        yield return new WaitForEndOfFrame();

        _rt.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            _rt.rect.height
            );
    }
}
