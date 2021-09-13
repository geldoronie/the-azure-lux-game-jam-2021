using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphGUI : MonoBehaviour
{
    [SerializeField] private Sprite _circleSprite;
    private RectTransform _container;

    // Start is called before the first frame update
    void Awake()
    {
        this._container = transform.Find("Container").GetComponent<RectTransform>();
        //List<float> values = new List<float>() { 3,10, 20,60,8,90,40,100};
        //this.LoadGraph(values);
    }

    private GameObject _createPoint(Vector2 anchoredPosition){
        GameObject gameObject = new GameObject("circle",typeof(Image));
        gameObject.transform.SetParent(this._container,false);
        gameObject.GetComponent<Image>().sprite = this._circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11,11);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
        return gameObject;
    }

    private void _createPointConnection(Vector2 dotA, Vector2 dotB){
        GameObject gameObject = new GameObject("connection", typeof(Image));
        gameObject.transform.SetParent(this._container,false);
        gameObject.GetComponent<Image>().color = new Color(1,1,1, 0.5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotB - dotA).normalized;
        float distance = Vector2.Distance(dotA,dotB);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);
        rectTransform.sizeDelta = new Vector2(distance,3f);
        rectTransform.anchoredPosition = dotA + dir * distance * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
        
    }

    public void LoadGraph(List<float> points){
        float graphHeight = this._container.sizeDelta.y;
        float yMaximum = 100f;
        float xSize = 50f;

        GameObject lastPointGameObject = null;
        for (int i = 0; i < points.Count; i++){
            float xPosition = xSize + i * xSize;
            float yPosition = (points[i] / yMaximum) * graphHeight;
            GameObject pointGameObject = this._createPoint(new Vector2(xPosition,yPosition));
            if (lastPointGameObject != null){
                this._createPointConnection(
                    lastPointGameObject.GetComponent<RectTransform>().anchoredPosition,
                    pointGameObject.GetComponent<RectTransform>().anchoredPosition
                );
            }
            lastPointGameObject = pointGameObject;
        }
    }

    private float GetAngleFromVectorFloat(Vector3 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

}
