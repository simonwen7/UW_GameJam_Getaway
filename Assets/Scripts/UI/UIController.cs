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
    private GameObject levelCompletedUIObject;

    [SerializeField]
    private TMP_Text counterText;

    void Awake()
    {
        UpdateCounter(0);
        gameOverObject.SetActive(false);
        playingUIObject.SetActive(true);
        levelCompletedUIObject.SetActive(false);
    }

    public void ActivateGameOverUI()
    {
        gameOverObject.SetActive(true);
        playingUIObject.SetActive(false);
        levelCompletedUIObject.SetActive(false);
    }

    public void ActivateLevelCompletedUI()
    {
        gameOverObject.SetActive(false);
        playingUIObject.SetActive(false);
        levelCompletedUIObject.SetActive(true);
    }

    public void OnMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnRestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnContinueButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void UpdateCounter(int currentValue)
    {
        counterText.text = currentValue + " / 25";
    }
}
