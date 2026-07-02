using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField]
    private Rigidbody2D rb;

    [Header("Velocities")]
    [SerializeField]
    private float moveSpeed;

    private float horizontal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        rb.linearVelocityX = horizontal * moveSpeed;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        horizontal = ctx.ReadValue<float>();
    }
}
