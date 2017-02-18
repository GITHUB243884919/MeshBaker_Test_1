/// <summary>
/// 战斗对象池实现
/// 所有需要被缓存的对象从DFObject派生
/// 
/// pool初始化 ：
///     函数 Init(DFCreator creator, DFCreatorFactory creatorFactory)
///     需要把对象生成器和工厂类，这两个必须有一个不为空。如果前者不为空，那么对象的
///     从pool中取出空闲的对象 ：函数 BorrowObj()
///     之所以用borrow这个单词的意思是要强调是“借”，既然是借就修改改对象，更不能删除对象！
///     不能修改是指不能修改DFObject范围内的字段。派生类的字段不受限。
/// 从对象暂时不用了放回pool：
///     函数 ReturnObj()
///     跟borrow对应，有借有还。
/// 
/// pool的缓存终归是要释放的，那究竟如何释放？
///     对象的基类DFObject的派生类各自实现单个对象的释放Realse。
///     pool提供释放所有对象的函数 Realse 
///     至于什么时候调用pool的Realse由外层逻辑，持有pool的class负责。
///     
/// 为什么DFObject是虚基类？
///     pool设计的最初始目的是为了缓存多种类似于这样的对象：包含GameObject和若干游戏逻辑属性（比如生命值）
///     的对象。但是相对于pool，并不需要这些内容，他需要关心的是能通过一些管理对象需要的字段。
///     
/// 为什么需要一个生成器DFCreator？
///     既然对象已经被抽象，那么对象如何生成pool也不需要知道细节。
///     
/// 为什么需要一个生成器工厂 DFCreatorFactory？
///     为了适应每次都需要一个新的生成器（新的指针）时。
///     
/// autor : fanzhengyong
/// date  : 2017-02-18
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class DFObject
{
    public enum E_STATE
    {
        FREE,     //未使用
        UNFREE    //已经使用
    }

    public int m_idx;
    public int m_nextFree;
    public E_STATE m_state;

    public virtual void Realse()
    {

    }
}

/// <summary>
/// 对象生成器，对象从这里创建出来
/// </summary>
public abstract class DFCreator
{

    //一次创建多少个对象
    public int m_countCreat = 32;
    public virtual DFObject [] CreateObjects() 
    {
        DFObject [] objs = null;
        return objs;
    }
}

/// <summary>
/// 对象生成器工厂类，需要派生类实现
/// </summary>
public abstract class DFCreatorFactory
{
    public virtual DFCreator CreatCreator()
    {
        DFCreator creator = null;
        return creator;
    }
}

public class DFObjectPool
{
    public List<DFObject> m_pool = new List<DFObject>();
    public int m_free = 0;
    public int m_nextFree = 1;
    public int m_countFree = 0;
    public readonly int BAD_INDEX = -1;
    public DFCreatorFactory m_creatorFactory;
    public DFCreator m_creator;
    public bool Init(DFCreator creator, DFCreatorFactory creatorFactory)
    {
        m_creator = creator;
        m_creatorFactory = creatorFactory;
        return Resize();
    }

    public DFObject BorrowObj()
    {
        DFObject obj = null;
        bool retCode = false;
        obj = GetFreeObj();
        if (obj == null)
        {
            return obj;
        }

        retCode = Resize();
        if (!retCode)
        {
            return obj;
        }
        obj = GetFreeObj();

        return obj;
    }

    public void ReturnObj(DFObject obj)
    {
        m_nextFree = m_free;
        m_free = obj.m_idx;
        obj.m_state = DFObject.E_STATE.FREE;
        m_countFree++;
    }

    public void Realse()
    {
        for (int i = m_pool.Count; i >= 0; i--)
        {
            m_pool[i].Realse();
            m_pool[i] = null;
        }

        m_pool.Clear();
    }

    private bool Resize()
    {
        DFObject[] objs = null;
        objs = CreatObjects(m_creator, m_creatorFactory);
        if (objs == null)
        {
            return false;
        }
        
        int oldCount = m_pool.Count;
        m_countFree += objs.Length;
        for (int i = 0; i < m_countFree; i++)
        {
            objs[i].m_idx = oldCount + i;
            objs[i].m_nextFree = oldCount + i + 1;
            m_pool[oldCount + i] = objs[i];
        }

        m_pool[m_pool.Count - 1].m_nextFree = BAD_INDEX;
        if (m_pool.Count > objs.Length)
        {
            m_pool[m_pool.Count - objs.Length - 1].m_nextFree =
                m_pool[m_pool.Count - objs.Length].m_idx;
        }
        
        return true;
    }

    public DFObject [] CreatObjects(DFCreator creator, DFCreatorFactory creatorFactory)
    {
        DFObject[] objs = null;
        if (creator != null)
        {
            m_creator = creator;
            objs = m_creator.CreateObjects();
        }
        else if (creatorFactory != null)
        {
            DFCreator _creator = creatorFactory.CreatCreator();
            objs = _creator.CreateObjects();
            m_creator = null;
        }
        else
        {
            //return false;
        }

        return objs;
    }

    private DFObject GetFreeObj()
    {
        DFObject obj = null;
        if (m_countFree > 0)
        {
            obj = m_pool[m_free];
            m_nextFree = obj.m_nextFree;
            m_countFree--;
            obj.m_state = DFObject.E_STATE.UNFREE;  
        }
        return obj;
    }

}
