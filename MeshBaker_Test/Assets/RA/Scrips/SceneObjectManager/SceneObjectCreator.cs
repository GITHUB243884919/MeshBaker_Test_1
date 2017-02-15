/// <summary>
/// 场景对象生成器
/// autor: fanzhengyong
/// date: 2017-02-15 
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//对象类型
public enum E_SCENE_OBJECT_TYPE
{
    TANK,
    SOLDIER
}

public class CreatorParam
{
    public string[] m_paths;
    public int m_count;
    public SceneObjectCreator m_creator;
}

public class SceneObjectCreator
{
    public GameObject m_creator;
    public E_SCENE_OBJECT_TYPE m_type;

    //初始化生成位置固定,是一个在场景中看不到的地方。
    private readonly Vector3 _m_pos = new Vector3(0f, -10f, 0f);
    private readonly float  _m_maxBoundSide = 1000f;

    //被生成的对象，称为种子。春天把一个坦克埋进去，到秋天长出好多坦克:)
    public GameObject m_seed;

    //生成的对象个数，生成后固定不变
    public int m_count;

    private MB3_MeshBaker _m_meshBaker;

    private SceneObjectCreator() { }

    /// <summary>
    /// </summary>
    /// <param name="path">
    /// path[0] meshbaker生成器资源路径
    /// path[1] meshbaker材质资源路径
    /// path[2] meshbaker贴图资源路径
    /// path[3] meshbaker合并对象资源路径
    /// </param>
    /// <param name="type"></param>
    /// <param name="count"></param>
    public SceneObjectCreator(string [] paths, E_SCENE_OBJECT_TYPE type, int count)
    {
        GameObject bakerRes = Resources.Load<GameObject>(paths[0]);
        m_creator = GameObject.Instantiate<GameObject>(bakerRes);

        MB3_TextureBaker textureBaker = m_creator.GetComponent<MB3_TextureBaker>();
        _m_meshBaker = m_creator.GetComponentInChildren<MB3_MeshBaker>();

        InitBaker(paths, textureBaker, _m_meshBaker);

        m_type = type;
        GameObject seedRes = Resources.Load<GameObject>(paths[3]);
        m_seed = GameObject.Instantiate<GameObject>(seedRes);
        m_seed.transform.position = _m_pos;

        m_count = count;
    }

    private bool InitBaker(string[] paths, MB3_TextureBaker textureBaker, MB3_MeshBaker meshBaker)
    {
        bool result = false;

        Material material = Resources.Load<Material>(paths[1]);
        if (material == null)
        {
            return result;
        }

        MB2_TextureBakeResults textureBakeResults =
            Resources.Load<MB2_TextureBakeResults>(paths[2]);
        if (textureBakeResults == null)
        {
            return result;
        }

        textureBaker.resultMaterial = material;
        textureBaker.textureBakeResults = textureBakeResults;
        meshBaker.textureBakeResults = textureBakeResults;

        result = true;
        return result;
    }

    private void SetCombinMeshBund()
    {
        //GameObject go = GameObject.Find("CombinedMesh-MeshBaker-mesh");
        //SkinnedMeshRenderer smr = go.GetComponentInChildren<SkinnedMeshRenderer>();

    }
    public GameObject[] CreateObject()
    {
        GameObject[] objects = new GameObject[m_count];
        for (int i = 0; i < m_count; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(m_seed);
            go.transform.position = _m_pos;
            objects[i] = go;
        }

        //人为调整合并后smr的bound
        objects[0].transform.position = new Vector3(_m_maxBoundSide, _m_pos.y, _m_pos.z);
        objects[1].transform.position = new Vector3(-_m_maxBoundSide, _m_pos.y, _m_pos.z);
        objects[2].transform.position = new Vector3(_m_pos.x, _m_pos.y, _m_maxBoundSide);
        objects[3].transform.position = new Vector3(_m_pos.x, _m_pos.y, -_m_maxBoundSide);

        _m_meshBaker.AddDeleteGameObjects(objects, null, true);
        _m_meshBaker.Apply();
        
        return objects;
    }
}
