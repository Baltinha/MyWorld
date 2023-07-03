using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveChar : MonoBehaviour
{
    //Movimento
    public CharacterController controller;
    public float moveSpeed = 5f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Vector2 moveD = Vector2.zero;
    //Pulo
    public float jumpVelocity = 5.0f;
    public float gravity = -1f;
    private bool isJumping = false;

    //camera
    public Transform cam;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveD = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        isJumping = context.ReadValueAsButton();
        Debug.Log(isJumping);
    }  
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        playerJump();
    }
    private void MovePlayer()
    {
        Vector3 movePos = new Vector3(moveD.x * moveSpeed, 0f, moveD.y * moveSpeed).normalized;

        if (movePos.magnitude >= 0.1f )
        {
            float targetAngle = Mathf.Atan2(moveD.x, moveD.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            controller.Move(moveDir * moveSpeed * Time.deltaTime);
        }
    }
    public void playerJump() 
    {
        Vector3 jumpD = new Vector3(0f, 0f, 0f);
        if (isJumping)
        {
            jumpD.y = jumpVelocity;
            controller.Move(jumpD * Time.deltaTime);
        }
        //jumpD.y += gravity;
        controller.Move(jumpD * Time.deltaTime);
    }
}
