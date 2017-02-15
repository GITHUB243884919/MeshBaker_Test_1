/// <summary>
/// 场景对象池管理，负责管理多个pool
/// autor: fanzhengyong
/// date: 2017-02-15 
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolManager
{
    public Dictionary<E_SCENE_OBJECT_TYPE, ObjectPool> m_pools =
        new Dictionary<E_SCENE_OBJECT_TYPE, ObjectPool>();

    /// <summary>
    /// 先把各对象池创建
    /// </summary>
    public void AddOnePool(GameObject[] objects, E_SCENE_OBJECT_TYPE type, SceneObjectCreator creator)
    {
        m_pools[type] = new ObjectPool(objects, type, creator);
    }

    public ObjectElement GetOneObject(E_SCENE_OBJECT_TYPE type)
    {
        ObjectPool pool = m_pools[type];
        return pool.GetOneObject();
    }

    public void RevertOneObject(ObjectElement obj)
    {
        ObjectPool pool = m_pools[obj.m_type];
        pool.RevertOneObject(obj);
    }

}
