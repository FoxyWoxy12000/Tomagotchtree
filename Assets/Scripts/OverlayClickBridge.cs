using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class OverlayClickBridge : MonoBehaviour
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
        EnableClickThrough();
#endif
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DisableClickThrough();
            StartCoroutine(CheckAndRestore());
        }
    }

    System.Collections.IEnumerator CheckAndRestore()
    {
        yield return null; // wait ONE frame so Unity can receive click

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;

        Collider2D hit = Physics2D.OverlapPoint(mouse);

        if (hit == null)
        {
            // clicked empty space → desktop
            EnableClickThrough();
        }
        // else: clicked a GameObject, leave click-through off
    }

    public void EnableClickThrough()
    {
#if UNITY_STANDALONE_WIN
        uint style = GetWindowLong(hwnd, GWL_EXSTYLE);
        SetWindowLong(hwnd, GWL_EXSTYLE, style | WS_EX_TRANSPARENT);
#endif
    }

    public void DisableClickThrough()
    {
#if UNITY_STANDALONE_WIN
        uint style = GetWindowLong(hwnd, GWL_EXSTYLE);
        SetWindowLong(hwnd, GWL_EXSTYLE, style & ~WS_EX_TRANSPARENT);
#endif
    }
}
