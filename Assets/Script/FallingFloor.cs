using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Method One
        //StartCoroutine(WaiThenFall());

        //Method Two
        Invoke("SetFalling", 1);
        


    }
    //Method One
    //IEnumerator WaiThenFall() {
    //    yield return new WaitForSeconds(1);
    //    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    //}

    private void SetFalling()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<CircleCollider2D>());
    }
}
