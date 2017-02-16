/// <summary>
/// 战斗动画
/// autor: fanzhengyong
/// date: 2017-02-16 
/// </summary>
using UnityEngine;
using System.Collections;

public class BattleAnimation
{
    public BattleFSMController m_FSMCtr;
    public Animator m_animator;
    public BattleAnimation(BattleFSMController ctr, Animator animator)
    {
        m_FSMCtr = ctr;
        m_animator = animator;
    }

    public void Idle()
    {
        Debug.Log("BattleAnimation Idle");
    }

    public void Attack()
    {
        Debug.Log("BattleAnimation Attack");
        m_animator.SetTrigger("Fire");
    }
}
