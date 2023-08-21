using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private static FeedbackModal feedbackModalInstance;

    public FeedbackModal feedbackModal;


    void Awake()
    {
        feedbackModalInstance = feedbackModal;
    }
   
    public static void ShowFeedbackModal(string title, string description, Action okayAction = null, Action cancelAction = null)
    {
        feedbackModalInstance.Open(title, description, okayAction, cancelAction);
    }
}
