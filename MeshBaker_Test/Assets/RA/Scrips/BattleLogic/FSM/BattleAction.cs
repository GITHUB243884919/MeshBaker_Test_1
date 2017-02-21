/// <summary>
/// 战斗行为类，聚合AI和动画
/// autor: fanzhengyong
/// date: 2017-02-16 
/// </summary>
using UnityEngine;
using System.Collections;

public class BattleAction 
{
    private BattleFSMController _m_FSMCtr;
    public BattleAnimation m_battleAnimation;
    public BattleAI m_battleAI;
    public BattleAction(BattleFSMController ctr)
    {
        _m_FSMCtr = ctr;
        
        
    }

    public void Idle()
    {
        if (m_battleAnimation == null)
        {
            m_battleAnimation = _m_FSMCtr.battleAnimation;
        }
        if (m_battleAnimation == null)
        {
            return;
        }

        m_battleAnimation.Idle();
    }

    public void Attack()
    {
        if (m_battleAnimation == null)
        {
            m_battleAnimation = _m_FSMCtr.battleAnimation;
        }
        if (m_battleAnimation == null)
        {
            return;
        }

        m_battleAnimation.Attack();
    }

    public void Move()
    {
        //Debug.Log("BattleAction Move");
        if (m_battleAI == null)
        {
            m_battleAI = _m_FSMCtr.battleAI;
        }
        if (m_battleAI == null)
        {
            Debug.LogError("m_battleAI == null");
            return;
        }
        
        m_battleAI.Arrive();
    }

}
