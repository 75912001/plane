using System.Collections.Generic;
using UnityEngine;

public class BtlEnemyLayer : MonoBehaviour {
    //敌机层 飞行时间
    public float enemyLayerFlyTime;
	// Use this for initialization
	void Start () {
        //BtlMgr btlMgr = Global.Instance.btlMgr;
        //BtlPlaneMgr btlPlaneMgr = btlMgr.btlPlaneMgr;
        {
            #region 加载敌人飞机 -> 停机坪
            XmlGameLevel xmlGameLevel = Global.Instance.xmlGameLevelMgr.Find(Global.Instance.btlMgr.gameLevel);
            int i = 0;
            foreach (var v in xmlGameLevel.enemyList)
            {
                BtlPlane plane = new BtlPlane();
                plane.xmlGameLevelEnemy = v;
                plane.xmlPlane = Global.Instance.xmlPlaneMgr.Find(v.planeId);
                plane.camp = EnumCamp.Red;
                plane.hp = plane.xmlPlane.hp;
                plane.hpMax = plane.xmlPlane.hp;

                GameObject planePrefabs = (GameObject)Resources.Load(plane.xmlPlane.prefabs);

                Vector3 newPosition = transform.position;
                newPosition.x = i;
                newPosition.y = 2 * Camera.main.orthographicSize;
                newPosition.z = transform.position.z + 1;

                plane.gameObject = Instantiate(planePrefabs, newPosition, transform.rotation);
                plane.gameObject.transform.SetParent(transform);
                plane.gameObject.layer = (int)EnumLayer.Enemy;

                Global.Instance.btlMgr.btlPlaneMgr.parkingApronBtlPlaneEnemyList.Add(plane);
                i++;
            }
            #endregion
        }
    }

    // Update is called once per frame
    void Update()
    {
        //更新飞行中的敌机的飞行时间
        foreach (var v in Global.Instance.btlMgr.btlPlaneMgr.btlPlaneEnemyList)
        {
            BtlPlane plane = v;
            plane.flyTime += Time.deltaTime;
        }
        this.EnemyFly();
    }
    //敌机入场
    private void EnemyFly()
    {
        #region 判断玩家飞机是否入场
        if (!Global.Instance.btlMgr.GetUserPlane().isEnterSceneEnd)
        {
            return;
        }
        #endregion

        this.enemyLayerFlyTime += Time.deltaTime;

        #region 从停机坪 -> 起飞战斗
        List<BtlPlane> btlPlaneEnemyList = Global.Instance.btlMgr.btlPlaneMgr.btlPlaneEnemyList;
        List<BtlPlane> parkingApronBtlPlaneEnemyList = Global.Instance.btlMgr.btlPlaneMgr.parkingApronBtlPlaneEnemyList;

        for (int i = parkingApronBtlPlaneEnemyList.Count - 1; i >= 0; i--)
        {
            BtlPlane plane = parkingApronBtlPlaneEnemyList[i];
            if (this.enemyLayerFlyTime < plane.xmlGameLevelEnemy.enterTime)
            {
                continue;
            }

            btlPlaneEnemyList.Add(plane);
            parkingApronBtlPlaneEnemyList.RemoveAt(i);

            plane.isEnterSceneEnd = true;
            #region 添加组件
            Rigidbody2D rigidbody2D = plane.gameObject.AddComponent<Rigidbody2D>();
            //无引力
            rigidbody2D.gravityScale = 0;
            BoxCollider2D boxCollider2D = plane.gameObject.AddComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = true;

            BtlPlaneMove btlPlaneMove = plane.gameObject.AddComponent<BtlPlaneMove>();
            btlPlaneMove.parent = plane;

            BtlHit btlHit = plane.gameObject.AddComponent<BtlHit>();
            btlHit.parent = plane;

            //移动速度
            plane.btlMove.speed = new Vector2(plane.xmlPlane.speedX, plane.xmlPlane.speedY);
            plane.btlMove.direction = new Vector2(plane.xmlGameLevelEnemy.directionX, plane.xmlGameLevelEnemy.directionY);
            plane.btlMove.moveTrace = EnumMoveTrace.Line;

            //入场位置
            Vector3 newPosition = plane.gameObject.transform.position;
            newPosition.x = plane.xmlGameLevelEnemy.enterX;
            newPosition.y = plane.xmlGameLevelEnemy.enterY;
            plane.gameObject.transform.position = newPosition;


            //加载子弹
            foreach (var v in plane.xmlPlane.bulletList)
            {
                XmlBullet xmlBullet = Global.Instance.xmlBulletMgr.Find(v);
                
                plane.btlBulletMgr.Add(xmlBullet);
                BtlFire btlFire = plane.gameObject.AddComponent<BtlFire>();
                btlFire.parent = plane;
            }
            
            #endregion
        }

        #endregion
    }
}
