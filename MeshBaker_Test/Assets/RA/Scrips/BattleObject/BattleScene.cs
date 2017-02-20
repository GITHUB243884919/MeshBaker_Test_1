using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleScene : MonoBehaviour 
{
    public enum E_BATTLE_OBJECT_TYPE
    {
        TANK,
        SOLDIER
    }

    Dictionary<E_BATTLE_OBJECT_TYPE, DFObjectPool> m_battleObjPools =
        new Dictionary<E_BATTLE_OBJECT_TYPE, DFObjectPool>();

    private static BattleScene s_Instance = null;
    public static BattleScene Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(BattleScene))
                        as BattleScene;
                if (s_Instance == null)
                    Debug.Log("Could not locate BattleScene Object");
            }
            return s_Instance;
        }
    }

	public void Init()
    {
        InitBattleObjPools();
    }

    private void InitBattleObjPools()
    {
        int countTank = 32;
        string[] pathsTank = new string[4] 
        {
            "TankRuntime_Bake/TankMeshBaker",
            "TankRuntime_Bake/TankRuntime_Bake-mat",
            "TankRuntime_Bake/TankRuntime_Bake",
            "TankRuntime_Bake/Tank_Seed"
        };
        InitBattleObjPool(pathsTank, E_BATTLE_OBJECT_TYPE.TANK, countTank);

        int countSoldier = 32;
        string[] pathsSoldier = new string[4] 
        {
            "SoldierRuntime_Bake/SoldierMeshBaker",
            "SoldierRuntime_Bake/SoldierRuntime_Bake-mat",
            "SoldierRuntime_Bake/SoldierRuntime_Bake",
            "SoldierRuntime_Bake/Soldier_Seed"
        };
        InitBattleObjPool(pathsSoldier, E_BATTLE_OBJECT_TYPE.SOLDIER, countSoldier);
    }

    private void InitBattleObjPool(string [] paths, E_BATTLE_OBJECT_TYPE type, int count)
    {
        BattleModelObjCreatorFactory creatorFactory = new BattleModelObjCreatorFactory(
            paths, type, count);
        DFCreator creator = creatorFactory.CreatCreator();

        DFObjectPool pool = new DFObjectPool();
        pool.Init(null, creatorFactory);

        m_battleObjPools.Add(type, pool);
    }

	void Start() 
    {
        Init();
	}
	
	void Update() 
    {
	
	}
}
