/// <summary>
/// 行为基类
/// autor: fanzhengyong
/// date: 2017-02-13 
/// </summary>
using UnityEngine;
using System.Collections;

public abstract class Steering : MonoBehaviour 
{

	public float m_weight = 1.0f;

	public virtual Vector3 Force()
	{
		return Vector3.zero;
	}
}
