using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleForeGround : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        #region 加载用户飞机
        {
            //BattlePlane plane = Global.Instance.battleMgr.GetUserPlane();
            //GameObject planeGameObject = plane.gameObject;
            Global.Instance.battleMgr.GetUserPlane().name = "Prefabs/Plane/p_09d_0";
            GameObject planePrefabs = (GameObject)Resources.Load(Global.Instance.battleMgr.GetUserPlane().name);
            if (null == planePrefabs)
            {
                Debug.LogErrorFormat("GroundScrolling未找到{0}", Global.Instance.battleMgr.GetUserPlane().name);
            }
            Vector3 newPosition = transform.position;
            newPosition.x = 0;
            newPosition.y = -Camera.main.orthographicSize;

            Global.Instance.battleMgr.GetUserPlane().gameObject = Instantiate(planePrefabs, newPosition, transform.rotation);
            Global.Instance.battleMgr.GetUserPlaneTransform().SetParent(transform);
            //移动速度
            Global.Instance.battleMgr.GetUserPlane().battleMoveMgr.battleMoveUser.speed.x = 2;
            Global.Instance.battleMgr.GetUserPlane().battleMoveMgr.battleMoveUser.speed.y = 2;



            Rigidbody2D rigidbody2D = Global.Instance.battleMgr.GetUserPlane().gameObject.AddComponent<Rigidbody2D>();
            //无引力
            rigidbody2D.gravityScale = 0;

            BattleUserMove battleUserMove = Global.Instance.battleMgr.GetUserPlane().gameObject.AddComponent<BattleUserMove>();

            BattleUserEnterScene battleUserEnterScene = Global.Instance.battleMgr.GetUserPlane().gameObject.AddComponent<BattleUserEnterScene>();
            BattleFire BattleFire = Global.Instance.battleMgr.GetUserPlane().gameObject.AddComponent<BattleFire>();
			BattleFire.battlePlane = Global.Instance.battleMgr.GetUserPlane();
        }

        #endregion
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
