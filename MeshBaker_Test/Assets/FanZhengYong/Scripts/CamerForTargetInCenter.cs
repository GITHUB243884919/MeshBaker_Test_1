using UnityEngine;
using System.Collections;

public class CamerForTargetInCenter : MonoBehaviour 
{
    public Transform  target;


	void Start () 
    {
        Vector3 origin = transform.position;
        transform.RotateAround(origin, new Vector3(160, 100, 0), 45f);
	}

}
