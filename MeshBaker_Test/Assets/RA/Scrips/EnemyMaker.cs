using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMaker : MonoBehaviour
{
    //我们的水管工人预置
    public GameObject prefab;
    //用来管理敌人的网格
    public MB3_MeshBaker meshBaker;
    //用来记录已经生成的预置
    private List<GameObject> prefabs;
    // Use this for initialization
    void Awake()
    {
        prefabs = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "增加预置"))
        {
            Vector3 pos = new Vector3(Random.Range(-2, 2), 1, Random.Range(-2, 2));
            Quaternion dir = Quaternion.Euler(new Vector3(0, Random.Range(0, 360.0f), 0));
            //生成一个 工人预设
            GameObject worker = Instantiate(prefab, pos, dir) as GameObject;
            GameObject firstMeshRenderer = null;
            //获取对象身上的所有renderer组件对象
            List<GameObject> needAddList = getRenderers(worker, ref firstMeshRenderer);

            meshBaker.AddDeleteGameObjects(needAddList.ToArray(), null, true);
            meshBaker.Apply();

            //由于一个物体身上SkinnedMeshRenderer和MeshRenderer一起合并的话不知道为什么人物无法移动，
            //但是把某个MeshRenderer启用就可以移动了，
            //不知道是什么原因，如果你知道为什么，请留言~~~谢谢
            if (firstMeshRenderer)
            {
                firstMeshRenderer.transform.GetComponent<Renderer>().materials = new Material[0] { };
                firstMeshRenderer.transform.GetComponent<Renderer>().enabled = true;
            }

            prefabs.Add(worker);

        }

        if (GUI.Button(new Rect(110, 0, 100, 100), "删除第一个"))
        {
            if (prefabs.Count > 0)
            {
                GameObject worker = prefabs[0];
                GameObject firstMeshRenderer = null;
                List<GameObject> needDelList = getRenderers(worker, ref firstMeshRenderer);

                meshBaker.AddDeleteGameObjects(null, needDelList.ToArray(), true);
                meshBaker.Apply();

                Destroy(prefabs[0].gameObject);

                prefabs.RemoveAt(0);


            }
        }

        GUI.Label(new Rect(0, 110, 200, 100), "当前预设数量：" + prefabs.Count);
    }


    List<GameObject> getRenderers(GameObject obj, ref GameObject firstMeshRenderer)
    {
        List<GameObject> needAddList = new List<GameObject>();

        SkinnedMeshRenderer[] smrs = obj.GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < smrs.Length; i++)
        {
            needAddList.Add(smrs[i].gameObject);
        }

        MeshRenderer[] mr = obj.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < mr.Length; i++)
        {
            if (i == 0)
            {
                firstMeshRenderer = mr[i].gameObject;
            }
            needAddList.Add(mr[i].gameObject);
        }
        return needAddList;
    }
}
