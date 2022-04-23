using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "hero") { 
            Destroy(this.gameObject);
            AudioHub.Instance.PlaySound("smw_coin");
        }
    }
}
