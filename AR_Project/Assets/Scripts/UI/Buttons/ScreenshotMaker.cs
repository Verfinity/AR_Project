using System;
using System.Collections;
using System.Data;
using UnityEngine;

public class ScreenshotMaker : MonoBehaviour
{
    [SerializeField]
    private Canvas _canvas;

    public void TakeScreenshot()
    {
        StartCoroutine(TakeScreenshotCoroutine());
    }

    private IEnumerator TakeScreenshotCoroutine()
    {
        _canvas.enabled = false;

        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();
        _canvas.enabled = true;

        string name = DateTime.Now.ToString();
        NativeGallery.SaveImageToGallery(ss, Application.productName + " Photos", name);
    }
}
