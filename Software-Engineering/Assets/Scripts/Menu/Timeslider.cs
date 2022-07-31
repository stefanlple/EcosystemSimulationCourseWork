using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timeslider : MonoBehaviour
{
    public Slider slider;
    public static float timef;
    public TextMeshProUGUI time;
   

    // Update is called once per frame
    void Update()
    {
        timef = (float)slider.value;
        time.text = slider.value.ToString();
    }
}
