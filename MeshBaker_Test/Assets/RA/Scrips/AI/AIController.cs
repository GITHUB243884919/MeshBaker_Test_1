/// <summary>
/// AI控制类
/// autor: fanzhengyong
/// date: 2017-02-13 
/// </summary>
using UnityEngine;
using System.Collections;

public class AIController : VehicleController 
{

    public bool          m_displayTrack = true;
	private Vector3      m_moveDistance;
    private Animator     m_animator   = null;

	void Start () 
	{
		m_moveDistance  = Vector3.zero;
        m_animator    = GetComponent<Animator>();
		base.Start();
	}
	
	void FixedUpdate()
	{
		m_velocity    += m_acceleration * Time.fixedDeltaTime;

		if (m_velocity.sqrMagnitude > m_sqrMaxSpeed)
        {
            m_velocity = m_velocity.normalized * m_maxSpeed;
        }
			
		m_moveDistance = m_velocity * Time.fixedDeltaTime;
		
		if (m_isPlanar)
		{
			m_velocity.y = 0;
			m_moveDistance.y = 0;
		}

		if (m_displayTrack)
        {
            Debug.DrawLine(transform.position, 
                transform.position + m_moveDistance, Color.red, 30.0f);
        }

        transform.position += m_moveDistance;

		if (m_velocity.sqrMagnitude > 0.00001)
		{
			Vector3 newForward = Vector3.Slerp(transform.forward, m_velocity, m_damping * Time.deltaTime);
			if (m_isPlanar)
				newForward.y = 0;
			transform.forward = newForward;
		}

	}
}
