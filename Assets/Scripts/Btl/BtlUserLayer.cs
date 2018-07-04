using System.IO;
using UnityEngine;

#region 战斗 用户 层
public class BtlUserLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        BtlMgr btlMgr = Global.Instance.btlMgr;
        BtlPlaneMgr btlPlaneMgr = btlMgr.btlPlaneMgr;

        {
            
            #region 加载用户飞机
            BtlPlane plane = btlPlaneMgr.btlPlaneUser;
            plane.xmlPlane = Global.Instance.xmlPlaneMgr.Find(1);

            plane.camp = EnumCamp.Blue;
            plane.isVisble = true;
            plane.hpMax = 1000;
            plane.hp = plane.hpMax;
            GameObject planePrefabs = (GameObject)Resources.Load(plane.xmlPlane.prefabs);
            if (null == planePrefabs)
            {
                Debug.LogErrorFormat("BtlFG未找到{0}", plane.xmlPlane.prefabs);
            }
            Vector3 newPosition = transform.position;
            newPosition.x = 0;
            newPosition.y = -Camera.main.orthographicSize;

            plane.gameObject = Instantiate(planePrefabs, newPosition, transform.rotation);
            plane.gameObject.transform.SetParent(transform);
            plane.gameObject.layer = (int)EnumLayer.User;
            

            Rigidbody2D rigidbody2D = plane.gameObject.AddComponent<Rigidbody2D>();
            //无引力
            rigidbody2D.gravityScale = 0;
            BoxCollider2D boxCollider2D = plane.gameObject.AddComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = true;

            //移动速度
            plane.btlMove.speed = new Vector2(plane.xmlPlane.speedX, plane.xmlPlane.speedY);

            BtlUserMove btlUserMove = plane.gameObject.AddComponent<BtlUserMove>();
            btlUserMove.parent = plane;

            BtlHit btlHit = plane.gameObject.AddComponent<BtlHit>();
            btlHit.parent = plane;

            BtlUserEnterScene battleUserEnterScene = plane.gameObject.AddComponent<BtlUserEnterScene>();
            battleUserEnterScene.parent = plane;

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
        {
            #region 加载飞机的闪电特效

            BtlPlane userPlane = Global.Instance.btlMgr.GetUserPlane();

            {
                GameObject prefabs = (GameObject)Resources.Load("Prefabs/Anim/p_09d_36");
                if (null == prefabs)
                {
                    Debug.LogErrorFormat("Prefabs/Anim/p_09d_36未找到");
                }
                GameObject gameObject = Instantiate(prefabs, userPlane.gameObject.transform.position, userPlane.gameObject.transform.rotation);
                gameObject.transform.SetParent(userPlane.gameObject.transform);
                gameObject.layer = (int)EnumLayer.User;
            }
            #endregion
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
#endregion