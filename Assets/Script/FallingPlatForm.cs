using System.Collections;
using UnityEngine;

public class FallingPlatForm : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(WaiThenFall());
    }

    IEnumerator WaiThenFall() {
        yield return new WaitForSeconds(1);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
