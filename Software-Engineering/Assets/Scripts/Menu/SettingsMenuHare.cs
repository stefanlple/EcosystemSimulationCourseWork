using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class SettingsMenuHare : MonoBehaviour
{
    public Slider slider;
    public static int hareNumber;
    public TextMeshProUGUI hareNumberText;
    // Update is called once per frame
    void Update()
    {
        hareNumber = (int)slider.value;
        hareNumberText.text = slider.value.ToString();        
    }
}
