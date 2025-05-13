using System;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public override void Start()
    {
        base.Start();
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

    public override void Update()
    {
        base.Update();
        healthDisplay.text = "Health: " + Mathf.Ceil(health);
    }

    public override void Attack(float attackSpeed, float damage, AttackRange attackRange, string tag){
        if (isAbleToAttack){
            isAbleToAttack = false;
            Invoke(nameof(ResetAttack), attackSpeed);
            attackRange.transform.SetParent(transform);
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPoint.z = 0;
            Quaternion lookTowardPlayer = Quaternion.LookRotation(worldPoint - transform.position,Vector3.up);
            float initDirection = lookTowardPlayer.eulerAngles.y-90;
            attackRange.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, initDirection + (initDirection < 90 && initDirection > -90 ? -lookTowardPlayer.eulerAngles.x : lookTowardPlayer.eulerAngles.x)));
            // attackRange.transform.SetPositionAndRotation(transform.position, transform.rotation);
            foreach (GameObject i in attackRange.CheckCollider()){
                if (i.CompareTag(tag))
                {
                    i.GetComponent<Character>().Damage(damage);
                }
            }
        }
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
