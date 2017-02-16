/// <summary>
/// 战斗状态机控制类
/// autor: fanzhengyong
/// date: 2017-02-16 
/// </summary>
using UnityEngine;
using System.Collections;

public class BattleFSMController : MonoBehaviour 
{

    private Transform _m_trs;
    private BattleFSM _m_FSM;
    private BattleAction _m_action;
    private BattleAI _m_AI;
    private BattleAnimation _m_animation;

    void Init()
    {
        _m_trs = transform;
        
        _m_FSM = _m_trs.gameObject.GetComponent<BattleFSM>();
        _m_FSM.SetFSMCtroller(this);
        //Debug.Log("_m_FSM.SetFSMCtroller");

        _m_action = new BattleAction(this);

        _m_AI = new BattleAI(this);
        
        Animator animator = gameObject.GetComponent<Animator>();
        _m_animation = new BattleAnimation(this, animator);
    }

    public void Trigger(BattleFSM.E_FSM_STATE state)
    {
        _m_FSM.Trigger(state);
    }

    //设置数据
    public void SetTargetForMove(Vector3 target)
    {
        _m_FSM.m_targetForMove = target;
    }

    //获取对象
    public Transform Trs {get {return _m_trs;}}
    
    public BattleFSM battleFSM {get {return _m_FSM;}}

    public BattleAction battleAction { get { return _m_action;}}

    public BattleAI battleAI { get { return _m_AI; } }

    public BattleAnimation battleAnimation { get { return _m_animation; } }


    //unity
    void Awake()
    {
        Init();
    }
}
