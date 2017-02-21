/// <summary>
/// 战斗AI
/// autor: fanzhengyong
/// date: 2017-02-16 
/// </summary>

using UnityEngine;
using System.Collections;

public class BattleAI
{
    private BattleFSMController _m_FSMCtr;

    public BattleFSM m_battleFSM;
    public BattleAISteerings2 m_steerings;
    public BattleAIArrive2 m_arrive;
    public BattleAI(BattleFSMController ctr)
    {
        _m_FSMCtr = ctr;
        if (_m_FSMCtr != null)
        {
            m_steerings = ctr.Trs.gameObject.GetComponent<BattleAISteerings2>();
            m_arrive = ctr.Trs.gameObject.GetComponent<BattleAIArrive2>();
        }
    }

    public void Arrive()
    {
        if (_m_FSMCtr == null)
        {
            return;
        }

        if (m_battleFSM == null)
        {
            m_battleFSM = _m_FSMCtr.battleFSM;
        }            
        if (m_battleFSM == null)
        {
            return;
        }

        //Debug.Log("BattleAI Arrive " + m_battleFSM.m_targetForMove);
        m_arrive.m_target = m_battleFSM.m_targetForMove;

    }

}
