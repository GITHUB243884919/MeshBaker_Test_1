using UnityEngine;
using System.Collections;

public class CamerForTargetInCenter : MonoBehaviour 
{
    public Vector3  targetPos;

    private Vector3 originCameraPos;
    private Vector3 normalOriginCameraPos;
    private Vector3 normalToTargetPos;
	void Start () 
    {
        originCameraPos = transform.position;
        normalOriginCameraPos = Vector3.Normalize(originCameraPos);
        normalToTargetPos = Vector3.Normalize(originCameraPos - targetPos);
        float angle = Vector3.Dot(normalToTargetPos, normalOriginCameraPos);
        Debug.Log("angle=" + angle);
        Debug.Log(Vector3.Angle(normalOriginCameraPos, normalToTargetPos));

	}
	

}
