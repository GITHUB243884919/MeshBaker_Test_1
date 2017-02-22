using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleScene2 : MonoBehaviour 
{
    public enum E_BATTLE_OBJECT_TYPE
    {
        TANK,
        SOLDIER
    }

    private static BattleScene2 s_Instance = null;
    public static BattleScene2 Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(BattleScene2))
                        as BattleScene2;
                if (s_Instance == null)
                    Debug.Log("场景中没有持有BattleScene2的对象");
            }
            return s_Instance;
        }
    }

    private void Init()
    {
        BattleModelObjectPoolManager.Instance.Init();
        ActiveBattleModelObjectCache.Instance.Init();
    }

    /// <summary>
    /// 在场景取一个对象必须是已知类型，服务器的两个编号的
    /// </summary>
    /// <param name="type"></param>
    /// <param name="serverEntityID"></param>
    /// <param name="serverEntityType"></param>
    /// <returns></returns>
    public BattleModelObj BorrowBattleModelObj(BattleScene.E_BATTLE_OBJECT_TYPE type,
        int serverEntityID, int serverEntityType)
    {
        BattleModelObj obj = null;
        ActiveBattleModelObject activeObj = null;

        //先从缓存中取
        activeObj = ActiveBattleModelObjectCache.Instance.Find(serverEntityID);
        if (activeObj != null)
        {
            obj = BattleModelObjectPoolManager.Instance.BorrowObj(
                activeObj.IdxOfPool, activeObj.Type);
            return obj;
        }

        //缓存中没有才从对象池中取
        obj = BattleModelObjectPoolManager.Instance.BorrowObj(type);

        //从对象池中取出的对象要放入缓存中
        obj.ServerEntityID = serverEntityID;
        obj.ServerEntityType = serverEntityType;
        ActiveBattleModelObjectCache.Instance.Add(obj);

        return obj;
    }

    public void ReturnBattleModelObj(BattleModelObj obj)
    {
        //先从缓存中移除
        ActiveBattleModelObjectCache.Instance.Remove(obj);
        //再还给对象池
        BattleModelObjectPoolManager.Instance.ReturnObj(obj);
    }

    void Start()
    {
        Init();
    } 

	void Update() 
    {
	
	}
}
