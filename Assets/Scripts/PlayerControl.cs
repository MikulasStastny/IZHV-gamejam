using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float movementSpeed = 10f;
    //public float movementJumpSpeed = 7f;
    public CharacterController controller;
    private float horizontalInput;
    private float verticalInput;
    private bool spacePressed = false;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float jumpHeight = 3.5f;
    public float gravityValue = -15f;

    // Start is called before the first frame update
    void Start(){
        //transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update(){
        spacePressed = Input.GetKeyDown(KeyCode.Space);
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate(){

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0){
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(horizontalInput, 0, 0);
        controller.Move(move * Time.fixedDeltaTime * movementSpeed);

        if (spacePressed && groundedPlayer){
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            spacePressed = false;
        }

        playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        controller.Move(playerVelocity * Time.fixedDeltaTime);
    }
}
