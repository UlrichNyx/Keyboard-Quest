﻿/* 
# Author: Filippos Kontogiannis
# Description: The class that all entities that have stats inherit from
# Editors: ...
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class defines the basic values of each entity
public class Entity : MonoBehaviour
{
    [Range(1,100)]
    public int level; // The level of the entity (MAX 100)
    [Range(0,999)]
    public int hp; // The hp of the entity (MAX 999), this value depends on the resilience stat
    [Range(0,100)]
    public int mana; // The mana of the entity (MAX 100), this value depends on the faith stat
    [Range(0,1000)]
    public int exp; // The experience of the entity that is needed to reach the next level (MAX 1000)
    public Stats stats; // The combat related stats of the entity (As seen in the Stats class)
}

/* TODOS:

*/