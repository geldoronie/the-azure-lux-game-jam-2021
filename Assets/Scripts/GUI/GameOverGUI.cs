using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverGUI : MonoBehaviour
{

    [SerializeField] private TMP_Text _highestProgression;

    [SerializeField] private Image _gameOverPanel;

    // Start is called before the first frame update
    void Awake()
    {
        GameModeBase.Instance.OnChangeGameState += this.ShowGameOver;
    }

    // Update is called once per frame
    private void ShowGameOver()
    {
        if(GameModeBase.Instance.GameState == GameState.GameOver){
            this._highestProgression.text = "Highest Progression: " + GameModeBase.Instance.GameStats.highestProgression;
            this._gameOverPanel.gameObject.SetActive(true);
        }
    }

    private void Update(){
        if(GameModeBase.Instance.GameState != GameState.GameOver){
            this._gameOverPanel.gameObject.SetActive(false);
        }
    }
}
