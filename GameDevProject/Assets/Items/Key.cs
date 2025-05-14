using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Key : Item
{
    [SerializeField] private GameObject go;
    public override void Start()
    {
        base.Start();
        SetTitle(go.GetComponent<DoorEvent>().keyTag);
        this.tag = this.title;
    }

    public override void WriteStats(){
        stats.text = "";
    }
}