/* 
# Author: Filippos Kontogiannis
# Description: This script is used whenever a scene transition occurs
# Editors: ...
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // Necesary for working with scene transitions
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Animator transition; // The reference to the animator object of the screen-wide black box that covers the UI
    public float transitionTime = 1f; // The amount of time the transition should take

    public static LevelLoader instance; // The instance of the AudioManager object

    // This section is making sure that no other AudioManager instance exists
    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) // Temporary
        {
            //LoadNextLevel();
        }
    }

    IEnumerator Sleep()
    {
        transition.SetTrigger("Start"); // Play the animation
        yield return new WaitForSeconds(transitionTime);
    }

    public void LoadLevelNamed(string name, Vector3 position)
    {
        StartCoroutine(LoadNamedLevel(name, position)); // Load the next level in terms of indices
    }

    IEnumerator LoadNamedLevel(string name, Vector3 position)
    {
        transition.SetTrigger("Start"); // Play the animation
        yield return new WaitForSeconds(transitionTime); // Wait for transitionTime seconds
        SceneManager.LoadScene(name); // Load the actual scene
        FindObjectOfType<Player>().transform.position = position;
        transition.SetTrigger("End");
        
    }


    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)); // Load the next level in terms of indices
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start"); // Play the animation
        yield return new WaitForSeconds(transitionTime); // Wait for transitionTime seconds
        SceneManager.LoadScene(levelIndex); // Load the actual scene
    }
}

/* TODOS:

*/
