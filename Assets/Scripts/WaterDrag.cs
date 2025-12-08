using UnityEngine;

public class WaterDrag : MonoBehaviour
{
    public Rigidbody2D selectedObject;
    Vector3 offset;
    Vector3 mousePosition;

    ClickThroughFix ctf;

    void Start()
    {
        ctf = FindObjectOfType<ClickThroughFix>();
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // on click: turn OFF click-through
        if (Input.GetMouseButtonDown(0))
        {
            ctf.AllowClicks();

            Collider2D hit = Physics2D.OverlapPoint(mousePosition);
            if (hit != null && hit.attachedRigidbody != null)
            {
                selectedObject = hit.attachedRigidbody;
                offset = selectedObject.transform.position - mousePosition;
                selectedObject.isKinematic = true;
            }
        }

        // on release: re-enable click-through
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedObject != null)
            {
                selectedObject.isKinematic = false;
                selectedObject = null;
            }

            ctf.BlockClicks();
        }
    }

    void FixedUpdate()
    {
        if (selectedObject != null)
        {
            selectedObject.MovePosition(mousePosition + offset);
        }
    }
}
