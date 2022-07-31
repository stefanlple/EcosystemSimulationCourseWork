using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalUI : MonoBehaviour
{

    GameObject statsWindow;
    [SerializeField] Animal animal;

    private void Awake()
    {
        statsWindow = GameObject.FindGameObjectWithTag("StatsPanel");
    }

    private void OnMouseDown()
    {
        statsWindow.GetComponent<StatsWindow>().init(animal);
    }
}
