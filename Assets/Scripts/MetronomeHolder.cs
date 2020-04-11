using System .Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetronomeHolder : MonoBehaviour
{
    private float previousTapTime;

    private const float BPM_MIN = 15.0f;
    private const float BPM_MAX = 500.0f;

    private Metronome metronome;

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
            BPM = this.metronome.BPM,
            SPB = this.metronome.SPB,
            BeatCount = this.metronome.BeatCount,
            BeatProgress = this.metronome.BeatProgress,
            TimeFromPreviousBeat = this.metronome.TimeFromPreviousBeat,
        };
        return metronomeSnapshot;
    }

    void Start()
    {
        this.previousTapTime = Time.time;
        this.metronome = new Metronome(60, 32);
    }


    void FixedUpdate()
    {
        this.metronome.Update(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float time = Time.time;
            float duration = time - this.previousTapTime;
            float newBpm = 60.0f / duration;
            if (BPM_MIN <= newBpm && newBpm <= BPM_MAX)
            {
                this.metronome.BPM = newBpm;
            }
            this.previousTapTime = time;
        }
    }




}
