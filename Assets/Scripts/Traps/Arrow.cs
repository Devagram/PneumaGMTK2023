using System.Collections;
using UnityEngine;

namespace Traps
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField]
        private float movementSpeed;
        [SerializeField]
        private float despawnTime;

        void Update()
        {
            transform.position += (transform.up * movementSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Doesn't kill the playable ghost character
            if (!collision.gameObject.CompareTag("Ghost"))
            {
                movementSpeed = 0;
                collision.gameObject.SetActive(false);
                StartCoroutine(Despawn());
            }
        }

        private IEnumerator Despawn()
        {
            yield return new WaitForSeconds(despawnTime);
            Destroy(gameObject);
        }
    }
}
