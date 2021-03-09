using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Player_Class player_class;
    private int level;
    public Stats stats;
    //private God_Faith god_faith;

    void Start()
    {
        player_class = new Player_Class();
        level = 1;
        stats = new Stats(player_class, level);
    }

    void Update()
    {
        
    }
}
