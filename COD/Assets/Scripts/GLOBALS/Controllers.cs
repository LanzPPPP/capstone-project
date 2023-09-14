using UnityEngine;
using UnityEngine.SceneManagement;

public class Controllers : MonoBehaviour
{
    private static bool isInstanced;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (isInstanced)
            Destroy(gameObject);

        else
            isInstanced = true;
    }
}
