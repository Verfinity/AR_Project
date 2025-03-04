using Unity.VisualScripting;
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
    private GameObject _currentModel;

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
        _currentModel = model;
        if (model == null)
        {
            _placementInteractable.placementPrefab = null;
            return;
        }

        _logicPrefab.GetComponent<ARTranslationInteractable>().objectGestureTranslationMode =
            type == FurnitureType.Horizontal ? GestureTransformationUtility.GestureTranslationMode.Horizontal : GestureTransformationUtility.GestureTranslationMode.Vertical;
        _placementInteractable.placementPrefab = _logicPrefab;
    }

    public void OnPrefabPlaced(ARObjectPlacementEventArgs args)
    {
        var placedObject = args.placementObject;

        var spawnedFurniture = Instantiate(_currentModel, placedObject.transform);
        spawnedFurniture.transform.localPosition = Vector3.zero;

        var childs = spawnedFurniture.GetComponentsInChildren<MeshFilter>();
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].AddComponent<MeshCollider>();
        }

        placedObject.GetComponent<ARSelectionInteractable>().enabled = true;
    }
}
