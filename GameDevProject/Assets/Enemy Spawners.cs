
using UnityEngine;

public class EnemySpawners : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float frequency;
    public float timer;
    void Start()
    {
        timer = frequency;
    }

 
    void Update()
    {
        if(timer <= 0){
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            timer = frequency;
        }
        timer -= Time.deltaTime;
    }
}
