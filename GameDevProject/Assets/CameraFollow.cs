using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float flipYRotationTime = .5f;

    // private Coroutine turnCorutine;
    private CameraManager camMan;
    private bool isFacingRight;

    private void Awake()
    {
        camMan = playerTransform.gameObject.GetComponent<CameraManager>();

        isFacingRight = camMan.IsFacingRight;
    }

    private void Update()
    {
        transform.position = playerTransform.position;
    }

    public void CallTurn()
    {
        // turnCorutine = StartCoroutine(FlipYLerp());
        LeanTween.rotateY(gameObject, DetermineEndRotation(), flipYRotationTime).setEaseInOutSine();
    }

    // private IEnumerator FlipYLerp()
    // {
    //     float startRotation = transform.localEulerAngles.y;
    //     float endRotationAmount = DetermineEndRotation();
    //     float yRotation = 0f;

    //     float elapsedTime = 0f;

    //     while (elapsedTime < flipYRotationTime)
    //     {
    //         elapsedTime += Time.deltaTime;

    //         yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / flipYRotationTime));
    //         transform.rotation = UnityEngine.Quaternion.Euler(0f, yRotation, 0f);
    //     }

    //     yield return null;
    // }

    private float DetermineEndRotation()
    {
        isFacingRight = !isFacingRight;

        if (isFacingRight)
            return 180f;
        else
            return 0f;
    }
}
