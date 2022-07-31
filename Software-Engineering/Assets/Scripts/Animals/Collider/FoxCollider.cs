using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxCollider : AnimalCollider
{
 
    public List<Animal> preyList;

    public void checkPreyList()
    {
        for(var i = preyList.Count - 1; i > -1; i--)
        {   
        if (preyList[i] == null)
        preyList.RemoveAt(i);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
         //if a fox enters the Sight of the hare, the hare add this Fox to his list of Foxes nearby
        if(col.tag == "Prey")
        {
            preyList.Add(col.GetComponent<Animal>());
        }

       if(  col.tag.Equals("Fox") 
        &&  col.GetComponent<Animal>().gender != GetComponent<Animal>().gender 
        &&  col.GetComponent<Animal>().isAlive
        && !col.GetComponent<Animal>().isChild)     
        {

            if(lowestDistanceAnimal(GetComponent<Animal>(), potentialSexPartnerList) != null){
                GetComponent<Movement>().closestSexPartnerAnimal = lowestDistanceAnimal(GetComponent<Animal>(), potentialSexPartnerList);   
            }
            potentialSexPartnerList.Add(col.GetComponent<Animal>());
        }
    }

    private void OnTriggerExit(Collider col)
    {  
        if(col.gameObject.tag == "Prey")
        {
            preyList.Remove(col.GetComponent<Animal>());
        }
         if(col.tag == "Fox"){
            if(GetComponent<AnimalCollider>().potentialSexPartnerList.Count == 0){
                GetComponent<Movement>().closestSexPartnerAnimal = null;
            }
             GetComponent<AnimalCollider>().potentialSexPartnerList.Remove(col.GetComponent<Animal>());
        }
    }
}
