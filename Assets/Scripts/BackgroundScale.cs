using UnityEngine;

public class BackgroundScale: MonoBehaviour
{
    Camera camera;
    private int distanceFromCamera = 100;

    void Start()
    {
        camera = Camera.main;
        transform.position = camera.transform.position + camera.transform.forward * distanceFromCamera;

        transform.rotation = camera.transform.rotation;

        float height = camera.orthographicSize * 2f;
        float width = height * camera.aspect;

        transform.localScale = new Vector3(width, height, 1f);
    }
}
