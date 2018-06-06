using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleForeGround : MonoBehaviour {
	// Use this for initialization
	void Start (){
        BattleMgr battleMgr = Global.Instance.battleMgr;
        BattlePlaneMgr battlePlaneMgr = battleMgr.battlePlaneMgr;
        BattlePlane battlePlaneUser = battlePlaneMgr.battlePlaneUser; 
        #region 加载用户飞机
        {
            battlePlaneUser.name = "Prefabs/Plane/p_09d_0";
            battlePlaneUser.isVisble = true;
            GameObject planePrefabs = (GameObject)Resources.Load(battlePlaneUser.name);
            if (null == planePrefabs){
                Debug.LogErrorFormat("GroundScrolling未找到{0}", battlePlaneUser.name);
            }
            Vector3 newPosition = transform.position;
            newPosition.x = 0;
            newPosition.y = -Camera.main.orthographicSize;

            battlePlaneUser.gameObject = Instantiate(planePrefabs, newPosition, transform.rotation);
            battlePlaneUser.gameObject.transform.SetParent(transform);
            //移动速度
            //plane.battleMove.speed = new Vector2(2,2);

            Rigidbody2D rigidbody2D = battlePlaneUser.gameObject.AddComponent<Rigidbody2D>();
            //无引力
            rigidbody2D.gravityScale = 0;

            BattlePlaneMove battlePlaneMove = battlePlaneUser.gameObject.AddComponent<BattlePlaneMove>();
            battlePlaneMove.parent = battlePlaneUser;
            //移动速度
            battlePlaneMove.speed = new Vector2(2, 2);

            BattleUserMove battleUserMove = battlePlaneUser.gameObject.AddComponent<BattleUserMove>();
            battleUserMove.battlePlaneMove = battlePlaneMove;

            BattleUserEnterScene battleUserEnterScene = battlePlaneUser.gameObject.AddComponent<BattleUserEnterScene>();

            //加载子弹
            battlePlaneUser.battleBulletMgr.Add("Prefabs/Bullet/p_09d_15");
            BattleFire BattleFire = battlePlaneUser.gameObject.AddComponent<BattleFire>();
			BattleFire.parent = battlePlaneUser;
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
            newPosition.x = i;
            newPosition.y = 2 * Camera.main.orthographicSize;
            newPosition.z = transform.position.z + 1;

            plane.gameObject = Instantiate(planePrefabs, newPosition, transform.rotation);
            plane.gameObject.transform.SetParent(transform);

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

        #region 从停机坪 -> 起飞战斗
        List<BattlePlane> battlePlaneEnemyList = Global.Instance.battleMgr.battlePlaneMgr.battlePlaneEnemyList;
        List<BattlePlane> parkingApronBattlePlaneEnemyList =  Global.Instance.battleMgr.battlePlaneMgr.parkingApronBattlePlaneEnemyList;
        if (0 < parkingApronBattlePlaneEnemyList.Count){
            BattlePlane plane = parkingApronBattlePlaneEnemyList[0];
            battlePlaneEnemyList.Add(plane);
            parkingApronBattlePlaneEnemyList.RemoveAt(0);

            plane.isEnterSceneEnd = true;
            #region 添加组件
            Rigidbody2D rigidbody2D = plane.gameObject.AddComponent<Rigidbody2D>();
            //无引力
            rigidbody2D.gravityScale = 0;

            BattlePlaneMove battlePlaneMove = plane.gameObject.AddComponent<BattlePlaneMove>();
            battlePlaneMove.parent = plane;
            //移动速度
            battlePlaneMove.speed = new Vector2(1,1);
            battlePlaneMove.direction = new Vector2(0,-1);
            battlePlaneMove.moveTrace = EnumMoveTrace.Line;

            //加载子弹
            plane.battleBulletMgr.Add("Prefabs/Bullet/bullet-01_0");
            BattleFire BattleFire = plane.gameObject.AddComponent<BattleFire>();
            BattleFire.parent = plane;
            #endregion
        }
        #endregion
    }
}
