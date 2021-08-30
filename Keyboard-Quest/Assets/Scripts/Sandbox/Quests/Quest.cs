using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{

    public string questName;
    [TextArea(3,10)]
    public string questDescription;
    public enum Requirement
    {
        slay,
        obtain,
        escort
    }
    [Range(0,9999)]
    public int goldReward;
    public Item[] itemRewards;
    public Requirement requirement;
    [HideInInspector]
    public Enemy enemyToSlay;
    [HideInInspector]
    public Item itemToObtain;
    //[HideInInspector]
    //public Enemy npcToEscort;
    
}
