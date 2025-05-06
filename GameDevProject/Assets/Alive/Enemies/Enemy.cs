using UnityEngine;

[RequireComponent(typeof(PathFinding2))]
public class Enemy : Character
{
    Pathfinding pathfinder;

    private Player player;

    public override void Awake()
    {
        base.Awake();
        foreach (Item i in inventory.equippedItems){
            i.Hide(true);
        }
        foreach (Item i in inventory.unequippedItems){
            i.Hide(true);
        }
        pathfinder = GetComponent<Pathfinding>();
        player = FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        Vector2 vec = player.transform.position - transform.position;
        if (vec.magnitude < 5){
            Attack("Player");
        }
        transform.rotation = vec.x > 0 ? Quaternion.Euler(new Vector3(0,0,0)) : Quaternion.Euler(new Vector3(0,180,0));
        //TODO this will eventually be the case, waiting on other classes
        // movement.Move(pathfinder.getMove());
    }
}
