using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float moveJumpSpeed = 7f;
    private float horizontalInput;
    public Rigidbody2D body;
    private Vector3 playerVelocity;
    public bool isGrounded;
    public float jumpForce = 18;

    void checkIsGrounded(){
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1.8f), Vector2.down, 1f);
 
        if (hit.collider != null){
            if(hit.collider.tag == "Ground"){
                isGrounded = true;
            }
            else{
                isGrounded = false;
            }
        }
        else{
            isGrounded = false;
        }
    }

    // Start is called before the first frame update
    void Start(){
        isGrounded = true;
    }

    void Update() {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        checkIsGrounded();

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }

    }

    void FixedUpdate(){
        if(isGrounded){
            transform.position += new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;
        }
        else{
            transform.position += new Vector3(horizontalInput, 0, 0) * moveJumpSpeed * Time.deltaTime;
        }
    }
}
