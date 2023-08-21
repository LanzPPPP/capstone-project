using UnityEngine;

public class PauseModalControl : MonoBehaviour
{
    public PauseModal pauseModal;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseModal.Toggle();
        }
    }
}
