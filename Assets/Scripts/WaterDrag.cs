using UnityEngine;
using System;
using System.Runtime.InteropServices;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class WaterDrag : MonoBehaviour
{
#if UNITY_STANDALONE_WIN
    private const int GWL_EXSTYLE = -20;
    private const uint WS_EX_TRANSPARENT = 0x00000020;

    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    IntPtr hwnd;
#endif

    Rigidbody2D rb;
    SpriteRenderer sr;

    bool dragging = false;
    Vector3 offset;

    Vector3 baseScale;
    Color baseColor;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.gravityScale = 0;
        rb.freezeRotation = true;

        baseScale = transform.localScale;
        baseColor = sr.color;

#if UNITY_STANDALONE_WIN
        hwnd = GetActiveWindow();
#endif
    }

    void Update()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hit = Physics2D.OverlapPoint(mouse);

            if (hit && hit.gameObject == gameObject)
            {
                dragging = true;
                offset = transform.position - mouse;

                // visual feedback
                transform.localScale = baseScale * 1.12f;
                sr.color = new Color(
                    baseColor.r * 0.85f,
                    baseColor.g * 0.85f,
                    baseColor.b * 0.85f,
                    baseColor.a
                );

                DisableClickThrough();
            }
        }

        if (Input.GetMouseButtonUp(0) && dragging)
        {
            dragging = false;

            transform.localScale = baseScale;
            sr.color = baseColor;

            EnableClickThrough();
        }
    }

    void FixedUpdate()
    {
        if (!dragging) return;

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        rb.MovePosition(mouse + offset);
    }

    void DisableClickThrough()
    {
#if UNITY_STANDALONE_WIN
        if (hwnd == IntPtr.Zero) return;
        uint style = GetWindowLong(hwnd, GWL_EXSTYLE);
        SetWindowLong(hwnd, GWL_EXSTYLE, style & ~WS_EX_TRANSPARENT);
#endif
    }

    void EnableClickThrough()
    {
#if UNITY_STANDALONE_WIN
        if (hwnd == IntPtr.Zero) return;
        uint style = GetWindowLong(hwnd, GWL_EXSTYLE);
        SetWindowLong(hwnd, GWL_EXSTYLE, style | WS_EX_TRANSPARENT);
#endif
    }
}
