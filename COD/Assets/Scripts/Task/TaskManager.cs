using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static bool isLevelCompleted;

    public GameObject taskPrefab;
    public ModalControl levelCompleteModalControl;

    private static List<Task> tasks;
    private static TaskManager _instance;


    void Awake()
    {
        tasks = new List<Task>();
        _instance = this;
    }

    public static Task RegisterTask(string taskDescription, Color taskColor)
    {
        GameObject taskInstance = Instantiate(_instance.taskPrefab, _instance.transform);

        Task task = taskInstance.GetComponent<Task>();
        tasks.Add(task);

        task.Initialize(taskDescription, taskColor, CheckTasks);

        return task;
    }

    private static void CheckTasks()
    {
        foreach (Task task in tasks)
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
