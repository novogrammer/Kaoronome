using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloudGeomPlusController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public Metronome metronome;
    void Start()
    {
        this.meshRenderer = GetComponent<MeshRenderer>();


    }

    void Update()
    {
        this.meshRenderer.sharedMaterial.SetFloat("_SPB", this.metronome.SPB);
        this.meshRenderer.sharedMaterial.SetFloat("_TimeFromPreviousBeat", this.metronome.TimeFromPreviousBeat);

    }
}
