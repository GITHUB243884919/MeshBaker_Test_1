/// <summary>
/// 一种对象池实现。因为内部采用Queue存储对象，因此取名QObjPool
/// author : fanzhengyong
/// date  : 2017-02-22
/// 
/// 需要实现对象生成器和生成器工厂
/// 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 对象生成器
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class QObjCreator<T>
{
    public virtual T [] CreateObjects()
    {
        T [] objs = null;
        return objs;
    }

    public virtual void HideObject(T obj)
    {

    }

    public virtual void RealseObject(T obj)
    {

    }
}

/// <summary>
/// 对象生成器工厂类，需要派生类实现
/// </summary>
public abstract class QObjCreatorFactory<T>
{
    public virtual QObjCreator<T> CreatCreator()
    {
        QObjCreator<T> creator = null;
        return creator;
    }
}

public class QObjPool<T>
{
    private Queue<T> m_pool = new Queue<T>();
    public int FreeCount { get; private set; }
    public int TotleCount { get; private set; }

    public QObjCreatorFactory<T> m_creatorFactory;
    public QObjCreator<T> m_creator;

    public bool Init(QObjCreator<T> creator, QObjCreatorFactory<T> creatorFactory = null)
    {
        FreeCount = 0;
        TotleCount = 0;
        m_creator = creator;
        m_creatorFactory = creatorFactory;
        return Resize();
    }

    public T BorrowObj()
    {
        bool retCode = false;
        T obj = default(T);

        obj = m_pool.Dequeue();
        if (obj == null)
        {
            retCode = Resize();
            if (!retCode)
            {
                return default(T);
            }
            obj = m_pool.Dequeue();
        }
        FreeCount--;
        return obj;
    }

    public void ReturnObj(T obj)
    {
        m_pool.Enqueue(obj);
        FreeCount++;
    }

    private bool Resize()
    {
        bool result = false;

        //必须没有空闲的才执行
        if (FreeCount > 0)
        {
            return result;
        }

        T [] objs = null;
        objs = CreatObjects(m_creator, m_creatorFactory);
        if (objs == null)
        {
            return result;
        }

        int countForAdd = objs.Length;
        FreeCount += countForAdd;
        TotleCount += countForAdd;
        for (int i = 0; i < countForAdd; i++)
        {
            m_pool.Enqueue(objs[i]);
        }
        
        result = true;
        return result;
    }

    private T [] CreatObjects(QObjCreator<T> creator, QObjCreatorFactory<T> creatorFactory)
    {
        T [] objs = null;
        if (creator != null)
        {
            m_creator = creator;
            objs = m_creator.CreateObjects();
        }
        else if (creatorFactory != null)
        {
            QObjCreator<T> _creator = creatorFactory.CreatCreator();
            objs = _creator.CreateObjects();
            m_creator = null;
        }
        else
        {
            objs = null;
        }

        return objs;
    }


}
