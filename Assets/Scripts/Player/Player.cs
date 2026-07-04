using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField]
    private int maxHealth;

    [Header("Collectible Settings")]
    private int collectiblesObtained;

    [SerializeField]
    private UIController UIController;
    [SerializeField]
    private HeartUI heartUI;

    private int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Collect()
    {
        collectiblesObtained++;
        UIController.UpdateCounter(collectiblesObtained);

        if (collectiblesObtained == 10)
        {
            BackgroundShifter.isTransition = true;
        }

        if (collectiblesObtained == 25)
        {
            GameManager.Instance.LevelCompleted();
        }
    }

    public void Heal()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            heartUI.UpdateHearts(currentHealth, maxHealth);
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        heartUI.UpdateHearts(currentHealth, maxHealth);

        if (currentHealth <= 0)
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
