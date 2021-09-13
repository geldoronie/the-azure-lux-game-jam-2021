using System.Collections.Generic;


[System.Serializable]
public class GameStatsFullProgression
{
    public int turn;
    public double progress;
}

[System.Serializable]
public class GameStatsResourceProgression
{
    public int turn;
    public int wood;
    public int stone;
    public int gold;
    public int food;
    public int people;
    public int military;
}

[System.Serializable]
public class GameStats
{
    public string mode;
    public List<GameStatsResourceProgression> resourcesProgression;
    public List<GameStatsFullProgression> gameProgression;

}