using System.Runtime.InteropServices;
using System;
using UnityEngine;

public class ClickThroughGate : MonoBehaviour
{
#if UNITY_STANDALONE_WIN
    const int GWL_EXSTYLE = -20;
    const uint WS_EX_TRANSPARENT = 0x20;

    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern uint SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    IntPtr hwnd;
#endif

    void Awake()
    {
#if UNITY_STANDALONE_WIN
        hwnd = GetActiveWindow();
        EnableClickThrough(); // start transparent
#endif
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // STEP 1: allow Unity to receive input
            DisableClickThrough();

            // STEP 2: wait ONE frame so Unity can process the click
            StartCoroutine(HandleClick());
        }
    }

    System.Collections.IEnumerator HandleClick()
    {
        yield return null;

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        Collider2D hit = Physics2D.OverlapPoint(mouse);

        if (hit == null)
        {
            // clicked empty space → desktop
            EnableClickThrough();
        }
        // else: clicked a GameObject → Unity keeps the click
    }

    void EnableClickThrough()
    {
#if UNITY_STANDALONE_WIN
        uint style = GetWindowLong(hwnd, GWL_EXSTYLE);
        SetWindowLong(hwnd, GWL_EXSTYLE, style | WS_EX_TRANSPARENT);
#endif
    }

    void DisableClickThrough()
    {
#if UNITY_STANDALONE_WIN
        uint style = GetWindowLong(hwnd, GWL_EXSTYLE);
        SetWindowLong(hwnd, GWL_EXSTYLE, style & ~WS_EX_TRANSPARENT);
#endif
    }
}
