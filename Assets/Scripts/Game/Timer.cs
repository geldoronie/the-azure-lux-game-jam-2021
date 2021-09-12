using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    public float Remaining = 0;

    [SerializeField]
    public float StartTime = 10f;
    public bool Finished = true;
    private bool _isRunning = false;

    private void Update()
    {
        if (!this._isRunning)
            return;

        if (this.Remaining > 0)
        {
            this.Remaining -= Time.deltaTime;
        }
        else
        {
            this.Finished = true;
            this.ResetTimer();
        }
    }

    public void StartTimer()
    {
        if (this._isRunning)
            return;

        this.Remaining = this.StartTime;
        this.Finished = true;
        this._isRunning = true;
    }

    public void ResetTimer()
    {
        this.Remaining = this.StartTime;
        this._isRunning = false;
    }

    public void PauseTimer()
    {
        this._isRunning = false;
    }

    public void ResumeTimer()
    {
        this._isRunning = true;
    }
}
