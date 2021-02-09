using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCameraScript : MonoBehaviour
{
    public Camera cameraToLookAt;
    public Canvas controllingCanvas;

    // Start is called before the first frame update
    void Start()
    {
        cameraToLookAt = Camera.main;
        controllingCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        lookAt(cameraToLookAt.transform.position);
    }

    private void lookAt(Vector3 positionToLookAt)
    {
        //Vector3 forward = (positionToLookAt + cameraToLookAt.transform.rotation * Vector3.forward) - controllingCanvas.transform.position;
        Vector3 forward = cameraToLookAt.transform.rotation * Vector3.forward;
        Vector3 up = cameraToLookAt.transform.rotation * Vector3.up;
        //up.y = 90.0f;
        //up.z = 0.0f;

        controllingCanvas.transform.rotation = Quaternion.LookRotation(forward, up);
    }

}
