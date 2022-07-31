using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    Hare hare;
    Fox fox;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prey")
        {
            hare = other.GetComponent<Hare>();
            hare.isInWaterArea = true;
        }

        if (other.tag == "Fox")
        {
            fox = other.GetComponent<Fox>();
            fox.isInWaterArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Prey")
        {
            hare = other.GetComponent<Hare>();
            hare.isInWaterArea = false;
        }

        if (other.tag == "Fox")
        {
            fox = other.GetComponent<Fox>();
            fox.isInWaterArea = false;
        }
    }
}
