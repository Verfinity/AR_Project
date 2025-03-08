using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Image))]
public class UIActiveController : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Sprite _enableStateSprite;
    [SerializeField]
    private Sprite _disableStateSprite;
    [SerializeField]
    private GameObject[] _changableObjects;
    [SerializeField]
    private MonoBehaviour[] _changableScripts;

    private bool _state = true;

    private void Awake()
    {
        _state = true;
        _image.sprite = _enableStateSprite;
    }

    public void ChangeState()
    {
        _state = !_state;
        _image.sprite = _state ? _enableStateSprite : _disableStateSprite;

        for (int i = 0; i < _changableObjects.Length; i++)
        {
            _changableObjects[i].SetActive(!_changableObjects[i].activeSelf);
        }
        for (int i = 0; i < _changableScripts.Length; i++)
        {
            _changableScripts[i].enabled = (!_changableScripts[i].enabled);
        }
    }
}
