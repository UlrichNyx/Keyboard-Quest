using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Question
{
    public string optionA;
    public UnityEvent functionA;

    public string optionB;
    public UnityEvent functionB;
}
