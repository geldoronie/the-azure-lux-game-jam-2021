using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float tooltipDelay = 1.3f;
    [SerializeField] private string header;
    [TextArea]
    [SerializeField] private string content;

    private Coroutine coroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(ShowTooltip());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        MainCanvas.Instance.HideTooltip();
    }

    private IEnumerator ShowTooltip()
    {
        yield return new WaitForSeconds(tooltipDelay);
        MainCanvas.Instance.ShowTooltip(new ToolTipInformation(header, content));
    }
}
