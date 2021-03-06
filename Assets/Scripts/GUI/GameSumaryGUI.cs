using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSumaryGUI : MonoBehaviour
{

    [SerializeField] private PlayerStatsPlayfab _playerStatsPlayfab;
    [SerializeField] private TMP_Text _gameModeName;

    [SerializeField] private GraphGUI _chart;

    // Start is called before the first frame update
    void Awake()
    {
        this._playerStatsPlayfab = GameObject.Find("PlayerStatistics").GetComponent<PlayerStatsPlayfab>();
        this._gameModeName.text = this._playerStatsPlayfab.LastMatchStats.mode + " : Progression";
    }

    void Start(){
        List<int> points = new List<int>();
        this._playerStatsPlayfab.LastMatchStats.gameProgression.ForEach( p => {
            points.Add(Mathf.RoundToInt(p.progress / 1000));
        });
        this._chart.LoadGraph(points);
    }

    public void SaveStatistics(){
        this._playerStatsPlayfab.SaveLastMapStatistics();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void BackToMenu(){
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
