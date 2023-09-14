using TMPro;
using UnityEngine;


public class DebugTools : MonoBehaviour
{
    [Header("Keybinds")]
    public KeyCode toggleDebugInfoKey = KeyCode.F5;
    public KeyCode toggleVsyncKey = KeyCode.Q;

    [Header("FPS")]
    public TextMeshProUGUI fpsText;
    public float fpsUpdateRate = 1f;
    private float timer;

    [Header("VSync")]
    public TextMeshProUGUI vsyncText;


    void Update()
    {
        FPSCounter();

        vsyncText.text = QualitySettings.vSyncCount == 1 ? "V-Sync: true" : "V-Sync: false";
        if (Input.GetKeyDown(toggleVsyncKey))
            ToggleVSync();

        if (Input.GetKeyDown(toggleDebugInfoKey))
            ToggleDebugInfo();
    }

    void ToggleDebugInfo()
    {
        bool v = fpsText.gameObject.activeInHierarchy;
        fpsText.transform.parent.gameObject.SetActive(!v);
    }

    void FPSCounter()
    {
        if (Time.unscaledTime > timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = "FPS: " + fps;
            timer = Time.unscaledTime + fpsUpdateRate;
        }
    }

    void ToggleVSync()
    {
        QualitySettings.vSyncCount = QualitySettings.vSyncCount == 0 ? 1 : 0;
    }
}
