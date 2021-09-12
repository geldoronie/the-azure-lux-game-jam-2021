using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceIconGUI : MonoBehaviour
{

    [SerializeField] public int Value = 0;

    private TMP_Text _valueLabel;

    // Start is called before the first frame update
    void Awake()
    {
       this._valueLabel = gameObject.GetComponentInChildren<TMP_Text>();
       this._valueLabel.text = this.Value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        this._valueLabel.text = this.Value.ToString();
    }
}
