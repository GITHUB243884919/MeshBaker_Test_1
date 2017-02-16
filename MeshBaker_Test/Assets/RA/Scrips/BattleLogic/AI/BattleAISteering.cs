/// <summary>
/// AI行为基类
/// autor: fanzhengyong
/// date: 2017-02-16 
/// </summary>
using UnityEngine;
using System.Collections;

public abstract class BattleAISteering : MonoBehaviour 
{

	public float m_weight = 1.0f;

	public virtual Vector3 Force()
	{
		return Vector3.zero;
	}
}
