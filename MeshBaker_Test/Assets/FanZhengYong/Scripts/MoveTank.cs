using UnityEngine;
using System.Collections;

public class MoveTank : MonoBehaviour {

	// Use this for initialization
    //Transform trs = null;
	void Start () 
    {
	    //trs 
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y,
            transform.position.z + 5 * Time.deltaTime);
	
	}
}
