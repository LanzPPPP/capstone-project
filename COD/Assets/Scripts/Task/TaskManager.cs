using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static bool isLevelCompleted;

    public GameObject taskPrefab;
    public ModalControl levelCompleteModalControl;

    private static List<UserTask> tasks;
    private static TaskManager _instance;


    void Awake()
    {
        isLevelCompleted = false;
        tasks = new List<UserTask>();
        _instance = this;
    }

    public static UserTask RegisterTask(string taskDescription, Color taskColor)
    {
        GameObject taskInstance = Instantiate(_instance.taskPrefab, _instance.transform);

        UserTask task = taskInstance.GetComponent<UserTask>();
        tasks.Add(task);

        task.Initialize(taskDescription, taskColor, CheckTasks);

        return task;
    }

    private static void CheckTasks()
    {
        foreach (UserTask task in tasks)
        {
            if (!task.isFixed)
                return;
        }

        isLevelCompleted = true;
        _instance.LevelComplete();
    }

    private void LevelComplete()
    {
        levelCompleteModalControl.Open();
    }
}
