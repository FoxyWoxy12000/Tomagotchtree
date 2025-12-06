
TransparentOverlay v2 - Desktop reparenting and click-through control
====================================================================

This update adds three new scripts to control behavior and modes:

1) DesktopWindowHelper.cs
   - Reparents the Unity window into the Windows desktop WorkerW, which causes the window to sit as a wallpaper overlay.
   - Provides SetTopMost(true/false) to switch between overlay (topmost) and wallpaper (bottom) z-order.

2) ClickThroughController.cs
   - Dynamically toggles WS_EX_TRANSPARENT based on hit-tests against Collider2D objects in a specified layer.
   - Add Collider2D to interactive sprites and assign them to the layer specified by interactiveLayer.

3) OverlayModeController.cs
   - Simple manager that toggles between Wallpaper, Overlay, and Auto modes.
   - Auto mode switches to overlay while you interact and reverts to wallpaper after idleTimeout seconds.

Installation
------------
1. Drop the "Assets/TransparentOverlay" folder into your project's Assets folder (or import via package).
2. In your first scene, create an empty GameObject 'TransparentWindowRoot' and add:
   - TransparentWindowManager (previous)
   - DesktopWindowHelper
   - ClickThroughController
   - OverlayModeController
3. Configure ClickThroughController.interactiveLayer to the layer your interactive sprites use.
   Ensure those sprites have Collider2D components.
4. Ensure Main Camera background is EXACTLY RGBA(0,0,0,0) and in Player Settings use Direct3D11.
5. Build & Run (Editor will not show desktop transparency).

Notes
-----
- This package uses a color-key/window reparenting approach and toggles click-through by changing window styles.
- It is tested on Windows desktop (Win10/11) with DX11. Results may vary by GPU/drivers.
- For true semi-transparent per-pixel alpha you would need UpdateLayeredWindow and native texture blitting; I can add that later.
