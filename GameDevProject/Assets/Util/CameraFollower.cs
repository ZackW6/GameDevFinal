using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject player;
    private Camera cam;
    // Start is called before the first frame updat
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
            //make it follow 
            // crash physics

        Vector3 diffPos = player.transform.position - transform.position;
        print(diffPos.magnitude);

        
        if(diffPos.x <  -cam.orthographicSize * 1.3f){
          transform.position = new Vector3(transform.position.x + (diffPos.x + cam.orthographicSize * 1.3f)   ,transform.position.y, transform.position.z);
          }else if(diffPos.x >  cam.orthographicSize * 1.3f){
          transform.position = new Vector3(transform.position.x + (diffPos.x - cam.orthographicSize * 1.3f)   ,transform.position.y, transform.position.z);
          }
        if(diffPos.y <  -cam.orthographicSize * 0.9f){
          transform.position = new Vector3( transform.position.x  ,transform.position.y + (diffPos.y + cam.orthographicSize * 0.9f), transform.position.z);
          }else if(diffPos.y >  cam.orthographicSize * 0.9f){
          transform.position = new Vector3(transform.position.x   ,transform.position.y + (diffPos.y - cam.orthographicSize * 0.9f), transform.position.z);
          }
       
    }

}
