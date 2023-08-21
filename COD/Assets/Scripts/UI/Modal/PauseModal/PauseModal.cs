using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseModal : MonoBehaviour
{
    public ModalControl modalControl;


    public void Toggle()
    {
        if (gameObject.activeInHierarchy)
            modalControl.Close();

        else
            modalControl.Open();
    }
    
    public void Resume()
    {
        modalControl.Close();
    }

    public void Restart()
    {
        UIController.ShowFeedbackModal(
            "Restart?",
            "Restart current level? Your current progress will be lost.",
            () => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); },
            () => {  });
    }

    public void MainMenu()
    {
        UIController.ShowFeedbackModal(
            "Restart?",
            "Return to main menu? Your current progress will be lost.",
            () => { /* TODO: Do the main menu thing here */ },
            () => {  });
    }

    void OnEnable()
    {
        Time.timeScale = 0f;
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
