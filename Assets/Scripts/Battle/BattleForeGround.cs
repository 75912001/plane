using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleForeGround : MonoBehaviour {

	// Use this for initialization
	void Start () {
        #region 加载用户飞机
        {
            string strPlaneName = "Prefabs/Plane/p_09d_0";
            GameObject planePrefabs = (GameObject)Resources.Load(strPlaneName);
            if (null == planePrefabs)
            {
                Debug.LogErrorFormat("GroundScrolling未找到{0}", strPlaneName);
            }
            Vector3 newPosition = transform.position;
            newPosition.x = 0;
            newPosition.y = -Camera.main.orthographicSize;

            Global.Instance.battleMgr.battlePlaneMgr.battlePlaneUser.gameObject = Instantiate(planePrefabs, newPosition, transform.rotation);
            Global.Instance.battleMgr.GetUserPlaneTransform().SetParent(transform);

            Global.Instance.battleMgr.GetUserPlane().battleMoveMgr.battleMoveUser.speed.x = 2;
            Global.Instance.battleMgr.GetUserPlane().battleMoveMgr.battleMoveUser.speed.y = 2;

            Rigidbody2D rigidbody2D = Global.Instance.battleMgr.GetUserPlaneGameObject().AddComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 0;
            BattleUserMove battleUserMove = Global.Instance.battleMgr.GetUserPlaneGameObject().AddComponent<BattleUserMove>();

            BattleUserEnterScene battleUserEnterScene = Global.Instance.battleMgr.GetUserPlaneGameObject().AddComponent<BattleUserEnterScene>();
            BattleFire BattleFire = Global.Instance.battleMgr.GetUserPlaneGameObject().AddComponent<BattleFire>();
			BattleFire.battlePlane = Global.Instance.battleMgr.GetUserPlane();

        }

        #endregion
    }

    // Update is called once per frame
    void Update () {
		
	}
}
