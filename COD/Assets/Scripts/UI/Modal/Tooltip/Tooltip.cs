using TMPro;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    public RectTransform titleRect;
    public RectTransform descriptionRect;

    public LayoutElement layoutElement;

    public RectTransform baseRect;

    private CanvasGroup canvasGroup;


    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Open(string title, string description)
    {
        this.title.text = title;
        this.title.gameObject.SetActive(!string.IsNullOrEmpty(title));

        this.description.text = description;

        canvasGroup.LeanAlpha(1f, 0.1f);

        layoutElement.enabled = 
            LayoutUtility.GetPreferredWidth(titleRect) >= layoutElement.preferredWidth - 10 ||
            LayoutUtility.GetPreferredWidth(descriptionRect) >= layoutElement.preferredWidth - 10;
    }

    public void Close()
    {
        canvasGroup.LeanAlpha(0f, 0.1f);
    }

    void Update()
    {
        Vector2 position = Input.mousePosition;
        Vector2 pivot = Vector2.zero;

        pivot.y = position.y + baseRect.sizeDelta.y / 2 <= Screen.height ? 0 : 1;

        if (position.x + baseRect.sizeDelta.x / 2 > Screen.width)
            pivot.x = 1f;

        else if (position.x - baseRect.sizeDelta.x / 2 < 0)
            pivot.x = 0f;
        
        else
            pivot.x = 0.5f;

        baseRect.position = position;
        baseRect.pivot = pivot;
    }
}
