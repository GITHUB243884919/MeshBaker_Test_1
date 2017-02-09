using UnityEngine;
using System.Collections;

public class CamerForTargetInCenter : MonoBehaviour 
{
    public Transform  target;

    private Transform oldTarTrs;

    private Transform oldCamTrs;
	void Start () 
    {
        oldTarTrs = target;
        transform.LookAt(oldTarTrs);
        oldCamTrs = transform;
	}

    void LateUpdate()
    {
        Vector3 offsetPos = transform.position - oldCamTrs.position;
        Vector3 offsetAng = transform.eulerAngles - oldCamTrs.eulerAngles;

        oldTarTrs.position += offsetPos;
        oldTarTrs.eulerAngles += offsetAng;
        transform.LookAt(oldTarTrs);
        oldCamTrs = transform;
    }

}
