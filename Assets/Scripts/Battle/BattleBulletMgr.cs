using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo 将子弹中的属性 提取出来 放在 子弹的配置表中


#region 战斗中子弹
public class BattleBullet{
	//归属
	public BattleBulletMgr parent;
	public GameObject battleBulletMoveGameObject ;
	//子弹名称
	public string bulletName;
    //子弹类型
    public int bulletId;
    //开火冷却 秒
    public float fireCoolDown;
    //开火 秒
    public float fireTime;
	//破坏力
	public int damage;

    //碰撞对象
    //碰撞后是否消失
    //二次爆炸后 触发新的子弹管理器
    //子弹归属者消失后,子弹是否消失
    //是否暂停
    //子弹发射位置(相对发射器的偏移量)
    public Vector2 firePositionOffset = new Vector2(0, 0);
    public BattleBullet(){
    }
    //是否可以攻击
    public bool CanFire(){
		return this.fireCoolDown <= 0.0f;
    }
	public void Clear(){
		this.parent = null;
		this.battleBulletMoveGameObject = null;
		this.bulletName = "";
		this.bulletId = 0;
		this.fireCoolDown = 0f;
		this.fireTime = 0f;
		this.damage = 0;
        this.firePositionOffset = new Vector2(0, 0);
	}
}
#endregion
#region 战斗中子弹管理器
public class BattleBulletMgr {
    public List<BattleBullet> battleBulletList;
	//归属飞机
	public BattlePlane parent;
    public BattleBulletMgr(){
        this.battleBulletList = new List<BattleBullet>();
    }
    public void Add(string bulletName){
        BattleBullet battleBullet = new BattleBullet();
		battleBullet.parent = this;
        battleBullet.bulletName = bulletName;
		battleBullet.bulletId = 1;
		battleBullet.fireCoolDown = 0.1f;
		battleBullet.fireTime = 0.2f;



        
		battleBullet.damage = 1;
        battleBullet.firePositionOffset = new Vector2(0,0);


        this.battleBulletList.Add(battleBullet);
    }
	public void Clear(){
		this.battleBulletList.Clear ();
	}
}
#endregion

