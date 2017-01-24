using UnityEngine;
using System.Collections;

public class FireTank : MonoBehaviour {

    // Use this for initialization
    private Animator m_animator = null;
    private float m_interval = 3.0f;
    private float m_timer = 0; 
    void Start()
    {

        m_animator = GetComponent<Animator>();
        m_animator.SetTrigger("Fire");
    }

    // Update is called once per frame
    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer >= m_interval)
        {
            m_animator.SetTrigger("Fire");
            m_timer = 0;
        }
    }
}
