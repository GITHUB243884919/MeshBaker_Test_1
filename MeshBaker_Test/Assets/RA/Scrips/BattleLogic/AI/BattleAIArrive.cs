/// <summary>
/// AI行为类：到达指定目标
/// autor: fanzhengyong
/// date: 2017-02-16 
/// </summary>
using UnityEngine;
using System.Collections;

public class BattleAIArrive : BattleAISteering 
{
	public bool  m_isPlanar        = true;
	public float m_arrivalDistance = 0.3f;
	public float m_characterRadius = 1.2f;
    
	public float m_slowDownDistance = 2.0f;
    public float m_nearDistance     = 0.5f;

	public Vector3 m_target;
	private Vector3 m_desiredVelocity;
    private BattleAISteeringController m_vehicle;
	private float m_maxSpeed;

	void Start () 
    {
        m_vehicle = GetComponent<BattleAISteeringController>();
		m_maxSpeed = m_vehicle.m_maxSpeed;
		m_isPlanar = m_vehicle.m_isPlanar;
	}
	
	public override Vector3 Force()
	{
        Debug.Log("Force");
        Vector3 returnForce = Vector3.zero;
        if (m_target == null)
        {
            return returnForce;
        }

		Vector3 toTarget = m_target - transform.position;
		Vector3 desiredVelocity;
        
        if (m_isPlanar)
        {
            toTarget.y = 0;
        }
		float distance = toTarget.magnitude;

		if (distance > m_slowDownDistance)
		{
			desiredVelocity = toTarget.normalized * m_maxSpeed;
			returnForce = desiredVelocity - m_vehicle.m_velocity;
		}
        else if (distance <= m_nearDistance)
        {
            m_vehicle.m_velocity = Vector3.zero;
            returnForce = Vector3.zero;
        }
		else
		{
			desiredVelocity = toTarget - m_vehicle.m_velocity;
			returnForce = desiredVelocity - m_vehicle.m_velocity;
		}

		return returnForce;
	}

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(target.transform.position, slowDownDistance);
    }
}
