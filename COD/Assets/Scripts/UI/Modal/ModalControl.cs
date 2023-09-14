using System;
using UnityEngine;

public class ModalControl : MonoBehaviour
{
    public bool animate;
    public LeanTweenType tweenType;
    public float duration = 0.2f;
    public GameObject baseChild;

    public Action onOpen;
    public Action onClose;


    public void Toggle()
    {
        if (gameObject.activeInHierarchy)
            Close();

        else
            Open();
    }

    public void Open()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        gameObject.SetActive(true);

        if (animate)
        {
            canvasGroup.alpha = 0f;
            baseChild.LeanMoveLocalY(-20f, 0f)
                .setIgnoreTimeScale(true); 

            canvasGroup.LeanAlpha(1f, duration)
                .setIgnoreTimeScale(true)
                .setEase(tweenType);
        
            baseChild.LeanMoveLocalY(0f, duration)
                .setIgnoreTimeScale(true)
                .setEase(tweenType)
                .setOnComplete(() =>
                {
                    onOpen?.Invoke();
                });

            return;
        }

        onOpen?.Invoke();
    }

    public void Close()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (animate)
        {
            canvasGroup.LeanAlpha(0f, duration)
                .setIgnoreTimeScale(true)
                .setEase(tweenType);

            baseChild
                .LeanMoveLocalY(-20f, duration)
                .setIgnoreTimeScale(true)
                .setEase(tweenType)
                .setOnComplete(() =>
                {
                    gameObject.SetActive(false);
                    onClose?.Invoke();
                });

            return;
        }

        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        ModalObserver.activeModalCount++;
    }

    void OnDisable()
    {
        ModalObserver.activeModalCount--;
    }
}
