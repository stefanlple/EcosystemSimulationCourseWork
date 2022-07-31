using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Collections;

public class HareMovement : Movement
{

    //reference to the hare itself
    public Hare hare;

    public GameObject closestFox;

    public Hare closestSexPartner;
    public bool inDanger = false;
    public bool isFleeing = false;

    private void Start()
    {
        rangeToHaveSex = 2;
        isWandering = false;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        hare = GetComponent<Hare>();
    }

    private void Update()
    {
        if (hare.isAlive)
        {
            if (inDanger)
            {
                escape();
            }
            else
            {
                //HUNGRY
                if (hare.isHungry && hare.hasFoundGrass() && !isFleeing && !hare.isDrinking && !hare.isUnderwater && !hare.isThirsty)
                {
                    agent.SetDestination(hare.moveToNearestGrass());
                    if (hare.isInGrassArea)
                    {
                        agent.isStopped = hare.eatGrass();
                    }
                }
                //THIRSY
                if (hare.isThirsty && !isFleeing && !hare.isEating && !hare.isUnderwater)
                {
                    agent.SetDestination(hare.waterPosition);
                    if (hare.isInWaterArea)
                    {
                        agent.isStopped = hare.drinkWater();
                    }
                }
                //DROWNING
                if (hare.isUnderwater)
                {
                    StartCoroutine(getOutOfWater());
                }


                if (hare.isChild && hare.isAlive)
                {
                    if (GetComponent<Animal>().myFather != null)
                    {
                        if(agent.isActiveAndEnabled){
                            agent.SetDestination(GetComponent<Animal>().myFather ? GetComponent<Animal>().myFather.transform.position  : transform.position);
                        }
                        
                    }

                }

                //HORNY
                if (hare.isHorny && !isFleeing && !hare.isUnderwater)
                {
                    reproduce();
                }
                //WANDER
                if (!isWandering && !isFleeing && !hare.isUnderwater && !hare.isHungry && !hare.isThirsty)
                {
                    StartCoroutine(setWanderDestination());
                }
                GetComponent<Animal>().TestInputs();
            }
        }

    }


    public void setLowestDistanceFox(Vector3 harePosition)
    {
        List<GameObject> foxList = GetComponent<HareCollider>().foxList;
        float _distanceToFox;
        float lowestDistance = 100;
        foreach (GameObject fox in foxList)
        {
            Vector3 foxPosition = fox.transform.position;
            _distanceToFox = Vector3.Distance(harePosition, foxPosition);

            if (_distanceToFox < lowestDistance)
            {
                //Der Fox der am dichtesten ist wird zum gameObject Fox vor dem der Hase wegrennt
                closestFox = fox;
                lowestDistance = _distanceToFox;
            }
        }
    }

    private void escape()
    {
        isFleeing = true;

        agent.isStopped = false;
        hare.isHavingAReallyGoodTime = false;
        hare.isHorny = false;
        hare.isLookingForSex = false;
        hare.isDrinking = false;
        hare.isEating = false;

        agent.speed = sprintSpeed;
        agent.acceleration = sprintSpeed;
        //the direction in wich the hare is fleeing if a Fox is around
        Vector3 fleeDirection;
        Vector3 harePosition = transform.position;

        //set the lowest distance Fox in range as the Fox too flee from
        try
        {
            setLowestDistanceFox(harePosition);

            //in which direction is the nearest Fox?
            Vector3 dirToFox = harePosition - closestFox.transform.position;
           
            // Escape direction
            fleeDirection = harePosition + (dirToFox).normalized;
           

            //Tell Agent where to go  
            agent.SetDestination(fleeDirection);
        }
        catch (MissingReferenceException)
        {
        }
    }



    IEnumerator getOutOfWater()
    {
        if (transform.position.z > 71)
        {
            if (transform.position.x > 43)
            {
                //Hase ist im rechten oberen Viertel
                GetComponent<Movement>().agent.SetDestination(new Vector3(100f, 0f, 100f));
            }
            else
            {
                //Hase ist im linken oberen Viertel
                GetComponent<Movement>().agent.SetDestination(new Vector3(0f, 0f, 100f));
            }
        }
        else
        {
            if (transform.position.x > 43)
            {
                //Hase ist im rechten unteren Viertel
                GetComponent<Movement>().agent.SetDestination(new Vector3(100f, 0f, 0f));
            }
            else
            {
                //Hase ist im linken unteren Viertel
                GetComponent<Movement>().agent.SetDestination(new Vector3(0f, 0f, 0f));
            }
        }

        yield return new WaitForSeconds(2.0f);
        hare.isUnderwater = false;
    }
}
