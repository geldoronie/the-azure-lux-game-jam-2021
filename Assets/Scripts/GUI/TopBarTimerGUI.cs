using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBarTimerGUI : MonoBehaviour
{
    [SerializeField] Timer Timer;
    [SerializeField] TextIconGUI Display;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int minutes =  Mathf.FloorToInt(Timer.Remaining / 60);
        int seconds = Mathf.FloorToInt(Timer.Remaining % 60);
        Display.Value = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
}
