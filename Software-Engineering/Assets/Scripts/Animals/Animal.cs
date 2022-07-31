using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : Food
{

    public int health;
    public int hunger;
    public int thirst;
    public int reproductionDrive;
    public int generation = 0;
    public string gender;
    public bool isAlive = true;
    public bool isEating;
    public bool isHungry;
    public bool isThirsty;
    public bool isDrinking;
    public bool isInWaterArea;
    public bool isUnderwater;
    public bool isHorny;
    public bool isPregnant;
    public bool isLookingForSex;
    public bool isHavingAReallyGoodTime;
    public bool isChild;
   

    public Vector3 waterPosition = new Vector3(43.8f, 1.3f, 71.4f);

    //BARS Start:
    public int currentHealth;
    public int currentHunger;
    public int currentThirst;
    public int currentHorny;

    public int hungerLoss;
    public int thirstLoss;
    public int hornyLoss;

    public int hungerGain;
    public int thirstGain;


    public Bars healthBar;
    public Bars hungerBar;
    public Bars thirstBar;
    public Bars hornyBar;
    protected bool updatingBars = false;

    public float timePassed = 0f;
    public float eatTimer = 0f;
    public float drinkTimer = 0f;
    public float sexTimer = 0f;
    public float lifeTime;
    bool lifeTimeOver = false;


    public GameObject babyPrefab; //Prefab vom Hare (manuell über die grafische Oberfläche reinziehen)
    public Animal myFather;
    

    public string animalName;
    public string[] namesMale = { "Bob", "Dave", "Ted", "Marvin", "Oscar", "Victor" };
    private string[] namesFemale = { "Alice", "Carol", "Eve", "Mallory", "Peggy", "Trudy" };


    protected void Start()
    { 
        //FUCHS
        if (GetComponent<Hare>() != null)
        {
            if (generation > GameManager.hareGeneration)
            {
                GameManager.hareGeneration += 1;
            }
        }
        else
        {
            if (generation > GameManager.foxGeneration)
            {
                GameManager.foxGeneration += 1;
            }
        }
    }

    protected void Update()
    {
        if (!updatingBars)
        {
            StartCoroutine(updateBars());
            stillHorny();
        }
        timePassed += Time.deltaTime;
        if(timePassed > lifeTime && !isHavingAReallyGoodTime){
            lifeTimeOver = true;
            die(false, false);
        }
    }

    protected void setRandomName()
    {
        if (gender.Equals("male"))
        {
            animalName = namesMale[Random.Range(0, namesMale.Length)];
        }
        else
        {
            animalName = namesFemale[Random.Range(0, namesFemale.Length)];
        }
        this.name = this.tag + "_" + animalName;
    }

    public void changeBar(Bars bar, int damage, ref int currentNumber, string operations)
    {
        currentNumber = operations.Equals("plus") ? currentNumber += damage : currentNumber -= damage;
        bar.setValue(currentNumber);
        if (currentNumber < 0)
        {
            currentNumber = 0;
        }
    }

    public void setBar(ref int currentNumber, int number, Bars bar)
    {
        currentNumber = number;
        bar.setMaxValue(number);
    }

    protected IEnumerator updateBars()
    {
        updatingBars = true;
        if (thirstBar.slider.value == 0)
        {
            changeBar(healthBar, 20, ref currentHealth, "minus");
        }
        if (hungerBar.slider.value == 0)
        {
            changeBar(healthBar, 20, ref currentHealth, "minus");
        }
        if (healthBar.slider.value == 0)
        {
            die(false,false);
        }

        changeBar(hungerBar, hungerLoss, ref currentHunger, "minus");
        changeBar(thirstBar, thirstLoss, ref currentThirst, "minus");

        if (!isPregnant && !isChild && currentHorny < reproductionDrive && !isHungry && !isThirsty)
        {
            changeBar(hornyBar, 1, ref currentHorny, "plus");
        }
        yield return new WaitForSeconds(3f);
        updatingBars = false;
    }

    public void eat(int food)
    {
        currentHunger += food;
    }
    public void drink(int water)
    {
        currentThirst += water;
    }
    public void haveSex(int endurance)
    {
        currentHorny -= endurance;
    }
    
    public int mutate()
    {
        int mutation = Random.Range(0, 100);
        return mutation > 5 ? 0 : mutation > 2 ? Random.Range(1, 5) : Random.Range(-4, 0);
    }

    private Animal setBabyValues(Animal male, Animal female, GameObject child)
    {
        Animal baby = child.GetComponent<Animal>();
        baby.myFather = male;

        baby.generation = male.generation >= female.generation ? male.generation + 1 : female.generation + 1;
        baby.isPregnant = false;
        baby.isChild = true;

        baby.health = (male.health + female.health) / 2 + mutate() < 1 ? 1 : (male.health + female.health) / 2 + mutate();
        baby.hunger = (male.hunger + female.hunger) / 2 + mutate() < 1 ? 1 : (male.hunger + female.hunger) / 2 + mutate();
        baby.thirst = (male.thirst + female.thirst) / 2 + mutate() < 1 ? 1 : (male.thirst + female.thirst) / 2 + mutate();
        baby.reproductionDrive =  (male.reproductionDrive + female.reproductionDrive) / 2 + mutate() < 1 ? 1 : (male.reproductionDrive + female.reproductionDrive) / 2 + mutate();

        baby.GetComponent<Movement>().normalSpeed = (male.GetComponent<Movement>().normalSpeed + female.GetComponent<Movement>().normalSpeed) / 2 + mutate() < 1 ? 1 : (male.GetComponent<Movement>().normalSpeed + female.GetComponent<Movement>().normalSpeed) / 2 + mutate();
        baby.GetComponent<Movement>().sprintSpeed = (male.GetComponent<Movement>().sprintSpeed + female.GetComponent<Movement>().sprintSpeed) / 2 + mutate() < 1 ? 1 : (male.GetComponent<Movement>().sprintSpeed + female.GetComponent<Movement>().sprintSpeed);

        baby.hungerLoss = (male.hungerLoss + female.hungerLoss) / 2 + mutate() < 1 ? 1 : (male.hungerLoss + female.hungerLoss) / 2 + mutate();
        baby.thirstLoss = (male.thirstLoss + female.thirstLoss) / 2 + mutate() < 1 ? 1 : (male.thirstLoss + female.thirstLoss) / 2 + mutate();
        baby.hornyLoss = (male.hornyLoss + female.hornyLoss) / 2 + mutate() < 1 ? 1 : (male.hornyLoss + female.hornyLoss) / 2 + mutate();

        baby.hungerGain = (male.hungerGain + female.hungerGain) / 2 + mutate() < 1 ? 1 : (male.hungerGain + female.hungerGain) / 2 + mutate();
        baby.thirstGain = (male.thirstGain + female.thirstGain) / 2 + mutate() < 1 ? 1 : (male.thirstGain + female.thirstGain) / 2 + mutate();

        baby.GetComponentInChildren<SphereCollider>().radius = (male.GetComponentInChildren<SphereCollider>().radius + female.GetComponentInChildren<SphereCollider>().radius) / 2 + mutate() < 1 ? 1 : (male.GetComponentInChildren<SphereCollider>().radius + female.GetComponentInChildren<SphereCollider>().radius) / 2 + mutate();

        baby.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        return baby;
    }

    protected IEnumerator grow()
    {
        yield return new WaitForSeconds(45);
        transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
        isChild = false;
    }
    public bool isHavingFun()
    {
        Animal male = this;
        Animal myFemale = GetComponent<Movement>().closestSexPartnerAnimal;
        isHavingAReallyGoodTime = true;
        myFemale.isHavingAReallyGoodTime = true;
        myFemale.GetComponent<Movement>().agent.isStopped = true;

        if (sexTimer > 0.5f)
        {
            haveSex(hornyLoss);
            myFemale.haveSex(hornyLoss);

            sexTimer = 0f;

            hornyBar.setValue(currentHorny);
            myFemale.hornyBar.setMaxValue(currentHorny);
        }

        if (currentHorny <= 0)
        {
            
            isHavingAReallyGoodTime = false;
            myFemale.isHavingAReallyGoodTime = false;

            myFemale.GetComponent<Movement>().agent.isStopped = false;


            currentHorny = 0;
            myFemale.currentHorny = 0;

            isHorny = false;
            myFemale.isHorny = false;

            myFemale.isPregnant = true; 
            myFemale.pregnancy(male, myFemale);
        }
        return isHavingAReallyGoodTime;
    }

  
    IEnumerator spawnChild(Animal male, Animal female)
    {
        Vector3 pos = female.transform.position;

        int minChildren;
        int maxChildren;
        if(GetComponent<Hare>() != null){
            minChildren = 5;
            maxChildren = 10;
        }else{
            minChildren = 1;
            maxChildren = 4;
        }
        int childCounter = Random.Range(minChildren, maxChildren); //Random Integer zwischen 1 und 3, der die Anzahl der zu spawnenden Kinder angibt
        yield return new WaitForSeconds(5f);

        GetComponent<Movement>().agent.isStopped = true;
        for (int i = 1; i <= childCounter; i++) //Entsprechende Anzahl von Kindern wird gespawnt
        {
            if(male != null && female != null){
                Instantiate(setBabyValues(male, female, babyPrefab), pos + new Vector3(i, 0, i), Quaternion.identity);
            }
        }

        isPregnant = false;

        GetComponent<Movement>().agent.isStopped = false;
    }

    public void pregnancy(Animal male, Animal female)
    {
        StartCoroutine(spawnChild(male, female));
    }

    public bool drinkWater()
    {
        isDrinking = true;
        if (drinkTimer > 0.5f)
        {
            drink(thirstGain);
            drinkTimer = 0f;
            thirstBar.setValue(currentThirst);
        }
        if (currentThirst > thirst - 1)
        {
            isThirsty = false;
            isDrinking = false;
        }
        return isDrinking;
    }

    protected void setGenerationZeroValues()
    {

        GetComponentInChildren<SphereCollider>().radius = Random.Range(5, 10);
        health = Random.Range(100, 200);
        hunger = Random.Range(100, 200);
        thirst = Random.Range(100, 200);
        reproductionDrive = Random.Range(5, 11);

        hungerLoss = Random.Range(5, 11);
        thirstLoss = Random.Range(5, 11);
        hornyLoss = Random.Range(1, 5);

        hungerGain = Random.Range(16, 21);
        thirstGain = Random.Range(16, 21);

        GetComponent<Movement>().normalSpeed = Random.Range(1,4);
        GetComponent<Movement>().sprintSpeed = Random.Range(4,10);
    }

    

    //Wenn der Methode "true" �bergeben wird, so verschwindet die Leiche, nachdem die Sterbeanimation durchgelaufen ist
    //Andernfalls bleibt sie Liegen (z.B. falls sie noch gefressen werden soll) und verliert pro Sekunde einen N�hrwertpunkt
    public void die(bool instantDespawn, bool killed)
    {
        GetComponent<Movement>().agent.isStopped = true;

        if (instantDespawn)
        {
            Destroy(this.gameObject, 5f);
        }
        else
        {
            StartCoroutine(decreaseNutritionalValue());
        }
        isAlive = false;

        if (GetComponent<Fox>() != null)
        {
            if (!lifeTimeOver)
            {
                GameManager.foxesStarved += 1;
            }

        }
        else
        { 
            if(!killed && !lifeTimeOver){
                GameManager.haresStarved += 1;
            }else if(!lifeTimeOver){
                GameManager.haresKilled += 1;
            }
        
        }

    }

    public abstract string getAnimalInfo();

    public void TestInputs()
    {
            
        if (Input.GetKeyDown("h"))
        {
            currentHunger = 10;
        }
        if (Input.GetKeyDown("t"))
        {
            currentHunger += 100;
        }
        if (Input.GetKeyDown("k"))
        {
            GetComponent<Movement>().agent.isStopped = true;
            die(false, false);
        }
        if (Input.GetKeyDown("l"))
        {
            currentThirst = 10;
        }
    }

    protected string getGender()
    {
        gender = Random.Range(0, 2) == 1 ? "male" : "female";
        return gender;
    }

    void stillHorny()
    {
        if (isHungry || isThirsty)
        {
            currentHorny = currentHorny - 1;
            isHorny = false;
            isLookingForSex = false;
            isHavingAReallyGoodTime = false;
            hornyBar.slider.value = hornyBar.slider.value - 1;
        }
    }
}
