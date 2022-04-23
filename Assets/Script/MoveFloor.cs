using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    private Transform tf;
    public int dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        tf = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * dir * Time.deltaTime * 1f);

        if (tf.position.x >= 1.82 || tf.position.x <= -3.73) {
            dir *= -1;
        }
    }
}
