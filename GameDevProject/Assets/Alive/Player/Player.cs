using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(CameraManager))]
[RequireComponent(typeof(PlayerInventory))]
public class Player : Character
{
    [SerializeField] private CameraManager camMan;

    [Header("Inputs")]
    [SerializeField] private float kHorizontal;

    [Header("Jump Rules")]
    [SerializeField] private float jumpCooldown = 0;
    [SerializeField] private float lastJumpTime = 0;

    [SerializeField] private Vector2 targetVector = new Vector2();

    public Text healthDisplay;

    public override void Awake()
    {
        base.Awake();
        camMan = GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        kHorizontal = Input.GetAxisRaw("Horizontal");
        if (kHorizontal > 0 || kHorizontal < 0) camMan.TurnCheck(kHorizontal);

        targetVector = kHorizontal * Vector2.right;

        if (Input.GetKey(KeyCode.Space) && lastJumpTime + jumpCooldown < Time.time && movement.IsGrounded())
        {
            targetVector = kHorizontal * Vector2.right + Vector2.up;
            lastJumpTime = Time.time;
        }
        movement.Move(targetVector);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Attack("Enemy");
        }
        
    }

    public void Update()
    {
        healthDisplay.text = "Health: " + Mathf.Ceil(health);
    }
    public override void Kill()
    {
            GameManager.instance.Restart();
        
        // GetComponent<SpriteRenderer>().enabled = false;
        // this.enabled = false;
        // GetComponent<Rigidbody2D>().simulated = false;
        // GetComponent<Collider2D>().enabled = false;
        // inventory.Kill();
    }
}
