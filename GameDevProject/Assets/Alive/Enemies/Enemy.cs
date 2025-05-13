using UnityEngine;

public class Enemy : Character
{
    PathFinding2 pathfinder;

    private Player player;
    private Collider2D cd;

    [Header("Follow Rules")]
    [SerializeField] private float maxFollowCost = 30;
    [SerializeField] private float attackFrom = 4;

    public override void Start()
    {
        base.Start();
        if (Random.Range(0,2) == 1){
            int nextRand = Random.Range(0,4);
            GameObject newItem = null;
            if (nextRand == 0){
                newItem = Instantiate(GameManager.instance.weaponPrefab,FindObjectOfType<Canvas>().transform);
                newItem.GetComponent<Weapon>().attackSpeed = attackSpeed*Random.Range(0.8f,1.25f);
                newItem.GetComponent<Weapon>().damage = damage*Random.Range(0.6f,1.1f);
            }
            if (nextRand == 1){
                newItem = Instantiate(GameManager.instance.headPrefab,FindObjectOfType<Canvas>().transform);
                newItem.GetComponent<Item>().bonusHealth = maxHealth*Random.Range(0.25f,0.5f);
            }
            if (nextRand == 2){
                newItem = Instantiate(GameManager.instance.chestPrefab,FindObjectOfType<Canvas>().transform);
                newItem.GetComponent<Item>().bonusHealth = maxHealth*Random.Range(0.25f,1f);
            }
            if (nextRand == 3){
                newItem = Instantiate(GameManager.instance.legsPrefab,FindObjectOfType<Canvas>().transform);
                newItem.GetComponent<Legs>().bonusHealth = maxHealth*Random.Range(0.25f,1.25f);
                newItem.GetComponent<Legs>().addedJump = movement.jumpForce*Random.Range(0.3f,0.8f);
            }
            if (newItem){
                inventory.addUnequipped(newItem.GetComponent<Item>());
            }
        }
        
        foreach (Item i in inventory.equippedItems){
            if (i){
                i.Hide(true);
            }
        }
        foreach (Item i in inventory.unequippedItems){
            if(i){
                i.Hide(true);
            }
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
