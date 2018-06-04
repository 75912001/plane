using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 开火 组件
public class BattleFire : MonoBehaviour {
	//归属 飞机
	public BattlePlane parent;
	void Awake(){
	}
	// Use this for initialization
	void Start (){
	}
	
	// Update is called once per frame
	void Update (){
		foreach (var battleBullet in this.parent.battleBulletMgr.battleBulletList){
            if (0 < battleBullet.fireCoolDown){
                battleBullet.fireCoolDown -= Time.deltaTime;
            }
        }
        this.Fire();
	}

    //开火
    public void Fire(){
		if (!this.parent.CanFire ()) {
			return;
		}
		foreach (var battleBullet in this.parent.battleBulletMgr.battleBulletList){
            if (!battleBullet.CanFire()){
                continue;
            }
            battleBullet.fireCoolDown = battleBullet.fireTime;

            GameObject bulletPrefabs = (GameObject)Resources.Load(battleBullet.bulletName);
            if (null == bulletPrefabs){
                Debug.LogErrorFormat("开火失败{0}", battleBullet.bulletName);
                return;
            }
			//todo 计算子弹的偏移量，得出发射的初始位置
            GameObject bulletPrefab = Instantiate(bulletPrefabs, transform.position, transform.rotation);
			bulletPrefab.transform.SetParent(this.parent.gameObject.transform);

            Rigidbody2D rigidbody2D = bulletPrefab.AddComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 0;

            BattleBulletMove battleBulletMove = bulletPrefab.AddComponent<BattleBulletMove>();
            if (null == battleBulletMove){
                Debug.LogErrorFormat("开火失败 BattleBulletMove");
                return;
            }
			battleBulletMove.parent = battleBullet;

			if(Global.Instance.battleMgr.GetUserPlane() == battleBullet.parent.parent){
                battleBullet.battleBulletMoveMgr.direction.y = 1;
            } else {
                battleBullet.battleBulletMoveMgr.direction.y = -1;
            }
        }
    }
}
#endregion