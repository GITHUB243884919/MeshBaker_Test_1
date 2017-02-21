/// <summary>
/// 战斗中模型使用的对象池管理器
/// author: fanzhengyong
/// date: 2017-02-21 
/// 
/// 包含多个DFObjectPool，分别用于坦克，士兵。。。。
/// 取用对象BorrowObj，归还用ReturnObj，
/// BorrowObj的对象后不能修改DFObject范围内的字段，更不能删除对象！！！
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleModelObjectPoolManager
{
    Dictionary<BattleScene.E_BATTLE_OBJECT_TYPE, DFObjectPool> m_pools =
        new Dictionary<BattleScene.E_BATTLE_OBJECT_TYPE, DFObjectPool>();

    private static BattleModelObjectPoolManager s_Instance = null;
    public static BattleModelObjectPoolManager Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = new BattleModelObjectPoolManager();
            }
            return s_Instance;
        }
    }

    public  void Init()
    {
        int countTank = 32;
        string[] pathsTank = new string[4] 
        {
            "TankRuntime_Bake/TankMeshBaker",
            "TankRuntime_Bake/TankRuntime_Bake-mat",
            "TankRuntime_Bake/TankRuntime_Bake",
            "TankRuntime_Bake/Tank_Seed"
        };
        InitBattleObjPool(pathsTank,
            BattleScene.E_BATTLE_OBJECT_TYPE.TANK, countTank);

        int countSoldier = 32;
        string[] pathsSoldier = new string[4] 
        {
            "SoldierRuntime_Bake/SoldierMeshBaker",
            "SoldierRuntime_Bake/SoldierRuntime_Bake-mat",
            "SoldierRuntime_Bake/SoldierRuntime_Bake",
            "SoldierRuntime_Bake/Soldier_Seed"
        };
        InitBattleObjPool(pathsSoldier, 
            BattleScene.E_BATTLE_OBJECT_TYPE.SOLDIER, countSoldier);
    }

    public BattleModelObj BorrowObj(BattleScene.E_BATTLE_OBJECT_TYPE type)
    {
        BattleModelObj obj = null;

        DFObjectPool pool = null;
        m_pools.TryGetValue(type, out pool);
        if (pool == null)
        {
            return obj;
        }

        obj = pool.BorrowObj() as BattleModelObj;

        return obj;
    }

    public BattleModelObj BorrowObj(int idx, BattleScene.E_BATTLE_OBJECT_TYPE type)
    {
        BattleModelObj obj = null;
        DFObjectPool pool = null;
        m_pools.TryGetValue(type, out pool);
        if (pool == null)
        {
            return obj;
        }

        obj = pool.BorrowObj(idx) as BattleModelObj;

        return obj;
    }

    public void ReturnObj(BattleModelObj obj)
    {
        DFObjectPool pool = null;
        m_pools.TryGetValue(obj.m_type, out pool);
        if (pool == null)
        {
            return;
        }

        pool.ReturnObj(obj);
    }

    private void InitBattleObjPool(string[] paths, BattleScene.E_BATTLE_OBJECT_TYPE type, int count)
    {
        BattleModelObjCreatorFactory creatorFactory = new BattleModelObjCreatorFactory(
            paths, type, count);
        DFCreator creator = creatorFactory.CreatCreator();

        DFObjectPool pool = new DFObjectPool();
        pool.Init(null, creatorFactory);

        m_pools.Add(type, pool);
    }
}
