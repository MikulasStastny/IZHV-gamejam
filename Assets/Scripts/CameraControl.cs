using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    //public Vector3 offset = new Vector3(0, 4, -10);
    public float smoothTime = 0.125f;
    Vector3 currentVelocity;
    public Camera mainCamera;

    private float screenHalfWidth;
    private float xLeftStopper;
    private float xRightStopper;

    // Start is called before the first frame update
    void Start(){
        // Calculates left and right stoppers in world space based on screen width.
        // Camera won't follow player after these stoppers.
        screenHalfWidth = Mathf.Abs(mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, -10)).x
                                    - mainCamera.ViewportToWorldPoint(new Vector3(0, 0, -10)).x);
        xLeftStopper = -10f + screenHalfWidth;
        xRightStopper = 40f - screenHalfWidth;
    }

    // Update is called once per frame
    void FixedUpdate(){
        Vector3 cameraPosition = new Vector3(0, 0, -10);

        if(target.position.x < xLeftStopper){
            cameraPosition.x = xLeftStopper;
        }
        else if(target.position.x > xRightStopper){
            cameraPosition.x = xRightStopper;
        }
        else{
            cameraPosition.x = target.position.x;
        }

        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref currentVelocity, smoothTime);
    }
}
