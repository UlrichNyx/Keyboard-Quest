/* 
# Author: Filippos Kontogiannis
# Description: The class that represents all AudioSource objects!
# Editors: ...
*/

// This class was borrowed from Brackeys on Youtube: https://www.youtube.com/watch?v=6OT43pvUyfY

using UnityEngine.Audio;
using UnityEngine;

// The custom Sound class that allows for defining sounds in the editor
[System.Serializable] // This is what allows the above ^^
public class Sound
{
    public string name; // The name of the sound
    public AudioClip clip; // The reference to the AudioClip in the Assets folder
    [Range(0f,1f)]
    public float volume; // The desired volume of the clip
    [Range(0.1f, 3f)]
    public float pitch; // The desired pitch of the clip
    public bool loop; // Whether or not the clip should loop
    [HideInInspector]
    public AudioSource source; // An AudioSource instance, to be used by AudioManager to populate its private sounds[] field
}

/* TODOS:

*/
