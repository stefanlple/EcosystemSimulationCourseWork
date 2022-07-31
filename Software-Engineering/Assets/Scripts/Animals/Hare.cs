using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


public class Hare : Animal
{
    public bool isInGrassArea;
    public List<Vector3> grassPositionList;

    public void addGrassToList(Collider col)
    {
        GameObject Grass = col.gameObject;
        Vector3 grassPosition = Grass.transform.position;
        if (!grassPositionList.Contains(grassPosition))
        {
            grassPositionList.Add(grassPosition);
        }
    }
    public Vector3 moveToNearestGrass()
    {
        //Default: Dichtestes Gras ist das, das er zuerst entdeckt hat
        float distanceToNearestGrass = Vector3.Distance(grassPositionList[0], transform.position);
        Vector3 nearestGrassPosition = grassPositionList[0];
        foreach (Vector3 grassPosition in grassPositionList)
        {
            float distanceToGrass = Mathf.Abs(Vector3.Distance(grassPosition, transform.position));
            //Dichtestes Gras finden
            if (distanceToGrass < distanceToNearestGrass)
            {
                distanceToNearestGrass = distanceToGrass;
                nearestGrassPosition = grassPosition;
            }
        }
        //Zum dichtesten bekannten Gras laufen
        return nearestGrassPosition;
    }

    public bool hasFoundGrass()
    {
        return grassPositionList.Count > 0;
    }


    public bool eatGrass()
    {
        isEating = true;
        if (eatTimer > 0.5f)
        {
            eat(GetComponent<Animal>().hungerGain);
            eatTimer = 0f;
            hungerBar.setValue(currentHunger);
        }
        if (currentHunger > hunger - 1)
        {
            isHungry = false;
            isEating = false;
        }
        return isEating;
    }

    void Start()
    {
        base.Start();
        lifeTime = 300;
        if (isChild)
        {
            StartCoroutine(grow());
        }

        getGender();

        if (generation == 0)
        {
            setGenerationZeroValues();
        }

        setRandomName();

        setBar(ref currentHealth, health, healthBar);
        setBar(ref currentHunger, hunger, hungerBar);
        setBar(ref currentThirst, thirst, thirstBar);

        hornyBar.slider.maxValue = reproductionDrive;
        hornyBar.slider.value = 0;
        currentHorny = 0;

    }

    void Update()
    {
        base.Update();

        eatTimer += Time.deltaTime;
        drinkTimer += Time.deltaTime;
        sexTimer += Time.deltaTime;

        if (currentHorny == reproductionDrive)
        {
            isHorny = true;
        }
        if (currentHunger < Mathf.Floor(hunger / 2))
        {
            isHungry = true;
        }
        if (currentThirst < Mathf.Floor(thirst / 2))
        {
            if (!isInGrassArea)
            {
                isThirsty = true;
            }

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
        return sb.ToString();

    }
}
