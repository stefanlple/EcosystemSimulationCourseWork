using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuFox : MonoBehaviour
{
    public Slider slider;
    public static int fox;
    public TextMeshProUGUI foxNumber;
    
    // Update is called once per frame
    void Update()
    {
        fox = (int)slider.value;
        foxNumber.text= slider.value.ToString();

    }
}
