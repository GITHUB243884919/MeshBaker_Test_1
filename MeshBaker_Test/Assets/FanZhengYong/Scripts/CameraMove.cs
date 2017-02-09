using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnDrawGizmos()
    {
        Vector3 start = transform.position;
        Vector3 end = start;
        end.x += 32;
        end.z -= 32;

        Debug.DrawLine(start, end, Color.red);
    }
}
