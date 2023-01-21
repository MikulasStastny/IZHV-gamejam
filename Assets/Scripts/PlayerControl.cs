using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float movementJumpSpeed = 7f;
    public CharacterController controller;
    private float horizontalInput;
    private float verticalInput;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool jumped = false;
    public float jumpHeight = 3.5f;
    public float gravityValue = -15f;

    // Start is called before the first frame update
    void Start(){
        //transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update(){
        groundedPlayer = controller.isGrounded;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(!jumped && Input.GetKeyDown(KeyCode.Space) && groundedPlayer){
            jumped = true;
        }
    }
    void FixedUpdate(){

        if (groundedPlayer && playerVelocity.y < 0){
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(horizontalInput, 0, 0);
        if(groundedPlayer){
            controller.Move(move * Time.fixedDeltaTime * movementSpeed);
        }
        else{
            controller.Move(move * Time.fixedDeltaTime * movementJumpSpeed);
        }

        if (jumped){
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            jumped = false;
        }

        playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        controller.Move(playerVelocity * Time.fixedDeltaTime);
    }
}
