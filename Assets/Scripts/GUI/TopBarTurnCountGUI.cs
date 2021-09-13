using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBarTurnCountGUI : MonoBehaviour
{
    [SerializeField] TextIconGUI Display;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Display.Value = ((GameModeBase.Instance.CurrentTurn == 0 ? GameModeBase.Instance.CurrentTurn : GameModeBase.Instance.CurrentTurn / 2 ) + 1).ToString();
    }
}
