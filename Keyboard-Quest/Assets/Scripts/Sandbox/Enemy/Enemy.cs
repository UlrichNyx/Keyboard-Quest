using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    [Range(0,9999)]
    public int goldDrop;
    [Range(0,9999)]
    public int expDrop;
    public Item[] itemDrops;
    public Sprite sprite;
    public AnimatorController animator;
    
    public enum EnemyType
    {
        shooter,
        slasher,
        defender
    }

    public EnemyType type;

    public bool isQuestTrigger;

    public void Move()
    {

    }

    public void Attack()
    {

    }
}
