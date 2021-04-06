/* 
# Author: Filippos Kontogiannis
# Description: The script that is responsible for managing all sounds in the game
# Editors: ...
*/

using UnityEngine.Audio;
using System;
using UnityEngine;

// This class was borrowed from Brackeys on Youtube: https://www.youtube.com/watch?v=6OT43pvUyfY

// This script should always be attached to the unique AudioManager object
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; // The list of known sounds
    public static AudioManager instance; // The instance of the AudioManager object

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
        DontDestroyOnLoad(gameObject); // Don't destroy the AudioManager instance when moving from scene to scene (to continue music etc)
        foreach (Sound s in sounds) // For every sound in the known sounds
        {
            s.source = gameObject.AddComponent<AudioSource>(); // Create an audio source component with the following parameters:
            s.source.clip  = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        //Play("DeepForest"); // When the game starts, play the sound known as DeepForest
    }
        
    // This function should be called whenever a sound should be played
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // Find the mentioned name of the sound in the valid sounds list
        if(s == null)
        {
            Debug.LogWarning(("Sound: " + name + "not found!")); // If the name is not found then throw a warning and return
            return;
        }
        if(!s.source.isPlaying)
        {
            s.source.Play(); // Otherwise, if the sound is not playing already, play the sound again (this is to prevent spamming of sounds)
        }
    }

    // This function should be used whenever a sound should be stopped (usually this applies to background music)
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        if(s.source.isPlaying)
        {
            s.source.Stop(); // This function is almost identical to the above, except the sound is stopped if its playing
        }
        
    }
}

/* TODOS:

*/
