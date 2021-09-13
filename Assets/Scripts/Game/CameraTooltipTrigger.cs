using UnityEngine;

public class CameraTooltipTrigger : MonoBehaviour
{
    [SerializeField] private float _delayTooltipTime = 1;

    private Terrain _currentTerrain;
    private float _currentTime;
    private bool _isShowingTooltip = false;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Terrain terrain = hit.collider.GetComponent<Terrain>();
            if (terrain != null)
            {
                if (_currentTerrain != null && _currentTerrain != terrain)
                {
                    HideTooltip();
                }
                else
                {
                    _currentTime += Time.deltaTime;
                    if (_currentTime > _delayTooltipTime)
                    {
                        _currentTerrain = terrain;
                        MainCanvas.Instance.ShowTooltip(_currentTerrain.GetTooltip());
                        _isShowingTooltip = true;
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
        if (_isShowingTooltip)
        {
            _isShowingTooltip = false;
            _currentTime = 0;
            _currentTerrain = null;
            MainCanvas.Instance.HideTooltip();
        }
    }
}