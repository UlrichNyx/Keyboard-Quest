using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string destinationSceneName;
    public Vector3 position;
    
    public void GoToDestination()
    {
        FindObjectOfType<LevelLoader>().GetComponent<LevelLoader>().LoadLevelNamed(destinationSceneName, position);
        
    }
}
