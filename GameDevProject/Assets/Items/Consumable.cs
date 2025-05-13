using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public float health = 0;
    
    public void Interact(Character enactingCharacter){
        //TODO heal character based on health stat, don't go over max
        Destroy(this);
    }
}
