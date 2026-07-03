using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField]
    private int health;

    [Header("Collectible Settings")]
    private int collectiblesObtained;

    public void Collect()
    {
        collectiblesObtained++;
    }

    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
