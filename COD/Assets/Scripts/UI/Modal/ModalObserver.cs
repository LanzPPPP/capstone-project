using UnityEngine;

public class ModalObserver : MonoBehaviour
{
    public static int activeModalCount;


    void Update()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                activeModalCount++;
            }
        }

        Cursor.lockState = activeModalCount == 0 ? CursorLockMode.Locked : CursorLockMode.None;
    }

    void LateUpdate()
    {
        activeModalCount = 0;
    }
}
