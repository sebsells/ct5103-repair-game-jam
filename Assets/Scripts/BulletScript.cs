using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float speed = 40.0f; // Speed of the bullet
    [SerializeField] float maxTime = 2.0f; // Maximum time the bullet will exist for
    float currentTime = 0.0f; // Current time bullet has existed for

    void Update()
    {
        // Check if bullet has existed for too long
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            Destroy(gameObject);
        }

        // Move bullet
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Generator")
        {
            other.GetComponent<GeneratorScript>().PlayerShot();
            Destroy(gameObject);
        }
    }
}
