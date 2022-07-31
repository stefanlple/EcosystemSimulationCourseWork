using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsWindow : MonoBehaviour
{
    [SerializeField] GameObject statsWindow;
    [SerializeField] TextMeshProUGUI animalName;
    [SerializeField] Bars healthBar;
    [SerializeField] Bars hungerBar;
    [SerializeField] Bars thirstBar;
    [SerializeField] Bars reproductionDriveBar;
    [SerializeField] TextMeshProUGUI typeSpecificText;

    private Animal animal1;
    private bool updating = false;

    private void Update()
    {
        if(animal1 != null && !updating)
        {
            StartCoroutine(updateStats());
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            statsWindow.SetActive(false);
        }
    }

    public void init(Animal animal)
    {
        statsWindow.SetActive(true);
        animal1 = animal;
        
        this.healthBar.setMaxValue(animal1.health);
        this.hungerBar.setMaxValue(animal1.hunger);
        this.thirstBar.setMaxValue(animal1.thirst);
        this.reproductionDriveBar.setMaxValue(animal1.reproductionDrive);
    }

    IEnumerator updateStats()
    {
        updating = true;
        this.healthBar.setValue(animal1.currentHealth);
        this.hungerBar.setValue(animal1.currentHunger);
        this.thirstBar.setValue(animal1.currentThirst);
        this.reproductionDriveBar.setValue(animal1.currentHorny);

        if (animal1 is Fox)
        {
            foxStats();
        }
        else if (animal1 is Hare)
        {
            hareStats();
        }

        yield return new WaitForSeconds(1f);
        updating = false;
    }

    private void foxStats()
    {
        //TODO fox stats
        this.animalName.text = "Fuchs: " + animal1.animalName;
        Fox fox = (Fox)animal1;
        typeSpecificText.text = fox.getAnimalInfo();
    }
    
    private void hareStats()
    {
        //TODO hare stats
        this.animalName.text = "Hase: " + animal1.animalName;
        Hare hare = (Hare)animal1;
        typeSpecificText.text = hare.getAnimalInfo();
    }
}
