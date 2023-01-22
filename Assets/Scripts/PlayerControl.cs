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
    // private Collider2D coll;
    public bool isGrounded;

    public bool isDead;
    public int deathCount;

    public float jumpForce = 18;
    private bool isSpriteFlipped = false;
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Trap")
        {
            Debug.Log("Triggered a trap");
            isDead = true;
            ++deathCount;
            // died
        }
        else if (other.gameObject.tag == "WinZone")
        {
            Debug.Log("Yay you did it");
        }

    }

    void checkIsGrounded(){
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.55f), Vector2.down, 1f);
 
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
        // coll = GetComponent<Collider2D>();
        isGrounded = true;
        isDead = false;
        deathCount = 0;
        // For some reason it does not scale correctly on the start from editor
        transform.localScale = new Vector2(1, 1);
    }

    void Update() {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0) {
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }

        // Flip the sprite in accordance with direction
        if (horizontalInput < 0) {
            transform.localScale = new Vector2(-1, 1);
            isSpriteFlipped = true;
        } else if (isSpriteFlipped) {
            transform.localScale = new Vector2(1, 1);
        }

        checkIsGrounded();

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))&& isGrounded){
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
