using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * TODO:
 * 二点を移動する、二次ベジエなどが必要かもしれない。
 * メトロノーム機能、タップ機能
 * githubへプロジェクトを登録する
 * 二台目追加、シリアルナンバーを指定すると可能
 * ポイントクラウドをカスタムする
 * 
 * 
 */



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
