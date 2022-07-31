using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalCollider : MonoBehaviour
{
    public List<Animal> potentialSexPartnerList;
    public void removeMissingObjectsFromAnimalList(List<Animal> animalList)
    {
        for (var i = animalList.Count - 1; i > -1; i--)
        {
            if (animalList[i] == null)
                animalList.RemoveAt(i);
        }
    }

    public Animal lowestDistanceAnimal(Animal self, List<Animal> animalList)
    {
        Vector3 selfPosition = self.transform.position;
        float distanceToOther;
        float lowestDistance = 100;
        Animal closestAnimal = null;

        if (animalList != null)
        {
            foreach (Animal animal in animalList)
            {
                if (animal != null)
                {
                    Vector3 otherAnimalPosition = animal.transform.position;
                    distanceToOther = Vector3.Distance(selfPosition, otherAnimalPosition);
                    if (distanceToOther < lowestDistance)
                    {
                        closestAnimal = animal;
                        lowestDistance = distanceToOther;
                    }
                }

            }
            return closestAnimal;
        }
        return null;
    }

}
