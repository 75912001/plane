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
            if (null == planePrefabs){
                Debug.LogErrorFormat("GroundScrolling未找到{0}", plane.name);
            }
            Vector3 newPosition = transform.position;
            newPosition.x = 0;
            newPosition.y = -Camera.main.orthographicSize;

            plane.gameObject = Instantiate(planePrefabs, newPosition, transform.rotation);
            plane.gameObject.transform.SetParent(transform);
            //移动速度
            plane.battleMoveMgr.battleMove.speed.x = 2;
            plane.battleMoveMgr.battleMove.speed.y = 2;

            Rigidbody2D rigidbody2D = plane.gameObject.AddComponent<Rigidbody2D>();
            //无引力
            rigidbody2D.gravityScale = 0;

            BattleUserMove battleUserMove = plane.gameObject.AddComponent<BattleUserMove>();

            BattleUserEnterScene battleUserEnterScene = plane.gameObject.AddComponent<BattleUserEnterScene>();

            //加载子弹
            plane.battleBulletMgr.Add("Prefabs/Bullet/p_09d_15");
            BattleFire BattleFire = plane.gameObject.AddComponent<BattleFire>();
			BattleFire.parent = plane;

            
        }
        #endregion

        #region 加载敌人飞机
        
        for (int i = 0; i < 10; i++){
            BattlePlane plane = new BattlePlane {
                name = "Prefabs/Enemy/a-11_0"
            };
            GameObject planePrefabs = (GameObject)Resources.Load(plane.name);
            if (null == planePrefabs){
                Debug.LogErrorFormat("BattleForeGround未找到{0}", plane.name);
            }
            Vector3 newPosition = transform.position;
            newPosition.x = 0;
            newPosition.y = 2 * Camera.main.orthographicSize;
            newPosition.z = transform.position.z + 1;

            plane.gameObject = Instantiate(planePrefabs, newPosition, transform.rotation);
            plane.gameObject.transform.SetParent(transform);
            //移动速度
            plane.battleMoveMgr.battleMove.speed.x = 1;
            plane.battleMoveMgr.battleMove.speed.y = 1;

            Global.Instance.battleMgr.battlePlaneMgr.parkingApronBattlePlaneEnemyList.Add(plane);
        }
        
        #endregion
    }

    // Update is called once per frame
    void Update (){
        this.EnemyFly();
    }
    //敌机入场
    private void EnemyFly(){
        #region 判断玩家飞机是否入场
        if (!Global.Instance.battleMgr.GetUserPlane().isEnterSceneEnd){
            return;
        }
        #endregion

        Global.Instance.battleMgr.battlePlaneMgr.enemyFlyTime -= Time.deltaTime;
        if (0 < Global.Instance.battleMgr.battlePlaneMgr.enemyFlyTime){
            return;
        }
        Global.Instance.battleMgr.battlePlaneMgr.enemyFlyTime = Global.Instance.battleMgr.battlePlaneMgr.enemyFlyCoolDownTime;

        //定时创造敌机
        BattlePlane plane = new BattlePlane();

        plane.name = "Prefabs/Enemy/a-11_0";
        GameObject planePrefabs = (GameObject)Resources.Load(plane.name);
        if (null == planePrefabs){
            Debug.LogErrorFormat("BattleForeGround未找到{0}", plane.name);
        }
        Vector3 newPosition = transform.position;
        newPosition.x = 0;
        newPosition.y = 2 * Camera.main.orthographicSize;
        newPosition.z = transform.position.z + 1;

        plane.gameObject = Instantiate(planePrefabs, newPosition, transform.rotation);
        plane.gameObject.transform.SetParent(transform);
        //移动速度
        plane.battleMoveMgr.battleMove.speed.x = 1;
        plane.battleMoveMgr.battleMove.speed.y = 1;

        Rigidbody2D rigidbody2D = plane.gameObject.AddComponent<Rigidbody2D>();
        //无引力
        rigidbody2D.gravityScale = 1;

 //       BattleUserMove battleUserMove = plane.gameObject.AddComponent<BattlePlaneMove>();

        //加载子弹
        plane.battleBulletMgr.Add("Prefabs/Bullet/bullet-01_0");
        BattleFire BattleFire = plane.gameObject.AddComponent<BattleFire>();
        BattleFire.parent = plane;
    }
}
