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
    private Tilemap tilemap;
    public String keyTag;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponentInParent<Tilemap>();
    }

    private void DoorOpen()
    {
        for (int i = 0; i < tilesToDestroy.Count; i++) tilemap.SetTile(tilesToDestroy[i], null);
        Destroy(this.gameObject);
    }

    private void DoorCheck(GameObject go)
    {
        PlayerInventory playerInv = go.GetComponent<PlayerInventory>();
        if (playerInv.equippedItems.Find(x => x.title == keyTag))
        {
            DoorOpen();
            Destroy(playerInv.equippedItems.Find(x => x.title == keyTag).gameObject);
        }
        else if (playerInv.unequippedItems.Find(x => x.title == keyTag))
        {
            DoorOpen();
            Destroy(playerInv.unequippedItems.Find(x => x.title == keyTag).gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(keyTag))
        {
            DoorOpen();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            DoorCheck(collision.gameObject);
        }
    }
}