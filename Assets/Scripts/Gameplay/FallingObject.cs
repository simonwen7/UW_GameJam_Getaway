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
            Player p = collision.GetComponent<Player>();

            if (p == null) return;

            switch (type)
            {
                case ObjectType.Collectible:
                    p.Collect();
                    break;
                case ObjectType.Enemy:
                    p.TakeDamage();
                    break;
            }

            Destroy(gameObject);

        } else if (collision.CompareTag("ObjectCollector"))
        {
            Destroy(gameObject);
        } 
    }
}
