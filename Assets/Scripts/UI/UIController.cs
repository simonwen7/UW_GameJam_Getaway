using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("Menu Object")]
    [SerializeField]
    private GameObject gameOverObject;
    [SerializeField]
    private GameObject playingUIObject;

    [SerializeField]
    private TMP_Text counterText;

    void Awake()
    {
        gameOverObject.SetActive(false);
        playingUIObject.SetActive(true);
    }

    public void ActivateGameOverUI()
    {
        gameOverObject.SetActive(true);
        playingUIObject.SetActive(false);
    }

    public void OnMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateCounter(int currentValue)
    {
        counterText.text = currentValue + " / 25";
    }
}
