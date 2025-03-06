using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

[RequireComponent(typeof(ARSelectionInteractable))]
public class PrefabDeletionController : MonoBehaviour
{
    [SerializeField]
    private ARSelectionInteractable _selectionInteractable;
    private GlobalEvents _globalEvents;

    private void Awake()
    {
        _globalEvents = GlobalEvents.GetInstance();
    }

    private void DeleteObject()
    {
        if (_selectionInteractable.isSelected)
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        _globalEvents.DeleteButtonPressed += DeleteObject;
    }

    private void OnDisable()
    {
        _globalEvents.DeleteButtonPressed -= DeleteObject;
    }
}
