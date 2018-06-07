using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 战斗 前景 组件
public class BtlFG : MonoBehaviour {
	// Use this for initialization
	void Start (){
        BtlMgr btlMgr = Global.Instance.btlMgr;
        BtlPlaneMgr btlPlaneMgr = btlMgr.btlPlaneMgr;

        {
            #region 加载用户飞机
            BtlPlane plane = btlPlaneMgr.btlPlaneUser;
            plane.name = "Prefabs/Plane/p_09d_0";
            plane.isVisble = true;
            GameObject planePrefabs = (GameObject)Resources.Load(plane.name);
            if (null == planePrefabs){
                Debug.LogErrorFormat("BtlFG未找到{0}", plane.name);
            }
            Vector3 newPosition = transform.position;
            newPosition.x = 0;
            newPosition.y = -Camera.main.orthographicSize;

            plane.gameObject = Instantiate(planePrefabs, newPosition, transform.rotation);
            plane.gameObject.transform.SetParent(transform);

            Rigidbody2D rigidbody2D = plane.gameObject.AddComponent<Rigidbody2D>();
            //无引力
            rigidbody2D.gravityScale = 0;

            //移动速度
            plane.btlMove.speed = new Vector2(2, 2);

            BtlUserMove btlUserMove = plane.gameObject.AddComponent<BtlUserMove>();
            btlUserMove.parent = plane;

            BtlUserEnterScene battleUserEnterScene = plane.gameObject.AddComponent<BtlUserEnterScene>();
            battleUserEnterScene.parent = plane;

            //加载子弹
            plane.btlBulletMgr.Add("Prefabs/Bullet/p_09d_15");
            BtlFire btlFire = plane.gameObject.AddComponent<BtlFire>();
            btlFire.parent = plane;
            #endregion
        }

        {
            #region 加载敌人飞机 -> 停机坪
            for (int i = 0; i < 10; i++){
                BtlPlane plane = new BtlPlane{
                    name = "Prefabs/Enemy/a-11_0"
                };
                GameObject planePrefabs = (GameObject)Resources.Load(plane.name);
                if (null == planePrefabs){
                    Debug.LogErrorFormat("BtlFG未找到{0}", plane.name);
                }
                Vector3 newPosition = transform.position;
                newPosition.x = i;
                newPosition.y = 2 * Camera.main.orthographicSize;
                newPosition.z = transform.position.z + 1;

                plane.gameObject = Instantiate(planePrefabs, newPosition, transform.rotation);
                plane.gameObject.transform.SetParent(transform);

                Global.Instance.btlMgr.btlPlaneMgr.parkingApronBtlPlaneEnemyList.Add(plane);
            }
            #endregion
        }
    }

    // Update is called once per frame
    void Update (){
        this.EnemyFly();
    }
    //敌机入场
    private void EnemyFly(){
        #region 判断玩家飞机是否入场
        if (!Global.Instance.btlMgr.GetUserPlane().isEnterSceneEnd){
            return;
        }
        #endregion

        Global.Instance.btlMgr.btlPlaneMgr.enemyFlyTime -= Time.deltaTime;
        if (0 < Global.Instance.btlMgr.btlPlaneMgr.enemyFlyTime){
            return;
        }
        Global.Instance.btlMgr.btlPlaneMgr.enemyFlyTime = Global.Instance.btlMgr.btlPlaneMgr.enemyFlyCoolDownTime;

        #region 从停机坪 -> 起飞战斗
        List<BtlPlane> btlPlaneEnemyList = Global.Instance.btlMgr.btlPlaneMgr.btlPlaneEnemyList;
        List<BtlPlane> parkingApronBtlPlaneEnemyList =  Global.Instance.btlMgr.btlPlaneMgr.parkingApronBtlPlaneEnemyList;
        if (0 < parkingApronBtlPlaneEnemyList.Count){
            BtlPlane plane = parkingApronBtlPlaneEnemyList[0];
            btlPlaneEnemyList.Add(plane);
            parkingApronBtlPlaneEnemyList.RemoveAt(0);

            plane.isEnterSceneEnd = true;
            #region 添加组件
            Rigidbody2D rigidbody2D = plane.gameObject.AddComponent<Rigidbody2D>();
            //无引力
            rigidbody2D.gravityScale = 0;

            BtlPlaneMove btlPlaneMove = plane.gameObject.AddComponent<BtlPlaneMove>();
            btlPlaneMove.parent = plane;
            //移动速度
            plane.btlMove.speed = new Vector2(1,1);
            plane.btlMove.direction = new Vector2(0,-1);
            plane.btlMove.moveTrace = EnumMoveTrace.Line;

            //加载子弹
            plane.btlBulletMgr.Add("Prefabs/Bullet/bullet-01_0");
            BtlFire btlFire = plane.gameObject.AddComponent<BtlFire>();
            btlFire.parent = plane;
            #endregion
        }
        #endregion
    }
}
#endregion