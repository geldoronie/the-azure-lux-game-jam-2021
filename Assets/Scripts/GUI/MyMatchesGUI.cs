using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyMatchesGUI : MonoBehaviour
{

    [SerializeField] private PlayerStatsPlayfab _playerStatsPlayfab;

    [SerializeField] private GraphGUI _chart; 

    [SerializeField]
    private int _currentStatistic = 0;

    // Start is called before the first frame update
    void Awake()
    {
        this._playerStatsPlayfab = GameObject.Find("PlayerStatistics").GetComponent<PlayerStatsPlayfab>();
    }

    void Start(){

    }

    public void NextStatistic(){
        if(this._currentStatistic + 1 > this._playerStatsPlayfab.Matches.Count)
            return;

        this._currentStatistic++;
        this._loadStatistic(this._currentStatistic - 1);
    }

    public void PrevStatistic(){
        if(this._currentStatistic - 1 < 0)
            return;

        this._currentStatistic--;
        this._loadStatistic(this._currentStatistic - 1);
    }

    private void _loadStatistic(int index){
        List<int> points = new List<int>();
        this._playerStatsPlayfab.Matches[index].gameProgression.ForEach( p => {
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
