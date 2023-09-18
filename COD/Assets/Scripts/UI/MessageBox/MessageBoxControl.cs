using System;
using UnityEngine;

public class MessageBoxControl : MonoBehaviour
{
    [SerializeField] private GameObject messageBoxPrefab;
    [SerializeField] private Transform messageBoxParent;

    private static GameObject _messageBoxPrefab;
    private static Transform _messageBoxParent;


    void Awake()
    {
        if (_messageBoxParent != null)
            return;

        _messageBoxPrefab = messageBoxPrefab;
        _messageBoxParent = messageBoxParent;
    }

    private static MessageBox Open()
    {
        GameObject messageBox = Instantiate(_messageBoxPrefab, _messageBoxParent);
        return messageBox.GetComponent<MessageBox>();
    }

    public static void ShowOk(string title, string content, Action okAction = null)
    {
        MessageBox messageBox = Open();
        messageBox.ShowOk(title, content, okAction);
    }

    public static void ShowYesNo(string title, string content, Action yesAction = null, Action noAction = null)
    {
        MessageBox messageBox = Open();
        messageBox.ShowYesNo(title, content, yesAction, noAction);
    }

    public static void ShowYesNoCancel(string title, string content, Action yesAction = null, Action noAction = null, Action cancelAction = null)
    {
        MessageBox messageBox = Open();
        messageBox.ShowYesNoCancel(title, content, yesAction, noAction, cancelAction);
    }
}
