using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEditor.U2D.Aseprite;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class DoorEvent : MonoBehaviour
{
    public List<Vector3Int> tilesToDestroy;
    public event UnityAction act;
    private Tilemap tilemap;
    [SerializeField] private TagField doorTag;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponentInParent<Tilemap>();
    }

    private void DoorOpen()
    {
        for (int i = 0; i < tilesToDestroy.Count; i++)
        {
            tilemap.SetTile(tilesToDestroy[i], null);
        }
    }

    private void DoorCheck(GameObject go)
    {
        foreach (Item x in go.GetComponent<Inventory>().equippedItems)
        {
            if (x.Equals("Key"))
            {
                // EventManager.DoorOpen

            }
        }
        // delete key from inventory
        // go.GetComponent<Inventory>().equippedItems.Find
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            // EventManager.DoorOpen
            DoorOpen();
        }
        else if (collision.CompareTag("Player"))
        {
            DoorCheck(collision.gameObject);
        }

        // if (collision.CompareTag("Doors"))
        // {
        //     tilesToDestroy.Add(collision.gameObject.transform.ConvertTo<Vector3Int>());
        //     print("ahhhh");
        // }
    }

}