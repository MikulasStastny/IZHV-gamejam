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
    public float gravityValue = -25f;

    // Start is called before the first frame update
    void Start(){

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
            move.x *= movementSpeed;
        }
        else{
            move.x *= movementJumpSpeed;
        }

        if (jumped){
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            jumped = false;
        }
        if((controller.collisionFlags & CollisionFlags.Above) != 0){
            playerVelocity.y = -1;
        }
        else{
            playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        }
        move.y = playerVelocity.y;
        
        controller.Move(move * Time.fixedDeltaTime);
    }
}
