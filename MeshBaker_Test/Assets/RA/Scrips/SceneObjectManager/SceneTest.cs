using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneTest : MonoBehaviour 
{
	void Start () 
    {
        Debug.Log("Start");
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(BtnClick);
	}

    public void BtnClick()
    {
        Debug.Log("BtnClick");
        GameObject go = GameObject.Find("SceneObjectManager");
        Debug.Log(go == null);
        SceneObjectManager manager = go.GetComponent<SceneObjectManager>();
        Debug.Log(manager == null);
        ObjectElement obj = manager.GetOneObject(E_SCENE_OBJECT_TYPE.TANK);
        obj.m_go.transform.position = new Vector3(10, 0, 10);
        obj.m_go.name += "USED";
    }

	// Update is called once per frame
	void Update () 
    {
	
	}
}
