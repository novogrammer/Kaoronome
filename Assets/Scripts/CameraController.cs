using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject VirtualCameraA;
    public GameObject VirtualCameraB;
    public GameObject VirtualCameraC;
    private MetronomeHolder metronomeHolder;

    // Start is called before the first frame update
    void Start()
    {
        this.metronomeHolder = GetComponent<MetronomeHolder>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionA = VirtualCameraA.transform.localPosition;
        Quaternion rotationA = VirtualCameraA.transform.localRotation;
        Vector3 positionB = VirtualCameraB.transform.localPosition;
        Quaternion rotationB = VirtualCameraB.transform.localRotation;
        Vector3 positionC = VirtualCameraC.transform.localPosition;
        Quaternion rotationC = VirtualCameraC.transform.localRotation;

        Metronome.MetronomeSnapshot metronomeSnapshot = this.metronomeHolder.GetMetronomeSnapshot();
        float beatProgress = metronomeSnapshot.BeatProgress;
        int beatCount = metronomeSnapshot.BeatCount;
        Vector3 position;
        Quaternion rotation;
        if (beatCount % 2 == 0)
        {
            position = this.QuadraticBezierInterpolation(positionA, positionB, positionC, beatProgress);
            rotation = this.QuadraticBezierInterpolation(rotationA, rotationB, rotationC, beatProgress);
        }
        else
        {
            position = this.QuadraticBezierInterpolation(positionC, positionB, positionA, beatProgress);
            rotation = this.QuadraticBezierInterpolation(rotationC, rotationB, rotationA, beatProgress);
        }
        this.transform.SetPositionAndRotation(position, rotation);
    }
    private Vector3 QuadraticBezierInterpolation(Vector3 a, Vector3 b, Vector3 c, float progress)
    {
        var ab = Vector3.Lerp(a, b, progress);
        var bc = Vector3.Lerp(b, c, progress);
        var abc = Vector3.Lerp(ab, bc, progress);
        return abc;
    }
    private Quaternion QuadraticBezierInterpolation(Quaternion a, Quaternion b, Quaternion c, float progress)
    {
        var ab = Quaternion.Slerp(a, b, progress);
        var bc = Quaternion.Slerp(b, c, progress);
        var abc = Quaternion.Slerp(ab, bc, progress);
        return abc;
    }
}
;