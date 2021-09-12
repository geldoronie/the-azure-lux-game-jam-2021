using UnityEngine;
using UnityEngine.UI;

public class TurnTypeGUI : MonoBehaviour
{
    [SerializeField] private GameModeBase _gameMode;
    [SerializeField] private Image _youSelection;
    [SerializeField] private Image _worldSelection;
    [SerializeField] private Color _isTurnColor;
    [SerializeField] private Color _isNotTurnColor;

    private void Update()
    {
        if (this._gameMode.CurrentTurnType == TurnType.CPU)
        {
            this._youSelection.color = _isNotTurnColor;
            this._worldSelection.color = _isTurnColor;
        }
        else if (this._gameMode.CurrentTurnType == TurnType.Player)
        {
            this._youSelection.color = _isTurnColor;
            this._worldSelection.color = _isNotTurnColor;
        }
        else
        {
            this._youSelection.color = _isNotTurnColor;
            this._worldSelection.color = _isTurnColor;
        }
    }
}
