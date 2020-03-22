using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome : MonoBehaviour
{
    public double bpm=120;
    public int beatCountQty = 32;
    private int beatCount;
    private double timeFromPreviousBeat;
    
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

        }
        
    }


    private double GetSPB()
    {
        if (this.bpm<=0.0)
        {
            return double.MaxValue;
        }
        return 60.0/this.bpm;
    }

}
