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
    }
}
