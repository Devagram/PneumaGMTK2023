using UnityEngine;

public class RecurringPrefabSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float interval;

    void Start()
    {
        if (prefab != null)
        {
            InvokeRepeating("SpawnPrefab", interval, interval);
        }
    }

    private void SpawnPrefab()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}
