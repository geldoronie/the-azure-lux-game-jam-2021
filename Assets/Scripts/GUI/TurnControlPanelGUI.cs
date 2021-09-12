using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnControlPanelGUI : MonoBehaviour
{
    [SerializeField] private Button _endTurnButton;
    [SerializeField] private Button _drawNewHandButton;

    // Update is called once per frame
    void Update()
    {
        if(
            GameModeBase.Instance.GameState == GameState.Playing &&
            GameModeBase.Instance.CurrentTurnType == TurnType.Player && 
            GameModeBase.Instance.CurrentTurnPhase == TurnPhase.Play
        ){
            this._endTurnButton.gameObject.SetActive(true);
        } else {
            this._endTurnButton.gameObject.SetActive(false);
        }
    }
}
