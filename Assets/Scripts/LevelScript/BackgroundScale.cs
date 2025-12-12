using UnityEngine;

public class BackgroundScale: MonoBehaviour
{
    Camera _camera;
    private int distanceFromCamera = 100;

    
    void Start()
    {
        _camera = Camera.main;
        transform.position = _camera.transform.position + _camera.transform.forward * distanceFromCamera;

        transform.rotation = _camera.transform.rotation;

        float height = _camera.orthographicSize * 2f;
        float width = height * _camera.aspect;

        transform.localScale = new Vector3(width, height, 1f);

        float targetAspect = 16f / 9f;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Rect rect = _camera.rect;

        if (scaleHeight < 1f)
        {
            rect.width = 1f;
            rect.height = scaleHeight;
            rect.x = 0f;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            float scaleWidth = 1f / scaleHeight;
            rect.width = scaleWidth;
            rect.height = 1f;
            rect.x = (1f - scaleWidth) / 2f;
            rect.y = 0f;
        }

        _camera.rect = rect;
    }
}
