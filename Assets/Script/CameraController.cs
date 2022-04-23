using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //follow target
    public Transform target;

    //border
    public float MinX;
    public float MaxX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get Camera Positon
        Vector3 vector = transform.position;
        vector.x = target.position.x;
        if (vector.x > MaxX) {
            vector.x = MaxX;
        }
        else if(vector.x < MinX){
            vector.x = MinX;
        }

        transform.position = vector;
    }
}
