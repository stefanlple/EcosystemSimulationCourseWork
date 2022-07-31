using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : Food
{
    Hare hare;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Prey")
        {
            hare = other.GetComponent<Hare>();
            hare.isInGrassArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Prey")
        {
            hare = other.GetComponent<Hare>();
            hare.isInGrassArea = false;
            hare.isEating = false;
        }
    }
}
