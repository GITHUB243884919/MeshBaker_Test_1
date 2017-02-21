using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneTest_1 : MonoBehaviour {

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(BtnClick);
    }



    public void BtnClick()
    {
        Debug.Log("Begin click " + Time.realtimeSinceStartup);
        StartCoroutine("CreateTanks");
        //for (int i = 0; i < 128; i++ )
        //{
        //    //CreateTank(i, false, false);
        //    CreateTank(i, true, false);

        //}
        Debug.Log("End click " + Time.realtimeSinceStartup);
            
    }

    IEnumerator CreateTanks()
    {
        yield return null;
        for (int i = 0; i < 128; i++)
        {
            //CreateTank(i, false, false);
            CreateTank(i, true, false);

        }
    }
    void CreateTank(int entityID, bool withAI, bool withEffect)
    {
        BattleScene.E_BATTLE_OBJECT_TYPE type
            = BattleScene.E_BATTLE_OBJECT_TYPE.TANK;
        int serverEntityID = entityID;
        int serverEntityType = 1;

        Vector3 initPos = new Vector3(0f, 0f, 0f);

        BattleModelObj obj = BattleScene.Instance.BorrowBattleModelObj(
            type, serverEntityID, serverEntityType);

        //Quaternion quaternion = new Quaternion(0, 0, 0, 1);
        //obj.m_go.transform.rotation = quaternion;
        obj.m_go.transform.position = initPos;
        
        //
        if (withAI)
        {
            BattleAIArrive2 arrive = obj.m_go.AddComponent<BattleAIArrive2>();
            arrive.enabled = false;

            BattleAISteerings2 steers = obj.m_go.AddComponent<BattleAISteerings2>();
            steers.enabled = false;

            BattleFSM FSM = obj.m_go.AddComponent<BattleFSM>();
            //FSM.enabled = false;

            BattleFSMController controller = obj.m_go.AddComponent<BattleFSMController>();

            arrive.enabled = true;
            steers.enabled = true;
            //FSM.enabled = true;
            
            
            
            //BattleFSMController FSMCtr = obj.m_go.GetComponent<BattleFSMController>();
            Vector3 target = new Vector3(Random.Range(10, 310), 0f, Random.Range(10, 310));
            Debug.Log(target);
            controller.SetTargetForMove(target);
            controller.Trigger(BattleFSM.E_FSM_STATE.MOVE);
            
        }


        if (withEffect)
        {
            string effectPath = "Tank/Prefab/Tank_dapao_fire";
            GameObject effectRes = Resources.Load<GameObject>(effectPath);
            GameObject effectGo = GameObject.Instantiate<GameObject>(effectRes);
            effectGo.transform.Rotate(new Vector3(0f, -90f, 0f));
            Transform mount = obj.m_go.transform.Find("Bone01/Bone02/Dummy01");
            effectGo.transform.SetParent(mount, false);
        }

    }

}
