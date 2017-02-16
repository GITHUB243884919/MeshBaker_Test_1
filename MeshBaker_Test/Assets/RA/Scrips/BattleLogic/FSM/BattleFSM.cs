/// <summary>
/// 战斗状态机
/// autor: fanzhengyong
/// date: 2017-02-16 
/// </summary>
using UnityEngine;
using System.Collections;

public class BattleFSM : MonoBehaviour 
{
    public enum E_FSM_STATE 
    {
        NONE,
        IDLE,     //待机
        MOVE,     //移动
        READY,    //架起
        FIND,     //锁敌
        ATTACK,   //攻击
        ATTACKED, //受击
        DEAD      //死亡
    }

    public E_FSM_STATE  m_state       = E_FSM_STATE.IDLE;
    public Vector3      m_targetForMove   = Vector3.zero;
    public Vector3      m_targetForAttack = Vector3.zero;
    public float        m_health          = 1.0f;

    private BattleFSMController _m_FSMCtr;
    public void Trigger(E_FSM_STATE state)
    {
        m_state = state;
    }

    public void SetFSMCtroller(BattleFSMController ctr)
    {
        _m_FSMCtr = ctr;
    }

    void Init()
    {
        //设置初始化状态
        m_state = E_FSM_STATE.IDLE;
    }

    void FSMUpdate()
    {
        switch(m_state)
        {
            case E_FSM_STATE.NONE:
                Debug.Log("BattleFSM FSMUpdate E_FSM_STATE.NONE");
                break;
            case E_FSM_STATE.IDLE:
                Idle();
                break;
            case E_FSM_STATE.MOVE:
                Move();
                break;
            case E_FSM_STATE.READY:
                Ready();
                break;
            case E_FSM_STATE.FIND:
                Find();
                break;
            case E_FSM_STATE.ATTACK:
                Attack();
                break;
            case E_FSM_STATE.ATTACKED:
                Attacked();
                break;
            case E_FSM_STATE.DEAD:
                break;
        }

        if (m_health <= 0)
        {
            m_state = E_FSM_STATE.DEAD;
        }
            
    }

    /*以下是各个状态内该执行的逻辑*/

    void Idle()
    {
        //调用BattleAction，由其调用BattleAnimate执行一个动画
        _m_FSMCtr.battleAction.Idle();
    }

    void Move()
    {
        //调用BattleAction，尤其调用BattleAI,到达指定目的地

    }

    void Ready()
    {
        //调用BattleAction，由其调用BattleAnimate执行一个动画
    }

    void Find()
    {
        //目前没有，
    }

    void Attack()
    {
        //调用BattleAction，由其调用BattleAnimate执行一个动画
        _m_FSMCtr.battleAction.Attack();
    }

    void Attacked()
    {
        //调用BattleAction，由其调用BattleAnimate执行一个动画
    }

    void Dead()
    {
        //调用BattleAction，由其调用BattleAnimate执行一个动画
    }
    
	void Start () 
    {
        Init();
	}
	
	void Update () 
    {
        FSMUpdate();
	}
}
