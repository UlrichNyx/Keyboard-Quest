/* 
# Author: Filippos Kontogiannis
# Description: The script that is responsible for the title card that appears every time the player enters a new zone
# Editors: ...
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necessary import for working with UI

public class TitleManager : MonoBehaviour // This script is typically attached to a AreaTitle object in the UI Canvas
{
    private GameObject player; // The reference to the player
    private Text text; // The reference to the text gameobject in the UI
    private float timer; // A local timer the determines the duration of the fade-in to fade-out

    // Start is called before the first frame update
    void Start()
    {
        // Initialize values and fetch references
        player = GameObject.Find("Player"); 
        text = GetComponent<Text>(); 
        text.enabled = false;
        // Start from -4 so that when the timer reaches 0 the fade out takes precedence
        timer = -4f;
        // Set the starting alpha to 0 so that the fade in works properly
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // If the player has reached the desired position and the timer has not started or if the timer has started and the text is enabled 
        if(player.transform.position.x < 0 && timer < 0 || timer < 0 && text.enabled)
        {
            text.enabled = true;
            StartCoroutine(FadeTextToFullAlpha(10, text)); // Start fading in
            timer += Time.deltaTime;
        }
        // Otherwise, if the time for the fade in is over, start fading out
        else if(timer < 4 && timer >= 0)
        {
            StartCoroutine(FadeTextToZeroAlpha(10, text));
            timer += Time.deltaTime;
        }
    }

    // These two functions were borrowed from https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/

    public IEnumerator FadeTextToFullAlpha(float t, Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / t));
            yield return null; // Essentially, this function is called every frame and thus incrementally updates the alpha over time t
        }
    }
 
    public IEnumerator FadeTextToZeroAlpha(float t, Text text) // Fade Text to nothing
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}

/* TODOS:

*/
