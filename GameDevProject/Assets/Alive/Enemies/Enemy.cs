using UnityEngine;

[RequireComponent(typeof(Pathfinding))]
public class Enemy : Character
{
    Pathfinding pathfinder;

    public override void Awake()
    {
        base.Awake();
        foreach (Item i in inventory.equippedItems){
            i.Hide(true);
        }
        foreach (Item i in inventory.unequippedItems){
            i.Hide(true);
        }
    }

    void FixedUpdate()
    {
        //TODO this will eventually be the case, waiting on other classes
        // movement.Move(pathfinder.getMove());
        pathfinder = GetComponent<Pathfinding>();
    }

    public override void Kill()
    {
        base.Kill();
        //Run some kill animation
        //Then delete
        Invoke("Destroy",1);
    }

    protected void Destroy(){
        Destroy(this);
    }
}
