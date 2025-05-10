using UnityEngine;

public class Enemy : Character
{
    PathFinding2 pathfinder;

    private Player player;
    private Collider2D cd;

    [Header("Follow Rules")]
    [SerializeField] private float maxFollowCost = 30;
    [SerializeField] private float attackFrom = 4;

    public override void Awake()
    {
        //Give the enemy stuff automatically, should be pretty easy
        base.Awake();
        foreach (Item i in inventory.equippedItems){
            i.Hide(true);
        }
        foreach (Item i in inventory.unequippedItems){
            i.Hide(true);
        }
        cd = GetComponent<Collider2D>();
        pathfinder = FindObjectOfType<PathFinding2>();
        player = FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        Vector2 vec = player.transform.position - transform.position;
        if (vec.magnitude < attackFrom){
            Attack("Player");
        }
        Vector3 checkVec = transform.position;
        float minFollowY = 5;
        if (cd){
            checkVec = new Vector3(cd.bounds.center.x, cd.bounds.min.y+1f);
            minFollowY = cd.bounds.max.y - cd.bounds.center.y;
        }
        transform.rotation = vec.x > 0 ? Quaternion.Euler(new Vector3(0,0,0)) : Quaternion.Euler(new Vector3(0,180,0));
        MultiType<float, Vector2> multiType = pathfinder.DirectionOfFollower(checkVec);
        if (multiType.a < maxFollowCost){
            movement.Move(multiType.b * (Mathf.Abs(vec.x) < attackFrom/2 && Mathf.Abs(vec.y) < minFollowY*2 ? new Vector2(0,1) : new Vector2(1,1)));
        }
        
        //TODO this will eventually be the case, waiting on other classes
        // movement.Move(pathfinder.getMove());
    }

    public override void Attack(float attackSpeed, float damage, AttackRange attackRange, string tag){
        if (isAbleToAttack){
            isAbleToAttack = false;
            Invoke(nameof(ResetAttack), attackSpeed);
            attackRange.transform.SetParent(transform);
            Quaternion lookTowardPlayer = Quaternion.LookRotation(player.transform.position - transform.position,Vector3.up);
            float initDirection = lookTowardPlayer.eulerAngles.y-90;
            attackRange.transform.SetPositionAndRotation(attackRange.transform.position, Quaternion.Euler(0, 0, initDirection + (initDirection < 90 && initDirection > -90 ? -lookTowardPlayer.eulerAngles.x : lookTowardPlayer.eulerAngles.x)));
            foreach (GameObject i in attackRange.CheckCollider()){
                if (i.CompareTag(tag))
                {
                    i.GetComponent<Character>().Damage(damage);
                }
            }
        }
    }
}
