using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHub : MonoBehaviour
{
    public static AudioHub Instance;

    //Get audios
    private AudioSource player;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        player = GetComponent<AudioSource>();
    }


    //play Audio play
    public void PlaySound(string name) {
        AudioClip clip =  Resources.Load<AudioClip>(name);

        player.PlayOneShot(clip);
    }

    //Stop Audio play
    public void StopSound() {
        player.Stop();
    }
}
