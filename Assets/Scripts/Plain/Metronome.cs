using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronome
{
    private float bpm;
    private int beatCountQty;
    private int beatCount;
    private float timeFromPreviousBeat;

    public float BPM
    {
        get { return this.bpm; }
        set{
            float newBpm = value;
            if (0.5f<this.BeatProgress)
            {
                this.beatCount = (this.beatCount + 1) % this.beatCountQty;
            }
            this.bpm = newBpm;
            Debug.Log("bpm:" + this.bpm);

            this.timeFromPreviousBeat = 0.0f;

        }
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
    public int BeatCountQty
    {
        get { return this.beatCountQty; }
    }
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
    public float Time
    {
        get {
            float time = this.SPB * this.beatCount + this.timeFromPreviousBeat;
            return time;
        }
        set
        {
            float time = value;
            this.timeFromPreviousBeat = 0;
            this.beatCount = 0;
            this.Update(time);
        }
    }

    public struct MetronomeSnapshot
    {
        public float BPM;
        public float SPB;
        public int BeatCount;
        public float BeatProgress;
        public float TimeFromPreviousBeat;

    }
    public MetronomeSnapshot GetMetronomeSnapshot()
    {
        MetronomeSnapshot metronomeSnapshot = new MetronomeSnapshot()
        {
            BPM = this.BPM,
            SPB = this.SPB,
            BeatCount = this.BeatCount,
            BeatProgress = this.BeatProgress,
            TimeFromPreviousBeat = this.TimeFromPreviousBeat,
        };
        return metronomeSnapshot;
    }



    public Metronome(float bpm, int beatCountQty)
    {
        this.bpm = bpm;
        this.beatCountQty = beatCountQty;

    }
    public void Update(float dt)
    {
        float spb = this.SPB;
        this.timeFromPreviousBeat += dt;
        while(spb <= this.timeFromPreviousBeat)
        {
            this.beatCount = (this.beatCount + 1) % this.beatCountQty;
            this.timeFromPreviousBeat -= spb;
        }

    }
}
