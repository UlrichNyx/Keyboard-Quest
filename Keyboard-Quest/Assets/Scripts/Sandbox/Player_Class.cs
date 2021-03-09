using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Class
{
    private string class_name;
    private string[] classes = {"Knight"};

    public Player_Class()
    {
        class_name = classes[0];
    }

    public string GetName()
    {
        return class_name;
    }
}
