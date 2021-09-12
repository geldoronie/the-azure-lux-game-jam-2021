using UnityEngine;

public class CameraTooltipTrigger : MonoBehaviour
{
    [SerializeField] private float delayTooltipTime = 1;

    private Terrain currentTerrain;
    private float currentTime;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Terrain terrain = hit.collider.GetComponent<Terrain>();
            if (terrain != null)
            {
                if (currentTerrain != null && terrain != currentTerrain)
                {
                    HideTooltip();
                }
                else
                {
                    currentTerrain = terrain;
                    currentTime += Time.deltaTime;
                    if (currentTime > delayTooltipTime)
                    {
                        MainCanvas.Instance.ShowTooltip(currentTerrain.GetTooltip());
                    }
                }
            }
            else
            {
                HideTooltip();
            }
        }
        else
        {
            HideTooltip();
        }

    }

    private void HideTooltip()
    {
        currentTime = 0;
        currentTerrain = null;
        MainCanvas.Instance.HideTooltip();
    }
}