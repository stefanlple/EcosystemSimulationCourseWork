using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoxMovement : Movement
{
    public Fox fox;
    private Vector3 nearestHarePosition;
    private Vector3 huntDirection;
    private float distanceToPrey;
    public bool isHunting = false;
    public bool isUnderwater;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fox = GetComponent<Fox>();
        agent = GetComponent<NavMeshAgent>();
        rangeToHaveSex = 3;
    }

    private void Update()
    {
        stillHunting();
        if (isUnderwater)
        {
            StartCoroutine(getOutOfWater());
        }
        //HUNTING
        if (fox.isHungry && !fox.isEating && !isUnderwater && GameManager.haresAlive > 0)
        {
            if (GetComponent<FoxCollider>().preyList.Count > 0)
            {
                isWandering = false;
                hunt();
            }
        }
        //DRINKING
        if (fox.isThirsty && !isHunting && !fox.isEating)
        {
            agent.SetDestination(fox.waterPosition);
            if (fox.isInWaterArea)
            {
                agent.isStopped = fox.drinkWater();
            }
        }
        //HORNY
        if (fox.isHorny && !fox.isHungry && !fox.isThirsty && !fox.isUnderwater)
        {
            reproduce();
        }

        if (!isWandering && !isHunting && !fox.isEating && !isUnderwater && !fox.isDrinking)
        {
            StartCoroutine(setWanderDestination());
        }

    }

    public Vector3 runToHare(Vector3 foxPosition, Animal hare)
    {
        //Look for nearest Fox
        if (hare != null)
        {
            Vector3 dirToHare = foxPosition - hare.transform.position;
            // Escape direction
            huntDirection = foxPosition - (dirToHare).normalized;
        }
        return huntDirection;

    }

    private void stillHunting(){
         // is there anything to hunt?
            if (GetComponent<FoxCollider>().preyList.Count == 0)
            {
                isHunting = false;
            }
    }
    private void hunt()
    {

        isHunting = true;
        agent.speed = sprintSpeed;
        agent.acceleration = sprintSpeed;

        try
        {
            Vector3 foxPosition = transform.position;

            //get the distance to the nearest fox
            //setLowestDistanceHare(foxPosition);

            Animal closestHare = GetComponent<AnimalCollider>().lowestDistanceAnimal(fox, GetComponent<FoxCollider>().preyList);

            // If the Hunted Animal is allready dead -> stop hunting and remove it from preyList
            if (closestHare != null && closestHare.isAlive == false)
            {
                GetComponent<FoxCollider>().preyList.Remove(closestHare);
                isHunting = false;
            }

            // is there anything to hunt?
            if (GetComponent<FoxCollider>().preyList.Count == 0)
            {
                isHunting = false;
            }

            if (closestHare != null)
            {
                nearestHarePosition = closestHare.transform.position;
            }

            if (nearestHarePosition == transform.position)
            {
                isHunting = false;
            }
            distanceToPrey = Vector3.Distance(foxPosition, nearestHarePosition);
            //Debug.DrawLine(foxPosition, nearestHarePosition, Color.black);
            if (distanceToPrey < fox.killRange)
            {
                isHunting = false;

                if (closestHare != null && closestHare.isAlive)
                {
                    fox.kill(closestHare.gameObject);
                    //StartCoroutine(runToDeadHare());
                    // TODO hier laufe zum hasen einfuegen
                    fox.eatHare(closestHare.gameObject);
                }


            }
            //Tell Agent where to go
            agent.SetDestination(runToHare(foxPosition, closestHare));
        }
        catch (MissingReferenceException)
        {
            //Debug.LogException(e,this);
        }
        finally
        {
            //check if there are any missing GameObjects in preyList and remove them
            gameObject.GetComponent<FoxCollider>().checkPreyList();
        }

    }

    IEnumerator getOutOfWater()
    {
        if (fox.transform.position.z > 71)
        {
            if (fox.transform.position.x > 43)
            {
                //Fuchs ist im rechten oberen Viertel
                agent.SetDestination(new Vector3(100f, 0f, 100f));
            }
            else
            {
                //Fuchs ist im linken oberen Viertel
                agent.SetDestination(new Vector3(0f, 0f, 100f));
            }
        }
        else
        {
            if (fox.transform.position.x > 43)
            {
                //Fuchs ist im rechten unteren Viertel
                agent.SetDestination(new Vector3(100f, 0f, 0f));
            }
            else
            {
                //Fuchs ist im linken unteren Viertel
                agent.SetDestination(new Vector3(0f, 0f, 0f));
            }
        }

        yield return new WaitForSeconds(2.0f);
        isUnderwater = false;
    }
}


