using UnityEngine;

public class UserManual : MonoBehaviour, IInteractable
{
    public ModalControl computerManualModal;


    public void Interact()
    {
        computerManualModal.Open();
    }
}
