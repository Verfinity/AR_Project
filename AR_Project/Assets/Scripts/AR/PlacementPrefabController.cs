using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;
using static UnityEngine.XR.Interaction.Toolkit.AR.GestureTransformationUtility;

[RequireComponent(typeof(ARPlacementInteractable))]
public class PlacementPrefabController : MonoBehaviour
{
    [SerializeField]
    private ARPlacementInteractable _placementInteractable;
    [SerializeField]
    private ARRaycastManager _raycastManager;
    [SerializeField]
    private GameObject _logicPrefab;

    private GlobalEvents _globalEvents;
    private Furniture _currentFurniture;

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

    private void OnCurrentFurnitureChanged(Furniture furniture)
    {
        _currentFurniture = furniture;
        if (_currentFurniture == null)
        {
            _placementInteractable.placementPrefab = null;
            return;
        }

        if (_currentFurniture.FurnitureType != PlaneAlignment.Vertical)
            _logicPrefab.GetComponent<ARTranslationInteractable>().objectGestureTranslationMode = GestureTranslationMode.Horizontal;
        else
            _logicPrefab.GetComponent<ARTranslationInteractable>().objectGestureTranslationMode = GestureTranslationMode.Vertical;
        _placementInteractable.placementPrefab = _logicPrefab;
    }

    public void OnPrefabPlaced(ARObjectPlacementEventArgs args)
    {
        if (!CanPlaceObject(args.placementObject.transform.position))
        {
            Destroy(args.placementObject);
            return;
        }

        var placedObject = args.placementObject;

        var spawnedFurniture = Instantiate(_currentFurniture.FurnitureModel, placedObject.transform);
        spawnedFurniture.transform.localPosition = Vector3.zero;

        var childs = spawnedFurniture.GetComponentsInChildren<MeshFilter>();
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].AddComponent<MeshCollider>();
        }

        placedObject.GetComponent<ARSelectionInteractable>().enabled = true;
    }

    private bool CanPlaceObject(Vector3 objectPosition)
    {
        var screenPosition = Camera.main.WorldToScreenPoint(objectPosition);
        var hitResults = new List<ARRaycastHit>();

        if (_raycastManager.Raycast(screenPosition, hitResults, TrackableType.PlaneWithinPolygon))
        {
            var hit = hitResults[0];
            if (hit.trackable is ARPlane plane)
            {
                if (_currentFurniture.FurnitureType == plane.alignment)
                    return true;
            }
        }

        return false;
    }
}
