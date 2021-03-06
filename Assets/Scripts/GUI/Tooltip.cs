using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private float scaleFactor = 0.421f;
    [SerializeField] private TMP_Text headerText;
    [SerializeField] private TMP_Text contentText;
    [SerializeField] private LayoutElement layoutElement;
    [SerializeField] private int characterWrapLimit = 100;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private Canvas canvas;

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        transform.position = mousePosition;

        Vector2 anchoredPosition = rectTransform.anchoredPosition;
        anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, 0, canvasRect.rect.width - rectTransform.rect.width * scaleFactor);
        anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, 0, canvasRect.rect.height - rectTransform.rect.height * scaleFactor);
        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void UpdateBoxSize()
    {
        int headerLength = headerText.text.Length;
        int contentLength = contentText.text.Length;

        layoutElement.enabled = headerLength > characterWrapLimit || contentLength > characterWrapLimit;
    }

    public void SetTexts(ToolTipInformation information)
    {
        UpdatePosition();
        if (!string.IsNullOrEmpty(information.header))
        {
            headerText.gameObject.SetActive(true);
            headerText.SetText(information.header);
        }
        else
        {
            headerText.gameObject.SetActive(false);
        }
        contentText.SetText(information.content);
        UpdateBoxSize();
    }
}

public class ToolTipInformation
{
    public readonly string header;
    public readonly string content;

    public ToolTipInformation(string header, string content)
    {
        this.header = header;
        this.content = content;
    }
}