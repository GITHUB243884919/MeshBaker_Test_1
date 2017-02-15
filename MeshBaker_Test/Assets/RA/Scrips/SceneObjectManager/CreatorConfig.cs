/// <summary>
/// 场景对象生成器参数常量
/// autor: fanzhengyong
/// date: 2017-02-15 
/// </summary>
using UnityEngine;
using System.Collections;

public class CreatorConfig
{
    public static readonly  int MAX_MESHBAKER_TANK = 32;
    public static readonly string[] TANK_PATHS = new string[4] 
    {
        "TankRuntime_Bake/TankMeshBaker",
        "TankRuntime_Bake/TankRuntime_Bake-mat",
        "TankRuntime_Bake/TankRuntime_Bake",
        "TankRuntime_Bake/Tank_Seed"
    };

    public static readonly int MAX_MESHBAKER_SOLDIER = 32;
    public static readonly string[] SOLDIER_PATHS = new string[4] 
    {
        "SoldierRuntime_Bake/SoldierMeshBaker",
        "SoldierRuntime_Bake/SoldierRuntime_Bake-mat",
        "SoldierRuntime_Bake/SoldierRuntime_Bake",
        "SoldierRuntime_Bake/Soldier_Seed"
    };

}
