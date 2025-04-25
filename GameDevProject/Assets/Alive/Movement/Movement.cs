using System;
using Unity.Mathematics;
using UnityEngine;

//1. Need to create Constriants on inputs


public class Movement : MonoBehaviour
{

    [SerializeField]private Statistics playerStats;
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private CameraManager CamMan;
    [SerializeField]private bool grounded;
    [SerializeField]private float hori;
    [SerializeField]private Vector2 moveDirection;
    [SerializeField]private float moveSpeed = 100f;
    [SerializeField]private float jumpForce = 10f;
    [SerializeField]private bool readyToJump;
    [SerializeField]private float jumpCooldown = .25f;
    [SerializeField]private LayerMask platLayer;
    [SerializeField]private float groundDrag = 5f;
    [SerializeField]private float airMultiplier = .2f;
    // public Movement(Statistics stats)
    // {
    //     playerStats = stats;
    // }

    public void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        CamMan = GetComponent<CameraManager>();
        
    }

    void Start()
    {
        readyToJump = true;
    }

    public void Update()
    {
        grounded = Physics2D.Raycast(transform.position, Vector2.down, 0.7431644f, platLayer); // Checks if player is on ground. Calculates Drag
        print(grounded);
        MyInput();
        SpeedControl();
        rb.drag = grounded ? groundDrag : 2f;

        // if (hori > 0 || hori < 0) CamMan.Turn();
    }
    public void FixedUpdate()
    {
        Move();
    }

    public void MyInput()
    {
        hori = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    // Makes sure speed does not exceed limit
    public void SpeedControl()
    {
        Vector2 flatVel = new Vector2(rb.velocity.x, 0f);

        if ((flatVel.magnitude > moveSpeed) && !grounded)
        {
            Vector2 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector2(limitedVel.x, rb.velocity.y);
        }

    }
    // Calculates the x direction of player on ground and air. Does not allow player the jump.
    public void Move()
    {
        moveDirection = Vector2.right * hori;

        if (grounded) rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode2D.Force);
        else if (!grounded) rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode2D.Force);
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void ResetJump()
    {
        readyToJump = true;
    }

    // public void Move(Vector2 input)
    // {

    //     if (math.abs(rb.velocity.x) < 5)
    //     {
    //         rb.velocity += input;
    //         //rb.AddForce( input, ForceMode2D.Impulse);
    //         // ,         
    //     }
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         Vector2 dir = new Vector2(0, 40);
    //         if (!isFalling)
    //         {
    //             rb.AddForce(dir, ForceMode2D.Impulse);

    //         }
    //     }
    // }



    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     LayerMask Platforms = LayerMask.GetMask("Platforms");
    //     RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, Platforms);
    //     // Debug.DrawRay(transform.position, Vector3.down, Color.red);
    //     if (other.gameObject.CompareTag("Cave") && hit.distance <= .5)
    //     {
    //         isFalling = false;

    //     }
    //     print(hit.distance);
    // }

    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Cave"))
    //     {
    //         isFalling = true;
    //     }
    //     print(isFalling);
    // }
    /* 
    * Get inputs 
            movment 

        Add constraitnes based on the stats of the character
        change the position and rotation of the character based on stats



    */
}

