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
    
    private float lastJumpTime = 0;

    private Vector2 targetVector = new Vector2();


    public override void Awake()
    {
        base.Awake();
        camMan = GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {
        kHorizontal = Input.GetAxis("Horizontal");
        if (kHorizontal > 0 || kHorizontal < 0) camMan.TurnCheck(kHorizontal);

        targetVector.x = kHorizontal;

        if (Input.GetKey(KeyCode.Space) && lastJumpTime + jumpCooldown < Time.time){
            targetVector.y = 1;
            lastJumpTime = Time.time;
        }else{
            targetVector.y = 0;
        }

        Move(targetVector);
    }
    
    public override void Move(Vector2 vec)
    {
        movement.Move(vec);
    }
}
