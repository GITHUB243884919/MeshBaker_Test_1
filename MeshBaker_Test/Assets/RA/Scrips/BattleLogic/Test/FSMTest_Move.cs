using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FSMTest_Move : MonoBehaviour 
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
        FSMCtr.SetTargetForMove(new Vector3(1000f, 0, 1000f));
        FSMCtr.Trigger(BattleFSM.E_FSM_STATE.MOVE);
    }
}
