using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloudGeomPlusController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public MetronomeHolder metronomeHolder;
    void Start()
    {
        this.meshRenderer = GetComponent<MeshRenderer>();


    }

    void Update()
    {
        MetronomeHolder.MetronomeSnapshot metronomeSnapshot = this.metronomeHolder.GetMetronomeSnapshot();
        this.meshRenderer.material.SetFloat("_SPB", metronomeSnapshot.SPB);
        this.meshRenderer.material.SetFloat("_TimeFromPreviousBeat", metronomeSnapshot.TimeFromPreviousBeat);

    }

    public void UpdateTexture(Texture texture)
    {
        this.meshRenderer.material.SetTexture("_MainTex", texture);
    }
}
