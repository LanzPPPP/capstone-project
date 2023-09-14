using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public InteractOverlayControl overlayControl;

    public Camera cameraSource;
    public float interactRange = 1.25f;
    public KeyCode keybind;

    private LayerMask interactableLayerMask;


    void Awake()
    {
        interactableLayerMask = LayerMask.GetMask("Interactable");
    }

    void Update()
    {
        Ray ray = new Ray(cameraSource.transform.position, cameraSource.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out Interactable interactable))
            {
                if (!interactable.isInteractable)
                {
                    overlayControl.Hide();
                    return;
                }

                overlayControl.Show(interactable);
                
                if (Input.GetKeyDown(interactable.keybind) && ModalObserver.activeModalCount == 0)
                {
                    interactable.Interact();
                    DebugConsole.Log("Interacted with " + interactable.name);
                }
            }
            else
            {
                overlayControl.Hide();
            }
        }
        else
        {
            overlayControl.Hide();
        }
    }
}
