using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhaseAnnouncementGUI : MonoBehaviour
{
    [SerializeField]
    private GameModeBase _gameMode;

    [SerializeField]
    private Timer _timer;

    [SerializeField]
    private TMP_Text _textLabel;
    // Start is called before the first frame update

    private TurnPhase _lastTurnPhase = TurnPhase.Play;

    private bool _isVisible = false;
    void Awake()
    {
        this._hide();
    }

    // Update is called once per frame
    void Update()
    {
        if(!this._gameMode.IsRunning)
            return;

        this._hideUpdateCheck();

        if(this._gameMode.CurrentTurnPhase == this._lastTurnPhase)
            return;

        if(this._gameMode.CurrentTurnType == TurnType.CPU)
            this._textLabel.text = "World: ";
        else if (this._gameMode.CurrentTurnType == TurnType.Player)
            this._textLabel.text = "You: ";
        else 
            this._textLabel.text = "";

        if(this._gameMode.CurrentTurnPhase == TurnPhase.Main){
            this._textLabel.text += "Main Phase";
        } else if(this._gameMode.CurrentTurnPhase == TurnPhase.Destroy){
            this._textLabel.text += "Destroy Phase";
        } else if(this._gameMode.CurrentTurnPhase == TurnPhase.Disaster){
            this._textLabel.text += "Disaster Phase";
        } else if(this._gameMode.CurrentTurnPhase == TurnPhase.Draw){
            this._textLabel.text += "Draw Phase";
        } else if(this._gameMode.CurrentTurnPhase == TurnPhase.Military){
            this._textLabel.text += "Military Phase";
        } else if(this._gameMode.CurrentTurnPhase == TurnPhase.Play){
            this._textLabel.text += "Play Phase";
        } else if(this._gameMode.CurrentTurnPhase == TurnPhase.Refill){
            this._textLabel.text += "Refill Phase";
        }

        this._show();
    }

    private void _show(){
        this.gameObject.GetComponent<Image>().enabled = true;
        this._textLabel.transform.gameObject.SetActive(true);
        this._isVisible = true;
        this._timer.StartTime = 3;
        this._timer.ResetTimer();
        this._timer.StartTimer();
    }

    private void _hide(){
        this.gameObject.GetComponent<Image>().enabled = false;
        this._textLabel.transform.gameObject.SetActive(false);
        this._isVisible = false;
    }

    private void _hideUpdateCheck(){
        if (!this._isVisible)
            return;

        if(this._timer.Remaining <= 0)
            this._hide();
    }
}
