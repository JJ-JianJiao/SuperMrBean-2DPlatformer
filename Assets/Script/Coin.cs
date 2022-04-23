using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject soundHub = GameObject.Find("SoundHub");
        soundHub.GetComponent<SoundHub>().PlayCoinSound();

        Destroy(this.gameObject);
    }
}
