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
        MessageBoxControl.ShowYesNo(
            "RESTART?", 
            "Your progress will be lost.",
            () => { SceneSwitcher.Restart(); }
            );
    }

    public void Settings()
    {
        settingsModalControl.Open();
    }

    public void MainMenu()
    {
        MessageBoxControl.ShowYesNo(
            "RETURN TO MAIN MENU?",
            "Your current progress will be lost.",
            () => { SceneSwitcher.LoadMenu(); }
            );
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
