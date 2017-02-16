using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FSMTest_Idle: MonoBehaviour 
{
	void Start () 
    {

        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(BtnClick);
	}

    public void BtnClick()
    {
        GameObject go = GameObject.Find("Tank_FSM");
        BattleFSMController FSMCtr = go.GetComponent<BattleFSMController>();
        FSMCtr.Trigger(BattleFSM.E_FSM_STATE.IDLE);
    }

}
