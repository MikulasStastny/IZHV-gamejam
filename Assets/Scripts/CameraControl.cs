using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 1, -10);
    public float smoothTime = 0.125f;
    private float originalSmoothTime;
    Vector3 currentVelocity;
    public Camera mainCamera;
    public float platformHeight = 4.5f;
    private float screenHalfWidth;
    private float screenHalfHeight;
    private float leftStopper;
    private float rightStopper;
    private float downStopper;
    private float upStopper;
    private bool playerIsFalling;

    // Start is called before the first frame update
    void Start(){
        // Calculates left and right stoppers in world space based on screen width.
        // Camera won't follow player after these stoppers.
        screenHalfWidth = Mathf.Abs(mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, -10)).x
                                    - mainCamera.ViewportToWorldPoint(new Vector3(0, 0, -10)).x);
        screenHalfHeight = Mathf.Abs(mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, -10)).y
                                    - mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, -10)).y);
        leftStopper = -10 + screenHalfWidth;
        rightStopper = 40 - screenHalfWidth;
        downStopper = -1 + screenHalfHeight;
        upStopper = 31 - screenHalfHeight;

        originalSmoothTime = smoothTime;
    }

    // Update is called once per frame
    void FixedUpdate(){
        Vector3 cameraPosition = offset;

        if(playerIsFalling && GameObject.FindWithTag("Player").GetComponent<PlayerControl>().isGrounded){
            playerIsFalling = false;
            smoothTime = originalSmoothTime;
        }

        /*if(target.position.x < leftStopper){
            cameraPosition.x = leftStopper;
        }
        else if(target.position.x > rightStopper){
            cameraPosition.x = rightStopper;
        }
        else{
            cameraPosition.x = target.position.x;
        }*/

        cameraPosition.x = target.position.x;
        cameraPosition.y = target.position.y;

        /*if(target.position.y < downStopper){
            cameraPosition.y = downStopper;
        }
        else if(target.position.y > upStopper){
            cameraPosition.y = upStopper;
        }
        else{
            if(playerIsFalling){
                cameraPosition.y = target.position.y;
            }
            else{
                // Camera y value changes only when player enter different platform - removes unnecessary camera movement
                int platform = (int)(target.position.y/platformHeight);
                cameraPosition.y += platform*platformHeight;
            }
        }*/

        // Case for very fast player speed (when falling) - speeds up the camera
        /*if(Vector3.Distance(transform.position, cameraPosition) > 5){
            smoothTime = 0.1f;
            playerIsFalling = true;
            cameraPosition.y = target.position.y;
        }*/

        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref currentVelocity, smoothTime);
    }
}
