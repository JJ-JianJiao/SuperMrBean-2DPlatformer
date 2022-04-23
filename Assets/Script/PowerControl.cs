using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerControl : MonoBehaviour
{

    GameObject hero;
    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("hero");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "hero")
        {
            Destroy(this.gameObject);
            hero.GetComponent<CharacterController>().canFireBall = true;
            AudioHub.Instance.PlaySound("smb_powerup");
        }
    }
}
