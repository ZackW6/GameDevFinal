using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Statistics;

public class Consumable : Item
{
    public Consumable(string name) 
        : base(name, new Statistics(SetMarkers.Health))
    {

    }

    public void Interact(Character enactingCharacter){
        //TODO heal character based on health stat, don't go over max
        Destroy(this);
    }
}
