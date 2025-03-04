using UnityEngine;

public class ActiveController : MonoBehaviour
{
    [SerializeField]
    private GameObject _changableObject;

    public void ChangeState()
    {
        _changableObject.SetActive(!_changableObject.activeSelf);
    }
}
