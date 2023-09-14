using System;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private static float totalTime;
    public static string totalTimeString
    {
        get
        {
            float currentTotalTime = totalTime;
            int totalHours = 0;
            int totalMinutes = 0;

            if (currentTotalTime >= 3600)
            {
                totalHours = (int)currentTotalTime / 3600;
                currentTotalTime -= totalHours * 3600;
            }

            if (currentTotalTime >= 60)
            {
                totalMinutes = (int)currentTotalTime / 60;
                currentTotalTime -= totalMinutes * 60;
            }

            int totalSeconds = (int)Math.Round(currentTotalTime);

            string timeString = "";

            if (totalHours > 0)
                timeString += $"{totalHours}h";

            if (totalMinutes > 0 || totalHours > 0)
                timeString += $" {totalMinutes}m";

            timeString += $" {totalSeconds}s";
            return timeString;
        }
    }

    void Awake()
    {
        totalTime = 0f;
        InvokeRepeating("UpdateTimeDisplay", 1f, 1f);
    }

    void UpdateTimeDisplay()
    {
        timeText.text = totalTimeString;
    }

    void Update()
    {
        if (TaskManager.isLevelCompleted) 
            return;

        totalTime += Time.deltaTime;
    }
}
