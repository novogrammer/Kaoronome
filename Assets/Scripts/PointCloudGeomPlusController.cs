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
        this.meshRenderer.material.SetFloat("_SPB", this.metronome.SPB);
        this.meshRenderer.material.SetFloat("_TimeFromPreviousBeat", this.metronome.TimeFromPreviousBeat);

    }

    public void UpdateTexture(Texture texture)
    {
        this.meshRenderer.material.SetTexture("_MainTex", texture);
    }
}
