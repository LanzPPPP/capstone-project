using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public Camera cameraSource;
    public float interactRange = 1.25f;
    public KeyCode keybind;


    void Update()
    {
        if (!Input.GetKeyDown(keybind)) return;

        Ray ray = new Ray(cameraSource.transform.position, cameraSource.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }
}
