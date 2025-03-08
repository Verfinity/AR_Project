using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

[RequireComponent(typeof(ARSelectionInteractable))]
public class OutlineController : MonoBehaviour
{
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        _outline.enabled = true;
    }

    public void OnSelectedExit(SelectExitEventArgs args)
    {
        _outline.enabled = false;
    }
}
