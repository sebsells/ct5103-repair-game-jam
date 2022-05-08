using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        transform.parent.GetComponent<EnemyMovement>().OnRadiusEnter(transform.name, other);
    }
    private void OnTriggerExit(Collider other)
    {
        transform.parent.GetComponent<EnemyMovement>().OnRadiusExit(transform.name, other);
    }
}
