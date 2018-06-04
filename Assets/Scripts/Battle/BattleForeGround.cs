using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleForeGround : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        #region 加载用户飞机
        {
            BattlePlane plane = Global.Instance.battleMgr.GetUserPlane();
            //GameObject planeGameObject = plane.gameObject;
            plane.name = "Prefabs/Plane/p_09d_0";
            GameObject planePrefabs = (GameObject)Resources.Load(plane.name);
            if (null == planePrefabs)
            {
                Debug.LogErrorFormat("GroundScrolling未找到{0}", plane.name);
            }
            Vector3 newPosition = transform.position;
            newPosition.x = 0;
            newPosition.y = -Camera.main.orthographicSize;

            plane.gameObject = Instantiate(planePrefabs, newPosition, transform.rotation);
            plane.gameObject.transform.SetParent(transform);
            //移动速度
            plane.battleMoveMgr.battleMoveUser.speed.x = 2;
            plane.battleMoveMgr.battleMoveUser.speed.y = 2;



            Rigidbody2D rigidbody2D = plane.gameObject.AddComponent<Rigidbody2D>();
            //无引力
            rigidbody2D.gravityScale = 0;

            BattleUserMove battleUserMove = plane.gameObject.AddComponent<BattleUserMove>();

            BattleUserEnterScene battleUserEnterScene = plane.gameObject.AddComponent<BattleUserEnterScene>();

            //加载子弹
            plane.battleBulletMgr.Add("Prefabs/Plane/p_09d_15");
            BattleFire BattleFire = plane.gameObject.AddComponent<BattleFire>();
			BattleFire.battlePlane = plane;
        }

        #endregion
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
