/// <summary>
/// AI行为基类
/// 派生类 完成自己的初始化函数和给力函数,最终输出一个力（向量）
/// autor: fanzhengyong
/// date: 2017-02-16 
/// </summary>
using UnityEngine;
using System.Collections;

public abstract class BattleAISteering : MonoBehaviour 
{

	public float m_weight = 1.0f;

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {

    }

    /// <summary>
    /// 给力函数，派生类按各自的需求实现逻辑，计算出需要施加的力
    /// </summary>
    /// <returns></returns>
	public virtual Vector3 Force()
	{
		return Vector3.zero;
	}
}
