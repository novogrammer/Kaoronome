using System .Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    public float bpm =60;
    public int beatCountQty = 32;
    private int beatCount;
    public int BeatCount
    {
        get { return this.beatCount; }
    }
    public float BeatProgress
    {
        get { return this.timeFromPreviousBeat/ this.GetSPB(); }
    }
    private float timeFromPreviousBeat;
    
    void Start()
    {
        
        
    }


    void FixedUpdate()
    {
        var spb = this.GetSPB();
        this.timeFromPreviousBeat += Time.fixedDeltaTime;
        if (spb <= this.timeFromPreviousBeat)
        {
            this.beatCount=(this.beatCount+1)%this.beatCountQty;
            this.timeFromPreviousBeat -= spb;
            Debug.Log(this.beatCount);
        }
        
    }


    private float GetSPB()
    {
        if (this.bpm<=0.0)
        {
            return float.MaxValue;
        }
        return 60.0f/this.bpm;
    }

}
