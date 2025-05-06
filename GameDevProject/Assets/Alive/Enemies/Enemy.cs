using UnityEngine;

public class Enemy : Character
{
    PathFinding2 pathfinder;

    private Player player;

    [Header("Jump Rules")]
    [SerializeField] private float jumpCooldown = 0;
    [SerializeField] private float lastJumpTime = 0;

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
        if (vec.magnitude < 5){
            Attack("Player");
        }
        transform.rotation = vec.x > 0 ? Quaternion.Euler(new Vector3(0,0,0)) : Quaternion.Euler(new Vector3(0,180,0));
        if (pathfinder.DirectionOfFollower(transform.position).a < 30){
            movement.Move(pathfinder.DirectionOfFollower(transform.position).b);
        }
        
        //TODO this will eventually be the case, waiting on other classes
        // movement.Move(pathfinder.getMove());
    }
}
