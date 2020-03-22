using System .Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    private float bpm =60;
    public float BPM
    {
        get { return this.bpm; }
    }
    public float SPB
    {
        get
        {
            if (this.bpm <= 0.0)
            {
                return float.MaxValue;
            }
            return 60.0f / this.bpm;
        }
    }
    public int beatCountQty = 32;
    private int beatCount;
    public int BeatCount
    {
        get { return this.beatCount; }
    }
    public float BeatProgress
    {
        get { return this.TimeFromPreviousBeat / this.SPB; }
    }
    public float TimeFromPreviousBeat
    {
        get { return this.timeFromPreviousBeat; }
    }
    private float timeFromPreviousBeat;

    private float previousTapTime;

    private const float BPM_MIN = 15.0f;
    private const float BPM_MAX = 500.0f;
    
    void Start()
    {
        this.previousTapTime = Time.time;

    }


    void FixedUpdate()
    {
        float spb = this.SPB;
        this.timeFromPreviousBeat += Time.fixedDeltaTime;
        if (spb <= this.timeFromPreviousBeat)
        {
            this.beatCount=(this.beatCount+1)%this.beatCountQty;
            this.timeFromPreviousBeat -= spb;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float time = Time.time;
            float duration = time - this.previousTapTime;
            float newBpm = 60.0f / duration;
            if (BPM_MIN <= newBpm && newBpm <= BPM_MAX)
            {
                if (0.5f<this.BeatProgress)
                {
                    this.beatCount = (this.beatCount + 1) % this.beatCountQty;
                }
                this.bpm = newBpm;
                Debug.Log("bpm:" + this.bpm);

                this.timeFromPreviousBeat = 0.0f;
            }
            this.previousTapTime = time;
        }
    }




}
