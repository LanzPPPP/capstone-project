using UnityEngine;

public class ElectronicDevice : MonoBehaviour
{
    public Material fixedScreenMaterial;
    public ProblemSolving problemModal;

    private DeviceScreen deviceScreen;
    private Interactable interactable;
    private Outline outline;

    private UserTask task;
    private bool isFixed;


    void Awake()
    {
        outline = GetComponent<Outline>();
        deviceScreen = GetComponent<DeviceScreen>();

        if (problemModal == null)
        {
            deviceScreen.ChangeScreen(fixedScreenMaterial);
            Destroy(outline);
            return;
        }

        interactable = GetComponent<Interactable>();
    }

    void Start()
    {
        if (problemModal == null)
            return;

        task = TaskManager.RegisterTask($"Fix Device ({name})", outline.OutlineColor);
        problemModal.onFix += OnFix;

        interactable.interactionEvent.AddListener(Interact);
    }

    public void Interact()
    {
        problemModal.modalControl.Open();
    }

    private void OnFix()
    {
        if (isFixed)
            return;

        deviceScreen.ChangeScreen(fixedScreenMaterial);
        Destroy(outline);

        interactable.isInteractable = false;

        task.Finish();
        isFixed = true;
    }
}
