using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
public class MoveTransform : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private Vector2 moveD = Vector2.zero;

    public Transform cam;

    public void OnMove(InputAction.CallbackContext context) 
    {
        moveD = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movePos = new Vector3(moveD.x * moveSpeed, 0f, moveD.y * moveSpeed).normalized;

        if (movePos.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveD.x, moveD.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
        }
    }


}
