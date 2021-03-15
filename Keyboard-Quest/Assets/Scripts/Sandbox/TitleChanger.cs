using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleChanger : MonoBehaviour
{
    private GameObject player;
    private Text text;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        text = GetComponent<Text>();
        text.enabled = false;
        timer = -4f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x < 0 && timer < 0 || timer < 0 && text.enabled)
        {
            text.enabled = true;
            StartCoroutine(FadeTextToFullAlpha(10, text));
            timer += Time.deltaTime;
        }
        else if(timer < 4 && timer >= 0)
        {
            StartCoroutine(FadeTextToZeroAlpha(10, text));
            timer += Time.deltaTime;
        }
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
 
    public IEnumerator FadeTextToZeroAlpha(float t, Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
