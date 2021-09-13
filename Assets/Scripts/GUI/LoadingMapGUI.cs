using UnityEngine;
using UnityEngine.UI;

public class LoadingMapGUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        slider.value = GameModeBase.Instance.Map.GenerationsStatus;

        if (GameModeBase.Instance.Map.GenerationsStatus >= 1)
        {
            gameObject.SetActive(false);
        }
    }
}
