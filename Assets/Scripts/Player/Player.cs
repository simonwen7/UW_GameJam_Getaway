using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField]
    private int health;

    [Header("Collectible Settings")]
    private int collectiblesObtained;

    [SerializeField]
    private UIController UIController;

    public void Collect()
    {
        collectiblesObtained++;
        UIController.UpdateCounter(collectiblesObtained);
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
        GameManager.Instance.GameOver();
        Destroy(gameObject);
    }
}
