using TMPro;
using UnityEngine;

public class LevelCompleteModal : MonoBehaviour
{
    public TextMeshProUGUI text;

    void OnEnable()
    {
        text.text = $"<b>Time Taken</b>: {LevelTimer.totalTimeString}";
    }

    public void MainMenu()
    {

    }

    public void NextLevel(int levelId)
    {
        
    }
}
