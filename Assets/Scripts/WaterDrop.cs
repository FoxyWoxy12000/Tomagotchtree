using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public float growScale = 1.2f;
    public float darkenAmount = 0.4f;

    private Vector3 originalScale;
    private SpriteRenderer sr;
    private Color originalColor;

    private bool dragging = false;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        originalColor = sr.color;
    }

    void OnMouseDown()
    {
        dragging = true;

        // Juicy effect
        transform.localScale = originalScale * growScale;
        sr.color = originalColor * (1f - darkenAmount);
    }

    void OnMouseDrag()
    {
        if (!dragging) return;

        Vector3 mouse = Input.mousePosition;
        mouse.z = cam.nearClipPlane + 0.1f;
        Vector3 world = cam.ScreenToWorldPoint(mouse);
        world.z = 0;
        transform.position = world;
    }

    void OnMouseUp()
    {
        dragging = false;

        // Restore look
        transform.localScale = originalScale;
        sr.color = originalColor;

        // Check tree collision
        Collider2D tree = Physics2D.OverlapCircle(transform.position, 0.4f, LayerMask.GetMask("Interactive"));
        if (tree != null && tree.CompareTag("Tree"))
        {
            FindObjectOfType<TreeHealth>().AddWater();
            Destroy(gameObject);
        }
    }
}
