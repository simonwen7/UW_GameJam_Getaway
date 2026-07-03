using UnityEngine;

public enum ObjectType
{
    Collectible,
    Enemy
}

public class FallingObject : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField]
    private Rigidbody2D rb;

    [Header("Identity")]
    [SerializeField]
    private ObjectType type;

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
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (type)
            {
                case ObjectType.Collectible:
                    // Add collectible
                    break;
                case ObjectType.Enemy:
                    // Damage player
                    break;
            }
        } else if (collision.CompareTag("ObjectCollector"))
        {
            Destroy(gameObject);
        } else return;
    }
}
