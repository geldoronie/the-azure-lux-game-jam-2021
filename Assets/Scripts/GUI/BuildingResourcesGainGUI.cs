using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingResourcesGainGUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _foodText;
    [SerializeField] private TMP_Text _woodText;
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private TMP_Text _stoneText;
    [SerializeField] private TMP_Text _peopleText;
    [SerializeField] private TMP_Text _militaryText;
    [SerializeField] private float _stayTime;
    [SerializeField] private float _fadeTime;

    private Transform _follower;

    public void Initialize(Transform follower, ResourcesAmounts resources)
    {
        _foodText.SetText(resources.Food.ToString("+#;-#;0"));
        _foodText.transform.parent.gameObject.SetActive(resources.Food != 0);

        _woodText.SetText(resources.Wood.ToString("+#;-#;0"));
        _woodText.transform.parent.gameObject.SetActive(resources.Wood != 0);

        _goldText.SetText(resources.Gold.ToString("+#;-#;0"));
        _goldText.transform.parent.gameObject.SetActive(resources.Gold != 0);

        _stoneText.SetText(resources.Stone.ToString("+#;-#;0"));
        _stoneText.transform.parent.gameObject.SetActive(resources.Stone != 0);

        _peopleText.SetText(resources.People.ToString("+#;-#;0"));
        _peopleText.transform.parent.gameObject.SetActive(resources.People != 0);

        _militaryText.SetText(resources.Military.ToString("+#;-#;0"));
        _militaryText.transform.parent.gameObject.SetActive(resources.Military != 0);

        _follower = follower;

        StartCoroutine(FadeCoroutine());
    }

    private void Update()
    {
        Vector3 convertedPositon = Camera.main.WorldToScreenPoint(_follower.position);
        transform.position = convertedPositon;
    }

    private IEnumerator FadeCoroutine()
    {
        yield return new WaitForSeconds(_stayTime);
        float timer = 0;
        float alpha = 1;
        Image[] images = GetComponentsInChildren<Image>();
        while (timer < _fadeTime)
        {
            timer += Time.deltaTime;
            alpha = 1 - timer / _fadeTime;
            _foodText.color = new Color(_foodText.color.r, _foodText.color.g, _foodText.color.b, alpha);
            _woodText.color = new Color(_woodText.color.r, _woodText.color.g, _woodText.color.b, alpha);
            _goldText.color = new Color(_goldText.color.r, _goldText.color.g, _goldText.color.b, alpha);
            _stoneText.color = new Color(_stoneText.color.r, _stoneText.color.g, _stoneText.color.b, alpha);
            _peopleText.color = new Color(_peopleText.color.r, _peopleText.color.g, _peopleText.color.b, alpha);
            _militaryText.color = new Color(_militaryText.color.r, _militaryText.color.g, _militaryText.color.b, alpha);
            for (int i = 0; i < images.Length; i++)
            {
                images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, alpha);
            }
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
