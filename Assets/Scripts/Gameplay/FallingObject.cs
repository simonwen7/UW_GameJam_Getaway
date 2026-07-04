using System.Collections;
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

    private Animator anim;
    private float animDuration;

    void Awake()
    {
        if (type == ObjectType.Collectible)
        {
            anim = GetComponent<Animator>();
            AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;

            foreach (AnimationClip  clip in clips)
            {
                if (clip.name == "suitcaseCollected")
                {
                    animDuration = clip.length;
                }
            }
        }
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
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        bc.enabled = false;
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
                    StartCoroutine(playCollectedAnimation());
                    break;

                case ObjectType.Enemy:
                    p.TakeDamage();
                    CameraShake.Instance?.Shake();
                    GameFeedback.Instance?.PlayHitFeedback();
                    Destroy(gameObject);
                    break;
            }
            
        } else if (collision.CompareTag("ObjectCollector"))
        {
            Destroy(gameObject);
        } 
    }

    IEnumerator playCollectedAnimation()
    {
        anim.SetTrigger("Collected");
        
        yield return new WaitForSeconds(animDuration);

        Destroy(gameObject);
    }
}
