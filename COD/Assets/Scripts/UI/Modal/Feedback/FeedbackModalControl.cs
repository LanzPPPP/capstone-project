using System;
using UnityEngine;

public class FeedbackModalControl : MonoBehaviour
{
    public FeedbackModal feedbackModal;

    private static FeedbackModal instance;


    void Awake()
    {
        if (instance != null)
            return;

        instance = feedbackModal;
    }
   
    public static void Open(string title, string description, Action okayAction = null, Action cancelAction = null)
    {
        instance.Open(title, description, okayAction, cancelAction);
    }
}
