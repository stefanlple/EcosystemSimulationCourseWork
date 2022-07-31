using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public float range = 10.0f;
    protected float rangeToHaveSex;
    public bool isWandering = false;
    public int normalSpeed = 2;
    public int sprintSpeed = 8;
    public NavMeshAgent agent;
    protected Rigidbody rb;
    public Animal closestSexPartnerAnimal;



    public IEnumerator setWanderDestination()
    {
        isWandering = true;
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            agent.destination = hit.position;
            yield return new WaitForSeconds(3f);
        }
        isWandering = false;
    }

    protected void reproduce()
    {
        GetComponent<Animal>().isLookingForSex = true;
        GetComponent<AnimalCollider>().removeMissingObjectsFromAnimalList(GetComponent<AnimalCollider>().potentialSexPartnerList);
        //meine position
        Vector3 thisPosition = transform.position;

        //welches ist der naechste hase
        closestSexPartnerAnimal = GetComponent<AnimalCollider>().lowestDistanceAnimal(GetComponent<Animal>(), GetComponent<AnimalCollider>().potentialSexPartnerList);
        if (closestSexPartnerAnimal != null)
        {
            if (Vector3.Distance(thisPosition, closestSexPartnerAnimal.transform.position) < rangeToHaveSex             // in range of a potential sex Partner
                                && GetComponent<Animal>().gender.Equals("male")                                         // the active hare is male
                                && closestSexPartnerAnimal.GetComponent<Animal>().isHorny                               // the target hare isHorny
                                && !closestSexPartnerAnimal.GetComponent<Animal>().isPregnant                           // the target hare is NOT pregnant
                                && closestSexPartnerAnimal.GetComponent<Movement>().closestSexPartnerAnimal == GetComponent<Animal>()     //the target of my closest sex partner is me
                                )
            {
                GetComponent<Animal>().GetComponentInChildren<ParticleSystem>().Play();
                closestSexPartnerAnimal.GetComponentInChildren<ParticleSystem>().Play();
                GetComponent<Animal>().isLookingForSex = false;
                agent.isStopped = GetComponent<Animal>().isHavingFun();                                       // start to have sex
                //Debug.Log("IM HAVING A REALLY GOOD TIME");
            }

            if (closestSexPartnerAnimal.GetComponent<Animal>().isPregnant)
            {                                                                                   // if the target is allready pregnant:
                GetComponent<AnimalCollider>().potentialSexPartnerList.Remove(closestSexPartnerAnimal); // remove it from potentialSexPartnerList
            }
            else
            {
                agent.SetDestination(closestSexPartnerAnimal.transform.position);                     // else: run to the closestSexPartner
            }

        }
    }
}
