using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private Tooltip tooltip;
    [SerializeField] private BuildingResourcesGainGUI _buildingResourcesGainPrefab;

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

    public void ShowResourcesGain(Transform follow, ResourcesAmounts resources)
    {
        Vector3 convertedPositon = Camera.main.WorldToScreenPoint(follow.position);
        BuildingResourcesGainGUI instance = Instantiate<BuildingResourcesGainGUI>(_buildingResourcesGainPrefab, convertedPositon, Quaternion.identity, transform);
        instance.Initialize(follow, resources);
    }

    public static MainCanvas Instance { get => instance; }
}
