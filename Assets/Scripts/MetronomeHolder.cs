using System .Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetronomeHolder : MonoBehaviour
{
    private float previousTapTime;

    private const float BPM_MIN = 15.0f;
    private const float BPM_MAX = 500.0f;

    private Metronome metronome;


    void Start()
    {
        this.previousTapTime = Time.time;
        this.metronome = new Metronome(60, 32);
    }
    public Metronome.MetronomeSnapshot GetMetronomeSnapshot()
    {
        Metronome m = new Metronome(this.metronome.BPM, this.metronome.BeatCountQty);
        m.Time = this.metronome.Time;
        return m.GetMetronomeSnapshot();
        // return this.metronome.GetMetronomeSnapshot();
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
