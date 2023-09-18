using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float showDelay = 0.5f;
    public string title;
    [Multiline] public string description;

    private static LTDescr delay;


    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(showDelay, () =>
        {
            TooltipControl.Open(title, description);
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipControl.Close();
    }
}
