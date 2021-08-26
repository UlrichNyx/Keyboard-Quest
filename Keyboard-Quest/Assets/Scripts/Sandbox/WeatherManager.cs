using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Experimental.Rendering.Universal;

public class WeatherManager : MonoBehaviour
{
    public float currentTime = 0;
    private float maxTime =  2400;
    //private float sunriseTime = 600;
    private float maxSunHeight = 1200;
    //private float sundownTime = 2100;
    private float midTime = 1200;
    private float hourTimer = 100;
    private string amPM = "AM";
    public Text timeIndicator;  
    public Light2D light;


    // Change global light color + position + intensity over time
    void Update()
    {
        currentTime += Time.deltaTime * 500;
        if(currentTime >= maxTime)
        {
            currentTime = 0;
        }
        if(currentTime < maxSunHeight)
        {
            light.intensity = 1/((maxSunHeight / currentTime));
            //light.color = new Color();
        }
        else
        {
            //light.intensity = maxSunHeight / currentTime;
            light.intensity = (maxTime - currentTime) / maxSunHeight;
        }


        if(currentTime > midTime && currentTime < maxTime)
        {
            amPM = "PM";
        }
        else
        {
            amPM = "AM";
        }
        timeIndicator.text = "Time: " + Math.Floor(currentTime / hourTimer).ToString() + " " + amPM;
    }
}
