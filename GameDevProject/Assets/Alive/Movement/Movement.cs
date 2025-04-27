using System;
using UnityEngine;

// 4/25/2025(rain) Collider2D should in theory be the generic thing. However you cannot reference the collider's size if you use collider2d. If you use BoxCollider2D
// you are able to access the size of the collider. We need to access it because of grounded check depends on how far the entity is away from the ground. Using the
// collider's y size is the correct size for the raycast. I believe there is a work around but ima work on something else

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D cd;
    [SerializeField] private LayerMask platLayer;

    [Header("Ground Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float groundDrag = 5f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 1500;
    [SerializeField] private float airMultiplier = .2f;

    [SerializeField] private float airDrag = 4f;
    [SerializeField] private bool grounded;
    public void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cd = GetComponent<BoxCollider2D>();
    }

    public void FixedUpdate()
    {
        grounded = Physics2D.Raycast(transform.position, Vector2.down, cd.size.y, platLayer); // Checks if player is on ground.
        // print("X" + rb.velocity.x);
        // print("Y" + rb.velocity.y);
        SpeedControl();
        rb.drag = grounded ? groundDrag : airDrag; //Calculates drag based on if entity is in air or not
    }

    // Makes sure speed does not exceed limit
    // 4/25/2025 I don't think this works as intended

    //Im not sure exactly what was intended, it doesn't work correctly even with what you wanted,
    //but the idea is solid to limit how fast some actions can happen seperately from the 
    //movement function - Zack
    public void SpeedControl()
    {
        Vector2 flatVel = new Vector2(rb.velocity.x, 0f);

        if ((flatVel.magnitude > moveSpeed) && !grounded)
        {
            Vector2 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector2(limitedVel.x, rb.velocity.y);
        }

    }

    //Meant to be called once per frame in the character class which holds it, using whatever data they give
    public void Move(Vector2 direction)
    {
        if (grounded){
            direction.x = Math.Clamp(direction.x, -1,1);
            direction.y = Math.Clamp(direction.y, -1,1);
            direction.y *= jumpForce;
            direction.x *= moveSpeed;
        }else{
            direction.y = 0;
            direction = airMultiplier * moveSpeed * direction.normalized;
        }
        rb.AddForce(direction, ForceMode2D.Force);
        // Vector2 moveDirection = Vector2.right * kHorizontal; // Uses user input to find what direction player is moving

        // if (grounded) rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode2D.Force); // Speed while grounded
        // else if (!grounded) rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode2D.Force); // Speed while in Air(Maintains the momentum before in air)
    }
}
