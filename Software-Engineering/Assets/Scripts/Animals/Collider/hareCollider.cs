using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HareCollider : AnimalCollider
{
  
    public List<GameObject> foxList;

    private void OnTriggerEnter(Collider col)
    {
        //if a fox enters the Sight of the hare, the hare add this Fox to his list of Foxes nearby
        if (col.tag == "Fox")
        {
            GetComponent<HareMovement>().inDanger = true;
            GetComponent<HareMovement>().closestFox = col.gameObject;
            foxList.Add(GetComponent<HareMovement>().closestFox);
        }
        if (col.tag == "Prey"
            && col.GetComponent<Hare>().gender != gameObject.GetComponent<Hare>().gender
            && col.GetComponent<Animal>().isAlive
            && !col.GetComponent<Animal>().isChild)
        {
            potentialSexPartnerList.Add(col.GetComponent<Animal>());
        }

        if (col.tag == "Grass")
        {
            GetComponent<HareMovement>().hare.addGrassToList(col);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Fox")
        {
            foxList.Remove(col.gameObject);

            //set isFleeing to false when there is no fox around
            if (foxList.Count == 0)
            {
                GetComponent<HareMovement>().agent.speed = GetComponent<Movement>().normalSpeed;
                GetComponent<HareMovement>().agent.acceleration = GetComponent<Movement>().normalSpeed;
                GetComponent<HareMovement>().inDanger = false;
                GetComponent<HareMovement>().isFleeing = false;
            }
        }
        if (col.tag == "Prey")
        {
            if (GetComponent<AnimalCollider>().potentialSexPartnerList.Count == 0)
            {
                GetComponent<Movement>().closestSexPartnerAnimal = null;
            }
            GetComponent<AnimalCollider>().potentialSexPartnerList.Remove(col.GetComponent<Animal>());
        }


    }

}
