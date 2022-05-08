using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        transform.parent.GetComponent<GeneratorScript>().OnGenTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        transform.parent.GetComponent<GeneratorScript>().OnGenTriggerExit(other);
    }
}
