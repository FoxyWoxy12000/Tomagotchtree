using UnityEngine;
using UnityEngine.InputSystem;

public class WaterDrag : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 screenPos = Mouse.current.position.ReadValue();
            Vector3 mousepos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, mainCamera.nearClipPlane));
            mousepos.z = 0;

            transform.position = mousepos + offset;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;

        // Calculate offset so object doesn't snap to mouse center
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, mainCamera.nearClipPlane));
        mouseWorldPos.z = 0;

        offset = transform.position - mouseWorldPos;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}