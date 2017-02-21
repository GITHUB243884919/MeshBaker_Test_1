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
        Vector3 initPos = new Vector3(320, 0, 320);

        BattleModelObj obj = BattleScene.Instance.BorrowBattleModelObj(
            type, serverEntityID, serverEntityType);

        obj.m_go.transform.position = initPos;
        BattleFSMController FSMCtr = obj.m_go.GetComponent<BattleFSMController>();
        FSMCtr.Trigger(BattleFSM.E_FSM_STATE.ATTACK);

    }
}
