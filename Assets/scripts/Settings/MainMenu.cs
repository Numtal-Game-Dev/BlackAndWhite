using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public KeyCode menuKey = KeyCode.Escape;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject skillTreePanel;
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;
    public GameObject[] gameObjectsToDisable;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        Time.timeScale = 0f;
        foreach (GameObject obj in gameObjectsToDisable)
        {
            if (obj != null) obj.SetActive(false);
        }

        playButton.onClick.AddListener(() => {
            mainMenuPanel.SetActive(false);
            Time.timeScale = 1f;
            foreach (GameObject obj in gameObjectsToDisable)
            {
                if (obj != null) obj.SetActive(true);
            }
        });

        settingsButton.onClick.AddListener(() => {
            mainMenuPanel.SetActive(false);
            settingsPanel.SetActive(true);
        });

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(menuKey))
        {
            if (skillTreePanel != null && skillTreePanel.activeSelf)
            {
                return;
            }

            if (settingsPanel.activeSelf)
            {
                return;
            }

            if (mainMenuPanel.activeSelf)
            {
                mainMenuPanel.SetActive(false);
                Time.timeScale = 1f;
                foreach (GameObject obj in gameObjectsToDisable)
                {
                    if (obj != null) obj.SetActive(true);
                }
            }
            else
            {
                mainMenuPanel.SetActive(true);
                Time.timeScale = 0f;
                foreach (GameObject obj in gameObjectsToDisable)
                {
                    if (obj != null) obj.SetActive(false);
                }
            }
        }
    }
}
