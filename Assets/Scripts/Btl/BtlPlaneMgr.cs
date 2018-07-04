using System.Collections.Generic;
using UnityEngine;

#region 战斗中的飞机
public class BtlPlane{
    //飞机
    public GameObject gameObject;
	//阵营
	public EnumCamp camp;
    //对应配置表
    public XmlPlane xmlPlane;
    public XmlGameLevelEnemy xmlGameLevelEnemy;
    //子弹管理器
    public BtlBulletMgr btlBulletMgr;
    public BtlMove btlMove;
    //是否僚机
    public bool isWingman;
    //归属飞机
	public BtlPlane parentBtlPlane;

    public int hp;
    public int hpMax;

    //todo 僚机AI
    //todo 碰撞后给予敌方造成的伤害
    //入场飞行是否结束
    public bool isEnterSceneEnd;
    //是否可见的.在摄像机可见
    public bool isVisble;
    //飞行时间
    public float flyTime;

    public BtlPlane(){
        this.btlBulletMgr = new BtlBulletMgr();
        this.btlMove = new BtlMove();
		this.btlBulletMgr.parent = this;
    }
    public void Clear(){
        this.gameObject = null;
        this.btlBulletMgr.Clear();
        this.btlMove.Clear();
        this.isEnterSceneEnd = false;
        this.isWingman = false;
        this.isVisble = false;
        this.hp = 0;
        this.hpMax = 0;
        this.flyTime = 0;
    }
    //该飞机是否是用户
    public bool isUser()
    {
        return this == Global.Instance.btlMgr.btlPlaneMgr.btlPlaneUser;
    }
    //是否无敌
    public bool IsInvincible(){
        return false == this.isEnterSceneEnd;
    }
	//是否可以开火
	public bool CanFire(){
		return false != this.isEnterSceneEnd && this.isVisble;
	}
}
#endregion

#region 战斗中的飞机管理器
public class BtlPlaneMgr{
    public BtlPlane btlPlaneUser;
	public List<BtlPlane> btlPlaneEnemyList;
    public List<BtlPlane> parkingApronBtlPlaneEnemyList;
    public float enemyFlyTime;
    //敌机 起飞 冷却时间
    public float enemyFlyCoolDownTime;
    public BtlPlaneMgr(){
        this.btlPlaneUser = new BtlPlane();
		this.btlPlaneEnemyList = new List<BtlPlane> ();
        this.parkingApronBtlPlaneEnemyList = new List<BtlPlane>();
        this.enemyFlyCoolDownTime = 1.0f;
        this.enemyFlyTime = 1.0f;
    }
	public void Clear(){
		this.btlPlaneUser.Clear();
		this.btlPlaneEnemyList.Clear();
        this.parkingApronBtlPlaneEnemyList.Clear();
        this.enemyFlyCoolDownTime = 1.0f;
        this.enemyFlyTime = 1.0f;
    }
}
#endregion