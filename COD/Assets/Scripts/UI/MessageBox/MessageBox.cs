using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    [SerializeField] private ModalControl modalControl;
    [SerializeField] private RectTransform contentRectTransform;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI contentText;

    [Header("Buttons")]
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private Button cancelButton;

    private Action yesAction;
    private Action noAction;
    private Action cancelAction;

    
    void Awake()
    {
        modalControl.onClose += () => { Destroy(gameObject); };
    }

    private void FixLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);
    }

    public void ShowOk(string title, string content, Action okAction = null)
    {
        this.yesAction += okAction;
        yesButton.onClick.AddListener(() => YesAction());
        yesButton.GetComponentInChildren<TextMeshProUGUI>().text = "OK";

        noButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        SetTitleAndContentText(title, content);
        Open();
    }

    public void ShowYesNo(string title, string content, Action yesAction = null, Action noAction = null)
    {
        this.yesAction += yesAction;
        this.noAction += noAction;

        yesButton.onClick.AddListener(() => YesAction());
        noButton.onClick.AddListener(() => NoAction());

        cancelButton.gameObject.SetActive(false);

        SetTitleAndContentText(title, content);
        Open();
    }

    public void ShowYesNoCancel(string title, string content, Action yesAction = null, Action noAction = null, Action cancelAction = null)
    {
        this.yesAction += yesAction;
        this.noAction += noAction;
        this.cancelAction += cancelAction;

        yesButton.onClick.AddListener(() => YesAction());
        noButton.onClick.AddListener(() => NoAction());
        cancelButton.onClick.AddListener(() => CancelAction());

        SetTitleAndContentText(title, content);
        Open();
    }

    private void SetTitleAndContentText(string title, string content)
    {
        titleText.text = title;
        contentText.text = content;
    }

    private void Open()
    {
        modalControl.Open();
        FixLayout();
    }

    private void Close()
    {
        modalControl.Close();
    }

    private void YesAction()
    {
        yesAction?.Invoke();
        Close();
    }

    private void NoAction()
    {
        noAction?.Invoke();
        Close();
    }

    private void CancelAction()
    {
        cancelAction?.Invoke();
        Close();
    }
}
