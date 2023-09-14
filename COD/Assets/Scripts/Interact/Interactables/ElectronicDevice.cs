using UnityEngine;

public class ElectronicDevice : MonoBehaviour
{
    public Material fixedScreenMaterial;
    public ProblemSolving problemModal;

    private DeviceScreen deviceScreen;
    private Interactable interactable;
    private Outline outline;

    private Task task;
    private bool isFixed;


    void Awake()
    {
        outline = GetComponent<Outline>();

        if (problemModal == null)
        {
            Destroy(outline);
            return;
        }

        deviceScreen = GetComponent<DeviceScreen>();
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

        deviceScreen.turnedOnMaterial = fixedScreenMaterial;
        deviceScreen.ReloadScreen();

        Destroy(outline);

        gameObject.name = "Computer (Fixed)";
        interactable.isInteractable = false;

        task.Finish();
        isFixed = true;
    }
}
