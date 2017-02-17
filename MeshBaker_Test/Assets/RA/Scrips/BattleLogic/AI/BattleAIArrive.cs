/// <summary>
/// AI行为类：到达指定目标
/// autor: fanzhengyong
/// date: 2017-02-16 
/// </summary>
using UnityEngine;
using System.Collections;

public class BattleAIArrive : BattleAISteering 
{
	private bool  m_isPlanar         = true;
    
    //减速区
	public float m_slowDownDistance  = 2.0f;
    
    //到达区，距离多少算到了，可以设置为0;
    public float m_nearDistance      = 0.5f;

    //目标位置
	public Vector3 m_target;

    //预期速度
	private Vector3 m_desiredVelocity;

    //最大速度
    private float m_maxSpeed;

    //容器
    private BattleAISteerings m_vehicle;

    public delegate void DelegateArrived();
    public DelegateArrived m_arrivedCallback = null;

	void Start () 
    {
        Init();
	}

    public override void Init()
    {
        m_vehicle  = GetComponent<BattleAISteerings>();
        m_maxSpeed = m_vehicle.m_maxSpeed;
        m_isPlanar = m_vehicle.m_isPlanar;
    }
	
	public override Vector3 Force()
	{
        //Debug.Log("Force");
        Vector3 force = Vector3.zero;
        if (m_target == null)
        {
            return force;
        }

		Vector3 toTarget = m_target - transform.position;
		Vector3 desiredVelocity;
        
        if (m_isPlanar)
        {
            toTarget.y = 0;
        }
		float distance = toTarget.magnitude;

        //到了
        if (distance <= m_nearDistance)
        {
            //m_vehicle.m_velocity = Vector3.zero;
            force = Vector3.zero;

            if (m_arrivedCallback != null)
            {
                m_arrivedCallback();
            }
        }
        //在减速区外
		else if (distance > m_slowDownDistance)
		{
			desiredVelocity = toTarget.normalized * m_maxSpeed;
            force = desiredVelocity - m_vehicle.m_velocity;
		}
        //减速区内
        else
		{
			desiredVelocity = toTarget - m_vehicle.m_velocity;
            force = desiredVelocity - m_vehicle.m_velocity;
		}

        return force;
	}

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(target, slowDownDistance);
    }
}
