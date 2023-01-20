using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float movementJumpSpeed = 7f;
    public Rigidbody2D rb;
    public float jumpForce = 10;
    private float horizontalInput;
    private bool spacePressed;

    // Start is called before the first frame update
    void Start(){
        //transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update(){
        spacePressed = Input.GetKeyDown(KeyCode.Space);
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }
    void FixedUpdate(){
        Vector3 direction = new Vector3(horizontalInput, 0 , 0);

        if(transform.position.y < -2.5f){
            //transform.Translate(direction * movementSpeed * Time.deltaTime);
            rb.MovePosition(transform.position + direction * movementSpeed * Time.fixedDeltaTime);
        }
        else{
            //transform.Translate(direction * movementJumpSpeed * Time.deltaTime);
            rb.MovePosition(transform.position + direction * movementJumpSpeed * Time.fixedDeltaTime);
        }

        if(spacePressed && transform.position.y < -2.5f){
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
