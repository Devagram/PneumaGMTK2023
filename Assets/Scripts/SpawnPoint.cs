using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject hero;
    // Start is called before the first frame update
    //Transform location;
    void Start()
    {
        // Get the position of the parent
        
    }

    private void OnDrawGizmos()
    {
        // Set the color of the Gizmo icon
        Gizmos.color = Color.yellow;

        // Draw a sphere Gizmo at the position of the GameObject
        Gizmos.DrawSphere(transform.position, 0.2f);
    }

    public void SpawnHero()
    {
        Vector3 spawnPosition = transform.position;
        GameObject instantiatedHero = Instantiate(hero, spawnPosition, Quaternion.identity);
    }


}
