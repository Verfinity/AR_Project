using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

[RequireComponent(typeof(ARPlacementInteractable))]
public class PlacementPrefabController : MonoBehaviour
{
    [SerializeField]
    private ARPlacementInteractable _placementInteractable;
    [SerializeField]
    private GameObject _logicPrefab;

    private GlobalEvents _globalEvents;

    private void Awake()
    {
        _globalEvents = GlobalEvents.GetInstance();
    }

    private void OnEnable()
    {
        _globalEvents.CurrentFurnitureChanged += OnCurrentFurnitureChanged;
    }

    private void OnDisable()
    {
        _globalEvents.CurrentFurnitureChanged -= OnCurrentFurnitureChanged;
    }

    private void OnCurrentFurnitureChanged(GameObject? model, FurnitureType? type)
    {
        Debug.Log($"{(model != null ? model.name : null)} {type}");
    }
}
