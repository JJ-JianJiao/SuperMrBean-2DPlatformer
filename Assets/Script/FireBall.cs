using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int dir = 1;
    public int hitTimes = 0;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * dir * Time.deltaTime * 10f);

        if (hitTimes >= 10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "hero")
        {
            return;
        }

        if (collision.collider.tag == "tube")
        {
            dir = -dir;
            hitTimes++;
        }

        if (collision.collider.tag == "DeadZone")
        {
            Destroy(this.gameObject, 1);
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<CircleCollider2D>());
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
