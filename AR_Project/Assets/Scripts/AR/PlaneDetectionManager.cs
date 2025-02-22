using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARPlaneManager))]
public class PlaneDetectionManager : MonoBehaviour
{
    private ARPlaneManager _planeManager;

    private bool _isFloorExists = false;

    private void Awake()
    {
        _planeManager = GetComponent<ARPlaneManager>();
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs planes)
    {
        if (planes.added.Count == 0)
            return;

        if (_isFloorExists)
        {
            foreach (var plane in planes.added)
            {
                if (plane.alignment != PlaneAlignment.Vertical)
                    plane.gameObject.SetActive(false);
            }
            return;
        }

        foreach (var plane in planes.added)
        {
            if (plane.alignment != PlaneAlignment.Vertical)
                _isFloorExists = true;
        }
    }

    private void OnEnable()
    {
        _planeManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        _planeManager.planesChanged -= OnPlanesChanged;
    }
}
