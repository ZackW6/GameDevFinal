using Unity.Mathematics;
using UnityEngine;

//1. Need to create Constriants on inputs


public class Movement : MonoBehaviour
{

    private Statistics playerStats;
    private Rigidbody2D rb;
    private CameraManager CamMan;
    private bool isFalling = false;
    private float jumpCount = 0f;
    private float hori;
    private float b;

    public Movement(Statistics stats)
    {
        playerStats = stats;
    }

    public void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        CamMan = GetComponent<CameraManager>();
    }

    public void Update()
    {
        hori = Input.GetAxis("Horizontal");
        b = Input.GetAxis("Vertical");
        Move(Vector2.right * hori * 39f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        // if (hori > 0 || hori < 0) CamMan.Turn();

    }
    // public void Update()
    // {
    //     float hori = Input.GetAxis("Horizontal");
    //     float b = Input.GetAxis("Vertical");
    //     Vector2 dir = new Vector2(a, 0);

    //     Move(dir * 0.5f);

    // }

    public void FixedUpdate()
    {

    }

    public void Move(Vector2 input)
    {

        if (math.abs(rb.velocity.x) < 5)
        {
            rb.velocity += input;
            //rb.AddForce( input, ForceMode2D.Impulse);
            // ,         
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 dir = new Vector2(0, 40);
            if (!isFalling)
            {
                rb.AddForce(dir, ForceMode2D.Impulse);

            }
        }

    }




    private void OnCollisionEnter2D(Collision2D other)
    {
        LayerMask Platforms = LayerMask.GetMask("Platforms");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, Platforms);
        // Debug.DrawRay(transform.position, Vector3.down, Color.red);
        if (other.gameObject.CompareTag("Cave") && hit.distance <= .5)
        {
            isFalling = false;

        }
        print(hit.distance);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Cave"))
        {
            isFalling = true;
        }
        print(isFalling);
    }
    /* 
    * Get inputs 
            movment 

        Add constraitnes based on the stats of the character
        change the position and rotation of the character based on stats



    */
}

