using UnityEngine;

[RequireComponent(typeof(Pathfinding))]
public class Enemy : Character
{
    Pathfinding pathfinder;

    void Update()
    {
        //TODO this will eventually be the case, waiting on other classes
        // movement.Move(pathfinder.getMove());
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

    public override void Move(Vector2 vec)
    {
        
    }
}
