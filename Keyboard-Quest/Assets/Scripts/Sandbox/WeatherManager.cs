using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Experimental.Rendering.Universal;

public class WeatherManager : MonoBehaviour
{
    public float currentTime = 1200;
    private float maxTime =  2400;
    //private float sunriseTime = 600;
    private float maxSunHeight = 1200;
    //private float sundownTime = 2100;
    private float midTime = 1200;
    private float hourTimer = 100;
    private string amPM = "PM";
    public Text timeIndicator;  
    public Light2D light;

    public Color afternoonColor;
    public Color nightColor;
    public Color dayColor;
    public Color currentColor;


    // Change global light color + position + intensity over time
    void Update()
    {
        currentColor = light.color;
        currentTime += Time.deltaTime;
        if(currentTime >= maxTime)
        {
            currentTime = 0;
        }
        if(currentTime <= maxSunHeight)
        {
            light.intensity = 1/((maxSunHeight / currentTime));
        }
        else
        {
            light.intensity = (maxTime - currentTime) / maxSunHeight;
        }


        if(currentTime > midTime && currentTime < maxTime)
        {
            amPM = "PM";
            
            if(currentTime < maxSunHeight + 600)
            {
                light.color = Color.Lerp(afternoonColor, dayColor, (maxTime - currentTime) / maxSunHeight);
            }
            else
            {
                light.color = Color.Lerp(nightColor, afternoonColor, (maxTime - currentTime) / maxSunHeight);
            }
            
        }
        else
        {
            amPM = "AM";
            light.color = Color.Lerp(nightColor, dayColor,  1/((maxSunHeight / currentTime)));
        }
        timeIndicator.text = "Time: " + Math.Floor(currentTime / hourTimer).ToString() + " " + amPM;
    }
}
