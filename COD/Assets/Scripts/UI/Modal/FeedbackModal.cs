using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackModal : MonoBehaviour
{
    public ModalControl modalControl;

    [Header("Containers")]
    public GameObject titleContainer;
    public GameObject descriptionContainer;

    [Header("Buttons")]
    public GameObject okayButton;
    public GameObject cancelButton;

    private Action okayAction;
    private Action cancelAction;


    public void Okay()
    {
        okayAction?.Invoke();
        modalControl.Close();
    }

    public void Cancel()
    {
        cancelAction?.Invoke();
        modalControl.Close();
    }

    public void Open(string title, string description, Action okayAction = null, Action cancelAction = null)
    {
        titleContainer.SetActive(title != "");
        descriptionContainer.SetActive(description != "");

        titleContainer.GetComponentInChildren<TextMeshProUGUI>().text = title;
        descriptionContainer.GetComponentInChildren<TextMeshProUGUI>().text = description;

        this.okayAction = okayAction;
        this.cancelAction = cancelAction;

        cancelButton.SetActive(cancelAction != null);

        modalControl.Open();
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Okay();

        else if (Input.GetKeyDown(KeyCode.Escape))
            Cancel();
    }
}
