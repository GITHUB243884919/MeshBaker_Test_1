/// <summary>
/// 战斗中模型使用的对象池实现
/// author: fanzhengyong
/// date: 2017-02-20 
/// 
/// 使用DFObjectPool，因此需要实现其使用到的3个抽象类的派生类。
/// </summary>
using UnityEngine;
using System.Collections;

public class BattleModelObj : DFObject
{
    public GameObject m_go;

    public BattleScene.E_BATTLE_OBJECT_TYPE m_type;


    //服务器定义的唯一编号和类型编号
    public static readonly int BAD_ENTITY_ID = -1;
    public static readonly int BAD_ENTITY_TYPE = -1;
    public int ServerEntityID { get; set; }
    public int ServerEntityType { get; set; }

    public BattleModelObj(GameObject go, BattleScene.E_BATTLE_OBJECT_TYPE type)
    {
        m_go    = go;
        m_type  = type;
        ServerEntityID = BAD_ENTITY_ID;
        ServerEntityType = BAD_ENTITY_TYPE;

        //BattleAISteering[] m_steerings = m_go.GetComponents<BattleAISteering>();
        //for (int i = 0; i < m_steerings.Length; i++)
        //{
        //    m_steerings[i].enabled = false;
        //}

    }

    private BattleModelObj() { }

    public override void Realse()
    {

    }
}

public class BattleModelObjCreator : DFCreator
{    
    //被生成并克隆的对象，称为种子。春天把一个坦克埋进去，到秋天长出好多坦克:)
    private GameObject m_seed;
    private GameObject m_meshbakerGo;
    private BattleScene.E_BATTLE_OBJECT_TYPE m_type;
    
    //一次生成的对象个数
    public int m_count;
    
    //初始化生成位置固定,是一个在场景中看不到的地方。
    private readonly Vector3 INIT_POS = new Vector3(0f, -10f, 0f);
    private readonly float   MAX_BOUND_SIDE = 1000f;

    private MB3_MeshBaker m_meshBaker;

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
    public BattleModelObjCreator(string[] paths, BattleScene.E_BATTLE_OBJECT_TYPE type, int count)
    {
        GameObject bakerRes = Resources.Load<GameObject>(paths[0]);
        m_meshbakerGo = GameObject.Instantiate<GameObject>(bakerRes);

        MB3_TextureBaker textureBaker = m_meshbakerGo.GetComponent<MB3_TextureBaker>();
        m_meshBaker = m_meshbakerGo.GetComponentInChildren<MB3_MeshBaker>();

        InitBaker(paths, textureBaker, m_meshBaker);

        m_type = type;
        GameObject seedRes = Resources.Load<GameObject>(paths[3]);
        m_seed = GameObject.Instantiate<GameObject>(seedRes);
        m_seed.transform.position = INIT_POS;

        m_count = count;
    }
    private BattleModelObjCreator() { }

    public override DFObject[] CreateObjects()
    {
        GameObject[] gos = CreateGameObjects();
        BattleModelObj[] objs = new BattleModelObj[m_count];
        for (int i = 0; i < gos.Length; i++)
        {
            objs[i] = new BattleModelObj(gos[i], m_type);
        }
        return objs;
    }

    private GameObject[] CreateGameObjects()
    {
        GameObject[] objects = new GameObject[m_count];
        for (int i = 0; i < m_count; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(m_seed);
            go.transform.position = INIT_POS;
            objects[i] = go;
        }

        //人为调整合并后smr的bound
        objects[0].transform.position = new Vector3(MAX_BOUND_SIDE, INIT_POS.y, INIT_POS.z);
        objects[1].transform.position = new Vector3(-MAX_BOUND_SIDE, INIT_POS.y, INIT_POS.z);
        objects[2].transform.position = new Vector3(INIT_POS.x, INIT_POS.y, MAX_BOUND_SIDE);
        objects[3].transform.position = new Vector3(INIT_POS.x, INIT_POS.y, -MAX_BOUND_SIDE);

        m_meshBaker.AddDeleteGameObjects(objects, null, true);
        m_meshBaker.Apply();

        return objects;
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
}

public class BattleModelObjCreatorFactory : DFCreatorFactory
{
    private string[] m_paths;
    private BattleScene.E_BATTLE_OBJECT_TYPE m_type;
    private int m_count;

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
    public BattleModelObjCreatorFactory(string[] paths, BattleScene.E_BATTLE_OBJECT_TYPE type, int count)
    {
        m_paths = paths;
        m_type = type;
        m_count = count;
    }

    private BattleModelObjCreatorFactory() { }
    public override DFCreator CreatCreator()
    {
        BattleModelObjCreator creator = new BattleModelObjCreator(
            m_paths, m_type, m_count);

        return creator;
    }
}
