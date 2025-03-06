using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

[RequireComponent(typeof(ARSelectionInteractable))]
[RequireComponent(typeof(Outline))]
public class OutlineController : MonoBehaviour
{
    [SerializeField]
    private Outline _outline;
    [SerializeField]
    private float _outlineWidth;

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        _outline.OutlineWidth = _outlineWidth;
    }

    public void OnSelectedExit(SelectExitEventArgs args)
    {
        _outline.OutlineWidth = 0;
    }
}
