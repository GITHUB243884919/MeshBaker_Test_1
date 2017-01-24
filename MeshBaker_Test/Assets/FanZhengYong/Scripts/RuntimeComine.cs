using UnityEngine;
using System.Collections;

public class RuntimeComine : MonoBehaviour
{
    public MB3_MeshBaker meshbaker;
    public GameObject prefab;
    GameObject go1, go2;
    void Start()
    {
        // Instantiate some prefabs
        go1 = (GameObject)Instantiate(prefab);
        go1.transform.position = new Vector3(5f, 5f, 5f);
        // Can use a prefab not baked into the materials to combine
        // as long as it uses a material that has been baked
        go2 = (GameObject)Instantiate(prefab);
        go1.transform.position = new Vector3(5f, 5f, 5f);
        //Add the objects to the combined mesh
        GameObject[] objsToCombine = new GameObject[2] { go1, go2 };
        meshbaker.AddDeleteGameObjects(objsToCombine, null, true);
        //apply the changes we made this can be slow. See docs
        
        //meshbaker.ApplyAll();
        meshbaker.Apply(false, true, false, false, false, false, 
            false, false);
    }
    void Update()
    {
        go1.transform.position =
        new Vector3(5f, 5f * Mathf.Sin(Time.time), 5f);
        go2.transform.position =
        new Vector3(5f, 5f, 5f * Mathf.Cos(Time.time));
        GameObject[] objsToUpdate = new GameObject[2] { go1, go2 };
        meshbaker.UpdateGameObjects(objsToUpdate);
    }
    void LateUpdate()
    {
        //Apply, after this and other scripts have made changes
        //Only want to call this once per frame since it is slow
        //Only change verts since it is slow to update everything
        meshbaker.Apply(false, true, false,false, false, false, 
            false, false);
    }
}