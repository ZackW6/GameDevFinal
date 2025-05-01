using System;
using UnityEngine;

// 4/25/2025(rain) Collider2D should in theory be the generic thing. However you cannot reference the collider's size if you use collider2d. If you use BoxCollider2D
// you are able to access the size of the collider. We need to access it because of grounded check depends on how far the entity is away from the ground. Using the
// collider's y size is the correct size for the raycast. I believe there is a work around but ima work on something else

[RequireComponent(typeof(BoxCollider2D))]
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
    [SerializeField] private float currentSpeed;
    public void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cd = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        grounded = Physics2D.Raycast(transform.position, Vector2.down, cd.size.y, platLayer); // Checks if player is on ground.
        rb.drag = 5;//grounded ? groundDrag : airDrag; //Calculates drag based on if entity is in air or not    
        SpeedControl();

        currentSpeed = rb.velocity.x; // Test code so you can view in inspector
    }

    // Movement speed control
    public void SpeedControl()
    {
        rb.velocity = new Vector2(Math.Clamp(rb.velocity.x, -moveSpeed, moveSpeed), rb.velocity.y);
    }

    //Meant to be called once per frame in the character class which holds it, using whatever data they give
    public void Move(Vector2 direction)
    {
        direction.x = (direction.x < .05f && direction.x > -.05f) ? 0 : direction.x > 0 ? 1 : -1;
        direction.y = (direction.y < .05f && direction.y > -.05f) ? 0 : direction.y > 0 ? 1 : -1;
        if (grounded)
        {
            if (direction.y > 0){
              rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            
            direction.x *= moveSpeed;
            if (direction.y > 0){
                direction.y *= jumpForce;
            }
        }
        else
        {
            direction.y = 0;
            direction = airMultiplier * moveSpeed * direction;
        }
        rb.AddForce(direction, ForceMode2D.Force);
        // Vector2 moveDirection = Vector2.right * kHorizontal; // Uses user input to find what direction player is moving

        // if (grounded) rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode2D.Force); // Speed while grounded
        // else if (!grounded) rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode2D.Force); // Speed while in Air(Maintains the momentum before in air)
    }

    public bool IsGrounded(){
      return grounded;
    }
}
