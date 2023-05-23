using UnityEngine;
using UnityEngine.InputSystem;
public class MoveRigidbody : MonoBehaviour
{
    public Rigidbody Rb;
    public float moveSpeed = 5f;
    Vector2 moveD = Vector2.zero;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveD = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Rb.velocity = new Vector3(moveD.x * moveSpeed,0f,moveD.y * moveSpeed);
    }


}
