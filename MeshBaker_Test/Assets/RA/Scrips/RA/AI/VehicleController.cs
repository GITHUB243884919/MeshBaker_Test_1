/// <summary>
/// 质点控制类，是AI控制的基类
/// autor: fanzhengyong
/// date: 2017-02-13 
/// </summary>
using UnityEngine;
using System.Collections;

public class VehicleController: MonoBehaviour 
{
    private Steering[]        m_steerings;
    public float              m_maxSpeed = 10;
    public float              m_maxForce = 100;
    protected float           m_sqrMaxSpeed;
    public float              m_mass = 1;
    public Vector3            m_velocity;
    public float              m_damping = 0.9f;
    public float              m_computeInterval = 0.2f;
    public bool               m_isPlanar = true;

	private Vector3           m_steeringForce;
	protected Vector3         m_acceleration;
	private float             m_timer = 0.0f;

	protected void Start () 
	{
        m_steeringForce  = Vector3.zero;
        m_sqrMaxSpeed    = m_maxSpeed * m_maxSpeed;
        m_timer          = 0.0f;
		m_steerings      = GetComponents<Steering>();
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

        m_steeringForce = Vector3.ClampMagnitude(m_steeringForce, m_maxForce);
		m_acceleration  = m_steeringForce / m_mass;
		m_timer         = 0;
		
        Profiler.EndSample();
	}
}
