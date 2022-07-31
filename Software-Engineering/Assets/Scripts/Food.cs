using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int nutritionalValue;

    private void despawn()
    {
        Destroy(gameObject);
    }

    //Zaehlt den Naehrwert pro Sekunde um 1 runter und despawnt dann das GameObject
    public IEnumerator decreaseNutritionalValue()
    {
        while (nutritionalValue > 0)
        {
            nutritionalValue--;
            yield return new WaitForSeconds(1.0f);
        }
        despawn();
    }

}
