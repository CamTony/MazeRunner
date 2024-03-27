using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{


    //orientation and movement of the player
    public Transform orientation;
    float horizontalInput;
    float verticalInput;

    Vector3 move;
    public float speed;

    //jumping variables
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce;
    public float jumpCooldown;
    bool readyToJump;

    Rigidbody rb;



    //for managing drag
    public float groundDrag;
    public float height;
    public LayerMask ground;
    bool isGrounded;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ResetJumpState();
    }

    // Update is called once per frame
    void Update()
    {
        //make sure speed is capped at the max speed
        Vector3 flatSpeed = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatSpeed.magnitude > speed)
        {
            Vector3 limitedSpeed = flatSpeed.normalized * speed;
            rb.velocity = new Vector3(limitedSpeed.x, rb.velocity.y, limitedSpeed.z);
        }


        //control the slipperiness of the ground
        //adds drag if on the ground using raycast to detect ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, height * 0.5f + 0.05f, ground);
        if (isGrounded)
        {
            rb.drag = groundDrag;
        } 
        else
        {
            rb.drag = 0;
        }



        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(Input.GetKey(jumpKey) && isGrounded && readyToJump)
        {
            readyToJump = false;
            Debug.Log("ACTIVATE JUMP");
            Jump();
            Invoke(nameof(ResetJumpState), jumpCooldown);
        }
    }


    void FixedUpdate()
    {
        move = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isGrounded)
        {
            rb.AddForce(move.normalized * speed * 10, ForceMode.Force);
        } else if (!isGrounded)
        {
            rb.AddForce(move.normalized * speed * 10 * 0.5f, ForceMode.Force);
        }
    }


    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJumpState()
    {
        readyToJump = true;
        Debug.Log("RESET JUMP");
    }
}
