using UnityEngine;

public class WaterSpawner : MonoBehaviour
{
    public GameObject waterPrefab;
    public float spawnInterval = 8f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        InvokeRepeating(nameof(TrySpawn), spawnInterval, spawnInterval);
    }

    void TrySpawn()
    {
        if (FindObjectOfType<WaterDrop>() != null)
            return; // only 1 drop alive

        Vector3 screenPos = new Vector3(
            Random.Range(0, Screen.width),
            Random.Range(0, Screen.height),
            cam.nearClipPlane + 0.1f
        );

        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;

        Instantiate(waterPrefab, worldPos, Quaternion.identity);
    }
}
