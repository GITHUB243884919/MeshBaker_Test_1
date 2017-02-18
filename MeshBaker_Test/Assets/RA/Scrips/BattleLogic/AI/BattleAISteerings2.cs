/// <summary>
/// AI行为容器
/// 负责计算出容器内有效的（可能是多个）行为类形成的合力，最终对其形成作用
/// autor: fanzhengyong
/// date: 2017-02-16 
/// </summary>
using UnityEngine;
using System.Collections;

public class BattleAISteerings2: MonoBehaviour 
{
    private BattleAISteering[] m_steerings;
    public float              m_maxSpeed = 10;
    public float              m_maxForce = 100;
    protected float           m_sqrMaxSpeed;
    public float              m_mass = 1;
    public Vector3            m_velocity;
    public float              m_damping = 0.9f;
    public float              m_computeInterval = 0.2f;
    public bool               m_isPlanar = true;

	private Vector3           m_steeringForce;
    private Vector3           m_acceleration;
	private float             m_timer = 0.0f;

    public bool               m_displayTrack = true;
    private Vector3           m_moveDistance;

	protected void Start () 
	{
        m_timer           = 0.0f;

        m_moveDistance   = Vector3.zero;
        m_steeringForce  = Vector3.zero;
        m_sqrMaxSpeed    = m_maxSpeed * m_maxSpeed;

        m_steerings      = GetComponents<BattleAISteering>();
	}

	void Update () 
	{
		m_timer         += Time.deltaTime;
		m_steeringForce  = Vector3.zero;

        if (m_timer < m_computeInterval)
        {
            return;
        }

        Profiler.BeginSample("begin calc all force");

        for (int i = 0; i < m_steerings.Length; i++)
        {
            if (m_steerings[i].enabled)
            {
                m_steeringForce += m_steerings[i].Force() * m_steerings[i].m_weight;
            }
        }

        //m_steeringForce = Vector3.ClampMagnitude(m_steeringForce, m_maxForce);
		m_acceleration  = m_steeringForce / m_mass;
		m_timer         = 0;
		
        Profiler.EndSample();
	}

    void FixedUpdate()
    {
        //m_velocity += m_acceleration * Time.fixedDeltaTime;

        //if (m_velocity.sqrMagnitude > m_sqrMaxSpeed)
        //{
        //    m_velocity = m_velocity.normalized * m_maxSpeed;
        //}

        //m_moveDistance = m_velocity * Time.fixedDeltaTime;

        //if (m_isPlanar)
        //{
        //    m_velocity.y = 0;
        //    m_moveDistance.y = 0;
        //}

        //if (m_displayTrack)
        //{
        //    Debug.DrawLine(transform.position,
        //        transform.position + m_moveDistance, Color.red, 30.0f);
        //}

        m_moveDistance = m_acceleration * Time.fixedDeltaTime;
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
