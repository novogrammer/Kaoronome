using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera camera;
    public GameObject VirtualCameraA;
    public GameObject VirtualCameraB;

    // Start is called before the first frame update
    void Start()
    {
        this.camera = GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionA = VirtualCameraA.transform.localPosition;
        Quaternion rotationA = VirtualCameraA.transform.localRotation;
        Vector3 positionB = VirtualCameraB.transform.localPosition;
        Quaternion rotationB = VirtualCameraB.transform.localRotation;

        float time = Time.time;
        float r = Mathf.Sin(time)*0.5f+0.5f ;
        Vector3 position = Vector3.Lerp(positionA, positionB, r);
        Quaternion rotation = Quaternion.Slerp(rotationA, rotationB, r);


        this.transform.SetPositionAndRotation(position, rotation);
    }
}
;