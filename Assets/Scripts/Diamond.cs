using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public AudioClip collectSound;

    public GameObject shadow;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

            Destroy(gameObject);
            Debug.Log("Diamond collected!");

            shadow.SetActive(false);
        }
    }
}
