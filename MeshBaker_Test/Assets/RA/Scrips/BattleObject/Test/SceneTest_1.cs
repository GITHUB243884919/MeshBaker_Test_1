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
        //GameObject go = GameObject.Find("Tank_FSM");
        //BattleFSMController FSMCtr = go.GetComponent<BattleFSMController>();
        //FSMCtr.Trigger(BattleFSM.E_FSM_STATE.ATTACK);

        BattleScene.E_BATTLE_OBJECT_TYPE type 
            = BattleScene.E_BATTLE_OBJECT_TYPE.TANK;
        int serverEntityID = 1;
        int serverEntityType = 1;
        //Vector3 initPos = new Vector3(0, 0, 0);
        Vector3 initPos = new Vector3(0, 0, 0);

        BattleModelObj obj = BattleScene.Instance.BorrowBattleModelObj(
            type, serverEntityID, serverEntityType);

        obj.m_go.transform.position = initPos;
        BattleFSMController FSMCtr = obj.m_go.GetComponent<BattleFSMController>();
        //FSMCtr.Trigger(BattleFSM.E_FSM_STATE.ATTACK);
        FSMCtr.SetTargetForMove(new Vector3(320f, 0f, 320f));
        FSMCtr.Trigger(BattleFSM.E_FSM_STATE.MOVE);
        
        //FSMCtr.Trigger(BattleFSM.E_FSM_STATE.NONE);
        string effectPath = "Tank/Prefab/Tank_dapao_fire";
        GameObject effectRes = Resources.Load<GameObject>(effectPath);
        GameObject effectGo = GameObject.Instantiate<GameObject>(effectRes);
        effectGo.transform.Rotate(new Vector3(0f, -90f, 0f));
        Transform mount = obj.m_go.transform.Find("Bone01/Bone02/Dummy01");
        effectGo.transform.SetParent(mount, false);

    }
}
