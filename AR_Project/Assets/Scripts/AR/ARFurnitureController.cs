using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

[RequireComponent(typeof(TouchesRegister))]
[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class ARFurnitureController : MonoBehaviour
{
    [SerializeField]
    private TouchesRegister _touchesRegister;
    [SerializeField]
    private ARRaycastManager _raycastManager;
    [SerializeField]
    private ARPlaneManager _planeManager;
    [SerializeField]
    private Text _t;
    [SerializeField]
    private Text _t1;

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
        _touchesRegister.TouchTapped += PlacePrefab;
    }

    private void OnDisable()
    {
        _globalEvents.CurrentFurnitureChanged -= OnCurrentFurnitureChanged;
        _touchesRegister.TouchTapped -= PlacePrefab;
    }

    private void OnCurrentFurnitureChanged(Furniture furniture)
    {
        _currentFurniture = furniture;
        if (_currentFurniture == null)
            return;

        _logicPrefab.GetComponent<ARTranslationInteractable>().objectGestureTranslationMode =
            _currentFurniture.FurnitureType != PlaneAlignment.Vertical ? GestureTransformationUtility.GestureTranslationMode.Horizontal : GestureTransformationUtility.GestureTranslationMode.Vertical;
    }

    private void PlacePrefab(Vector2 touchPosition)
    {
        var hitResults = new List<ARRaycastHit>();
        Pose hitPose = new Pose();
        if (_raycastManager.Raycast(touchPosition, hitResults, TrackableType.PlaneWithinPolygon))
        {
            var hit = hitResults[0];
            var plane = _planeManager.GetPlane(hit.trackableId);
            _t.text = plane.ToString();
            _t1.text = plane.alignment.ToString();

            if (_currentFurniture.FurnitureType != plane.alignment)
                return;

            hitPose = hit.pose;
        }

        var placedLogicPrefab = Instantiate(_logicPrefab, hitPose.position, hitPose.rotation);

        var spawnedFurniture = Instantiate(_currentFurniture.Model, placedLogicPrefab.transform);
        spawnedFurniture.transform.localPosition = Vector3.zero;

        var childs = spawnedFurniture.GetComponentsInChildren<MeshFilter>();
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].AddComponent<MeshCollider>();
        }

        placedLogicPrefab.GetComponent<ARSelectionInteractable>().enabled = true;
    }
}
