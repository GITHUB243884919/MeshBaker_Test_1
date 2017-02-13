using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmmyCreator : MonoBehaviour
{
    public enum Troops_Formation 
    {
        ELINE,
        ECOUNT
    }
    public int m_totalTroopsCount = 5;
    public int m_perTroopsCount   = 2;
    public Vector3 m_postion;
    public Vector3 m_target;

    public List<GameObject> m_allEntitys = new List<GameObject>();
	void Start () 
    {
        int totalEntity = m_totalTroopsCount * m_perTroopsCount;
        if (totalEntity <= 0)
        {
            return;
        }

        GameObject tankRes  = GetEntityRes("tank");
        Vector3    offset   = new Vector3(10, 0, 10);
        CreateLine(tankRes, totalEntity, m_postion, offset);
	}
	
    GameObject GetEntityRes(string path)
    {
        return  Resources.Load<GameObject>(path);
       
    }
    void CreateLine(GameObject res, int count, Vector3 pos, Vector3 offset)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject tankGo = GameObject.Instantiate<GameObject>(res);
            m_allEntitys.Add(tankGo);
            Vector3 newPos = pos + i * offset;
            tankGo.transform.position = newPos;
            tankGo.transform.SetParent(transform, false);

            SteeringForArrive steering = tankGo.GetComponent<SteeringForArrive>();
            steering.m_target = m_target + i * offset;

        }

    }
	
}
