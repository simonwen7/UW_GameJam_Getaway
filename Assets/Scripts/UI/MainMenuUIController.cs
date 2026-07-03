using UnityEngine;

public enum MenuState
{
    Main,
    Settings,
    Tutorial
}

public class MainMenuUIController : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField]
    private GameObject tutorialMenu;

    void Awake()
    {
        ShowMenu(MenuState.Main);
    }

    public void ShowMenu(MenuState menu)
    {
        mainMenu.SetActive(menu == MenuState.Main);
        settingsMenu.SetActive(menu == MenuState.Settings);
        tutorialMenu.SetActive(menu == MenuState.Tutorial);
    }

    public void OnSettingsButton()
    {
        ShowMenu(MenuState.Settings);
    }

    public void onTutorialButton()
    {
        ShowMenu(MenuState.Tutorial);
    }

    public void OnBackButton()
    {
        ShowMenu(MenuState.Main);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    } 
    
}
