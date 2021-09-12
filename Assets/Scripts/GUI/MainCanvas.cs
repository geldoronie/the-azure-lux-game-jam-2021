using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private Tooltip tooltip;

    private static MainCanvas instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There are more than one instance of the MainCanvas!");
        }
        else
        {
            instance = this;
            HideTooltip();
        }
    }

    public void ShowTooltip(ToolTipInformation information)
    {
        tooltip.SetTexts(information);
        tooltip.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltip.gameObject.SetActive(false);
    }

    public static MainCanvas Instance { get => instance; }
}
