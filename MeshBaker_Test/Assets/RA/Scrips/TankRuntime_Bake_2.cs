using UnityEngine;
using System.Collections;

public class TankRuntime_Bake_2 : MonoBehaviour
{
    //public MB3_MeshBaker meshbaker;
    public GameObject baker;
    public GameObject prefab;
    GameObject go1, go2, go3, go4;

    Material material;
    MB2_TextureBakeResults textureBakeResults;
    void Start_bak()
    {
        go1 = (GameObject)Instantiate(prefab);
        go1.transform.position = new Vector3(0f, 0f, 0f);

        go2 = (GameObject)Instantiate(prefab);
        go2.transform.position = new Vector3(10f, 0f, 10f);

        MB3_MeshBaker meshbaker = baker.GetComponentInChildren<MB3_MeshBaker>();
        //MB3_TextureBaker texturebaker = baker.GetComponent<MB3_TextureBaker>();
        // 材质烘焙结果集  
        //textureBaker.textureBakeResults = ScriptableObject.CreateInstance();
        //// 创建材质球  
        //textureBaker.resultMaterial = new Material(Shader.Find("Diffuse"));   
        //MB2_TextureBakeResults texturebakerResults = ScriptableObject.CreateInstance<MB2_TextureBakeResults>();
        //meshbaker.textureBakeResults = texturebakerResults;

        //texturebaker.textureBakeResults = ScriptableObject.CreateInstance<MB2_TextureBakeResults>();
        //texturebaker.resultMaterial = new Material(Shader.Find("Mobile/VertexLit"));
        //texturebaker.objsToMesh.Add(go1);
        //texturebaker.objsToMesh.Add(go2);
        //texturebaker.CreateAtlases();
        //meshbaker.textureBakeResults = texturebaker.textureBakeResults;



        GameObject[] objsToCombine = new GameObject[2] { go1, go2 };
        meshbaker.AddDeleteGameObjects(objsToCombine, null, true);
        //apply the changes we made this can be slow. See docs

        //meshbaker.ApplyAll();
        //meshbaker.Apply(false, true, false, false, false, false,
        //    false, false);
        //texturebaker.CreateAtlases();
        meshbaker.Apply();
    }

    void Start()
    {
        go1 = (GameObject)Instantiate(prefab);
        go1.transform.position = new Vector3(0f, 0f, 0f);

        go2 = (GameObject)Instantiate(prefab);
        go2.transform.position = new Vector3(0f, 0f, 0f);

        go3 = (GameObject)Instantiate(prefab);
        go3.transform.position = new Vector3(0f, 0f, 0f);

        go4 = (GameObject)Instantiate(prefab);
        go4.transform.position = new Vector3(0f, 0f, 0f);

        MB3_MeshBaker meshbaker = baker.GetComponentInChildren<MB3_MeshBaker>();
        MB3_TextureBaker texturebaker = baker.GetComponent<MB3_TextureBaker>();

        InitBaker(texturebaker, meshbaker);

        GameObject[] objsToCombine = new GameObject[4] { go1, go2, go3, go4};
        meshbaker.AddDeleteGameObjects(objsToCombine, null, true);
        meshbaker.Apply();

    }
    void InitBaker(MB3_TextureBaker texturebaker, MB3_MeshBaker meshbaker)
    {
        material = Resources.Load<Material>("TankRuntime_Bake/TankRuntime_Bake-mat");
        textureBakeResults = Resources.Load<MB2_TextureBakeResults>("TankRuntime_Bake/TankRuntime_Bake");
        texturebaker.resultMaterial = material;
        texturebaker.textureBakeResults = textureBakeResults;
        meshbaker.textureBakeResults = texturebaker.textureBakeResults;
        return;
    }

}