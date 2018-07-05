using UnityEngine;

#region 开火 组件
public class BtlFire : MonoBehaviour {
	//归属 飞机
	public BtlPlane parent;
	void Awake(){
	}
	// Use this for initialization
	void Start (){
	}
	
	// Update is called once per frame
	void Update (){
		foreach (var btlBullet in this.parent.btlBulletMgr.btlBulletList){
            if (0 < btlBullet.fireCoolDown){
                btlBullet.fireCoolDown -= Time.deltaTime;
            }
        }
        this.Fire();
	}

    //开火
    public void Fire(){
        BtlPlane plane = this.parent;
		if (!plane.CanFire ()) {
			return;
		}
		foreach (var bullet in plane.btlBulletMgr.btlBulletList){
            if (!bullet.CanFire()){
                continue;
            }
            bool is_user_plane = false;
            if (plane.isUser()){
                is_user_plane = true;
            }
            bullet.fireCoolDown = bullet.xmlBullet.coolDown;
            
            GameObject bulletPrefabs = (GameObject)Resources.Load(bullet.bulletName);
            if (null == bulletPrefabs){
                Debug.LogErrorFormat("开火失败{0}", bullet.bulletName);
                return;
            }

            BtlBullet newBullet = new BtlBullet();
            newBullet.camp = plane.camp;

            #region 计算子弹的偏移量，得出发射的初始位置
            Vector3 newBulletPosition = new Vector3();
            newBulletPosition.x = transform.position.x + bullet.xmlBullet.positionOffsetX;
            newBulletPosition.y = transform.position.y + bullet.xmlBullet.positionOffsetY;
            #endregion

            #region 计算出子弹到目标飞机的距离
            BtlPlane userPlane = Global.Instance.btlMgr.GetUserPlane();
            float x = newBulletPosition.x - userPlane.gameObject.transform.position.x;
            float y = newBulletPosition.y - userPlane.gameObject.transform.position.y;
            #endregion

            #region 旋转子弹
            float angleOfLine = (float)(System.Math.Atan2((-x), (y)) * 180 / System.Math.PI);
            if (!is_user_plane)
            {
                angleOfLine -= 180.0f;
            }
            Vector3 rotation = new Vector3(0, 0, angleOfLine);
            Quaternion quaternion = new Quaternion();
            quaternion = Quaternion.Euler(rotation);
            #endregion

            newBullet.gameObject = Instantiate(bulletPrefabs, newBulletPosition, quaternion);
			#region 此处设置,飞机销毁,子弹也会随父节点销毁
			//bulletPrefab.transform.SetParent(plane.gameObject.transform);
			#endregion

            Rigidbody2D rigidbody2D = newBullet.gameObject.AddComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 0;
            BoxCollider2D boxCollider2D = newBullet.gameObject.AddComponent<BoxCollider2D>();

            BtlBulletMove btlBulletMove = newBullet.gameObject.AddComponent<BtlBulletMove>();
            if (null == btlBulletMove){
                Debug.LogErrorFormat("开火失败 BattleBulletMove");
                return;
            }
            btlBulletMove.parent = newBullet;
            newBullet.btlMove.moveTrace = EnumMoveTrace.Line;
            newBullet.btlMove.speed.x = bullet.xmlBullet.speed;
            newBullet.btlMove.speed.y = bullet.xmlBullet.speed;

			if(is_user_plane){
                newBullet.gameObject.layer = (int)EnumLayer.UserBullet;
                newBullet.btlMove.direction.x = bullet.xmlBullet.directionOffsetX;
                newBullet.btlMove.direction.y = bullet.xmlBullet.directionOffsetY;
            } else {
                newBullet.gameObject.layer = (int)EnumLayer.EnemyBullet;
                newBullet.btlMove.direction.x = -x * bullet.xmlBullet.directionOffsetX;
                newBullet.btlMove.direction.y = y * bullet.xmlBullet.directionOffsetY;
            }
        }
    }
}
#endregion