using UnityEngine;

public class DebugConsoleControl : MonoBehaviour
{
    public ModalControl consoleModal;
    public KeyCode consoleKey;


    void Update()
    {
        if (Input.GetKeyDown(consoleKey))
            consoleModal.Toggle();
    }
}
