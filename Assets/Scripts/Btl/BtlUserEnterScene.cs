using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 战斗中 用户进入场景组件
public class BtlUserEnterScene : MonoBehaviour {
    //组件归属
    public BtlPlane parent;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        #region 飞机入场
        if (!Global.Instance.btlMgr.GetUserPlane().isEnterSceneEnd){
            Vector3 fromPosition = Global.Instance.btlMgr.GetUserPlaneTransform().parent.position;
            fromPosition.x = 0;
            fromPosition.y = -Camera.main.orthographicSize;
            Vector3 toPosition = Global.Instance.btlMgr.GetUserPlaneTransform().parent.position;
            toPosition.x = 0;
            toPosition.y = -Camera.main.orthographicSize / 1.5f;
            Global.Instance.btlMgr.GetUserPlaneTransform().position = Vector3.Lerp(fromPosition, toPosition, Time.timeSinceLevelLoad * 0.8f);
            if (Vector3.Distance(Global.Instance.btlMgr.GetUserPlaneTransform().position, toPosition) < 0.1f){
                Global.Instance.btlMgr.GetUserPlane().isEnterSceneEnd = true;
                Debug.LogFormat("玩家飞机入场结束");
            }
        }
        #endregion
    }
}
#endregion