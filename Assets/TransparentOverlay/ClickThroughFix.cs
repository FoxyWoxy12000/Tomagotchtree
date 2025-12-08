using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ClickThroughFix : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    const int GWL_EXSTYLE = -20;
    const int WS_EX_TRANSPARENT = 0x00000020;
    const int WS_EX_LAYERED = 0x00080000;

    IntPtr hwnd;

    void Start()
    {
        hwnd = GetActiveWindow();

        // make window transparent to clicks
        int style = GetWindowLong(hwnd, GWL_EXSTYLE);
        style |= WS_EX_LAYERED | WS_EX_TRANSPARENT;
        SetWindowLong(hwnd, GWL_EXSTYLE, style);
    }

    public void AllowClicks()
    {
        int style = GetWindowLong(hwnd, GWL_EXSTYLE);
        style &= ~WS_EX_TRANSPARENT;
        SetWindowLong(hwnd, GWL_EXSTYLE, style);
    }

    public void BlockClicks()
    {
        int style = GetWindowLong(hwnd, GWL_EXSTYLE);
        style |= WS_EX_TRANSPARENT;
        SetWindowLong(hwnd, GWL_EXSTYLE, style);
    }
}
