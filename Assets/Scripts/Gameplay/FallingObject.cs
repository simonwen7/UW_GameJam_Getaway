using System.Collections;
using UnityEngine;

public enum ObjectType
{
    Collectible,
    Enemy,
    Heart
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
                    maxFallSpeed = 8f;
                    p.Collect();
                    GameFeedback.Instance?.PlayCollectFeedback();
                    break;

                case ObjectType.Enemy:
                    p.TakeDamage();
                    CameraShake.Instance?.Shake();
                    GameFeedback.Instance?.PlayHitFeedback();
                    break;

                case ObjectType.Heart:
                    p.Heal();
                    break;
            }
            
            Destroy(gameObject);

        } else if (collision.CompareTag("ObjectCollector"))
        {
            Destroy(gameObject);
        } 
    }
}
