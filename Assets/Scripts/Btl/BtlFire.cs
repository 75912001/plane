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

            bullet.camp = plane.camp;
            bullet.fireCoolDown = bullet.fireTime;

            GameObject bulletPrefabs = (GameObject)Resources.Load(bullet.bulletName);
            if (null == bulletPrefabs){
                Debug.LogErrorFormat("开火失败{0}", bullet.bulletName);
                return;
            }

			//todo 计算子弹的偏移量，得出发射的初始位置
			bullet.gameObject = Instantiate(bulletPrefabs, transform.position, transform.rotation);
			#region 此处设置，飞机销毁，子弹也会随父节点销毁
			//bulletPrefab.transform.SetParent(plane.gameObject.transform);
			#endregion

			Rigidbody2D rigidbody2D = bullet.gameObject.AddComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 0;
			BoxCollider2D boxCollider2D = bullet.gameObject.AddComponent<BoxCollider2D>();

			BtlBulletMove btlBulletMove = bullet.gameObject.AddComponent<BtlBulletMove>();
            if (null == btlBulletMove){
                Debug.LogErrorFormat("开火失败 BattleBulletMove");
                return;
            }
			btlBulletMove.parent = bullet;

            bullet.btlMove.moveTrace = EnumMoveTrace.Line;
			if(is_user_plane){
                bullet.gameObject.layer = (int)EnumLayer.UserBullet;
                bullet.btlMove.speed = new Vector2(0, 10);
                bullet.btlMove.direction = new Vector2(1, 1);
            } else {
                bullet.gameObject.layer = (int)EnumLayer.EnemyBullet;
                bullet.btlMove.speed = new Vector2(0, 2);
                bullet.btlMove.direction = new Vector2(0, -1);
            }
        }
    }
}
#endregion