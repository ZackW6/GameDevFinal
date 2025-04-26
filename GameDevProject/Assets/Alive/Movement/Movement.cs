using UnityEngine;

// 4/25/2025(rain) Collider2D should in theory be the generic thing. However you cannot reference the collider's size if you use collider2d. If you use BoxCollider2D
// you are able to access the size of the collider. We need to access it because of grounded check depends on how far the entity is away from the ground. Using the
// collider's y size is the correct size for the raycast. I believe there is a work around but ima work on something else


public class Movement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Statistics playerStats;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CameraManager camMan;
    [SerializeField] private BoxCollider2D cd;
    [SerializeField] private LayerMask platLayer;

    [Header("Ground Movement")]
    [SerializeField] private float moveSpeed = 100f;
    [SerializeField] private float groundDrag = 5f;


    [Header("Inputs")]
    [SerializeField] private float hori;
    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private bool kSpace;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float airMultiplier = .2f;
    [SerializeField] private float jumpCooldown = .25f;
    [SerializeField] private bool readyToJump;
    [SerializeField] private bool grounded;



    // public Movement(Statistics stats)
    // {
    //     playerStats = stats;
    // }

    public void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        camMan = GetComponent<CameraManager>();
        cd = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        readyToJump = true;

    }

    public void Update()
    {
        grounded = Physics2D.Raycast(transform.position, Vector2.down, cd.size.y, platLayer); // Checks if player is on ground.
        print("X" + rb.velocity.x);
        print("Y" + rb.velocity.y);
        MyInput();
        SpeedControl();
        rb.drag = grounded ? groundDrag : 4f; //Calculates drag based on if entity is in air or not

        // if (hori > 0 || hori < 0) CamMan.Turn();
    }
    public void FixedUpdate()
    {
        Move();
    }

    // Gets input from user. Left,Right movement and Jump.
    public void MyInput()
    {
        // Keyboard Inputs
        hori = Input.GetAxis("Horizontal");
        kSpace = Input.GetKey(KeyCode.Space);

        // Checks if player can jump. Jumps if yes.
        if (kSpace && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown); // Jump Cooldown.
        }
    }

    // Makes sure speed does not exceed limit
    // 4/25/2025 I don't think this works as intended currently
    public void SpeedControl()
    {
        Vector2 flatVel = new Vector2(rb.velocity.x, 0f);

        if ((flatVel.magnitude > moveSpeed) && !grounded)
        {
            Vector2 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector2(limitedVel.x, rb.velocity.y);
        }

    }
    // Calculates the x velocity of player on ground and air.
    public void Move()
    {
        moveDirection = Vector2.right * hori; // Uses user input to find what direction player is moving

        if (grounded) rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode2D.Force); // Speed while grounded
        else if (!grounded) rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode2D.Force); // Speed while in Air(Maintains the momentum before in air)
    }

    // Calculates entity jump.
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f); // Keeps velocity in x speed. Resets velocity in y speed.
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    // Resets jump. Allows entity to jump when true.
    public void ResetJump()
    {
        readyToJump = true;
    }

}
