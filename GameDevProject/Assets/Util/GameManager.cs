using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(PathFinding2))]
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;

    void Awake()
    {
        if(instance){
            Destroy(this);
        }   else{
            instance = this;
        }
    }
      public void Restart(){
        SceneManager.LoadScene(0);
    }

    public void startGame(){
        SceneManager.LoadScene(1);
    }
}
