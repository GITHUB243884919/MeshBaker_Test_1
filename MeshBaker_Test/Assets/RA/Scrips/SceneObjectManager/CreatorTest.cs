using UnityEngine;
using System.Collections;

public class CreatorTest : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
        int count = 32;
        string [] paths = new string[4] 
        {
            "TankRuntime_Bake/TankMeshBaker",
            "TankRuntime_Bake/TankRuntime_Bake-mat",
            "TankRuntime_Bake/TankRuntime_Bake",
            "TankRuntime_Bake/Tank_Seed"
        };

        SceneObjectCreator creator = new SceneObjectCreator(
            paths, E_SCENE_OBJECT_TYPE.TANK, count);

        creator.CreateObject();
	}

}
