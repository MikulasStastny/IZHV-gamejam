using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    //public Vector3 offset = new Vector3(0, 4, -10);
    public float smoothTime = 0.5f;
    Vector3 currentVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 cameraPosition = new Vector3(target.position.x, 0, -10);

        if(cameraPosition.x < 0.25){
            cameraPosition.x = 0.25f;
        }
        else if(cameraPosition.x > 29.75){
            cameraPosition.x = 29.75f;
        }
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref currentVelocity, smoothTime);
    }
}
