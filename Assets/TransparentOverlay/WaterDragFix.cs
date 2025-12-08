using UnityEngine;

public class WaterDragFix : MonoBehaviour
{
    public LayerMask waterLayer; // set this in Inspector to "Water"
    public float dragScale = 1.15f;
    public float darkenAmount = 0.75f;

    Camera cam;
    Rigidbody2D selectedRb;
    SpriteRenderer selectedSprite;
    Vector3 offset;

    ClickThroughFix ctf;

    void Start()
    {
        cam = Camera.main;
        ctf = FindObjectOfType<ClickThroughFix>();
    }

    void Update()
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;

        // MOUSE DOWN
        if (Input.GetMouseButtonDown(0))
        {
            ctf.AllowClicks();

            // precise hit test
            RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero, 0.01f, waterLayer);

            if (hit.collider != null)
            {
                selectedRb = hit.collider.attachedRigidbody;
                selectedSprite = hit.collider.GetComponent<SpriteRenderer>();

                offset = selectedRb.transform.position - mouseWorld;

                selectedRb.isKinematic = true;

                // enlarge + darken
                selectedRb.transform.localScale *= dragScale;
                selectedSprite.color *= darkenAmount;
            }
        }

        // MOUSE UP
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedRb != null)
            {
                // shrink + brighten
                selectedRb.transform.localScale /= dragScale;
                selectedSprite.color /= darkenAmount;

                selectedRb.isKinematic = false;
                selectedRb = null;
                selectedSprite = null;
            }

            ctf.BlockClicks();
        }

        // UPDATE POSITION
        if (selectedRb != null)
        {
            selectedRb.MovePosition(mouseWorld + offset);
        }
    }
}
