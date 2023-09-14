using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseModal : MonoBehaviour
{
    public ModalControl pauseModalControl;
    public ModalControl settingsModalControl;

    private float defaultTimeScale = 1f;


    public void Toggle()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) // if the current scene is main menu
        {
            return;
        }

        if (gameObject.activeInHierarchy)
        {
            pauseModalControl.Close();
            settingsModalControl.Close();
        }

        else
            pauseModalControl.Open();
    }
    
    public void Resume()
    {
        pauseModalControl.Close();
    }

    public void Restart()
    {
        FeedbackModalControl.Open(
            "Restart?",
            "Restart current level? Your current progress will be lost.",
            () => { SceneSwitcher.LoadScene(SceneManager.GetActiveScene().buildIndex); },
            () => {  });
    }

    public void Settings()
    {
        settingsModalControl.Open();
    }

    public void MainMenu()
    {
        FeedbackModalControl.Open(
            "Return to main menu?",
            "Return to main menu? Your current progress will be lost.",
            () => { /* TODO: Main Menu */ },
            () => {  });
    }

    void OnEnable()
    {
        defaultTimeScale = Time.timeScale;
        Time.timeScale = 0f;
    }

    void OnDisable()
    {
        Time.timeScale = defaultTimeScale;
    }
}
