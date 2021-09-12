using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTypeGUI : MonoBehaviour
{
    [SerializeField]
    private GameModeBase _gameMode;
    [SerializeField]
    private Image _youSelection; 
    [SerializeField]
    private Image _worldSelection;

    // Start is called before the first frame update
    void Awake()
    {
        this._youSelection.gameObject.SetActive(false);
        this._worldSelection.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(this._gameMode.CurrentTurnType == TurnType.CPU){
            this._youSelection.gameObject.SetActive(false);
            this._worldSelection.gameObject.SetActive(true);
        } else if(this._gameMode.CurrentTurnType == TurnType.Player){
            this._youSelection.gameObject.SetActive(true);
            this._worldSelection.gameObject.SetActive(false);
        } else {
            this._youSelection.gameObject.SetActive(false);
            this._worldSelection.gameObject.SetActive(false);
        }
    }
}
