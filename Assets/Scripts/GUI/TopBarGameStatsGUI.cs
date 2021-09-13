using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopBarGameStatsGUI : MonoBehaviour
{

    [SerializeField] private TextIconGUI _progression;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameModeBase.Instance.GameState != GameState.Playing)
            return;

        if(GameModeBase.Instance.GameStats.gameProgression.Count == 0)
            return;

        GameStatsFullProgression progressionStats = GameModeBase.Instance.GameStats.gameProgression[GameModeBase.Instance.GameStats.gameProgression.Count - 1];
        this._progression.Value = Mathf.RoundToInt(progressionStats.progress).ToString();
    }
}
