using UnityEngine;

public class UserManual : MonoBehaviour
{
    public ModalControl computerManualModal;
    public Outline outline;

    private Task task;


    void Start()
    {
        task = TaskManager.RegisterTask("Read User Manual", outline.OutlineColor);

        computerManualModal.onClose += delegate
        {
            task.Finish();
            outline.OutlineColor = Color.cyan;
        };
    }

    public void Interact()
    {
        computerManualModal.Open();
    }
}
