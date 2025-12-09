using UnityEngine;

public class WaterDebug : MonoBehaviour
{
    void OnMouseDown()
    {
        Debug.Log(">>> WATER CLICKED <<<");
    }

    void Awake()
    {
        Debug.Log("Water Awake. Position: " + transform.position);
    }

}