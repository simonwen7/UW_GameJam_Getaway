using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField]
    private Rigidbody2D rb;

    [Header("Velocity")]
    [SerializeField]
    private float maxFallSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(rb.linearVelocityY) > maxFallSpeed)
        {
            rb.linearVelocityY = -maxFallSpeed;
        }

        print(rb.linearVelocityY);
    }
}
