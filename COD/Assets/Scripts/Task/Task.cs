using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image taskImage;
    public CanvasGroup canvasGroup;

    [HideInInspector] public bool isFixed;

    private Action OnFix;


    public void Initialize(string taskTitle, Color taskColor, Action onFix)
    {
        text.text = taskTitle;
        taskImage.color = taskColor;

        OnFix = onFix;
    }

    public void Finish()
    {
        if (isFixed)
            return;

        isFixed = true;
        OnFix?.Invoke();

        taskImage.gameObject.LeanScaleX(1f, 1f)
            .setEase(LeanTweenType.easeOutQuad)
            .setIgnoreTimeScale(true)
            .setOnComplete(() =>
            {
                canvasGroup.LeanAlpha(0f, 0.5f)
                    .setIgnoreTimeScale(true)
                    .setEase(LeanTweenType.easeOutQuad)
                    .setDelay(0.5f)
                    .setOnComplete(() =>
                    {
                        RectTransform parentRectTransform = transform.parent.GetComponent<RectTransform>();

                        gameObject.LeanScaleY(0f, 0.2f)
                            .setIgnoreTimeScale(true)
                            .setOnComplete(() => Destroy(gameObject))
                            .setEase(LeanTweenType.easeOutQuad)
                            .setOnUpdate((float f) =>
                            {
                                LayoutRebuilder.ForceRebuildLayoutImmediate(parentRectTransform);
                            });

                    });
            });
    }

    public void SetTitle(string taskTitle)
    {
        text.text = taskTitle;
    }
}
