using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DeleteButton : MonoBehaviour
{
    private GlobalEvents _globalEvents;

    private void Awake()
    {
        _globalEvents = GlobalEvents.GetInstance();
    }

    public void OnPressed() => _globalEvents.DeleteButtonPressed?.Invoke();
}
