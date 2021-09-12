using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhaseAnnouncementGUI : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private TMP_Text _textLabel;

    private TurnPhase _lastTurnPhase = TurnPhase.Play;
    private bool _isVisible = false;

    private void Awake()
    {
        this._hide();
    }

    private void Update()
    {
        if (GameModeBase.Instance.GameState != GameState.Playing)
            return;

        this._hideUpdateCheck();

        if (GameModeBase.Instance.CurrentTurnPhase == this._lastTurnPhase)
            return;

        if (GameModeBase.Instance.CurrentTurnType == TurnType.CPU)
            this._textLabel.text = "World: ";
        else if (GameModeBase.Instance.CurrentTurnType == TurnType.Player)
            this._textLabel.text = "Your: ";
        else
            this._textLabel.text = "";

        if (GameModeBase.Instance.CurrentTurnPhase == TurnPhase.Main)
        {
            this._textLabel.text += "Main Phase";
        }
        else if (GameModeBase.Instance.CurrentTurnPhase == TurnPhase.Destroy)
        {
            this._textLabel.text += "Destroy Phase";
        }
        else if (GameModeBase.Instance.CurrentTurnPhase == TurnPhase.Disaster)
        {
            this._textLabel.text += "Disaster Phase";
        }
        else if (GameModeBase.Instance.CurrentTurnPhase == TurnPhase.Draw)
        {
            this._textLabel.text += "Draw Phase";
        }
        else if (GameModeBase.Instance.CurrentTurnPhase == TurnPhase.Military)
        {
            this._textLabel.text += "Military Phase";
        }
        else if (GameModeBase.Instance.CurrentTurnPhase == TurnPhase.Play)
        {
            this._textLabel.text += "Play Phase";
        }
        else if (GameModeBase.Instance.CurrentTurnPhase == TurnPhase.Refill)
        {
            this._textLabel.text += "Refill Phase";
        }

        this._lastTurnPhase = GameModeBase.Instance.CurrentTurnPhase;

        this._show();
    }

    private void _show()
    {
        this.gameObject.GetComponent<Image>().enabled = true;
        this._textLabel.transform.gameObject.SetActive(true);
        this._isVisible = true;
        this._timer.StartTime = 3;
        this._timer.ResetTimer();
        this._timer.StartTimer();
    }

    private void _hide()
    {
        this.gameObject.GetComponent<Image>().enabled = false;
        this._textLabel.transform.gameObject.SetActive(false);
        this._isVisible = false;
    }

    private void _hideUpdateCheck()
    {
        if (!this._isVisible)
            return;

        if (this._timer.Remaining <= 0)
            this._hide();
    }
}
