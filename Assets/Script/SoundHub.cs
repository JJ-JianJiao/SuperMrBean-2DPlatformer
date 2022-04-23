using UnityEngine;

public class SoundHub : MonoBehaviour
{
    AudioSource[] sounds;
    private void Awake()
    {
        sounds = GetComponents<AudioSource>();
    }

    public void PlayCoinSound() 
    {
        sounds[0].Play();        
    }
}
