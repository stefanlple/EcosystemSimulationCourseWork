using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prey")
        {
            other.GetComponent<HareMovement>().hare.isUnderwater = true;
        }

        if (other.tag == "Fox")
        {
            other.GetComponent<FoxMovement>().isUnderwater = true;
        }
    }
}
