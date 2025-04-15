using UnityEngine;

public class CameraManager : MonoBehaviour
{
    bool IsFacingRight;
    void Start()
    {
        IsFacingRight = transform.rotation == Quaternion.Euler(new Vector3(transform.rotation.x, 0f, transform.rotation.z)) ? true : false;
    }
    public void TurnCheck(){
        // if( > 0 && !IsFacingRight){

        // }else if(> 0 && !IsFacingRight){

        // }
    }
    public void Turn()
    {
        if (IsFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
        else if (!IsFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
    }

















    // private Camera cam;
    // Start is called before the first frame updat
    // void Start()
    // {
    //     cam = GetComponent<Camera>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //         //make it follow 
    //         // crash physics


    //    transform.position.Set ((float)player.transform.position.x,(float)player.transform.position.y, (float)transform.position.z);
    //     transform.position = new Vector3((float)player.transform.position.x,(float)player.transform.position.y, (float)transform.position.z);

    //     Vector3 diffPos = player.transform.position - transform.position;
    //     print(diffPos.magnitude);


    //     if(diffPos.x <  -cam.orthographicSize * 1.3f){
    //       transform.position = new Vector3(transform.position.x + (diffPos.x + cam.orthographicSize * 1.3f)   ,transform.position.y, transform.position.z);
    //       }else if(diffPos.x >  cam.orthographicSize * 1.3f){
    //       transform.position = new Vector3(transform.position.x + (diffPos.x - cam.orthographicSize * 1.3f)   ,transform.position.y, transform.position.z);
    //       }
    //     if(diffPos.y <  -cam.orthographicSize * 0.9f){
    //       transform.position = new Vector3( transform.position.x  ,transform.position.y + (diffPos.y + cam.orthographicSize * 0.9f), transform.position.z);
    //       }else if(diffPos.y >  cam.orthographicSize * 0.9f){
    //       transform.position = new Vector3(transform.position.x   ,transform.position.y + (diffPos.y - cam.orthographicSize * 0.9f), transform.position.z);
    //       }

    // }

}
