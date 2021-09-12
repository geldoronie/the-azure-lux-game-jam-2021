using UnityEngine;
using UnityEngine.UI;

public class TurnControlPanelGUI : MonoBehaviour
{
    [SerializeField] private Button _endTurnButton;
    [SerializeField] private Button _drawNewHandButton;

    private void Update()
    {
        if (
            GameModeBase.Instance.GameState == GameState.Playing &&
            GameModeBase.Instance.CurrentTurnType == TurnType.Player &&
            GameModeBase.Instance.CurrentTurnPhase == TurnPhase.Play
        )
        {
            this._endTurnButton.interactable = true;
        }
        else
        {
            this._endTurnButton.interactable = false;
        }
    }
}
