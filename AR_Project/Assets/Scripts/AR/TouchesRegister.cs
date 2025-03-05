using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchesRegister : MonoBehaviour
{
    public event Action<Vector2> TouchTapped;

    [SerializeField]
    private GraphicRaycaster _graphicRaycaster;
    [SerializeField]
    private float _tapTime;

    private float?[] _touchesPressTime;

    private void Awake()
    {
        _touchesPressTime = new float?[10];
    }

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            int fingerId = touch.fingerId;
            if (touch.phase == UnityEngine.TouchPhase.Began)
                _touchesPressTime[fingerId] = Time.time;
            else if (touch.phase == UnityEngine.TouchPhase.Moved)
                _touchesPressTime[fingerId] = null;
            else if (touch.phase == UnityEngine.TouchPhase.Ended)
            {
                if (_touchesPressTime[fingerId] == null)
                    return;

                if (Time.time - _touchesPressTime[fingerId] <= _tapTime &&
                    !IsPointerOverGameObject(touch.position))
                    TouchTapped?.Invoke(touch.position);
            }
        }
    }

    private bool IsPointerOverGameObject(Vector2 touchPosition)
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = touchPosition;

        var raycastResults = new List<RaycastResult>();

        _graphicRaycaster.Raycast(eventData, raycastResults);

        return raycastResults.Count > 0;
    }
}
