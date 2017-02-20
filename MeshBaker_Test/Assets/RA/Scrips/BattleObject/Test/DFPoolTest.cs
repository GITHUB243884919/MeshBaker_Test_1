using UnityEngine;
using System.Collections;

public class DFPoolTest : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        int count = 8;
        string[] paths = new string[4] 
        {
            "TankRuntime_Bake/TankMeshBaker",
            "TankRuntime_Bake/TankRuntime_Bake-mat",
            "TankRuntime_Bake/TankRuntime_Bake",
            "TankRuntime_Bake/Tank_Seed"
        };

        BattleModelObjCreatorFactory creatorFactory = new BattleModelObjCreatorFactory(
            paths, BattleScene.E_BATTLE_OBJECT_TYPE.TANK, count);
        DFCreator creator = creatorFactory.CreatCreator();
        //creator.CreateObjects();

        DFObjectPool pool = new DFObjectPool();
        pool.Init(null, creatorFactory);

        Test1(pool);
        //Test2(pool);
        //Test3(pool);
        //Test4(pool);

    }

    /// <summary>
    /// 中间分配，释放，再分配
    /// 缓存不够再分配
    /// </summary>
    /// <param name="pool"></param>
    void Test1(DFObjectPool pool)
    {
        BattleModelObj obj0 = pool.BorrowObj() as BattleModelObj;
        obj0.m_go.name += "+UNFREE";
        BattleModelObj obj1 = pool.BorrowObj() as BattleModelObj;
        obj1.m_go.name += "+UNFREE";
        BattleModelObj obj2 = pool.BorrowObj() as BattleModelObj;
        obj2.m_go.name += "+UNFREE";

        pool.ReturnObj(obj1);
        obj1.m_go.name += "+FREE";
        BattleModelObj obj3 = pool.BorrowObj() as BattleModelObj;
        obj3.m_go.name += "+UNFREE";

        pool.ReturnObj(obj0);
        obj0.m_go.name += "+FREE";
        BattleModelObj obj4 = pool.BorrowObj() as BattleModelObj;
        obj4.m_go.name += "+UNFREE";

        BattleModelObj obj5 = pool.BorrowObj() as BattleModelObj;
        obj5.m_go.name += "+UNFREE";

        BattleModelObj obj6 = pool.BorrowObj() as BattleModelObj;
        obj6.m_go.name += "+UNFREE";

        BattleModelObj obj7 = pool.BorrowObj() as BattleModelObj;
        obj7.m_go.name += "+UNFREE";

        BattleModelObj obj8 = pool.BorrowObj() as BattleModelObj;
        obj8.m_go.name += "+UNFREE";

        PrintPoolInfo(pool);

        BattleModelObj obj9 = pool.BorrowObj() as BattleModelObj;
        obj9.m_go.name += "+UNFREE";

        PrintPoolInfo(pool);

        BattleModelObj obj10 = SetUNFree(pool);

        PrintPoolInfo(pool);

        return;

    }

    /// <summary>
    /// 第一个释放，再分配
    /// </summary>
    /// <param name="pool"></param>
    void Test2(DFObjectPool pool)
    {
        BattleModelObj obj0 = SetUNFree(pool);
        BattleModelObj obj1 = SetUNFree(pool);
        BattleModelObj obj2 = SetUNFree(pool);

        SetFree(pool, obj0);
        PrintPoolInfo(pool);
        BattleModelObj obj3 = SetUNFree(pool);
    }

    /// <summary>
    /// 第一个释放，再分配，再分配若干，导致reszie后，
    /// 再执行第一个释放，在分配
    /// 
    /// </summary>
    /// <param name="pool"></param>
    void Test3(DFObjectPool pool)
    {
        BattleModelObj obj0 = SetUNFree(pool);
        BattleModelObj obj1 = SetUNFree(pool);
        BattleModelObj obj2 = SetUNFree(pool);

        SetFree(pool, obj0);
        PrintPoolInfo(pool);
        BattleModelObj obj3 = SetUNFree(pool);

        BattleModelObj obj4 = SetUNFree(pool);
        BattleModelObj obj5 = SetUNFree(pool);
        BattleModelObj obj6 = SetUNFree(pool);
        BattleModelObj obj7 = SetUNFree(pool);
        BattleModelObj obj8 = SetUNFree(pool);

        BattleModelObj obj9 = SetUNFree(pool);
        SetFree(pool, obj0);
        BattleModelObj obj10 = SetUNFree(pool);
    }

    /// <summary>
    /// resize前释放最后一个，再分配
    /// 再次分配，触发resize
    /// </summary>
    /// <param name="pool"></param>
    void Test4(DFObjectPool pool)
    {
        BattleModelObj obj0 = SetUNFree(pool);
        BattleModelObj obj1 = SetUNFree(pool);
        BattleModelObj obj2 = SetUNFree(pool);
        BattleModelObj obj3 = SetUNFree(pool);
        BattleModelObj obj4 = SetUNFree(pool);
        BattleModelObj obj5 = SetUNFree(pool);
        BattleModelObj obj6 = SetUNFree(pool);
        BattleModelObj obj7 = SetUNFree(pool);
        SetFree(pool, obj7);
        BattleModelObj obj8 = SetUNFree(pool);

        BattleModelObj obj9 = SetUNFree(pool);
        

    }

    void PrintPoolInfo(DFObjectPool pool)
    {
        Debug.Log("free " + pool.m_free
            + " nextfree " + pool.m_nextFree
            + " countFree " + pool.m_countFree);
    }

    BattleModelObj SetUNFree(DFObjectPool pool)
    {
        BattleModelObj obj = pool.BorrowObj() as BattleModelObj;
        obj.m_go.name += "+UNFREE";
        return obj;
    }

    void SetFree(DFObjectPool pool, BattleModelObj obj)
    {
        pool.ReturnObj(obj);
        obj.m_go.name += "+FREE";
    }


	
}
