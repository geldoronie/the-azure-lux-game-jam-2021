using UnityEngine;


[CreateAssetMenu(fileName = "Nothing Effect", menuName = "Game Jam/Effects/Nothing Terrain", order = 0)]
public class EffectBase : ScriptableObject
{
    public virtual void UseEffect(Player player, Terrain terrain)
    {
        Debug.Log("This effect does nothing");
    }
}