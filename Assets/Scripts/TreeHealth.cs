using UnityEngine;

public class TreeHealth : MonoBehaviour
{
    public int maxWater = 3;
    public int currentWater = 0;

    public void AddWater()
    {
        currentWater = Mathf.Clamp(currentWater + 1, 0, maxWater);
        Debug.Log("Tree watered: " + currentWater + "/" + maxWater);
    }
}
