using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public bool IsFacingRight;

    private CameraFollow camFol;

    [SerializeField] private GameObject camFolObj;

    private void Start()
    {
        camFol = camFolObj.GetComponent<CameraFollow>();
        IsFacingRight = transform.rotation == Quaternion.Euler(new Vector3(transform.rotation.x, 0f, transform.rotation.z)) ? true : false; // Checks what direction the entity is facing
    }

    // Checks if entity can turn.
    public void TurnCheck(float kHorizontal)
    {
        if (kHorizontal > 0 && !IsFacingRight || kHorizontal < 0 && IsFacingRight) Turn(); // Checks if player can turn or not. If going right and facing left, turns right. If going left and facing right, turn lefts. 
    }

    // Turn functionality
    private void Turn()
    {
        Vector3 rotator = IsFacingRight ? new Vector3(transform.rotation.x, 180f, transform.rotation.z) /* Facing right turns left */ : new Vector3(transform.rotation.x, 0f, transform.rotation.z) /* Facing left turns right */; 

        transform.rotation = Quaternion.Euler(rotator); // This does the turning
        IsFacingRight = !IsFacingRight;

        camFol.CallTurn();
    }

}
