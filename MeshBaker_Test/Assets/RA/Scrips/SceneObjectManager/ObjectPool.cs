/// <summary>
/// 场景对象池
/// autor: fanzhengyong
/// date: 2017-02-15 
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectElement
{
    public enum E_STATE
    {
        UNUSED, //未使用
        USED    //已经使用
    }

    public GameObject m_go;

    public E_SCENE_OBJECT_TYPE m_type;
    //状态，初始为未使用
    public E_STATE m_state;

    //pool中的索引
    public int m_index;

    public ObjectElement(GameObject go, E_SCENE_OBJECT_TYPE type, int index)
    {
        m_go    = go;
        m_type  = type;
        m_state = E_STATE.UNUSED;
        m_index = index;
    }

    private ObjectElement() { }
}

public class ObjectPool
{
    public int m_totalCount  = 0;
    public int m_unUsedCount = 0;
    public E_SCENE_OBJECT_TYPE m_type;
    public List<ObjectElement> m_pool = new List<ObjectElement>();
    public SceneObjectCreator  m_creator;

    public ObjectPool(GameObject[] objects, E_SCENE_OBJECT_TYPE type, SceneObjectCreator creator)
    {
        int count = objects.Length;
        for (int i = 0; i < count; i++)
        {
            ObjectElement obj = new ObjectElement(objects[i], type, i);
            m_pool.Add(obj);
        }

        m_type        = type;
        m_totalCount  = count;
        m_unUsedCount = count;
        m_creator     = creator;
    }

    private ObjectPool() { }

    /// <summary>
    /// 取一个未使用的
    /// </summary>
    /// <returns></returns>
    public ObjectElement GetOneObject()
    {
        ObjectElement obj = null;
        if (m_unUsedCount <= 0)
        {
            Resize();
        }

        for (int i = 0; i < m_totalCount; i++)
        {
            if (m_pool[i].m_state == ObjectElement.E_STATE.UNUSED)
            {
                obj = m_pool[i];
                break;
            }
        }

        obj.m_state = ObjectElement.E_STATE.USED;
        m_unUsedCount--;

        return obj;
    }

    /// <summary>
    /// 归还一个未使用的
    /// </summary>
    /// <param name="obj"></param>
    public void RevertOneObject(ObjectElement obj)
    {
        obj.m_state = ObjectElement.E_STATE.UNUSED;
        m_unUsedCount++;
    }

    /// <summary>
    /// 把pool变大，是不够分配时调用
    /// </summary>
    public void Resize()
    {
        GameObject[] objects;
        if (m_creator == null)
        {
            return;
        }

        objects = m_creator.CreateObject();
        int count = objects.Length;

        for (int i = 0; i < count; i++)
        {
            ObjectElement obj = new ObjectElement(objects[i], m_type, m_totalCount + i);
            m_pool.Add(obj);
        }

        m_totalCount  += count;
        m_unUsedCount += count;
    }

}
