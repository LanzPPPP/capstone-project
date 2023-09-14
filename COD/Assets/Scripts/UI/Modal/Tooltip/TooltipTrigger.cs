using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string title;
    [Multiline] public string description;

    private static LTDescr delay;


    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(0.5f, () =>
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
