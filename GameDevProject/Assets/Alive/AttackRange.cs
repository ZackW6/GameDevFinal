using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackRange : MonoBehaviour
{
    private Collider2D col;
    public List<GameObject> collidings = new List<GameObject>();
    void Awake()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }
    
    public List<GameObject> CheckCollider(){
        return collidings;
    }

    void OnTriggerEnter2D(Collider2D  col) {
		collidings.Add (col.gameObject);
	}

	void OnTriggerExit2D(Collider2D  col) {
		collidings.Remove (col.gameObject);
	}

    
}