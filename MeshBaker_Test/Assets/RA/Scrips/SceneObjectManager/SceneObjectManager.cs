/// <summary>
/// 场景对象管理
/// autor: fanzhengyong
/// date: 2017-02-15 
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneObjectManager : MonoBehaviour 
{
    //生成器缓存
    public Dictionary<E_SCENE_OBJECT_TYPE, CreatorParam> m_CreatorConfig =
        new Dictionary<E_SCENE_OBJECT_TYPE, CreatorParam>();
    //对象对象管理
    public ObjectPoolManager m_poolManager = new ObjectPoolManager();

    /// <summary>
    /// 初始化
    /// 1.读取生成器配置
    /// 2.创建生成器
    /// 3.预先生成对象
    /// </summary>
    public void Init()
    {
        InitCreatorConfig();
        CreateCreator();
        PreCreatorObjecs();
    }

    /// <summary>
    /// 生成器配置，每个生成器一次生成多少个，需要的资源路径
    /// </summary>
    public void InitCreatorConfig()
    {
        CreatorParam tankParam = new CreatorParam();
        tankParam.m_count = CreatorConfig.MAX_MESHBAKER_TANK;
        tankParam.m_paths = CreatorConfig.TANK_PATHS;
        m_CreatorConfig[E_SCENE_OBJECT_TYPE.TANK] = tankParam;

        CreatorParam soldierParam = new CreatorParam();
        soldierParam.m_count = CreatorConfig.MAX_MESHBAKER_SOLDIER;
        soldierParam.m_paths = CreatorConfig.SOLDIER_PATHS;
        m_CreatorConfig[E_SCENE_OBJECT_TYPE.SOLDIER] = soldierParam;
    }

    /// <summary>
    /// 创建生成器，每种生成器只创建一次，如果不够那就从这个生成器复制出
    /// </summary>
    public void CreateCreator()
    {
        //tank
        SceneObjectCreator tankCreator = new SceneObjectCreator(
            m_CreatorConfig[E_SCENE_OBJECT_TYPE.TANK].m_paths,
            E_SCENE_OBJECT_TYPE.TANK, m_CreatorConfig[E_SCENE_OBJECT_TYPE.TANK].m_count);
        m_CreatorConfig[E_SCENE_OBJECT_TYPE.TANK].m_creator = tankCreator;

        //soldier
        SceneObjectCreator soldierCreator = new SceneObjectCreator(
            m_CreatorConfig[E_SCENE_OBJECT_TYPE.SOLDIER].m_paths,
            E_SCENE_OBJECT_TYPE.SOLDIER, m_CreatorConfig[E_SCENE_OBJECT_TYPE.SOLDIER].m_count);
        m_CreatorConfig[E_SCENE_OBJECT_TYPE.SOLDIER].m_creator = soldierCreator;
    }

    /// <summary>
    /// 预先生成对象
    /// </summary>
    public void PreCreatorObjecs()
    {
        //tank
        GameObject [] tankObjs = m_CreatorConfig[E_SCENE_OBJECT_TYPE.TANK].
            m_creator.CreateObject();
        m_poolManager.AddOnePool(tankObjs, E_SCENE_OBJECT_TYPE.TANK, 
            m_CreatorConfig[E_SCENE_OBJECT_TYPE.TANK].m_creator);

        //soldier
        GameObject[] soldierObjs = m_CreatorConfig[E_SCENE_OBJECT_TYPE.SOLDIER].
            m_creator.CreateObject();
        m_poolManager.AddOnePool(soldierObjs, E_SCENE_OBJECT_TYPE.SOLDIER,
            m_CreatorConfig[E_SCENE_OBJECT_TYPE.SOLDIER].m_creator);
    }

    public ObjectElement GetOneObject(E_SCENE_OBJECT_TYPE type)
    {
        return m_poolManager.m_pools[type].GetOneObject();
    }

    public void RevertOneObject(ObjectElement obj)
    {
        m_poolManager.m_pools[obj.m_type].RevertOneObject(obj);
    }

	void Start () 
    {
        Init();
	}

}
