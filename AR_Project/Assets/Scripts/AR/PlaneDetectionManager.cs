using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARPlaneManager))]
public class PlaneDetectionManager : MonoBehaviour
{
    [SerializeField]
    private ARPlaneManager _planeManager;

    private bool _isFloorExists = false;

    private void OnPlanesChanged(ARPlanesChangedEventArgs planes)
    {
        if (planes.added.Count == 0)
            return;

        foreach (var plane in planes.added)
        {
            if (plane.alignment == PlaneAlignment.NotAxisAligned)
                plane.gameObject.SetActive(false);
            else if (plane.alignment != PlaneAlignment.Vertical)
            {
                if (!_isFloorExists)
                    _isFloorExists = true;
                else
                    plane.gameObject.SetActive(false);
            }
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
