using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RecurringPrefabSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float interval;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (prefab != null)
        {
            InvokeRepeating("SpawnPrefab", interval, interval);
        }
    }

    private void SpawnPrefab()
    {
        animator.SetTrigger("SpawnPrefab");
        Instantiate(prefab, transform.position, transform.rotation);;
    }
}
