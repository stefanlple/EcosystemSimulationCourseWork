using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


public class Fox : Animal
{
    private Movement movement;
    public int killedHares = 0;
    public int killRange = 4;

    void Start()
    {
        base.Start();
        lifeTime = 600;
        if (isChild)
        {
            StartCoroutine(grow());
        }
        if (generation == 0)
        {
            setGenerationZeroValues();
        }
        getGender();
        setRandomName();
        movement = gameObject.GetComponent<Movement>();
        setBar(ref currentHealth, health, healthBar);
        setBar(ref currentHunger, hunger, hungerBar);
        setBar(ref currentThirst, thirst, thirstBar);

        hornyBar.slider.maxValue = reproductionDrive;
        hornyBar.slider.value = 0;
        currentHorny = 0;
    }

    public void kill(GameObject hare)
    {
        hare.GetComponent<HareMovement>().agent.isStopped = true;
        hare.GetComponent<Hare>().isAlive = false;
        hare.GetComponent<Hare>().die(false, true);
    }

    public IEnumerator fillStomach(GameObject hare)
    {
        while (currentHunger < hunger)
        {
            GetComponent<FoxMovement>().isHunting = false;
            if (hare != null)
            {
                if (hare.GetComponent<Hare>().nutritionalValue == 1)
                {
                    break;
                }
                hare.GetComponent<Food>().nutritionalValue -= hungerGain;

            }
            currentHunger += hungerGain;

            yield return new WaitForSeconds(1.0f);
        }
        isEating = false;
        isHungry = false;
        movement.agent.isStopped = false;
        if (hare != null)
        {
            gameObject.GetComponent<FoxCollider>().preyList.Remove(hare.GetComponent<Animal>());
        }
    }

    public void eatHare(GameObject hare)
    {
        isEating = true;
        movement.agent.SetDestination(hare.transform.position);
        movement.agent.isStopped = true;
        StartCoroutine(fillStomach(hare));

    }
    void Update()
    {
        base.Update();
        TestInputs();
        drinkTimer += Time.deltaTime;
        sexTimer += Time.deltaTime;


        if (currentThirst < Mathf.Floor(thirst / 2))
        {
            isThirsty = true;
            isHungry = false;
        }
        if (currentHunger < Mathf.Floor(hunger / 2) && !isThirsty)
        {
            isHungry = true;
        }
        if (currentHorny == reproductionDrive)
        {
            isHorny = true;
        }
    }

    public override string getAnimalInfo()
    {
        StringBuilder sb = new StringBuilder("Infos: ", 50);
        string format = "{0}: {1}";
        sb.AppendLine();
        sb.AppendFormat(format, "Geschlecht", gender);
        sb.AppendLine();
        sb.AppendFormat(format, "Generation", generation);
        sb.AppendLine();
        sb.AppendFormat(format, "Geschwindigkeit", GetComponent<Movement>().normalSpeed);
        sb.AppendLine();
        sb.AppendFormat(format, "Sprintgeschwindigkeit", GetComponent<Movement>().sprintSpeed);

        sb.AppendLine();
        sb.AppendFormat(format, "Getoetete Hasen", killedHares);

        return sb.ToString();

    }
}


