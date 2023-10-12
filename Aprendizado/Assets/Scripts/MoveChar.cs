using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveChar : MonoBehaviour
{
    //Movimento
    [Header("Movimento")]
    public CharacterController controller;
    public float moveSpeed = 5f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Vector2 moveD = Vector2.zero;
    //Pulo
    [Header("Pulo")]
    public float jumpVelocity = 5.0f;
    public float gravity = -0.9f;
    private bool isJumping;
    public Transform isGrounded;
    public LayerMask ground;
    //interagir
    [Header("Interact")]
    public Transform pInicial;
    public float distanciaFinal = 1f;
    private bool isInteract = false;

    //camera
    [Header("Camera")]
    public Transform cam;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveD = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        isJumping = context.ReadValueAsButton();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        isInteract = context.ReadValueAsButton();
    }
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        playerJump();
        Interact();
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
        jumpD.y = jumpVelocity;


        if (isJumping && IsGrounded())
        {
            controller.Move(jumpD * Time.deltaTime);
        }
        else if(!IsGrounded())
        {
            controller.Move(jumpD * Time.deltaTime);
        }

    }

    bool IsGrounded() 
    {

        return Physics.CheckSphere(isGrounded.position,0.1f,ground);
    }

    void Interact() 
    {
        RaycastHit HitInfo;
        if (isInteract)
        {
            if (Physics.Raycast(pInicial.transform.position,pInicial.transform.forward,out HitInfo,distanciaFinal,LayerMask.GetMask("Interact")))
            {
                IInteractable obj = HitInfo.transform.GetComponent<IInteractable>();

                if (obj == null) return;

                obj.Interact();

                Debug.Log("interage");
            }
        }
    }
}
