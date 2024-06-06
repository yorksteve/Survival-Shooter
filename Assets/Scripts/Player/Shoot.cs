using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var center = new Vector3(.5f, .5f, 0);
            var rayOrigin = _camera.ViewportPointToRay(center);

            if (Physics.Raycast(rayOrigin, out var hitInfo))
            {
                Debug.Log($"Hit: {hitInfo.collider.name}");
            }
        }
    }
}
