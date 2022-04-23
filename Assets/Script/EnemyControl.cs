using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public int Hp = 1;
    private int dir = 1;
    public bool eeMove = false;
    public bool eeMove2 = false;
    Vector3 heroPoositon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        heroPoositon = GameObject.Find("hero").transform.position;
        if (Hp <= 0) {
            return;        
        }

        if (heroPoositon.x >= -20 && !eeMove) {
            eeMove = true;
        }
        if (heroPoositon.x >= 3 && !eeMove2)
        {
            eeMove2 = true;
        }

        if (this.name == "ee04") {
            int a = 0;
        }

        if (this.name != "ee01" && this.name != "ee02" && this.name != "ee03" && this.name != "ee04" && this.name != "ee05") { 
            transform.Translate(Vector2.right * dir * Time.deltaTime * 1f);
        }

        if ((this.name == "ee01" || this.name == "ee02" || this.name == "ee03") && eeMove) {
            transform.Translate(Vector2.right * dir * Time.deltaTime * 1f);
        }

        if ((this.name == "ee04" || this.name == "ee05") && eeMove2)
        {
            transform.Translate(Vector2.right * dir * Time.deltaTime * 1f);
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        dir = -dir;

        if (collision.collider.tag == "DeadZone")
        {
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<CircleCollider2D>());
        }
        if (collision.collider.tag == "FireBall")
        {
            this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, 180f, this.transform.rotation.w);
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(collision.gameObject);
            AudioHub.Instance.PlaySound("smb_fireworks");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "groundCheck")
        {
            Hp--;
            if (Hp <= 0)
            {
                this.transform.localScale = new Vector2(this.transform.localScale.x, this.transform.localScale.y / 2);
                Destroy(gameObject, 1f);
                AudioHub.Instance.PlaySound("smb_kick");
                GameObject.Find("hero").GetComponent<Rigidbody2D>().AddForce(Vector2.up * 350);
                //collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300f);
                Destroy(GetComponent<Collider2D>());
                Destroy(GetComponent<Rigidbody2D>());
            }
        }
    }
}
