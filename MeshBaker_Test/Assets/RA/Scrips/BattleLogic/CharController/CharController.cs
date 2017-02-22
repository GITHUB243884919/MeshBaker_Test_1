/// <summary>
/// 适用于外部触发的角色控制
/// author : fanzhengyong
/// date  : 2017-02-22
/// 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharController : MonoBehaviour 
{
    public enum E_COMMOND
    {
        NONE,
        IDLE,
        ARRIVE,
        ATTACK,
        ATTACKED
    }

    public delegate void CommondCallback();

    public Dictionary<E_COMMOND, CommondCallback> m_commondCallbacks =
        new Dictionary<E_COMMOND, CommondCallback>();

    TankCommond m_commond;
    MoveSteers m_steers;
    //date
    public Vector3 TargetForArrive { get; set; }

    public void Init()
    {
        //到达行为统一处理
        RegCommond(E_COMMOND.ARRIVE, Arrive);

        m_commond = new TankCommond(this);
        m_commond.Init();

        m_steers = new MoveSteers(this);
        m_steers.Init();
    }
    public void RegCommond(E_COMMOND commond, CommondCallback callback)
    {
        CommondCallback _callback = null;
        m_commondCallbacks.TryGetValue(commond, out _callback);
        if (_callback != null)
        {
            Debug.LogWarning("CharController已存在Commond " + commond);
            return;
        }

        m_commondCallbacks.Add(commond, callback);
        
    }

    public void Commond(E_COMMOND commond)
    {
        CommondCallback callback = null;
        m_commondCallbacks.TryGetValue(commond, out callback);
        if (callback == null)
        {
            Debug.LogWarning("CharController不存在Commond " + commond);
            return;
        }
        //Debug.Log("Commond = " + commond + " " + TargetForArrive 
        //    + " Time " + Time.realtimeSinceStartup);
        callback();
    }

    public void Arrive()
    {
        MoveSteer arrive = m_steers.m_steers[MoveSteers.E_STEER_TYPE.ARRIVE];
        arrive.Active = true;
        m_steers.Active = true;
    }
	
    //Unity
	void Awake () 
    {
        Init();
	}
	
	void Update () 
    {
        m_commond.Update();
        if (m_steers.Active)
        {
            m_steers.Update();
        }
        
	}

    void FixedUpdate()
    {
        if (m_steers.Active)
        {
            m_steers.FixedUpdate();
        }
    }
}
