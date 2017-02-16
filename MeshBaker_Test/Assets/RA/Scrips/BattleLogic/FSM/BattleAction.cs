/// <summary>
/// 战斗行为类，聚合AI和动画
/// autor: fanzhengyong
/// date: 2017-02-16 
/// </summary>
using UnityEngine;
using System.Collections;

public class BattleAction 
{
    //public enum E_ACTION_STATE
    //{
    //    IDLE,     //待机
    //    MOVE,     //移动
    //    READY,    //架起
    //    FIND,     //锁敌
    //    ATTACK,   //攻击
    //    ATTACKED, //受击
    //    DEAD      //死亡
    //}
    private BattleFSMController _m_FSMCtr;
    public BattleAction(BattleFSMController ctr)
    {
        _m_FSMCtr = ctr;
    }

    //public void Action(BattleFSM.E_FSM_STATE state)
    //{
    //    switch (state)
    //    {
    //        case BattleFSM.E_FSM_STATE.IDLE:
    //            //Idle();
    //            break;
    //        case BattleFSM.E_FSM_STATE.MOVE:
    //            //Move();
    //            break;
    //        case BattleFSM.E_FSM_STATE.READY:
    //            //Ready();
    //            break;
    //        case BattleFSM.E_FSM_STATE.FIND:
    //            //Find();
    //            break;
    //        case BattleFSM.E_FSM_STATE.ATTACK:
    //            //Attack();
    //            break;
    //        case BattleFSM.E_FSM_STATE.ATTACKED:
    //            //Attacked();
    //            break;
    //        case BattleFSM.E_FSM_STATE.DEAD:
    //            break;
    //    }
    //}

    public void Idle()
    {
        BattleAnimation battleAnimation = _m_FSMCtr.battleAnimation;
        battleAnimation.Idle();
    }

    public void Attack()
    {
        BattleAnimation battleAnimation = _m_FSMCtr.battleAnimation;
        battleAnimation.Attack();
    }

}
