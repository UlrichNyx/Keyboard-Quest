using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public Item[] drops;

    // Start is called before the first frame update
    void Start()
    {
        //Determine what will drop
    }

    // Update is called once per frame
    void Update()
    {
        if(this.HP <= 0)
        {
            // Give Exp
            // Drop items + currency
            // Despawn
        }
    }
}
