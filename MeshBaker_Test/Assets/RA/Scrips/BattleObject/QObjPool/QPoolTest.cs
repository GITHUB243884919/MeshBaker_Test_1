using UnityEngine;
using System.Collections;

public class QPoolTest : MonoBehaviour 
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

        QObjCreatorFactory<GameObject> creatorFactory = new QObjCreatorFactoryForMeshBaker(
            paths, BattleScene.E_BATTLE_OBJECT_TYPE.TANK, count);

        //QObjCreator<GameObject> creator = creatorFactory.CreatCreator();
        //creator.CreateObjects();


        QObjPool<GameObject> pool = new QObjPool<GameObject>();
        pool.Init(null, creatorFactory);

        //Test1(pool);
        Test2(pool);
        //Test3(pool);
        //Test4(pool);

    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="pool"></param>
    void Test2(QObjPool<GameObject> pool)
    {
        GameObject obj0 = SetUNFree(pool);
        GameObject obj1 = SetUNFree(pool);
        GameObject obj2 = SetUNFree(pool);

        SetFree(pool, obj1);
        GameObject obj3 = SetUNFree(pool);
    }

    //void PrintPoolInfo(DFObjectPool pool)
    //{
    //    Debug.Log("free " + pool.m_free
    //        + " nextfree " + pool.m_nextFree
    //        + " countFree " + pool.m_countFree);
    //}

    GameObject SetUNFree(QObjPool<GameObject> pool)
    {
        GameObject obj = pool.BorrowObj();
        obj.name += "+UNFREE";
        return obj;
    }

    void SetFree(QObjPool<GameObject> pool, GameObject obj)
    {
        pool.ReturnObj(obj);
        obj.name += "+FREE";
    }


	
}
