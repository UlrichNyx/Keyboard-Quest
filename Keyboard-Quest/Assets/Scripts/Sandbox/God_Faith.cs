using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God_Faith
{
    private string faith_name;
    private string[] faiths = {"Fire"};

    public God_Faith()
    {
        faith_name = faiths[0];
    }

    public string GetName()
    {
        return faith_name;
    }
}
