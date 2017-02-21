/// <summary>
/// 战斗中模型使用的对象池缓存
/// author: fanzhengyong
/// date: 2017-02-21 
/// 
/// 这里存的都是已经从对象池中取过的对象，
/// 以服务器定义的编号（ServerEntityID）为key，value为DFObjectPool中的index
/// 这样方便用ServerEntityID查找
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveBattleModelObject
{
    public BattleScene.E_BATTLE_OBJECT_TYPE Type { get; set; }

    //服务器定义的唯一编号和类型编号
    public int ServerEntityID { get; set; }
    public int ServerEntityType { get; set; }

    //在对象池中的索引
    public int IdxOfPool { get; set; }
}

public class ActiveBattleModelObjectCache
{
    private Dictionary<int, ActiveBattleModelObject> m_cache
       = new Dictionary<int, ActiveBattleModelObject>();

    private static ActiveBattleModelObjectCache s_Instance = null;
    public static ActiveBattleModelObjectCache Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = new ActiveBattleModelObjectCache();
            }
            return s_Instance;
        }
    }
    public void Init() { }

    public bool Add(BattleModelObj obj)
    {
        bool result = false;

        if (obj == null)
        {
            return result;
        }

        ActiveBattleModelObject activeObj = null;
        m_cache.TryGetValue(obj.ServerEntityID, out activeObj);
        if (activeObj != null)
        {
            result = true;
            return result;
        }

        activeObj = new ActiveBattleModelObject();
        activeObj.ServerEntityID = obj.ServerEntityID;
        activeObj.ServerEntityType = obj.ServerEntityType;
        activeObj.IdxOfPool = obj.m_idx;

        m_cache.Add(obj.ServerEntityID, activeObj);

        result = true;
        return result;
    }

    public bool Remove(BattleModelObj obj)
    {
        bool result = false;

        if (obj == null)
        {
            return result;
        }

        ActiveBattleModelObject activeObj = null;
        m_cache.TryGetValue(obj.ServerEntityID, out activeObj);
        if (activeObj == null)
        {
            result = true;
            return result;
        }

        m_cache[obj.ServerEntityID] = null;
        m_cache.Remove(obj.ServerEntityID);

        result = true;
        return result;
    }

    public ActiveBattleModelObject Find(int serverEntityID)
    {
        ActiveBattleModelObject obj = null;

        m_cache.TryGetValue(serverEntityID, out obj);

        return obj;
    }
}
