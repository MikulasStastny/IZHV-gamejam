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
    public float platformHeight = 4f;
    private float screenHalfWidth;
    private float screenHalfHeight;
    private float leftStopper;
    private float rightStopper;
    private float downStopper;
    private float upStopper;

    // Start is called before the first frame update
    void Start(){
        // Calculates left and right stoppers in world space based on screen width.
        // Camera won't follow player after these stoppers.
        screenHalfWidth = Mathf.Abs(mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, -10)).x
                                    - mainCamera.ViewportToWorldPoint(new Vector3(0, 0, -10)).x);
        screenHalfHeight = Mathf.Abs(mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, -10)).y
                                    - mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, -10)).y);
        leftStopper = -10f + screenHalfWidth;
        rightStopper = 40f - screenHalfWidth;
        downStopper = -1f + screenHalfHeight;
        upStopper = 30f - screenHalfHeight;
    }

    // Update is called once per frame
    void FixedUpdate(){
        Vector3 cameraPosition = new Vector3(0, 1, -10);

        if(target.position.x < leftStopper){
            cameraPosition.x = leftStopper;
        }
        else if(target.position.x > rightStopper){
            cameraPosition.x = rightStopper;
        }
        else{
            cameraPosition.x = target.position.x;
        }

        if(target.position.y < downStopper){
            cameraPosition.y = downStopper;
        }
        else if(target.position.y > upStopper){
            cameraPosition.y = upStopper;
        }
        else{
            int platform = (int)(target.position.y/platformHeight);
            cameraPosition.y += platform*platformHeight;
        }

        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref currentVelocity, smoothTime);
    }
}
