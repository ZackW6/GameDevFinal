using System;
using System.Threading;
using UnityEngine;
[RequireComponent(typeof(CameraManager))]
[RequireComponent(typeof(PlayerInventory))]
public class Player : Character
{
    [SerializeField] private CameraManager camMan;

    [Header("Inputs")]
    [SerializeField] private float kHorizontal;

    [Header("Jump Rules")]
    [SerializeField] private float jumpCooldown = .25f;
    [SerializeField] private float lastJumpTime = 0;

    [SerializeField] private Vector2 targetVector = new Vector2();


    public override void Awake()
    {
        base.Awake();
        camMan = GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        kHorizontal = Input.GetAxisRaw("Horizontal");
        if (kHorizontal > 0 || kHorizontal < 0) camMan.TurnCheck(kHorizontal);

        targetVector = kHorizontal * Vector2.right;

        if (Input.GetKey(KeyCode.Space) && lastJumpTime + jumpCooldown < Time.time)
        {
            targetVector = kHorizontal * Vector2.right + Vector2.up;
            lastJumpTime = Time.time;
        }
        Move(targetVector);
    }

    public override void Move(Vector2 vec)
    {
        movement.Move(vec);
    }
}
