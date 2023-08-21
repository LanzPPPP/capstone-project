using UnityEngine;

public class ComputerProblem : MonoBehaviour, IInteractable
{
    public Material turnedOnMaterial;
    public int screenMaterialIndex = 0;
    public ProblemSolving problemModal;


    void Awake()
    {
        problemModal.onFix += OnFix;
    }

    public void Interact()
    {
        if (problemModal.isSolved) 
            return;

        problemModal.modalControl.Open();
    }

    void OnFix()
    {
        Renderer screenRenderer = GetComponent<Renderer>();
        Material[] materials = screenRenderer.materials;

        materials[screenMaterialIndex] = turnedOnMaterial;
        screenRenderer.materials = materials;

        Outline outline = GetComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.green;
    }
}
