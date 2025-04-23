using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statistics;

public class Consumable : Item
{
    public override void Awake()
    {
        base.Awake();
        stats = new Statistics(SetMarkers.Health);
    }

    public void Interact(Character enactingCharacter){
        //TODO heal character based on health stat, don't go over max
        Destroy(this);
    }
}
