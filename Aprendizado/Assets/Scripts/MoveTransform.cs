using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
public class MoveTransform : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveD = Vector2.zero;

    public void OnMove(InputAction.CallbackContext context) 
    {
        moveD = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    public void movePlayer() 
    {
        Vector3 movePos = new Vector3(moveD.x * moveSpeed, 0f, moveD.y * moveSpeed);
        transform.Translate(movePos * moveSpeed * Time.deltaTime, Space.World);
    }

}
