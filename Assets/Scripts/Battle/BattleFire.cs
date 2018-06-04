using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//开火
public class BattleFire : MonoBehaviour {

	public BattlePlane battlePlane;
	void Awake()
    {
		//this.battlePlane = new BattlePlane ();
	}
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        foreach (var battleBullet in this.battlePlane.battleBulletMgr.battleBulletList)
        {
            if (0 < battleBullet.fireCoolDown)
            {
                battleBullet.fireCoolDown -= Time.deltaTime;
            }
        }
        this.Fire();
	}

    //开火
    public void Fire()
    {
        foreach (var battleBullet in this.battlePlane.battleBulletMgr.battleBulletList)
        {
            if (!battleBullet.CanFire())
            {
                continue;
            }
            battleBullet.fireCoolDown = battleBullet.fireTime;
            battleBullet.battlePlane = this.battlePlane;

            GameObject bulletPrefabs = (GameObject)Resources.Load(battleBullet.bulletName);
            if (null == bulletPrefabs)
            {
                Debug.LogErrorFormat("开火失败{0}", battleBullet.bulletName);
                return;
            }

            GameObject bulletPrefab = Instantiate(bulletPrefabs, transform.position, transform.rotation);
            bulletPrefab.transform.SetParent(this.battlePlane.gameObject.transform);
            Rigidbody2D rigidbody2D = bulletPrefab.AddComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 0;

            BattleBulletMove battleBulletMove = bulletPrefab.AddComponent<BattleBulletMove>();
            if (null == battleBulletMove)
            {
                Debug.LogErrorFormat("开火失败 BattleBulletMove");
                return;
            }

            battleBulletMove.battleBullet = battleBullet;
            if(Global.Instance.battleMgr.GetUserPlane() == battleBullet.battlePlane)
            {
                battleBullet.battleBulletMoveMgr.direction.y = 1;
            } else
            {
                battleBullet.battleBulletMoveMgr.direction.y = -1;
            }
        }
    }
}
