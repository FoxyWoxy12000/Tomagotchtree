using UnityEngine;

public class WaterDrag : MonoBehaviour
{
    private bool dragging;
    private Vector3 offset;

    void OnMouseDown()
    {
        // This ONLY fires if a collider exists
        dragging = true;

        Vector3 mouseWorld = GetMouseWorld();
        offset = transform.position - mouseWorld;
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    void Update()
    {
        if (!dragging) return;

        Vector3 mouseWorld = GetMouseWorld();
        transform.position = mouseWorld + offset;
    }

    Vector3 GetMouseWorld()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = Mathf.Abs(Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(mouse);
    }
}
