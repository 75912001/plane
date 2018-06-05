using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 战斗中的飞机
public class BattlePlane{
    public BattlePlaneMgr parent;
    //飞机
    public GameObject gameObject;
    //飞机名称
    public string name;
    //子弹管理器
    public BattleBulletMgr battleBulletMgr;
    //移动管理器
    public BattleMoveMgr battleMoveMgr;
    //是否僚机
	public bool isWingman;
    //归属飞机
	public BattlePlane parentBattlePlane;
    //todo 僚机AI
    //todo hp
    //todo 碰撞后给予敌方造成的伤害
    //todo 是否可碰撞己方飞机
    //入场飞行是否结束
    public bool isEnterSceneEnd;
    //是否在飞翔
    public bool isFly;
    public BattlePlane(){
        this.battleBulletMgr = new BattleBulletMgr();
		this.battleBulletMgr.parent = this;
        this.battleMoveMgr = new BattleMoveMgr();
    }
    public void Clear(){
        this.gameObject = null;
        this.name = "";
        this.battleBulletMgr.Clear();
        this.battleMoveMgr.Clear();
        this.isEnterSceneEnd = false;
        this.isWingman = false;
        this.isFly = false;
    }

    //是否无敌
    public bool IsInvincible(){
        return false == this.isEnterSceneEnd;
    }
	//是否可以开火
	public bool CanFire(){
		return false != this.isEnterSceneEnd;
	}
}
#endregion

#region 战斗中的飞机管理器
public class BattlePlaneMgr{
    public BattlePlane battlePlaneUser;
	public List<BattlePlane> battlePlaneEnemyList;
    public List<BattlePlane> parkingApronBattlePlaneEnemyList;
    public float enemyFlyTime;
    //敌机 起飞 冷却时间
    public float enemyFlyCoolDownTime;
    public BattlePlaneMgr(){
        this.battlePlaneUser = new BattlePlane();
        this.battlePlaneUser.parent = this;
		this.battlePlaneEnemyList = new List<BattlePlane> ();
        this.parkingApronBattlePlaneEnemyList = new List<BattlePlane>();
        this.enemyFlyCoolDownTime = 1.0f;
        this.enemyFlyTime = 1.0f;
    }
	public void Clear(){
		this.battlePlaneUser.Clear();
		this.battlePlaneEnemyList.Clear();
        this.parkingApronBattlePlaneEnemyList.Clear();
        this.enemyFlyCoolDownTime = 1.0f;
        this.enemyFlyTime = 1.0f;
    }
}
#endregion