using System;
using UnityEngine;

public class ProblemSolving : MonoBehaviour
{
    [HideInInspector]
    public bool isSolved;

    public ModalControl modalControl;
    public Action onFix;


    public void CheckAnswers()
    {
        SlotNode[] slotNodes = GetComponentsInChildren<SlotNode>();

        int totalCorrect = 0;

        foreach (SlotNode slotNode in slotNodes)
        {
            if (slotNode.IsAnswerCorrect())
                totalCorrect++;
        }

        if (totalCorrect != slotNodes.Length)
        {
            UIController.ShowFeedbackModal("Error", $"{slotNodes.Length - totalCorrect} out of {slotNodes.Length} slots are answered incorrectly or left unanswered.");
            return;
        }

        UIController.ShowFeedbackModal("Device Fixed", "Device is now fixed.", () =>
        {
            modalControl.Close();
            isSolved = true;
            onFix?.Invoke();
        });
    }

    public void ClearAnswers()
    {
        SlotNode[] slotNodes = GetComponentsInChildren<SlotNode>();

        foreach (SlotNode slotNode in slotNodes)
        {
            slotNode.ResetNodeParent();
        }
    }}
