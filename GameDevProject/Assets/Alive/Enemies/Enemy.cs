using UnityEngine;

public class Enemy : Character
{
    PathFinding2 pathfinder;

    private Player player;

    [Header("Follow Rules")]
    [SerializeField] private float maxFollowCost = 30;
    [SerializeField] private float attackFrom = 4;

    public override void Awake()
    {
        base.Awake();
        foreach (Item i in inventory.equippedItems){
            i.Hide(true);
        }
        foreach (Item i in inventory.unequippedItems){
            i.Hide(true);
        }
        pathfinder = FindObjectOfType<PathFinding2>();
        player = FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        Vector2 vec = player.transform.position - transform.position;
        if (vec.magnitude < attackFrom){
            Attack("Player");
        }
        transform.rotation = vec.x > 0 ? Quaternion.Euler(new Vector3(0,0,0)) : Quaternion.Euler(new Vector3(0,180,0));
        if (pathfinder.DirectionOfFollower(transform.position).a < maxFollowCost){
            movement.Move(pathfinder.DirectionOfFollower(transform.position).b * (Mathf.Abs(vec.x) < attackFrom/2 && Mathf.Abs(vec.y) < 5 ? new Vector2(0,1) : new Vector2(1,1)));
        }
        
        //TODO this will eventually be the case, waiting on other classes
        // movement.Move(pathfinder.getMove());
    }

    public override void Attack(float attackSpeed, float damage, AttackRange attackRange, string tag){
        if (isAbleToAttack){
            isAbleToAttack = false;
            Invoke(nameof(ResetAttack), attackSpeed);
            Quaternion lookTowardPlayer = Quaternion.LookRotation(player.transform.position - transform.position,Vector3.up);
            print(lookTowardPlayer.eulerAngles.x+"   "+lookTowardPlayer.eulerAngles.y+"    "+lookTowardPlayer.eulerAngles.z);
            float initDirection = lookTowardPlayer.eulerAngles.y-90;
            attackRange.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, initDirection + (initDirection < 90 && initDirection > -90 ? -lookTowardPlayer.eulerAngles.x : lookTowardPlayer.eulerAngles.x)));
            foreach (GameObject i in attackRange.CheckCollider()){
                if (i.CompareTag(tag))
                {
                    i.GetComponent<Character>().Damage(damage);
                }
            }
        }
    }
}
