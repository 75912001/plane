using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUserEnterScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        #region 飞机入场
        if (!Global.Instance.battleMgr.GetUserPlane().isEnterSceneEnd)
        {
            Vector3 fromPosition = Global.Instance.battleMgr.GetUserPlaneTransform().parent.position;
            fromPosition.x = 0;
            fromPosition.y = -Camera.main.orthographicSize;
            Vector3 toPosition = Global.Instance.battleMgr.GetUserPlaneTransform().parent.position;
            toPosition.x = 0;
            toPosition.y = -Camera.main.orthographicSize / 1.5f;
            Global.Instance.battleMgr.GetUserPlaneTransform().position = Vector3.Lerp(fromPosition, toPosition, Time.time * 0.8f);
            if (Vector3.Distance(Global.Instance.battleMgr.GetUserPlaneTransform().position, toPosition) < 0.1f)
            {
                Global.Instance.battleMgr.GetUserPlane().isEnterSceneEnd = true;
                Debug.LogFormat("玩家飞机入场结束");
            }
        }
        #endregion
    }
}
