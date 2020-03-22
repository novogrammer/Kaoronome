using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera camera;
    public GameObject VirtualCameraA;
    public GameObject VirtualCameraB;
    private Metronome metronome;

    // Start is called before the first frame update
    void Start()
    {
        this.camera = GetComponent<Camera>();
        this.metronome = GetComponent<Metronome>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionA = VirtualCameraA.transform.localPosition;
        Quaternion rotationA = VirtualCameraA.transform.localRotation;
        Vector3 positionB = VirtualCameraB.transform.localPosition;
        Quaternion rotationB = VirtualCameraB.transform.localRotation;

        float beatProgress=this.metronome.BeatProgress;
        int beatCount = this.metronome.BeatCount;
        Vector3 position;
        Quaternion rotation;
        if (beatCount % 2 == 0)
        {
            position = Vector3.Lerp(positionA, positionB, beatProgress);
            rotation = Quaternion.Slerp(rotationA, rotationB, beatProgress);
        }
        else
        {
            position = Vector3.Lerp(positionB, positionA, beatProgress);
            rotation = Quaternion.Slerp(rotationB, rotationA, beatProgress);
        }
        this.transform.SetPositionAndRotation(position, rotation);
    }
}
;