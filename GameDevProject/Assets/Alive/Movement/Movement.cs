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
    [SerializeField] public float jumpForce = 1500;
    [SerializeField] private float airMultiplier = .2f;

    public bool flying = false;

    [SerializeField] private float airDrag = 4f;
    [SerializeField] private bool grounded;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float defaultGrav;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cd = gameObject.GetComponent<BoxCollider2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;
        rb.drag = 5;//grounded ? groundDrag : airDrag; //Calculates drag based on if entity is in air or not
        defaultGrav = rb.gravityScale;
    }

    private void Update()
    {
        SpeedControl();
        if(rb.velocity.y < 0){
            rb.gravityScale = defaultGrav * 1.5f;
        }else{
            rb.gravityScale = defaultGrav;
        }

        currentSpeed = rb.velocity.x; // Test code so you can view in inspector
    }
    void FixedUpdate()
    {
        grounded = Physics2D.Raycast(new Vector3(cd.bounds.center.x,cd.bounds.min.y), Vector2.down, .1f, platLayer) || Physics2D.Raycast(new Vector3(cd.bounds.min.x,cd.bounds.min.y), Vector2.down, .1f, platLayer) ||Physics2D.Raycast(new Vector3(cd.bounds.max.x,cd.bounds.min.y), Vector2.down, .1f, platLayer); // Checks if player is on ground.
    }
    // Movement speed control
    private void SpeedControl()
    {
        rb.velocity = new Vector2(Math.Clamp(rb.velocity.x, -moveSpeed, moveSpeed), rb.velocity.y);
    }

    //Meant to be called once per frame in the character class which holds it, using whatever data they give
    public void Move(Vector2 direction)
    {
        if (flying){
            rb.AddForce(direction.normalized*moveSpeed, ForceMode2D.Force);
            return;
        }
        direction.x = (direction.x < .05f && direction.x > -.05f) ? 0 : direction.x > 0 ? 1 : -1;
        direction.y = (direction.y < .05f && direction.y > -.05f) ? 0 : direction.y > 0 ? 1 : -1;
        if (grounded)
        {
            if (direction.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            direction.x *= moveSpeed * 10f;
            if (direction.y > 0)
            {
                direction.y *= jumpForce;
            }
        }
        else
        {
            direction.y = 0;
            direction = airMultiplier * moveSpeed * 10f * direction;
        }
        rb.AddForce(direction, ForceMode2D.Force);
        // Vector2 moveDirection = Vector2.right * kHorizontal; // Uses user input to find what direction player is moving

        // if (grounded) rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode2D.Force); // Speed while grounded
        // else if (!grounded) rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode2D.Force); // Speed while in Air(Maintains the momentum before in air)
    }

    public bool IsGrounded()
    {
        return grounded;
    }
}
